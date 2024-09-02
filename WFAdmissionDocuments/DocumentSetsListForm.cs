using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using UnidecodeSharpFork;

using WFAdmissionDocuments.Code;
using WFAdmissionDocuments.Code.TemplateStuff.TemplateData;
using WFAdmissionDocuments.DocumentSet.Code;
using WFAdmissionDocuments.DocumentSets;
using WFAdmissionDocuments.Properties;
using WFAdmissionDocuments.Templates;

namespace WFAdmissionDocuments
{
    public partial class DocumentSetsListForm : Form
    {
        public Dictionary<string, PdfTemplatePropertiesAbriged> Templates { get; set; } = new Dictionary<string, PdfTemplatePropertiesAbriged>();

        //public Dictionary<string, BasePdfTemplateProperties> TemplatesAbriged { get; set; } = new Dictionary<string, BasePdfTemplateProperties>();
        public Dictionary<string, PdfDocumentSetProperties> DocumentSetProperties { get; set; } = new Dictionary<string, PdfDocumentSetProperties>();
        private TreeNode allTemplatesNode;
        private TreeNode selectedNode;


        public DocumentSetsListForm()
        {
            InitializeComponent();

            FormElementsCacheSingleton.Instance.Init(this);
            //initialize element cache ahead
        }

        private List<string> GetTemplateFilesList()
        {
            var templateDirectory = Path.Combine(Directory.GetCurrentDirectory(), Constants.TemplatesDirectory);
            if (!Directory.Exists(templateDirectory))
                Directory.CreateDirectory(templateDirectory);

            string[] files = Directory.GetFiles(templateDirectory, "*" + Constants.TemplatesExtension, SearchOption.AllDirectories);
            return files.ToList();
        }

        private List<string> GetDocumentSetList()
        {
            var documentsDirectory = Path.Combine(Directory.GetCurrentDirectory(), Constants.DocumentSetsDirectory);
            if (!Directory.Exists(documentsDirectory))
                Directory.CreateDirectory(documentsDirectory);

            string[] files = Directory.GetFiles(documentsDirectory, "*" + Constants.DocumentSetExtension, SearchOption.AllDirectories);
            return files.ToList();
        }

        private void Reload()
        {
            var templateDirectory = Path.Combine(Directory.GetCurrentDirectory(), Constants.TemplatesDirectory);
            var documentSetsDirectory = Path.Combine(Directory.GetCurrentDirectory(), Constants.DocumentSetsDirectory);

            Templates.Clear();
            documentSetTreeView.Nodes.Clear();
            DocumentSetProperties.Clear();
            allTemplatesNode = new System.Windows.Forms.TreeNode(Constants.AllTemplatesListNodename);
            documentSetTreeView.Nodes.Add(allTemplatesNode);
            SetElementSelected(null);

            var templateFiles = GetTemplateFilesList();

            var deserializeSettings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.None
            };

            //load templates list
            foreach (var templatePath in templateFiles)
            {
                string data = File.ReadAllText(templatePath, System.Text.Encoding.Unicode);

                PdfTemplatePropertiesAbriged serializedData = null;
                string filePath = string.Empty;
                try
                {
                    serializedData = JsonConvert.DeserializeObject<PdfTemplatePropertiesAbriged>(data, deserializeSettings);

                    filePath = templatePath.Remove(0, templateDirectory.Length).Trim('\\', '/');
                    serializedData.RootFolderPath = templateDirectory;
                    Templates[filePath] = serializedData;
                }
                catch (Exception ex)
                {

                }

                if (serializedData != null)
                {
                    var name = serializedData.Name;
                    if (!string.IsNullOrEmpty(serializedData.Settings.VisibleName))
                    {
                        name = serializedData.Settings.VisibleName;
                    }

                    var templateNode = new System.Windows.Forms.TreeNode(name);
                    templateNode.Tag = filePath;

                    allTemplatesNode.Nodes.Add(templateNode);
                }
            }

            //load document sets
            var documentSets = GetDocumentSetList();
            foreach (var documentSetPath in documentSets)
            {
                string data = File.ReadAllText(documentSetPath, System.Text.Encoding.Unicode);

                PdfDocumentSetProperties serializedData = null;
                string filePath = string.Empty;

                if (string.IsNullOrEmpty(data)) continue;

                try
                {
                    serializedData = JsonConvert.DeserializeObject<PdfDocumentSetProperties>(data, deserializeSettings);

                    filePath = documentSetPath.Remove(0, documentSetsDirectory.Length).Trim('\\', '/');
                    DocumentSetProperties[filePath] = serializedData;
                }
                catch (Exception ex)
                {

                }

                if (serializedData != null)
                {
                    serializedData.RootFolderPath = documentSetsDirectory;
                    var name = serializedData.Name;
                    if (!string.IsNullOrEmpty(serializedData.Settings.VisibleName))
                    {
                        name = serializedData.Settings.VisibleName;
                    }

                    var documentSetNode = new TreeNode(name);
                    documentSetNode.Tag = filePath;
                    documentSetTreeView.Nodes.Add(documentSetNode);
                    foreach (var templateKey in serializedData.Elements.Keys)
                    {
                        if (Templates.TryGetValue(templateKey, out var templateData))
                        {
                            var templateName = templateData.Name;
                            if (!string.IsNullOrEmpty(templateData.Settings.VisibleName))
                            {
                                templateName = templateData.Settings.VisibleName;
                            }

                            var templateNode = new TreeNode(templateName);
                            templateNode.Tag = templateKey;

                            documentSetNode.Nodes.Add(templateNode);
                        }
                        else
                        {
                            //no template found here
                            //var templateData1 = serializedData.Elements[templateKey];
                            //var templateName = templateData1.Name;
                            //if (!string.IsNullOrEmpty(templateData1.Settings.VisibleName))
                            //{
                            //    templateName = templateData1.Settings.VisibleName;
                            //}
                            var templateName = templateKey;

                            var templateNode = new TreeNode(templateName);
                            templateNode.ForeColor = Color.Red;
                            templateNode.Tag = templateKey;

                            documentSetNode.Nodes.Add(templateNode);
                        }
                    }
                }
            }
        }
        
        private void DocumentSetsListForm_Load(object sender, EventArgs e)
        {
            LoadSettings();

            Reload();
            LocalizeForm();
        }

        private void LoadSettings()
        {
            if (File.Exists(Constants.GeneralSettingsFile))
            {
                string fileData = File.ReadAllText(Constants.GeneralSettingsFile);

                try
                {
                    Constants.Settings = JsonConvert.DeserializeObject<GeneralSettings>(fileData);
                }
                catch (Exception ex)
                {
                    Constants.Settings = new GeneralSettings();
                }
            }
            else
            {
                Constants.Settings = new GeneralSettings();
            }
        }

        private void buttonNewDocumentSet_Click(object sender, EventArgs e)
        {
            using (DocumentSetCreateForm createForm = new DocumentSetCreateForm())
            {
                if (createForm.ShowDialog() == DialogResult.OK)
                {
                    PdfDocumentSetProperties props = new PdfDocumentSetProperties();

                    props.RootFolderPath = Path.Combine(Directory.GetCurrentDirectory(), Constants.DocumentSetsDirectory);
                    props.Name = createForm.FileName.Unidecode();
                    props.Settings.VisibleName = createForm.FriendlyName;


                    var name = string.IsNullOrWhiteSpace(props.Settings.VisibleName) ? props.Name : props.Settings.VisibleName;

                    var documentSetNode = new TreeNode(name);
                    documentSetNode.Tag = props.Name;

                    documentSetTreeView.Nodes.Add(documentSetNode);

                    DocumentSetProperties[props.Name] = props;
                    SaveDocumentSet(props);
                }
            }
        }

        private void UpdateTemplateNodes(TreeNode documentSetNode, PdfDocumentSetProperties documentSet)
        {
            documentSetNode.Nodes.Clear();

            foreach(var element in documentSet.Elements.Values)
            {
                var name = string.IsNullOrWhiteSpace(element.Settings.VisibleName) ? element.Name : element.Settings.VisibleName;
                
                var templateNode = new TreeNode(name);
                documentSetNode.Tag = element.Name;

                documentSetNode.Nodes.Add(documentSetNode);
            }
        }
        
        private void AddTemplateNode(TreeNode documentSetNode, BasePdfTemplateProperties template)
        {
            var name = string.IsNullOrWhiteSpace(template.Settings.VisibleName) ? template.Name : template.Settings.VisibleName;
            var templateNode = new TreeNode(name);
            templateNode.Tag = template.Name;

            documentSetNode.Nodes.Add(templateNode);
        }

        private void buttonAddPage_Click(object sender, EventArgs e)
        {
            if (selectedNode == null || selectedNode.Parent != null || selectedNode == allTemplatesNode) return;

            var selectedDocumentSet = selectedNode.Tag as string;
            if(DocumentSetProperties.TryGetValue(selectedDocumentSet, out var docSet))
            {
                SelectTemplatesForm selectForm = new SelectTemplatesForm();
                selectForm.PreSelectedElements = docSet.Elements.Keys.ToList();
                selectForm.SelectionValues = Templates;
                
                if (selectForm.ShowDialog() == DialogResult.OK && selectForm.SelectedElements.Count > 0)
                {
                    var elements = selectForm.SelectedElements.Select(s => s.Unidecode());

                    foreach (var selectedElementString in selectForm.SelectedElements)
                    {
                        docSet.Elements[selectedElementString] = Templates[selectedElementString];
                        AddTemplateNode(selectedNode, Templates[selectedElementString]);
                    }

                    RecalculateKeyDatas(docSet);
                    SaveDocumentSet(docSet);
                }
                
            }
        }

        private void buttonNewTemplatePage_Click(object sender, EventArgs e)
        {
            using (TemplateCreateForm createForm = new TemplateCreateForm())
            {
                if (createForm.ShowDialog() == DialogResult.OK)
                {
                    PdfTemplatePropertiesAbriged props = TemplateLibrary.CreateNewTemplate(createForm.FileName.Unidecode());
                    
                    props.RootFolderPath = Path.Combine(Directory.GetCurrentDirectory(), Constants.TemplatesDirectory);
                    props.Settings.VisibleName = createForm.FriendlyName;

                    var name = string.IsNullOrWhiteSpace(props.Settings.VisibleName) ? props.Name : props.Settings.VisibleName;

                    var templateNode = new TreeNode(name);
                    templateNode.Tag = props.Name;

                    allTemplatesNode.Nodes.Add(templateNode);

                    Templates[props.Name] = props;

                    var settings = new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.All
                    };
                    var filePath = Path.Combine(props.RootFolderPath, props.Name);
                    if (File.Exists(filePath))
                    {
                        //this is ok, there is an overwrite dialog prompt
                        string data2 = File.ReadAllText(filePath, System.Text.Encoding.Unicode);
                        PdfTemplateProperties props2 = null;
                        try
                        {
                            props2 = JsonConvert.DeserializeObject<PdfTemplateProperties>(data2, settings);
                        }
                        catch (Exception ex)
                        {

                        }

                        if (props2 != null)
                        {
                            foreach (var element in props2.Elements)
                            {
                                if (element is ImageTemplate image)
                                {
                                    var deletedFile = Path.Combine(props.RootFolderPath, image.ImagePath);
                                    if (File.Exists(deletedFile))
                                    {
                                        File.Delete(deletedFile);
                                    }
                                }
                            }
                        }
                    }

                    string serializedData = JsonConvert.SerializeObject(props, settings);
                    File.WriteAllText(filePath, serializedData, System.Text.Encoding.Unicode);
                }
            }
        }

        private void buttonLoadPage_Click(object sender, EventArgs e)
        {
            string file;
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Template json files (*.tmjson)|*.tmjson";
                openFileDialog.FilterIndex = 0;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    file = openFileDialog.FileName;
                }
                else return;
            }

            string data = File.ReadAllText(file, System.Text.Encoding.Unicode);

            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            };

            PdfTemplatePropertiesAbriged props = null;
            try
            {
                props = JsonConvert.DeserializeObject<PdfTemplatePropertiesAbriged>(data, settings);
            }
            catch (Exception ex)
            {
                return;
            }

            var templateFolderPath = Path.GetDirectoryName(file);
            using (TemplateCreateForm createForm = new TemplateCreateForm())
            {
                createForm.NewFileName = false;
                createForm.FileName = Path.GetFileNameWithoutExtension(file);
                createForm.FriendlyName = props.Settings.VisibleName;

                if (createForm.ShowDialog() == DialogResult.OK)
                {
                    props.RootFolderPath = Path.Combine(Directory.GetCurrentDirectory(), Constants.TemplatesDirectory);
                    props.Name = createForm.FileName;
                    props.Settings.VisibleName = createForm.FriendlyName;

                    var name = string.IsNullOrWhiteSpace(props.Settings.VisibleName) ? props.Name : props.Settings.VisibleName;

                    var templateNode = new TreeNode(name);
                    templateNode.Tag = props.Name;

                    allTemplatesNode.Nodes.Add(templateNode);
                    Templates[props.Name] = props;

                    var imagesPath = Path.Combine(props.RootFolderPath, Constants.ImagesDirectory);

                    //erase images if overwrite
                    var newFile = Path.Combine(props.RootFolderPath, props.Name);
                    if (File.Exists(newFile))
                    {
                        string data2 = File.ReadAllText(file, System.Text.Encoding.Unicode);
                        PdfTemplateProperties props2 = null;
                        try
                        {
                            props2 = JsonConvert.DeserializeObject<PdfTemplateProperties>(data2, settings);
                        }
                        catch (Exception ex)
                        {

                        }

                        if(props2 != null)
                        {
                            props.IsLoaded = true;
                            props.TemplateWithElements = props2;
                            foreach (var element in props2.Elements)
                            {
                                if (element is ImageTemplate image)
                                {
                                    var deletedFile = Path.Combine(props.RootFolderPath, image.ImagePath);
                                    if (File.Exists(deletedFile))
                                    {
                                        File.Delete(deletedFile);
                                    }
                                }
                            }
                        }
                    }

                    if (props.IsLoaded)
                    {
                        //transfer images
                        foreach (var element in props.TemplateWithElements.Elements)
                        {
                            if (element is ImageTemplate image)
                            {
                                var imageName = Path.GetFileName(image.ImagePath);
                                var imageExtension = Path.GetExtension(imageName);
                                while (File.Exists(Path.Combine(imagesPath, imageName)))
                                {
                                    imageName = Path.ChangeExtension(Guid.NewGuid().ToString(), imageExtension);
                                }

                                File.Copy(Path.Combine(templateFolderPath, image.ImagePath), Path.Combine(imagesPath, imageName));
                                image.ImagePath = Path.Combine(Constants.ImagesDirectory, imageName);
                            }
                        }
                    }

                    string serializedData = JsonConvert.SerializeObject(props, settings);

                    File.WriteAllText(newFile, serializedData, System.Text.Encoding.Unicode);
                }
            }
        }

        //edit page template
        private void buttonEdit_Click(object sender, EventArgs e)
        {
            string templateString = selectedNode.Tag as string;

            if(Templates.TryGetValue(templateString, out var template))
            {
                var templateDirectory = Path.Combine(Directory.GetCurrentDirectory(), Constants.TemplatesDirectory);

                var filePath = Path.Combine(templateDirectory, template.Name);
                if (!File.Exists(filePath))
                {
                    MessageBox.Show("Template page file does not exist");
                    return;
                }

                using (var TemplateEditForm = new TemplateEditForm()
                {
                    FileEdit = true,
                    FileToOpen = filePath,
                    FileSaved = true
                })
                {
                    if (TemplateEditForm.ShowDialog() == DialogResult.OK)
                    {
                        //update node in all template nodes
                        foreach (TreeNode setnode in documentSetTreeView.Nodes)
                        {
                            foreach (TreeNode templatenode in (setnode as TreeNode).Nodes)
                            {
                                if (templatenode.Tag as string == templateString)
                                {
                                    var name = TemplateEditForm.TemplateProperties.Settings.VisibleName;
                                    name = string.IsNullOrWhiteSpace(name) ? templateString : name;

                                    templatenode.Text = name;
                                    templatenode.ForeColor = Color.Black;

                                    break;
                                }
                            }
                        }

                        //reload edited template data
                        if (Templates.TryGetValue(templateString, out var templateProps2))
                        {
                            templateProps2.Settings = TemplateEditForm.TemplateProperties.Settings;
                            //var templatePath = Path.Combine(templateProps2.RootFolderPath, templateProps2.Name);
                        }

                        Templates[templateString] = TemplateEditForm.TemplateProperties;


                        //update all document templates
                        foreach (var documentTemplate in DocumentSetProperties.Values)
                        {
                            if (documentTemplate.Elements.TryGetValue(templateString, out var templateProps))
                            {
                                templateProps.Settings = TemplateEditForm.TemplateProperties.Settings;
                                RecalculateKeyDatas(documentTemplate);
                                SaveDocumentSet(documentTemplate);
                            }
                        }

                    }
                }
            }
            else
            {
                MessageBox.Show("Template page file does not exist");
                return;
            }
            
        }

        private void buttonReload_Click(object sender, EventArgs e)
        {
            Reload();
        }
        
        private void buttonDuplicate_Click(object sender, EventArgs e)
        {
           
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            string removeName = string.Empty;
            if(selectedNode != null)
            {
                removeName = selectedNode.Text;
                if (selectedNode.Parent == allTemplatesNode)
                {
                    //template
                    var dialogResult = MessageBox.Show("Are you sure you want to remove " + removeName + "? This is irreversible.", "Are you sure?", MessageBoxButtons.YesNo);

                    if (dialogResult == DialogResult.Yes)
                    {
                        var removeTemplateName = selectedNode.Tag as string;

                        var template = Templates[removeTemplateName];

                        //remove nodes in ui
                        foreach (TreeNode setnode in documentSetTreeView.Nodes)
                        {
                            for (int i = (setnode.Nodes.Count - 1); i >= 0;)
                            {
                                TreeNode templatenode = setnode.Nodes[i];

                                if (templatenode.Tag as string == removeTemplateName)
                                {
                                    setnode.Nodes.RemoveAt(i);
                                }
                                --i;
                            }
                        }

                        //remove nodes in document sets
                        foreach (var set in DocumentSetProperties.Values)
                        {
                            if (set.Elements.ContainsKey(removeTemplateName))
                            {
                                set.Elements.Remove(removeTemplateName);
                                //set.ElementsOrder.Remove(removeTemplateName);

                                RecalculateKeyDatas(set);
                                SaveDocumentSet(set);
                            }
                        }

                        if (Templates.ContainsKey(removeTemplateName))
                        {
                            Templates.Remove(removeTemplateName);
                        }

                        //remove files
                        var templateDirectory = Path.Combine(Directory.GetCurrentDirectory(), Constants.TemplatesDirectory);
                        var templatePath = Path.Combine(templateDirectory, removeTemplateName);

                        string data = File.ReadAllText(templatePath, System.Text.Encoding.Unicode);

                        var deserializeSettings = new JsonSerializerSettings
                        {
                            TypeNameHandling = TypeNameHandling.None
                        };
                        PdfTemplateProperties serializedData = null;
                        try
                        {
                            serializedData = JsonConvert.DeserializeObject<PdfTemplateProperties>(data, deserializeSettings);
                        }
                        catch
                        {

                        }

                        //delete images
                        if (serializedData != null)
                        {
                            foreach (var element in serializedData.Elements)
                            {
                                if (element is ImageTemplate image)
                                {
                                    var imagePath = Path.Combine(templateDirectory, image.ImagePath);
                                    if (File.Exists(imagePath))
                                        File.Delete(imagePath);
                                }
                            }
                        }

                        //delete file
                        File.Delete(templatePath);
                        
                        SetElementSelected(documentSetTreeView.SelectedNode);
                    }
                }
                else
                if (selectedNode.Parent != null)
                {
                    //template in document set
                    var templateName = selectedNode.Tag as string;
                    var documentSetName = selectedNode.Parent.Tag as string;

                    var documenntSet = DocumentSetProperties[documentSetName];
                    if (!string.IsNullOrEmpty(templateName))
                    {
                        documenntSet.Elements.Remove(templateName);
                        //documenntSet.ElementsOrder.Remove(templateName);
                    }
                    RecalculateKeyDatas(documenntSet);
                    SaveDocumentSet(documenntSet);

                    selectedNode.Parent.Nodes.Remove(selectedNode);
                    
                    SetElementSelected(documentSetTreeView.SelectedNode);
                }
                else
                {
                    //document set
                    var dialogResult = MessageBox.Show("Are you sure you want to remove " + removeName + "? This is irreversible.", "Are you sure?", MessageBoxButtons.YesNo);

                    if (dialogResult == DialogResult.Yes)
                    {
                        var removeSetName = selectedNode.Tag as string;
                        var documentSet = DocumentSetProperties[removeSetName];

                        var templateDirectory = Path.Combine(Directory.GetCurrentDirectory(), Constants.DocumentSetsDirectory);
                        var filePath = Path.Combine(templateDirectory, removeSetName);
                        
                        documentSetTreeView.Nodes.Remove(selectedNode);

                        if (File.Exists(filePath))
                            File.Delete(filePath);

                        SetElementSelected(documentSetTreeView.SelectedNode);
                    }
                }
            }
        }

        private void SaveDocumentSet(PdfDocumentSetProperties documentSet)
        {
            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            };
            string serializedData = JsonConvert.SerializeObject(documentSet, settings);
            var filePath = Path.Combine(documentSet.RootFolderPath, documentSet.Name);
            File.WriteAllText(filePath, serializedData, System.Text.Encoding.Unicode);
        }

        private void ButtonSettings_Click(object sender, EventArgs e)
        {
            if (selectedNode == null || selectedNode == allTemplatesNode) return;

            if (selectedNode.Parent != null)
            {
                //template
                var templateName = selectedNode.Tag as string;
                var template = Templates[templateName];

                TemplateSettingsForm settingsForm = new TemplateSettingsForm();
                settingsForm.SetSettingsData(template.Settings);

                if (settingsForm.ShowDialog() == DialogResult.OK)
                {
                    template.TemplateWithElements.Settings = template.Settings;

                    //update node in all template nodes
                    foreach (TreeNode setnode in documentSetTreeView.Nodes)
                    {
                        foreach (TreeNode templatenode in (setnode as TreeNode).Nodes)
                        {
                            if (templatenode.Tag as string == templateName)
                            {
                                var name = template.Settings.VisibleName;
                                name = string.IsNullOrWhiteSpace(name) ? templateName : name;

                                templatenode.Text = name;
                                templatenode.ForeColor = Color.Black;
                                break;
                            }
                        }
                    }

                    //update all document templates
                    foreach (var documentTemplate in DocumentSetProperties.Values)
                    {
                        if (documentTemplate.Elements.TryGetValue(templateName, out var templateProps))
                        {
                            templateProps.Settings = template.Settings;
                            templateProps.TemplateWithElements.Settings = template.Settings;
                        }
                    }
                    if (Templates.TryGetValue(templateName, out var templateProps3))
                    {
                        templateProps3.Settings = template.Settings;
                        templateProps3.TemplateWithElements.Settings = template.Settings;
                    }

                    var templateDirectory = Path.Combine(Directory.GetCurrentDirectory(), Constants.TemplatesDirectory);
                    var templateFile = Path.Combine(templateDirectory, templateName);

                    string data2 = File.ReadAllText(templateFile, System.Text.Encoding.Unicode);

                    var settings = new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.All
                    };

                    PdfTemplateProperties props2 = null;
                    try
                    {
                        props2 = JsonConvert.DeserializeObject<PdfTemplateProperties>(data2, settings);
                    }
                    catch
                    {

                    }

                    if (props2 != null)
                    {
                        props2.Settings = template.Settings;
                    }

                    string serializedData = JsonConvert.SerializeObject(props2, settings);
                    File.WriteAllText(templateFile, serializedData, System.Text.Encoding.Unicode);
                }
            }
            else
            {
                //document set
                var documentSetName = selectedNode.Tag as string;
                var template = DocumentSetProperties[documentSetName];

                DocumentSetSettingsForm settingsForm = new DocumentSetSettingsForm();
                settingsForm.SetSettingsData(template.Settings);

                if (settingsForm.ShowDialog() == DialogResult.OK)
                {
                    SaveDocumentSet(template);

                    foreach (TreeNode setnode in documentSetTreeView.Nodes)
                    {
                        if (setnode.Tag as string == documentSetName)
                        {
                            var name = template.Settings.VisibleName;
                            name = string.IsNullOrWhiteSpace(name) ? documentSetName : name;

                            setnode.Text = name;
                            setnode.ForeColor = Color.Black;
                            break;
                        }
                    }
                }
            }

        }

        private void buttonPrint_Click(object sender, EventArgs e)
        {
            if (selectedNode.Parent == allTemplatesNode)
            {
                //wtf?
            }
            else
            if (selectedNode.Parent != null)
            {
                //single template
                var templateName = selectedNode.Tag as string;

                if (Templates.TryGetValue(templateName, out var templateAbriged))
                {
                    string file;
                    using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                    {
                        saveFileDialog.Filter = "Pdf files (*.pdf)|*.pdf";
                        saveFileDialog.FilterIndex = 0;
                        saveFileDialog.RestoreDirectory = true;
                        saveFileDialog.OverwritePrompt = true;

                        if (saveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            file = saveFileDialog.FileName;
                        }
                        else return;
                    }

                    var templateDirectory = Path.Combine(Directory.GetCurrentDirectory(), Constants.TemplatesDirectory);
                    var templateFile = Path.Combine(templateDirectory, templateName);
                    string data2 = File.ReadAllText(templateFile, System.Text.Encoding.Unicode);

                    var settings = new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.All
                    };

                    PdfTemplateProperties props2 = null;

                    if (!templateAbriged.IsLoaded || templateAbriged.TemplateWithElements == null)
                    {
                        try
                        {
                            props2 = JsonConvert.DeserializeObject<PdfTemplateProperties>(data2, settings);
                            templateAbriged.IsLoaded = true;
                            templateAbriged.TemplateWithElements = props2;
                        }
                        catch (Exception ex)
                        {

                        }
                    }

                    PdfUtils.PrintPdf(props2, file, new Dictionary<string, string>());
                }
            }
            else
            {
                var documentSetName = selectedNode.Tag as string;

                if (DocumentSetProperties.TryGetValue(documentSetName, out var documentSet))
                {
                    string file;
                    using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                    {
                        saveFileDialog.Filter = "Pdf files (*.pdf)|*.pdf";
                        saveFileDialog.FilterIndex = 0;
                        saveFileDialog.RestoreDirectory = true;
                        saveFileDialog.OverwritePrompt = true;

                        if (saveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            file = saveFileDialog.FileName;
                        }
                        else return;
                    }

                    var settings = new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.All
                    };

                    var templateDirectory = Path.Combine(Directory.GetCurrentDirectory(), Constants.TemplatesDirectory);
                   
                    foreach (var templateName in documentSet.Elements.Keys)
                    {
                        PdfTemplatePropertiesAbriged props = null; 
                        var templateFilePath = Path.Combine(templateDirectory, templateName);
                        string data2 = string.Empty;

                        if (!Templates.ContainsKey(templateName))
                        {
                            data2 = File.ReadAllText(templateFilePath, System.Text.Encoding.Unicode);

                            try
                            {
                                props = JsonConvert.DeserializeObject<PdfTemplatePropertiesAbriged>(data2, settings);
                            }
                            catch (Exception ex)
                            {

                            }

                            if (props != null)
                            {
                                props.RootFolderPath = templateDirectory;
                                Templates[templateName] = props;
                            }
                        }
                        else
                        {
                            props = Templates[templateName];
                        }

                        if (props != null)
                        {
                            if (!props.IsLoaded || props.TemplateWithElements == null)
                            {
                                if(string.IsNullOrEmpty(data2))
                                    data2 = File.ReadAllText(templateFilePath, System.Text.Encoding.Unicode);

                                try
                                {
                                    props.TemplateWithElements = JsonConvert.DeserializeObject<PdfTemplateProperties>(data2, settings);
                                    props.IsLoaded = true;
                                }
                                catch (Exception ex)
                                {

                                }

                            }
                        }
                    }
                    
                    PdfUtils.PrintDocumentSetPdf(documentSet, Templates, file);
                }
            }
        }

        private void SetElementSelected(TreeNode node)
        {
            panelUpDown.Visible = false;
            selectedNode = node;

            if (node == null || selectedNode == allTemplatesNode)
            {
                buttonAddPageDocumentSet.Enabled = false;
                buttonEditTemplate.Enabled = false;
                buttonRemoveUniversal.Enabled = false;
                buttonItemSettingsUniversal.Enabled = false;
                buttonToPfdUniversal.Enabled = false;
                buttonEditValues.Enabled = false;
                return;
            }
            
            if (selectedNode.Parent != null)
            {
                //template node
                buttonAddPageDocumentSet.Enabled = false;
                buttonEditValues.Enabled = false;
                buttonEditTemplate.Enabled = true;
            }
            else
            {
                //document set node
                buttonAddPageDocumentSet.Enabled = true;
                buttonEditValues.Enabled = true;
                buttonEditTemplate.Enabled = false;
            }

            buttonRemoveUniversal.Enabled = true;
            buttonItemSettingsUniversal.Enabled = true;
            buttonToPfdUniversal.Enabled = true;
        }

        private void documentSetTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if(e.Action == TreeViewAction.ByKeyboard || e.Action == TreeViewAction.ByMouse)
            {
                SetElementSelected(e.Node);
            }
        }
        
        //recalculates key data for templates, requires file reload
        private void RecalculateKeyDatas(PdfDocumentSetProperties docSet)
        {
            var templateDirectory = Path.Combine(Directory.GetCurrentDirectory(), Constants.TemplatesDirectory);
            var jsonSettings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            };

            List<PdfTemplateProperties> TemplatePropertiesList = new List<PdfTemplateProperties>();
            foreach (var templateName in docSet.Elements.Keys)
            {
                //load template if needed
                PdfTemplatePropertiesAbriged props = null;
                string data = string.Empty;
                var templateFilePath = Path.Combine(templateDirectory, templateName);
                if (!Templates.ContainsKey(templateName))
                {
                    if (File.Exists(templateFilePath))
                    {
                        data = File.ReadAllText(templateFilePath, System.Text.Encoding.Unicode);

                        try
                        {
                            props = JsonConvert.DeserializeObject<PdfTemplatePropertiesAbriged>(data, jsonSettings);
                            Templates[templateName] = props;
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }
                else
                {
                    props = Templates[templateName];
                }

                if (props != null)
                {
                    if (!props.IsLoaded || props.TemplateWithElements == null)
                    {
                        if (string.IsNullOrEmpty(data))
                            data = File.ReadAllText(templateFilePath, System.Text.Encoding.Unicode);

                        try
                        {
                            props.TemplateWithElements = JsonConvert.DeserializeObject<PdfTemplateProperties>(data, jsonSettings);
                            props.IsLoaded = true;
                        }
                        catch (Exception ex)
                        {

                        }

                    }

                    if (props.TemplateWithElements != null)
                        TemplatePropertiesList.Add(props.TemplateWithElements);
                }

            }

            docSet.KeyDatas = PdfUtils.GatherKeyDatas(TemplatePropertiesList);

            for(int i = docSet.KeysOrder.Count - 1; i >= 0; --i)
            {
                var key = docSet.KeysOrder[i];
                if (!docSet.KeyDatas.ContainsKey(key))
                {
                    docSet.KeysOrder.RemoveAt(i);
                }
            }

            foreach(var key in docSet.KeyDatas.Keys)
            {
                if (!docSet.KeysOrder.Contains(key)) docSet.KeysOrder.Add(key);
            }
        }

        private void EditTemplateValuesButton_Click(object sender, EventArgs e)
        {
            if (selectedNode == null || selectedNode.Parent != null || selectedNode == allTemplatesNode) return;

            var selectedDocumentSet = selectedNode.Tag as string;
            if (DocumentSetProperties.TryGetValue(selectedDocumentSet, out var docSet))
            {
                if(docSet.KeyDatas.Keys.Count == 0)
                {
                    RecalculateKeyDatas(docSet);
                    SaveDocumentSet(docSet);
                }

                using (InputKeyPropertiesForm keyedVariablesDataForm = new InputKeyPropertiesForm())
                {
                    keyedVariablesDataForm.Elements = docSet.Elements;
                    keyedVariablesDataForm.TemplateKeyDatas = docSet.KeyDatas;
                    keyedVariablesDataForm.TemplateKeys = docSet.TemplateKeys;
                    keyedVariablesDataForm.TemplateKeyInputProps = docSet.TemplateKeyInputProps;
                    keyedVariablesDataForm.KeysOrdered = docSet.KeysOrder;
                    keyedVariablesDataForm.ExcelKeys = docSet.ExcelKeys;
                    

                    if (keyedVariablesDataForm.ShowDialog() == DialogResult.OK)
                    {
                        docSet.TemplateKeys = keyedVariablesDataForm.TemplateKeys;
                        docSet.TemplateKeyInputProps = keyedVariablesDataForm.TemplateKeyInputProps;
                        docSet.KeysOrder = keyedVariablesDataForm.KeysOrdered;
                        docSet.ExcelKeys = keyedVariablesDataForm.ExcelKeys;
                        SaveDocumentSet(docSet);
                    }
                }
            }
        }

        private void DocumentSetsListForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormElementsCacheSingleton.Instance.Dispose();
        }

        private void buttonSettings_Click_1(object sender, EventArgs e)
        {
            using (GeneralSettingsForm form = new GeneralSettingsForm())
            {
                form.ShowDialog();
                LocalizeForm();
            }
        }

        private void LocalizeForm()
        {
            Thread.CurrentThread.CurrentCulture = Constants.CurrentCulture;
            Thread.CurrentThread.CurrentUICulture = Constants.CurrentCulture;

            this.Text = Resources.DocumentSetsListForm_Title;

            groupBoxDocumentSetOps.Text = Resources.DocumentSetsListForm_DocumentSetOps_Text;
            groupBoxUniversalOps.Text = Resources.DocumentSetsListForm_UniversalOps_Text;
            groupBoxTemplateOps.Text = Resources.DocumentSetsListForm_TemplateOps_Text;

            buttonNewDocumentSet.Text = Resources.DocumentSetsListForm_NewDocumentSet_Text;
            buttonAddPageDocumentSet.Text = Resources.DocumentSetsListForm_AddPageDocumentSet_Text;

            buttonEditValues.Text = Resources.DocumentSetsListForm_EditValues_Text;

            buttonRemoveUniversal.Text = Resources.DocumentSetsListForm_RemoveUniversal_Text;
            buttonItemSettingsUniversal.Text = Resources.DocumentSetsListForm_SettingsUniversal_Text;
            buttonToPfdUniversal.Text = Resources.DocumentSetsListForm_ToPfdUniversal_Text;

            buttonNewPageTemplate.Text = Resources.DocumentSetsListForm_NewPageTemplate_Text;
            buttonEditTemplate.Text = Resources.DocumentSetsListForm_EditTemplate_Text;
            buttonLoadTemplate.Text = Resources.DocumentSetsListForm_LoadTemplate_Text;

            buttonSettingsGeneral.Text = Resources.DocumentSetsListForm_SettingsGeneral_Text;

            buttonReload.Text = Resources.DocumentSetsListForm_Reload_Text;
        }
    }
}
