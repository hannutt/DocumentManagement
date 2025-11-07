using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace DocumentManagement
{
    /// <summary>
    /// Interaction logic for imageWindow.xaml
    /// </summary>
    public partial class imageWindow : Window
    {
        public imageWindow()
        {
            InitializeComponent();
        }

        private void rotateImage_Click(object sender, RoutedEventArgs e)
        {
            //Create source
            string imageSource = ImgView.Source.ToString();
            BitmapImage bi = new BitmapImage();
            //BitmapImage properties must be in a BeginInit/EndInit block
            bi.BeginInit();
            bi.UriSource = new Uri(imageSource);
            //Set image rotation
            bi.Rotation = Rotation.Rotate270;
            bi.EndInit();
            //set image source
            ImgView.Source = bi;
        }
    }
}
