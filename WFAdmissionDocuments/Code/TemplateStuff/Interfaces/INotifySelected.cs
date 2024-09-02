using System.Collections.Generic;
using System.Drawing;

namespace WFAdmissionDocuments.Code.TemplateStuff
{
    public interface INotifySelected
    {
        void NotifySelected(ITemplateElement element);
    }
}
