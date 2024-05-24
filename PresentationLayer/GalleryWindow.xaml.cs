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
using DataObjects;
using LogicLayer;

namespace PresentationLayer
{
    /// <summary>
    /// Interaction logic for GalleryWindow.xaml
    /// </summary>
    public partial class GalleryWindow : Window
    {
        private User _user = null;

        public GalleryWindow(User user)
        {
            _user = user;
            InitializeComponent();
        }

        private void DataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (datGallery.Items.Count == 0)
                {
                    var artManager = new ArtManager();
                    datGallery.ItemsSource = artManager.RetrieveAllArt();

                }
            }
            catch (Exception up)
            {
                MessageBox.Show(up.Message);
            }
        }

        private void datGallery_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selected = (Art)datGallery.SelectedItem;
            var artView = new ArtViewWindow(_user, selected);
            bool result = (bool)artView.ShowDialog();

            if(result)
            {
                var artManager = new ArtManager();
                datGallery.ItemsSource = artManager.RetrieveAllArt();
            }
        }
    }
}
