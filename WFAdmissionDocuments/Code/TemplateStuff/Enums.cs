
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

using WFAdmissionDocuments.Properties;

namespace WFAdmissionDocuments.Code
{
    public enum TemplateEditMode
    {
        AddElement,
        EditElement,
        ResizeElement,
        MoveElement
    }

    public static class Enums
    {
        public static string GetEnumDescription(Enum value)
        {
            FieldInfo field = value.GetType().GetField(value.ToString());
            DescriptionAttribute attribute = (DescriptionAttribute)field.GetCustomAttribute(typeof(DescriptionAttribute));
            string description = attribute?.Description ?? value.ToString();

            // Retrieve localized string from resource files
            string localizedDescription = Resources.ResourceManager.GetString(description);
            return localizedDescription ?? description;
        }

    }
}
