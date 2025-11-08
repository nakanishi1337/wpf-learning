using System;
using System.Windows;
using System.Windows.Controls;

namespace PhotoViewer
{
    public partial class ImagePreviewPage : Page
    {
        public ImagePreviewPage(string filePath)
        {
            InitializeComponent();
            PreviewImage.Source = new System.Windows.Media.Imaging.BitmapImage(new System.Uri(filePath));
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService?.Navigate(new GridPhotoPage());
        }
    }
}