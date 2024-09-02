using System;
using System.Windows.Forms;

namespace WFAdmissionDocuments.Code.TemplateStuff.Properties
{
    public class SizeProperty : ElementPropertyProperties
    {
        public override string PropertyName { get => nameof(Control.Size); }

        public SizeProperty()
        {
        }
    }

    //public class SizeProperty : ElementProperty
    //{
    //    public float Width { get; set; }
    //    public float Height { get; set; }
        
    //    internal SizeProperty(ElementPropertyProperties props) : base (props)
    //    {

    //    }
    //}
}
