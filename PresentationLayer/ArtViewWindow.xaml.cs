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
using LogicLayerInterfaces;

namespace PresentationLayer
{
    /// <summary>
    /// Interaction logic for InformationWindow.xaml
    /// </summary>
    public partial class ArtViewWindow : Window
    {

        private bool followClicked = false;
        private List<Art> _artList = null;
        private Art _art = null;
        private User _user = null;
        private BitmapImage _image = null;
        private bool needsRefresh = false;
        private ArtManager _artManager = null;

        public ArtViewWindow(User user, Art art, List<Art> artList = null)
        {
            _user = user;
            _art = art;
            _artList = artList;
            LocateImage();
            InitializeComponent();
            LoadArtInfo(user, art);
        }

        private void LocateImage()
        {   
            _image = new BitmapImage();
            _image.BeginInit();
            _image.UriSource = new Uri(@"C:\Users\schoo\source\repos\PixelBuilder2\PresentationLayer\images\" + _art.ArtName + "big_" + _art.UserID + ".png");
            _image.EndInit();
        }

        private void LoadArtInfo(User user, Art art)
        {
            btnDelete.Visibility = Visibility.Hidden;
            btnEditDesc.Visibility = Visibility.Hidden;
            lblArt.Content = art.ArtName;
            txtboxDescription.Text = art.Description;
            btnFollow.Content = "By: " + art.Username;

            if (_user != null && _art.Username == _user.Username)
            {
                btnFollow.IsEnabled = false;
                btnDelete.Visibility = Visibility.Visible;
                btnEditDesc.Visibility = Visibility.Visible;
            }

            imgArt.Source = _image;
        }

        private void btnFollow_MouseEnter(object sender, MouseEventArgs e)
        {

            if (followClicked)
            {
                btnFollow.Content = "Unfollow " + _art.Username + "?";
            }
            else
            {
                btnFollow.Content = "Follow " + _art.Username + "?";
            }
        }

        private void btnFollow_MouseLeave(object sender, MouseEventArgs e)
        {
            btnFollow.Content = "By: " + _art.Username;
        }

        private void btnFollow_Click(object sender, RoutedEventArgs e)
        {
            if (_user == null)
            {
                MessageBox.Show("You'll need to sign in before you can follow " + _art.Username, "Whoops!", MessageBoxButton.OK);
            }
            else if (_user != null && _art.Username != _user.Username)
            {
                followClicked = !followClicked;

                if (followClicked)
                {
                    MessageBox.Show(_art.Username + " followed! 🎉", "Followed", MessageBoxButton.OK);
                }
                else
                {
                    MessageBox.Show(_art.Username + " unfollowed 🙁", "Unfollowed", MessageBoxButton.OK);
                }
            }

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete " + _art.ArtName + "?", "Delete", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    _artManager = new ArtManager();
                    _artManager.DeleteArtFromUser(_user.UserID, _art.ArtID);
                }
                catch (Exception up)
                {
                    MessageBox.Show("Error! Please try again later.\n\n" + up.Message);
                }

                MessageBox.Show("Art successfully deleted.");
                needsRefresh = true;
                this.Close();
            }
        }

        private void btnEditDesc_Click(object sender, RoutedEventArgs e)
        {
            txtboxDescription.Focusable = true;
            txtboxDescription.Focus();
        }

        private void txtboxDescription_KeyDown(object sender, KeyEventArgs e)
        {

            _artManager = new ArtManager();

            if (e.Key == Key.Enter)
            {
                string update = txtboxDescription.Text;
                try
                {
                    _artManager.UpdateArtDescription(_art.ArtID, update);
                }
                catch (Exception up)
                {
                    MessageBox.Show(up.Message + "\n\n" + "up.InnerException.Message");
                }
                MessageBox.Show("Description successfully updated!");
                txtboxDescription.Focusable = false;
            }
        }
    }
}
