using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using WFAdmissionDocuments.Code.TemplateStuff.TemplateData.Interfaces;

namespace WFAdmissionDocuments.Code.TemplateStuff
{
    public interface ITemplateElement
    {
        Size Size { get; set; }
        Point Location { get; set; }
        bool Selected { get; set; }
        bool Resizing { get; set; }
        IEnumerable<ElementPropertyProperties> Properties { get; }

        EventHandler<MouseEventArgs> LongClick { get; set; }
        EventHandler<MouseEventArgs> LongPress { get; set; }

        EventHandler ResizeStart { get; set; }
        EventHandler ResizeEnd { get; set; }

        void InitDefaults();
        void SetContainerForm(INotifySelected parentForm);
        ITemplateData GetSaveData();
    }
}
