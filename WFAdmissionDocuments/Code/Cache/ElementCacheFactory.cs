using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using Timer = System.Threading.Timer;

namespace WFAdmissionDocuments.Code
{
    interface IElementCacheFactory
    {
        object GetCachedElement();

        void AddCleanupDelegate(object element, Action cleanAction);
        void ReleaseElement(object element);
        Task StartCaching();
        void StopCaching();
    }


    public class ElementCacheFactory<T> : IElementCacheFactory where T : Control
    {
        class RecycleTimerState
        {
            public bool isRunning { get; set; } = false;
        }

        private RecycleTimerState _recycleState;
        private Timer _recycleTimer;
        private int _recycleSeconds;

        private int _recheckSeconds;
        private int _cacheAmount;
        private Func<T> _createFunc;
        private Action<T> _cleanFunc;

        private Queue<T> _cachedElements = new Queue<T>();
        private Queue<T> _recycleElements = new Queue<T>();
        private Dictionary<T, Action> _recycleActions = new Dictionary<T, Action>();
        private HashSet<T> _usedElements = new HashSet<T>();

        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        private Type _templatedType;
        private Control _invokeControl;

        public ElementCacheFactory(
            Func<T> createFunc,
            Action<T> cleanFunc,
            Control invocationControl,
            int cacheAmount = 20,
            int recheckTimeSeconds = 10,
            int recycleTimer = 2)
        {
            _cacheAmount = cacheAmount;
            _createFunc = createFunc;
            _cleanFunc = cleanFunc;
            _recheckSeconds = recheckTimeSeconds;
            _recycleSeconds = recycleTimer;

            _recycleState = new RecycleTimerState();
            _recycleTimer = new Timer(DoRecycling, _recycleState, Timeout.Infinite, Timeout.Infinite);

            _templatedType = typeof(T);
            _invokeControl = invocationControl;

            StartCaching();
        }



        private void DoWork()
        {
            lock (_cachedElements)
            {
                T newElement = null;
                lock (_recycleElements)
                    if (_recycleElements.Any())
                    {
                        newElement = _recycleElements.Dequeue();
                        lock (_recycleActions)
                            if (_recycleActions.TryGetValue(newElement, out Action act))
                            {
                                _invokeControl.Invoke(act);
                                _recycleActions.Remove(newElement);
                            }

                        _invokeControl.Invoke(new Action(() => _cleanFunc(newElement)));
                        _cachedElements.Enqueue(newElement);
                    }


                if (newElement == null)
                {
                    _invokeControl.Invoke(new Action(() => {
                        newElement = _createFunc();
                    }));
                }

                _cachedElements.Enqueue(newElement);
            }
        }

        public async Task StartCaching()
        {
            bool shouldDoWork;

            while (!_cancellationTokenSource.Token.IsCancellationRequested)
            {
                shouldDoWork = false;

                lock (_cachedElements)
                    if (_cachedElements.Count <= _cacheAmount)
                        shouldDoWork = true;

                if (shouldDoWork)
                {
                    DoWork();
                }
                else
                {
                    await Task.Delay(TimeSpan.FromSeconds(_recheckSeconds), _cancellationTokenSource.Token);
                }
            }
        }

        private void DoRecycling(object state)
        {
            var timerState = state as RecycleTimerState;
            lock (timerState)
                timerState.isRunning = false;

            while (!_cancellationTokenSource.Token.IsCancellationRequested)
            {
                lock (_recycleElements)
                    if (!(_recycleElements.Count > 0)) break;


                lock (_recycleElements)
                    if (_recycleElements.Any())
                    {
                        var recycledElement = _recycleElements.Dequeue();
                        lock (_recycleActions)
                            if (_recycleActions.TryGetValue(recycledElement, out Action act))
                            {
                                _invokeControl.Invoke(act);
                                _recycleActions.Remove(recycledElement);
                            }

                        _invokeControl.Invoke(new Action(() => _cleanFunc(recycledElement)));

                        lock (_cachedElements)
                            _cachedElements.Enqueue(recycledElement);
                    }
            }
        }

        public void StopCaching()
        {
            _cancellationTokenSource.Cancel();
        }

        public object GetCachedElement()
        {
            T returnedElement = null;

            lock (_cachedElements)
                if(_cachedElements.Any())
                    returnedElement = _cachedElements.Dequeue();

            if (returnedElement == null)
                lock (_recycleElements)
                    if (_recycleElements.Any())
                    {
                        returnedElement = _recycleElements.Dequeue();

                        lock(_recycleActions)
                            if(_recycleActions.TryGetValue(returnedElement, out Action act)){
                                _invokeControl.Invoke(act);
                                _recycleActions.Remove(returnedElement);
                            }

                        _invokeControl.Invoke(new Action(() => _cleanFunc(returnedElement)));
                    }

            if(returnedElement == null)
            {
                _invokeControl.Invoke(new Action(() => {
                    returnedElement = _createFunc();
                }));
            }

            _usedElements.Add(returnedElement);

            return returnedElement;
        }

        public void ReleaseElement(object element)
        {
            if (!(element.GetType() == _templatedType || element.GetType().IsSubclassOf(_templatedType))) return;

            bool elementExists = false;

            lock (_usedElements)
                elementExists = _usedElements.Remove((T)element);

            if(elementExists) 
            {
                lock (_recycleElements)
                    _recycleElements.Enqueue((T)element);

                _recycleTimer.Change(_recycleSeconds * 1000, Timeout.Infinite);
            }
        }

        public void AddCleanupDelegate(object element, Action cleanAction)
        {
            if (!(element.GetType() == _templatedType || element.GetType().IsSubclassOf(_templatedType))) return;

            _recycleActions[(T)element] = cleanAction;
        }
    }
}

