using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace WFAdmissionDocuments.Code.TemplateStuff
{
    public static class PropertyDictionary
    {
        private static Dictionary<Type, ElementPropertyProperties> PropInstances = new Dictionary<Type, ElementPropertyProperties>();

        static PropertyDictionary()
        {
            Type thisType = typeof(ElementPropertyProperties);
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

            // Enumerate through each assembly
            foreach (Assembly assembly in assemblies)
            {
                // Get types from the assembly that are subclasses of the base type
                Type[] subTypes = assembly.GetTypes().Where(t => t.IsSubclassOf(thisType) && !t.IsAbstract).ToArray();

                // Print the names of the subclasses
                foreach (Type subType in subTypes)
                {
                    PropInstances[subType] = (ElementPropertyProperties)Activator.CreateInstance(subType);
                }
            }
        }

        public static ElementPropertyProperties Get(Type t)
        {
            var type = PropInstances[t];
            return type;
        }
    }

    public class ElementPropertyProperties
    {
        public virtual string PropertyName { get; }
        protected Control Target { get; set; }
        //property window is single, reusable
        protected Control Controls { get; set; }

        public virtual PropertyDescriptor SelectPropertyDescriptor(PropertyDescriptorCollection properties)
        {
            return properties.Find(PropertyName, true);
        }

        public virtual void FillPropertyBox(Control propertyBox, Control destinationControl)
        {
            throw new NotImplementedException();
        }

        public virtual void ReadPropertyBox(Control propertyBox, Control destinationControl)
        {
            throw new NotImplementedException();
        }

        public virtual void ReadSettingString(string setting)
        {
            throw new NotImplementedException();
        }
    }

    public class ElementProperty
    {
        //[JsonIgnore]
        public ElementPropertyProperties Props { get; }

        public ElementProperty()
        {
            Props = PropertyDictionary.Get(typeof(ElementPropertyProperties));
        }

        internal ElementProperty(ElementPropertyProperties props)
        {
            Props = props;
        }
    }
}
