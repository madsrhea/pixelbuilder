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
    /// Interaction logic for UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
        private User _user = null;
        private User _selectedUser = null;
        private UserManager _userManager = null;
        private List<BitmapImage> _images = null;

        public UserWindow(User user, User selectedUser = null)
        {
            _user = user;
            _selectedUser = selectedUser;
            InitializeComponent();
            LoadUserInfo(_user, _selectedUser);

        }

        private void LoadUserInfo(User user, User selectedUser)
        {
            if (selectedUser != null)
            {
                lblUsername.Content = selectedUser.Username;
                txtboxShortBio.Text = selectedUser.ShortBio;

            }
            else if (user != null)
            {
                lblUsername.Content = user.Username;
                txtboxShortBio.Text = user.ShortBio;
                btnFollow.IsEnabled = false;
            }

            if (_user != null && (string)lblUsername.Content == _user.Username)
            {
                btnFollow.IsEnabled = false;
            }

        }

   
        private void btnFollow_Click(object sender, RoutedEventArgs e)
        {
            if (_user == null)
            {
                MessageBox.Show("You need to be signed in before you can follow " + _selectedUser.Username + "!");
            }

        }


        private void btnEditBio_Click(object sender, RoutedEventArgs e)
        {
            btnEditBio.Visibility = Visibility.Hidden;
            txtboxShortBio.Focusable = true;
        }

        // visibility of the edit button for the user bio
        private void txtboxShortBio_MouseEnter(object sender, MouseEventArgs e)
        {
            if (!txtboxShortBio.Focusable)
            {
                txtboxShortBio.Cursor = Cursors.Arrow;
            }
            else
            {
                txtboxShortBio.Cursor = Cursors.IBeam;
            }

            if (_user != null && (string)lblUsername.Content == _user.Username && !txtboxShortBio.Focusable)
            {
                btnEditBio.Visibility = Visibility.Visible;
            }
        }
        private void btnEditBio_MouseEnter(object sender, MouseEventArgs e)
        {
            if ((string)lblUsername.Content == _user.Username && !txtboxShortBio.Focusable)
            {
                btnEditBio.Visibility = Visibility.Visible;
            }
        }

        private void txtboxShortBio_MouseLeave(object sender, MouseEventArgs e)
        {
            btnEditBio.Visibility = Visibility.Hidden;
        }
        private void btnEditBio_MouseLeave(object sender, MouseEventArgs e)
        {
            btnEditBio.Visibility = Visibility.Hidden;
        }

        private void txtboxShortBio_KeyDown(object sender, KeyEventArgs e)
        {
            string updateBio = txtboxShortBio.Text;
            _userManager = new UserManager();

          if (e.Key == Key.Enter)
            {
                try
                {
                    _user = _userManager.UpdateBio(_user, _user.Email, updateBio);
                }
                catch (Exception up)
                {
                    MessageBox.Show(up.Message + "\n\n" + "up.InnerException.Message");
                }
                MessageBox.Show("Bio successfully updated!");
                txtboxShortBio.Focusable = false;
            }
        }

        private void datUserGallery_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {   
                if (_user == null && datUserGallery.Items.Count == 0)
                {
                    var artManager = new ArtManager();
                    datUserGallery.ItemsSource = artManager.RetrieveArtByUser(_selectedUser.UserID);
                }
                else
                {
                    var artManager = new ArtManager();
                    datUserGallery.ItemsSource = artManager.RetrieveArtByUser(_user.UserID);

                }

            }
            catch (Exception up)
            {
                MessageBox.Show(up.Message);
            }

            lblWorksUploaded.Content = (datUserGallery.Items.Count == 1 ? datUserGallery.Items.Count + " Work in Gallery" : datUserGallery.Items.Count + " Works in Gallery");
        }

        private void datUserGallery_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selected = (Art)datUserGallery.SelectedItem;
            var artView = new ArtViewWindow(_user, selected);
            bool result = (bool)artView.ShowDialog();

            if (result)
            {
                var artManager = new ArtManager();
                datUserGallery.ItemsSource = artManager.RetrieveAllArt();
            }
        }
    }
}
