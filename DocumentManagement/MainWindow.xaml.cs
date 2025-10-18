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
            string[] files = Directory.GetFiles(@"C:\Users\Omistaja\Desktop\Udemy.Python\", selectedText, SearchOption.AllDirectories);
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
    }
}
