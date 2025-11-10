using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.VisualBasic.FileIO;
using SearchOption = System.IO.SearchOption;
namespace DocumentManagement
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public String DirectoryPath; // property
        DBconnection conn = new DBconnection();
        KeyboardCombinations kb = new KeyboardCombinations();
        FileSearch fs = new FileSearch();
        public string[] fileList;
        List<string> dirList = new List<string>();
        public bool topDirs = false;
        public bool Csearch = false;
        public bool mdata = false;
        public bool moveToRecycleBin = false;
        public string[] fileExtensionList;
        public MainWindow()
        {
            InitializeComponent();
            listDirectories();
            conn.fetchSqlData(options);
            conn.readSavedFiles(savedFilescb);
        }

        private void options_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            //ComboBoxItem cbi = (ComboBoxItem)options.SelectedItem;
            string selectedText = (String)options.SelectedItem;
            Trace.WriteLine(selectedText);
            //jos topdirs on true, etsitään vain pääkansiot ilman alikansioita
            if (topDirs)
            {
                fileList = Directory.GetFiles(DirectoryPath, selectedText, SearchOption.TopDirectoryOnly);

            }
            else
            {
                fileList = Directory.GetFiles(DirectoryPath, selectedText, SearchOption.AllDirectories);

            }

            foreach (string file in fileList)
            {

                lbFiles.Items.Add(file);

            }
        }
        //etsii hakemistot c-levyltä.
        private void listDirectories()
        {
            string[] dirs = Directory.GetDirectories("C:\\");
            foreach (string dir in dirs)
            {
                directories.Items.Add(dir);

            }
        }

        //tallentaa valitun tiedostokansion directorypath muuttujaan
        private void directories_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DirectoryPath = (string)directories.SelectedItem + "\\";
        }

        //avaa valitun tiedostotn tuplaklikkauksella
        private void lbFiles_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            var selectedFile = (string)lbFiles.SelectedItem;
            Process.Start(selectedFile);
        }

        private void lbFiles_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            var selectedFile = (string)lbFiles.SelectedItem;
            Trace.WriteLine(selectedFile);
        }


        //toteuttaa valitun tiedoston poiston hiiren oikealla painikkeella
        private void lbFiles_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var selectedFile = (string)lbFiles.SelectedItem;
                MessageBoxResult result = MessageBox.Show("Do you want to delete this file?", selectedFile.ToString() + "Confirmation", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes && moveToRecycleBin == true)
                {
                    FileSystem.DeleteFile(selectedFile,
                         Microsoft.VisualBasic.FileIO.UIOption.AllDialogs,
                         Microsoft.VisualBasic.FileIO.RecycleOption.SendToRecycleBin,
                         Microsoft.VisualBasic.FileIO.UICancelOption.ThrowException);
                    conn.trackDeletedFiles(selectedFile);

                }
                else if (result == MessageBoxResult.Yes && moveToRecycleBin == false)
                {
                    FileSystem.DeleteFile(selectedFile);
                }
                else
                {
                    Trace.WriteLine("Delete cancelled");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }
        //jos checkbox on valittu
        private void mainDirs_Checked(object sender, RoutedEventArgs e)
        {
            topDirs = true;

        }
        //jos checkboxia ei ole valittu
        private void mainDirs_Unchecked(object sender, RoutedEventArgs e)
        {
            topDirs = false;
        }
        //lisää käyttäjän syöttämät tiedostopäätteet fileExt listaan
        private void addFileExtensions_Click(object sender, RoutedEventArgs e)
        {
            string ext = fileExtensions.Text;
            fileExtensionList = ext.Split(',');
            selectedItems.Content += ext;
            fileExtensions.Text = "";


        }

        //2 for each silmukkaa, ensimmäinen käy läpi fileExtension
        //listan, joka sisältää käyttäjän syöttämät tiedostopäätteet
        //*.txt, *.jpg jne jokaisella päättellä etsitään directory getfiles
        //metodin avulla tiedostoja ja tallentaa löydetyt tiedostot
        //filelist listaan toinen silmukka käy läpi filelist sisällön
        //ja tulostaa alkiot  listbox elementtiin
        private void serachBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                foreach (var ext in fileExtensionList)
                {
                    fileList = Directory.GetFiles(DirectoryPath, ext, SearchOption.TopDirectoryOnly);
                    foreach (var file in fileList)
                    {
                        lbFiles.Items.Add(file);

                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void addExtensionToDB_Checked(object sender, RoutedEventArgs e)
        {
            extText.Visibility = Visibility.Visible;
            savedbButton.Visibility = Visibility.Visible;
        }

        private void addExtensionToDB_Unchecked(object sender, RoutedEventArgs e)
        {
            extText.Visibility = Visibility.Hidden;
            savedbButton.Visibility = Visibility.Hidden;
        }
        private void savedbButton_Click(object sender, RoutedEventArgs e)
        {
            conn.saveValues(extText);
            extText.Text = "";
        }
        //tiedoston etsiminen osittaisella nimellä esim readme.txt
        //löytyy readme* hakusanalla
        private void searchFileBtn_Click(object sender, RoutedEventArgs e)
        {
            fs.searchPartialFileName(searchFileTxt.Text, lbFiles, DirectoryPath);

        }

        private void partialSearch_Unchecked(object sender, RoutedEventArgs e)
        {
            searchFileTxt.Visibility = Visibility.Hidden;
            searchFileBtn.Visibility = Visibility.Hidden;

        }

        private void searchCdrive_Checked(object sender, RoutedEventArgs e)
        {
            Csearch = true;
        }

        private void backupBtn_Click(object sender, RoutedEventArgs e)
        {
            conn.BackUpFile(backupInput.Text);
        }

        private void partialSearch_Checked(object sender, RoutedEventArgs e)
        {
            searchFileTxt.Visibility = Visibility.Visible;
            searchFileBtn.Visibility = Visibility.Visible;

        }


        private void readBackup_Click(object sender, RoutedEventArgs e)
        {
            conn.readBackupFile(backupInput.Text, lbFiles);
        }

        private void metadata_Checked(object sender, RoutedEventArgs e)
        {
            mdata = true;

        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            var selectedFile = (string)lbFiles.SelectedItem;
            var multipleSelect = lbFiles.SelectedItems;
            Dictionary<Key, Action> combinations = new Dictionary<Key, Action>();
            //lisätään sanakitjaan näppäin ja sitä vastaava metodi, eli K arvo on copyfile jne
            //()=> avulla asetetaan parametri
            combinations.Add(Key.K, () => kb.copyFile(selectedFile));
            combinations.Add(Key.H, () => kb.hideFile(selectedFile));
            combinations.Add(Key.U, () => kb.unHideFile(hidddenFilesPopup, hdFileList));
            combinations.Add(Key.I, () => kb.imagePreview(selectedFile));
            combinations.Add(Key.Z, () => kb.zipMultipleSelectedFiles(multipleSelect));
            combinations.Add(Key.B, () => kb.UnzipFiles(selectedFile));
            combinations.Add(Key.R, () => kb.restoreFileFromRecycleBin(hidddenFilesPopup, hdFileList));
            //käydään dict foreachissa läpi
            foreach (var item in combinations)
            {
                //jos käyttäjä on painanut control + dictissä löytyvää näppäintä
                if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == item.Key)
                {
                    //kutsutaan painettua näppäintä vastaavaa metodia.
                    combinations[item.Key].Invoke();
                }

            }

        }

        private void addDir_Click(object sender, RoutedEventArgs e)
        {
            string directory = (String)directories.SelectedItem;
            dirList.Add(directory);
            selectedItems.Content += directory;


        }

        private void folderSearch_Click(object sender, RoutedEventArgs e)
        {
            fs.searchFolders(dirList, fName.Text, lbFiles, tView, tvItem);
        }

        //muuttaa piilotetun tiedoston näkyväksi
        private void hdFileList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            string fileName = (String)hdFileList.SelectedItem;
            try
            {
                File.SetAttributes(fileName, FileAttributes.Normal);
                conn.removeHiddeFileFromDb(fileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void hdFileList_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            string fileName = (String)hdFileList.SelectedItem;
            var recycleBin = "C:\\$Recycle.Bin";
            try
            {
                recycleBin.Restore()

            }

        }

        private void clearContent_Click(object sender, RoutedEventArgs e)
        {
            selectedItems.Content = "";
        }

        private void tViewCB_Checked(object sender, RoutedEventArgs e)
        {
            tView.Visibility = Visibility.Visible;
            lbFiles.Visibility = Visibility.Hidden;
        }

        private void tViewCB_Unchecked(object sender, RoutedEventArgs e)
        {
            tView.Visibility = Visibility.Hidden;
            lbFiles.Visibility = Visibility.Visible;
        }

        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            fs.deleteByExtension(directories.SelectedItem, options.SelectedItem, topDirs);
        }

        private void recyclebinCB_Checked(object sender, RoutedEventArgs e)
        {
            moveToRecycleBin = true;
        }

        private void recyclebinCB_Unchecked(object sender, RoutedEventArgs e)
        {
            moveToRecycleBin = false;
        }

        private void closePopup_Click(object sender, RoutedEventArgs e)
        {
            hidddenFilesPopup.IsOpen = false;

        }

      
    }
}
