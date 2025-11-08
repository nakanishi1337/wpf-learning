using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using WinForms = System.Windows.Forms;

namespace PhotoViewer
{
    public class ImgConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                var bi = new BitmapImage();
                bi.BeginInit();
                bi.UriSource = new Uri(value.ToString());
                bi.DecodePixelWidth = 200;
                bi.CacheOption = BitmapCacheOption.OnLoad;
                bi.EndInit();
                bi.Freeze();
                return bi;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }


    public partial class GridPhotoPage : Page
    {
        ObservableCollection<Photo> photos = new ObservableCollection<Photo>();

        public GridPhotoPage()
        {
            InitializeComponent();
            mylistview.ItemsSource = photos;

            var folderPath = AppState.CurrentDirectory;
            if (!string.IsNullOrEmpty(folderPath) && Directory.Exists(folderPath))
            {
                LoadPhotosFromDirectory(folderPath);
            }
        }

        private void LoadPhotosFromDirectory(string folderPath)
        {
            photos.Clear();
            var exts = new[] { ".jpg", ".jpeg", ".png", ".bmp", ".gif" };
            foreach (var file in Directory.GetFiles(folderPath))
            {
                if (Array.Exists(exts, ext => file.EndsWith(ext, StringComparison.OrdinalIgnoreCase)))
                {
                    photos.Add(new Photo { FilePath = file });
                }
            }
        }

        private void Image_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (sender is Image img && img.DataContext is Photo photo)
            {
                var previewPage = new ImagePreviewPage(photo.FilePath);
                this.NavigationService?.Navigate(previewPage);
            }
        }

        private void open_file(object sender, RoutedEventArgs e)
        {
            using (var dialog = new WinForms.FolderBrowserDialog())
            {
                if (dialog.ShowDialog() == WinForms.DialogResult.OK)
                {
                    AppState.CurrentDirectory = dialog.SelectedPath;
                    LoadPhotosFromDirectory(dialog.SelectedPath);
                }
            }
        }
    }
}