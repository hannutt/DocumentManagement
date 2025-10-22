using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
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
        public string[] fileList;
        public bool topDirs = false;
        public bool Csearch = false;
        public bool mdata = false;
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
                Trace.WriteLine(file);
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
                if (result == MessageBoxResult.Yes)
                {
                    File.Delete(selectedFile);
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
            try
            {

                string partialFileName = searchFileTxt.Text;
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

            if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.K)
            {
                var selectedFile = (string)lbFiles.SelectedItem;
                kb.copyFile(selectedFile);

            }
            else if (Keyboard.Modifiers == ModifierKeys.Control && e.Key==Key.H)
            {
                var selectedFile = (string)lbFiles.SelectedItem;
                kb.hideFile(selectedFile);
            }
        }
    }
}
