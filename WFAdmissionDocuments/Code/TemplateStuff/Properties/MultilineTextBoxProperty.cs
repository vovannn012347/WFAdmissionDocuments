﻿using System;
using System.Windows.Forms;
using System.Xml;

namespace WFAdmissionDocuments.Code.TemplateStuff.Properties
{
    public class MultilineTextBoxProperty : ElementPropertyProperties
    {
        public override string PropertyName { get => nameof(TextBox.Multiline); }
    }
}
