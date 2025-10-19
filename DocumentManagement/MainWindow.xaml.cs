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
        public MainWindow()
        {
            InitializeComponent();
            listDirectories();
        }

        private void options_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem cbi = (ComboBoxItem)options.SelectedItem;
            string selectedText = cbi.Content.ToString();
            Trace.WriteLine(selectedText);
            string[] files = Directory.GetFiles(DirectoryPath, selectedText, SearchOption.AllDirectories);
            foreach (string file in files)
            {
                Trace.WriteLine(file);
                lbFiles.Items.Add(file);

            }
        }
        private void listDirectories()
        {
            string[] dirs = Directory.GetDirectories("C:\\");
            foreach (string dir in dirs)
            {
                directories.Items.Add(dir);
                
            }
        }

        private void directories_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DirectoryPath = (string)directories.SelectedItem + "\\";
        }

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



        private void lbFiles_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            var selectedFile = (string)lbFiles.SelectedItem;
            MessageBoxResult result = MessageBox.Show("Do you want to delete this file?",selectedFile.ToString()+ "Confirmation", MessageBoxButton.YesNo);
            if (result==MessageBoxResult.Yes)
            {
                File.Delete(selectedFile);
            }
            else
            {
                Trace.WriteLine("Delete cancelled");
            }

        
       


        }
    }
}
