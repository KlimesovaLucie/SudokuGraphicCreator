using Microsoft.Win32;
using System.IO;

namespace SudokuGraphicCreator.Dialog
{
    /// <summary>
    /// Service for file dialogs.
    /// </summary>
    public class IOService : IIOService
    {
        public string OpenImage()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image files | *.jpg; *.png; *.svg; | All files | *.*";
            return ShowDialogForOpen(dialog);
        }

        public string OpenBooklet()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Xml files | *.xml; | All files | *.*";
            return ShowDialogForOpen(dialog);
        }

        public string OpenTextFile()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Text files | *.txt; | All files | *.*";
            return ShowDialogForOpen(dialog);
        }

        private static string ShowDialogForOpen(OpenFileDialog dialog)
        {
            if (dialog.ShowDialog() == true)
            {
                return dialog.FileName;
            }
            return "";
        }

        public string SaveImage()
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Image files | *.svg; | Image files | *.png; | All files | *.*";
            dialog.RestoreDirectory = true;

            if (dialog.ShowDialog() == true)
            {
                return Path.GetFullPath(dialog.FileName);
            }
            return "";
        }

        public string SaveBooklet()
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Pdf files | *.pdf; | All files | *.*";
            dialog.RestoreDirectory = true;

            if (dialog.ShowDialog() == true)
            {
                return Path.GetFullPath(dialog.FileName);
            }
            return "";
        }

        public string SaveWorkOnBooklet()
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Xml files | *.xml; | All files | *.*";
            dialog.RestoreDirectory = true;

            if (dialog.ShowDialog() == true)
            {
                return Path.GetFullPath(dialog.FileName);
            }
            return "";
        }
    }
}
