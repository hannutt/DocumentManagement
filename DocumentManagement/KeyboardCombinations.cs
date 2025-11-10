using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Aspose.Zip;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Path = System.IO.Path;

namespace DocumentManagement
{
    public class KeyboardCombinations
    {

        DBconnection conn = new DBconnection();
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
                conn.saveHiddenFileName(selectedFile);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }
        public void unHideFile(Popup hidddenFilesPopup, System.Windows.Controls.ListBox hdFileList)
        {
            hidddenFilesPopup.IsOpen = true;
            conn.getHiddenFileNames(hidddenFilesPopup, hdFileList);

        }
        //avaa valitun kuvatiedoston erillisessä wpf-ikkunnassa
        public void imagePreview(string selectedFile)
        {
            imageWindow iw = new imageWindow();
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(selectedFile);
            bitmap.EndInit();
            iw.ImgView.Source = bitmap;
            iw.Show();
        }

        public void MultipleSelectedFiles(System.Collections.IList multipleSelect)
        {
            try
            {
                var fbd = new FolderBrowserDialog();
                fbd.ShowDialog();
                var selectedPath = fbd.SelectedPath;
                var archive = new Archive();
                var filename = "";
                foreach (var item in multipleSelect)
                {
                    //haetaan polusta pelkkä tiedostonimi
                    filename=Path.GetFileName(item.ToString());
                    //parametreina tiedostonimi + koko tiedostopolku
                    archive.CreateEntry(filename, item.ToString());
                    archive.Save($"{selectedPath}\\result.zip");

                }
                
                MessageBox.Show("ZIP archive is ready.");

            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
         


        }
    }
}
