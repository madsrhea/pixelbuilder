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
using System.Windows.Navigation;
using System.Windows.Shapes;
using DataObjects;
using LogicLayer;
using LogicLayerInterfaces;

namespace PresentationLayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public User _user = null;

        public MainWindow()
        {
            InitializeComponent();
            updateGreeting();
            UpdateForLogin();

            menu.Visibility = Visibility.Hidden;
        }

            // customized greeting based off time
        public void updateGreeting()
        {
            DateTime dt = new DateTime();
            dt = DateTime.Now;

            if (dt.Hour >= 05 && dt.Hour <= 12)
            {
                lblGreeting.Content = "Good morning,";
            }
            else if (dt.Hour >= 12 && dt.Hour <= 17)
            {
                lblGreeting.Content = "Good afternoon,";
            }
            else if (dt.Hour >= 17 && dt.Hour <= 21)
            {
                lblGreeting.Content = "Good evening,";
            }
            else if (dt.Hour >= 21 && dt.Hour <= 24)
            {
                lblGreeting.Content = "Good night,";
            }
            else
            { 
                lblGreeting.Content = "Don't stay up too late,";
            }
        }

            // username the same as the users
        public void updateUsername()
        {
            string username = "";
            if (_user == null)
            {
                username = "Guest";
            }
            else
            {
                username = _user.Username;
            }
            lblUsername.Content = username + "!";
        }
        
        /////////////// UPDATE UI FOR USER //////////////

        public void UpdateForLogin()
        {
            updateUsername();

            if (_user != null)
            {
                menu.Visibility = Visibility.Visible;
            }

        }

        public void UpdateForLogout()
        {
            menu.Visibility = Visibility.Hidden;
            _user = null;

            updateUsername();
        }



        //////////////// CLICK EVENTS ////////////////////
  
        private void btnAbout_Click(object sender, RoutedEventArgs e)
        {
            var aboutWindow = new AboutWindow();
            aboutWindow.Show();
        }       

        private void btnUser_Click(object sender, RoutedEventArgs e)
        {
            var loginWindow = new LogInWindow();
            var userWindow = new UserWindow(_user);

            if (_user == null)
            {
                loginWindow.Show();

            }
            else
            {
                userWindow.Show();
            }
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            //var canvasWindow = new CanvasWindow();

            // only users logged in can access the canvas
            if ((string)lblUsername.Content == "Guest!")
            {
                var loginWindow = new LogInWindow();
                loginWindow.Show();
            }
            else
            {
                //canvasWindow.Show();
            }
        }
  
        private void btnGallery_Click(object sender, RoutedEventArgs e)
        {
            var galleryWindow = new GalleryWindow(_user);
            galleryWindow.Show();
        }

        private void btnUsers_Click(object sender, RoutedEventArgs e)
        {
            var usersWindow = new UsersWindow(_user);
            usersWindow.Show();
        }
        
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you'd like to exit?", "Exit application", MessageBoxButton.YesNo,MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Close();
            }
        }

        private void mnuLogOut_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to log out?", "Log-out", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                UpdateForLogout();

            }
        }

        private void mnuPassword_Click(object sender, RoutedEventArgs e)
        {
            var changePasswordWindow = new ChangePasswordWindow(_user);
            changePasswordWindow.Show();
        }
    }
}
