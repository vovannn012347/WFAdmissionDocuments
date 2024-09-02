using Newtonsoft.Json;
using System.Collections.Generic;
using WFAdmissionDocuments.Code;
using WFAdmissionDocuments.Code.TemplateStuff;

namespace WFAdmissionDocuments.DocumentSet.Code
{
    public class PdfDocumentSetProperties
    {
        public string Name { get; set; }
        [JsonIgnore]
        public string RootFolderPath { get; set; }

        public Dictionary<string, PdfTemplatePropertiesAbriged> Elements { get; set; } = new Dictionary<string, PdfTemplatePropertiesAbriged>();
        public List<string> ElementsOrder { get; set; } = new List<string>();

        public PdfDocumentSetSettingsData Settings { get; set; } = new PdfDocumentSetSettingsData();

        public Dictionary<string, KeyVariablesNameData> KeyDatas { get; set; } = new Dictionary<string, KeyVariablesNameData>();
        public Dictionary<string, KeyVariablesInputProperties> TemplateKeyInputProps { get; set; } = new Dictionary<string, KeyVariablesInputProperties>();
        public Dictionary<string, string> TemplateKeys { get; set; } = new Dictionary<string, string>();
        public List<string> KeysOrder { get; set; } = new List<string>();
        public List<string> ExcelKeys { get; set; } = new List<string>();
    }
}
