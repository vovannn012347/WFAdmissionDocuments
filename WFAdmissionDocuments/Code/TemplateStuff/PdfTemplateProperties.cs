using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using WFAdmissionDocuments.Code.TemplateStuff.TemplateData.Interfaces;

namespace WFAdmissionDocuments.Code
{
    public class PdfTemplatePropertiesAbriged : BasePdfTemplateProperties
    {
        [JsonIgnore]
        public bool IsSaved { get; set; }
        [JsonIgnore]
        public string RootFolderPath { get; set; }
        [JsonIgnore]
        public List<string> ImagesPathOld { get; set; } = new List<string>();

        [JsonIgnore]
        public bool IsLoaded { get; set; }
        [JsonIgnore]
        private PdfTemplateProperties _propertiesWithElements;
        [JsonIgnore]
        public PdfTemplateProperties TemplateWithElements { 
            get
            {
                if(_propertiesWithElements == null)
                {
                    _propertiesWithElements = new PdfTemplateProperties();
                    _propertiesWithElements.GetDataFromAbriged(this);
                }

                return _propertiesWithElements;
            }
            set
            {
                _propertiesWithElements = value;
                _propertiesWithElements?.GetDataFromAbriged(this);
            }
        }


        //tests whether path is packed for folder-dependency
        public bool IsNewImagePath(string ImagePath)
        {
            return ImagePath.StartsWith(RootFolderPath);
        }

        //unpacks path to full drive-rooted path
        public string UnpackPath(string imagePath)
        {
            if (imagePath.StartsWith(RootFolderPath))
            {
                return RootFolderPath;
            }
            return Path.Combine(RootFolderPath, imagePath);
        }

        //unpacks path to folder-dependent path
        public string PackPath(string imagePath)
        {
            if (imagePath.StartsWith(RootFolderPath))
            {
                return imagePath.Remove(0, RootFolderPath.Length);
            }
            return imagePath;
        }
    }

    public class PdfTemplateProperties : BasePdfTemplateProperties
    {
        public List<ITemplateData> Elements { get; set; } = new List<ITemplateData>();

        internal void GetDataFromAbriged(PdfTemplatePropertiesAbriged templateProperties)
        {
            this.Name = templateProperties.Name;
            this.PageSize = templateProperties.PageSize;
            this.Settings = templateProperties.Settings;

        }
    }
}
