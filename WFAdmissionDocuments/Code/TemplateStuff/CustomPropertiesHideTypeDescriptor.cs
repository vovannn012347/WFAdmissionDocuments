using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using WFAdmissionDocuments.Templates.TemplateElements;

namespace WFAdmissionDocuments.Code.TemplateStuff
{
    public class CustomPropertiesHideTypeDescriptor : ICustomTypeDescriptor
    {
        public object Instance => _instance;

        private object _instance;

        public void SetDescribedItem(object item)
        {
            _instance = item;
        }

        public CustomPropertiesHideTypeDescriptor(object instance)
        {
            _instance = instance;
        }

        public AttributeCollection GetAttributes() => TypeDescriptor.GetAttributes(_instance);
        public string GetClassName() => TypeDescriptor.GetClassName(_instance);
        public string GetComponentName() => TypeDescriptor.GetComponentName(_instance);
        public TypeConverter GetConverter() => TypeDescriptor.GetConverter(_instance);
        public EventDescriptor GetDefaultEvent() => TypeDescriptor.GetDefaultEvent(_instance);
        public PropertyDescriptor GetDefaultProperty() => TypeDescriptor.GetDefaultProperty(_instance);
        public object GetEditor(Type editorBaseType) => 
            TypeDescriptor.GetEditor(_instance, editorBaseType);
        public EventDescriptorCollection GetEvents(Attribute[] attributes) => TypeDescriptor.GetEvents(_instance, attributes);
        public EventDescriptorCollection GetEvents() => TypeDescriptor.GetEvents(_instance);
        public object GetPropertyOwner(PropertyDescriptor pd) => _instance;

        public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
        {
            if (typeof(ITemplateElement).IsAssignableFrom(_instance.GetType()))
            {
                var templateElement = (_instance as ITemplateElement);

                var properties = TypeDescriptor.GetProperties(_instance, attributes);
                var filteredProperties = new PropertyDescriptorCollection(null);

                foreach(var property in templateElement.Properties)
                {
                    var propfiltered = property.SelectPropertyDescriptor(properties);
                    if(propfiltered != null)
                    {
                        filteredProperties.Add(propfiltered);
                    }
                }

                if(_instance is ImageElement el)
                {

                }

                return filteredProperties;
            }

            return new PropertyDescriptorCollection(null);
        }

        public PropertyDescriptorCollection GetProperties()
        {
            return GetProperties(null);
        }
    }

    public class TypeDescriptorWrapper : ICustomTypeDescriptor
    {
        public PropertyGrid Parent => _parent;
        public CustomPropertiesHideTypeDescriptor Descriptor => _customTypeDescriptor;

        private readonly CustomPropertiesHideTypeDescriptor _customTypeDescriptor;
        private readonly PropertyGrid _parent;
        
        public TypeDescriptorWrapper(CustomPropertiesHideTypeDescriptor customTypeDescriptor, PropertyGrid parent)
        {
            _customTypeDescriptor = customTypeDescriptor;
            _parent = parent;
        }

        public AttributeCollection GetAttributes() => _customTypeDescriptor.GetAttributes();
        public string GetClassName() => _customTypeDescriptor.GetClassName();
        public string GetComponentName() => _customTypeDescriptor.GetComponentName();
        public TypeConverter GetConverter() => _customTypeDescriptor.GetConverter();
        public EventDescriptor GetDefaultEvent() => _customTypeDescriptor.GetDefaultEvent();
        public PropertyDescriptor GetDefaultProperty() => _customTypeDescriptor.GetDefaultProperty();

        public object GetEditor(Type editorBaseType) => 
            _customTypeDescriptor.GetEditor(editorBaseType);

        public EventDescriptorCollection GetEvents(Attribute[] attributes) => _customTypeDescriptor.GetEvents(attributes);
        public EventDescriptorCollection GetEvents() => _customTypeDescriptor.GetEvents();
        public object GetPropertyOwner(PropertyDescriptor pd) => _customTypeDescriptor.GetPropertyOwner(pd);
        public PropertyDescriptorCollection GetProperties(Attribute[] attributes) => _customTypeDescriptor.GetProperties(attributes);
        public PropertyDescriptorCollection GetProperties() => _customTypeDescriptor.GetProperties();
    }
}
