using System;
using System.Threading;
using System.Windows.Forms;
using WFAdmissionDocuments.Code.TemplateStuff;
using WFAdmissionDocuments.Properties;

namespace WFAdmissionDocuments.Templates
{
    public partial class TemplateElementPropertiesForm : Form
    {
        public bool SetClose { get; set; }
        protected readonly TemplateEditForm _parent;
        protected CustomPropertiesHideTypeDescriptor propertyDescriptor;
        protected TypeDescriptorWrapper typeDescriptor;

        public event EventHandler PropertyChanged;

        public TemplateElementPropertiesForm(TemplateEditForm parent)
        {
            Owner = parent;
            _parent = parent;

            InitializeComponent();
            parent.ElementSelected += Parent_ElementSelected;
            elementPropertyGrid.PropertyValueChanged += ElementPropertyGrid_PropertyValueChanged;
            SetElement(null);
        }

        private void ElementPropertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, null);
        }

        private void Parent_ElementSelected(object sender, EventArgs e)
        {
            SetElement(sender as ITemplateElement);
        }

        private void TemplateEditPropertiesForm_Load(object sender, EventArgs e)
        {
            LocalizeForm();
        }

        private void LocalizeForm()
        {
            Thread.CurrentThread.CurrentCulture = Constants.CurrentCulture;
            Thread.CurrentThread.CurrentUICulture = Constants.CurrentCulture;

            this.Text = Resources.TemplateElementPropertiesForm_Title;
        }

        internal void SetElement(ITemplateElement element)
        {
            if(element != null)
            {
                if (propertyDescriptor != null)
                {
                    propertyDescriptor.SetDescribedItem(element);
                }
                else
                {
                    propertyDescriptor = new CustomPropertiesHideTypeDescriptor(element);
                    typeDescriptor = new TypeDescriptorWrapper(propertyDescriptor, elementPropertyGrid);
                }
                elementPropertyGrid.SelectedObject = typeDescriptor;
            }
            else
            {
                elementPropertyGrid.SelectedObject = null;
            }

        }

        private void TemplateEditPropertiesForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!SetClose)
            {
                e.Cancel = true;
                this.Hide();
            }
        }
    }
}
