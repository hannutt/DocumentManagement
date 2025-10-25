using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static System.Net.Mime.MediaTypeNames;

namespace DocumentManagement
{
    public class FileSearch
    {
        public string[] fileList;
        public void searchFolders(List<string> dirList, string fname, System.Windows.Controls.ListBox lbFiles)
        {

            for (int i = 0; i < dirList.Count; i++)
            {
                Trace.WriteLine(dirList[i]);
                var folderFiles = Directory.GetFiles(dirList[i], fname, SearchOption.AllDirectories);
                //jos muuttujan ei ole tyhjä, eli jotain löytyy
                if (folderFiles.Length > 0)
                {
                    //lisätään löydetty tiedoston fileList listaan
                    fileList = folderFiles;
                }
            }
            foreach (string file in fileList)
            {
               
                lbFiles.Items.Add(file);

            }


        }
        public void searchPartialFileName(string partialFileName, System.Windows.Controls.ListBox lbFiles, string DirectoryPath)
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(DirectoryPath);
                FileInfo[] files = dir.GetFiles(partialFileName + "*", SearchOption.TopDirectoryOnly);
                foreach (var item in files)
                {
                    if (files.Length > 0)
                    {
                        lbFiles.Items.Add(DirectoryPath + item);

                    }
                    else
                    {
                        lbFiles.Items.Add("File not found");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }


}
