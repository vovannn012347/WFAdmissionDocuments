﻿using System;
using System.Windows.Forms;
using System.Xml;

namespace WFAdmissionDocuments.Code.TemplateStuff.Properties
{
    public class FontColorTextBoxProperty : ElementPropertyProperties
    {
        public override string PropertyName { get => nameof(TextBox.ForeColor); }
    }
}
