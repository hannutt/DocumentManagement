using Aspose.Zip;
using Aspose.Zip.Saving;
using Aspose.Zip.SevenZip;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Linq.Expressions;
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

        public void restoreFileFromRecycleBin(Popup hidddenFilesPopup, System.Windows.Controls.ListBox hdFileList)
        {
            hidddenFilesPopup.IsOpen = true;
            conn.getDeletedFiles(hidddenFilesPopup, hdFileList);

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

        public static string showFolderBrowser()
        {
            var fbd = new FolderBrowserDialog();
            fbd.ShowDialog();
            var selectedPath = fbd.SelectedPath;
            return selectedPath;
        }
        public static string CurrentTime()
        {
            string currentTime = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            return currentTime;
        }


        public void zipMultipleSelectedFiles(System.Collections.IList multipleSelect)
        {
            try
            {

                var zipPath = showFolderBrowser();
                var archive = new Archive();
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result = MessageBox.Show("Protect ZIP with password?", "Notice", buttons);
                if (result == DialogResult.Yes)
                {
                    ZipUsingPassword(zipPath, multipleSelect);

                }
                else
                {
                    var filename = "";
                   
                    foreach (var item in multipleSelect)
                    {
                        //haetaan polusta pelkkä tiedostonimi
                        filename = Path.GetFileName(item.ToString());
                        //parametreina tiedostonimi + koko tiedostopolku
                        archive.CreateEntry(filename, item.ToString());
                        archive.Save($"{zipPath}\\result{CurrentTime()}.zip");

                    }

                    MessageBox.Show("ZIP archive is ready.");
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
          
        
        public void ZipUsingPassword(string zipPath, System.Collections.IList multipleSelect)
        {
            try
            {
                var archive = new Archive(new ArchiveEntrySettings(encryptionSettings: new TraditionalEncryptionSettings("pass")));
                var filename = "";

                foreach (var item in multipleSelect)
                {
                    //haetaan polusta pelkkä tiedostonimi
                    filename = Path.GetFileName(item.ToString());
                    //parametreina tiedostonimi + koko tiedostopolku
                    archive.CreateEntry(filename, item.ToString());
                    archive.Save($"{zipPath}\\result{CurrentTime()}.zip");

                }

            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
         

        }

        public void Zip7z(System.Collections.IList multipleSelect)
        {
            try
            {
                var zipPath = showFolderBrowser();
                var archive = new SevenZipArchive();
                var filename = "";
                string currentTime = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                foreach (var item in multipleSelect)
                {
                    //haetaan polusta pelkkä tiedostonimi
                    filename = Path.GetFileName(item.ToString());
                    //parametreina tiedostonimi + koko tiedostopolku
                    archive.CreateEntry(filename, item.ToString());
                    archive.Save($"{zipPath}\\result{currentTime}.7z");

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        public void UnzipFiles(string selectedFile)
        {
            try
            {
                var unZipPath = showFolderBrowser();
                var archive = new Archive(selectedFile);
                archive.ExtractToDirectory(unZipPath);
                MessageBox.Show("ZIP package decompressed.");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }


        }
    }
}
