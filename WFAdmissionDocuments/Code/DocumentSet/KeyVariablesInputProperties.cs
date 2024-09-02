using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace WFAdmissionDocuments.Code.TemplateStuff
{
    public enum VariableInputType
    {
        //[Description("First Option")]
        TextBox = 0,
        ComboBox = 1,
        //Date = 2,
        //DateTime = 3,
        //File = 4
    }

    public class KeyVariablesInputProperties
    {
        public VariableInputType InputType { get; set; } = VariableInputType.TextBox;
        public string[] InputDatas { get; set; }
    }
}
