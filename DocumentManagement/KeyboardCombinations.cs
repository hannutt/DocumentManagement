using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Shapes;

namespace DocumentManagement
{
    public class KeyboardCombinations
    {
        public void copyFile(string selectedFile)
        {
            try
            {
                //pelkkä tiedostonnimi ilman polkua
                var filename = System.IO.Path.GetFileName(selectedFile);
                FolderBrowserDialog fbd = new FolderBrowserDialog();
                fbd.ShowDialog();
                var targetPath = fbd.SelectedPath;
                //var filename = System.IO.Path.GetFileName(selectedFile);

                File.Copy(selectedFile, targetPath + "\\" + filename);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
          
        }
        public void hideFile(string selectedFile)
        {
            try
            {
                File.SetAttributes(selectedFile, FileAttributes.Hidden);

            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           

        }
    }
}
