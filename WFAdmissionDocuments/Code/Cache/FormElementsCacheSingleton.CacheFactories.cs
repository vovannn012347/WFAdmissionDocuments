using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace WFAdmissionDocuments.Code
{
    public partial class FormElementsCacheSingleton
    {
        public static void RemoveAllEventHandlers(object target, string eventName)
        {
            var eventInfo = target.GetType().GetEvent(eventName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            if (eventInfo == null)
            {
                throw new ArgumentException("Event not found.");
            }

            var field = target.GetType().GetField(eventName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            if (field != null)
            {
                field.SetValue(target, null);
            }
            else
            {
                var prop = target.GetType().GetProperty(eventName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                if (prop != null)
                {
                    prop.SetValue(target, null, null);
                }
                else
                {
                    //throw new ArgumentException("No field or property found for event.");
                }
            }
        }

        public void Init(Control invokeControl)
        {
            InitLabelCacheFactory(invokeControl);
            InitTextBoxCacheFactory(invokeControl);
            InitComboBoxCacheFactory(invokeControl);
            InitButtonCacheFactory(invokeControl);
            InitCheckboxCacheFactory(invokeControl);
        }

        private void InitLabelCacheFactory(Control invokeControl)
        {
            Func<Label> createFunc = () => new Label();
            Action<Label> cleanFunc = (element) =>
            {
                element.Text = "";
                element.Dock = DockStyle.None;
                element.TextAlign = ContentAlignment.TopLeft;
                element.AutoSize = false;
                element.Size = Size.Empty;
                element.Padding = Padding.Empty;
            };

            AddElementTypeCache<Label>(createFunc, cleanFunc, invokeControl, 60);
        }

        private void InitTextBoxCacheFactory(Control invokeControl)
        {
            Func<TextBox> createFunc = () => new TextBox();
            Action<TextBox> cleanFunc = (element) =>
            {
                //RemoveAllEventHandlers(element, "TextChanged");

                element.Text = "";
                element.Tag = null;
                element.Dock = DockStyle.None;
                element.AutoSize = true;
                element.Multiline = false;
                element.Padding = Padding.Empty;
                element.Size = Size.Empty;
                element.Height = 0;
            };

            AddElementTypeCache<TextBox>(createFunc, cleanFunc, invokeControl, 40);
        }

        private void InitComboBoxCacheFactory(Control invokeControl)
        {
            Func<ComboBox> createFunc = () => new ComboBox();
            Action<ComboBox> cleanFunc = (element) =>
            {
                //RemoveAllEventHandlers(element, "SelectedIndexChanged");
                element.Text = "";
                element.Tag = null;
                element.Dock = DockStyle.None;
                element.AutoSize = true;
                element.DropDownStyle = ComboBoxStyle.DropDown;
                element.Padding = Padding.Empty;
                element.DataSource = null;
                element.SelectedIndex = -1;
                element.SelectedText = "";
                element.Size = Size.Empty;
            };
            
            AddElementTypeCache<ComboBox>(createFunc, cleanFunc, invokeControl, 40);
        }

        private void InitButtonCacheFactory(Control invokeControl)
        {
            Func<Button> createFunc = () => new Button();
            Action<Button> cleanFunc = (element) =>
            {
                //RemoveAllEventHandlers(element, "Click");
                element.Text = "";
                element.Tag = null;
                element.MinimumSize = Size.Empty;
                element.MaximumSize = Size.Empty;
                element.TextAlign = ContentAlignment.MiddleCenter;

            };

            AddElementTypeCache<Button>(createFunc, cleanFunc, invokeControl, 80);
        }

        private void InitCheckboxCacheFactory(Control invokeControl)
        {
            Func<CheckBox> createFunc = () => new CheckBox();
            Action<CheckBox> cleanFunc = (element) =>
            {
                element.Text = "";
                element.Tag = null;
                element.MinimumSize = Size.Empty;
                element.MaximumSize = Size.Empty;
                element.Checked = false;
                element.TextAlign = ContentAlignment.MiddleLeft;
            };

            AddElementTypeCache<CheckBox>(createFunc, cleanFunc, invokeControl, 40);
        }
    }
}
