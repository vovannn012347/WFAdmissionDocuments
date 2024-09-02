using System;
using System.Windows.Forms;

namespace WFAdmissionDocuments.Code
{
    public class SomeClass
    {
        

        public void OpenFileSelectionDialog()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "All files (*.*)|*.*|Text files (*.txt)|*.txt";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Get the path of specified file
                    string filePath = openFileDialog.FileName;
                    // Handle file path here
                }
            }
        }

        // Function to open folder dialog for directory selection
        public void OpenDirectorySelectionDialog()
        {
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                folderBrowserDialog.Description = "Select a folder";
                folderBrowserDialog.RootFolder = Environment.SpecialFolder.MyComputer;
                folderBrowserDialog.ShowNewFolderButton = false;

                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    // Get the path of selected directory
                    string directoryPath = folderBrowserDialog.SelectedPath;
                    // Handle directory path here
                }
            }
        }
    }
    

    //public class DocumentRoot
    //{
    //    public PanelElement Panel {get; set;}

    //}

    //public class ElementNode
    //{
    //    public Size Size { get; set; }
    //}

    //public static class TemplateUtils
    //{
    //    public static TemplateRoot LoadTemplate(string dirPath)
    //    {

    //    }

    //    public static void SaveTemplate(TemplateRoot template, string dirPath)
    //    {

    //    }

    //}
}
