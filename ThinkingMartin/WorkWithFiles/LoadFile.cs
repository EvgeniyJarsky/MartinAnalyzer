using System;
using System.IO;

namespace WorkWithFiles.LoadFile
{
    public static class LoadFiles
    {
        public static string LoadFile()
        {
            string filePath = "File load error";


            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                // Open document 
                if (File.Exists(dlg.FileName))
                    filePath = dlg.FileName;
            }
            return filePath;
        }
    }
}
