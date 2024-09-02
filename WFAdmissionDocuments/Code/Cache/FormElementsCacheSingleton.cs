using System;
using System.Collections.Generic;
using System.Security.RightsManagement;
using System.Windows.Forms;

namespace WFAdmissionDocuments.Code
{
    public partial class FormElementsCacheSingleton : IDisposable
    {
        private static readonly object _lock = new object();
        private static FormElementsCacheSingleton _instance;

        private Dictionary<Type, IElementCacheFactory> cacheFactories = new Dictionary<Type, IElementCacheFactory>();
        private FormElementsCacheSingleton()
        {
        }

        public static FormElementsCacheSingleton Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        _instance = new FormElementsCacheSingleton();
                    }
                }
                return _instance;
            }
        }


        public T GetElement<T>() where T : Control
        {
            if (!cacheFactories.ContainsKey(typeof(T))) return null;

            return (T)cacheFactories[typeof(T)].GetCachedElement();
        }

        public void AddCleanupDelegate<T>(T cleanedObject, Action action) where T : Control
        {
            if (!cacheFactories.ContainsKey(typeof(T))) return;

            cacheFactories[typeof(T)].AddCleanupDelegate(cleanedObject, action);
        }

        public void ReturnElement<T>(T element)
        {
            if (!cacheFactories.ContainsKey(element.GetType())) return;

            cacheFactories[element.GetType()].ReleaseElement(element);
        }

        public void AddElementTypeCache<T>(
            Func<T> createFunc,
            Action<T> cleanFunc,
            Control invokeControl,
            int numCache = 20) where T : Control
        {
            Type elementType = typeof(T);
            if (!cacheFactories.ContainsKey(elementType))
            {
                cacheFactories.Add(elementType, new ElementCacheFactory<T>(createFunc, cleanFunc, invokeControl, numCache));
            }
        }

        public void Dispose()
        {   
            foreach(var value in cacheFactories.Values)
            {
                value.StopCaching();
            }
        }
    }
}
