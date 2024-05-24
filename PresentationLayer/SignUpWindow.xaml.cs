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
    /// Interaction logic for SignUpWindow.xaml
    /// </summary>
    public partial class SignUpWindow : Window
    {
        private User _user = null;
        private UserManager _userManager = new UserManager();
        
        public SignUpWindow()
        {
            InitializeComponent();
        }

        private void btnSignUp_Click(object sender, RoutedEventArgs e)
        {
            string tbUsername = textboxUsername.Text;
            string tbEmail = textboxEmail.Text;

            if (tbUsername == "" || tbUsername == null)
            {
                MessageBox.Show("Username cannot be blank!");
                textboxUsername.Focus();
                return;
            }

            if (tbUsername.Length > 25)
            {
                MessageBox.Show("Username must be less than 25 characters!");
                textboxUsername.Clear();
                textboxUsername.Focus();
                return;
            }

            if (tbEmail == "" || tbEmail == null)
            {
                MessageBox.Show("Email cannot be blank!");
                textboxEmail.Focus();
                return;
            }

            _user = new User()
            {
                Username = tbUsername,
                Email = tbEmail
            };

            try
            {
                if (_userManager.AddUser(_user))
                {
                    MessageBox.Show("You're in!\nSign in on the next window with your email.\n\nYour temporary password is:\nnewuser", "Yippie!", MessageBoxButton.OK, MessageBoxImage.Exclamation, MessageBoxResult.OK);
                    var loginWindow = new LogInWindow();
                    this.Close();
                    loginWindow.Show();
                }
            }
            catch (Exception up)
            {
                MessageBox.Show("So sorry, an error occured.\nPlease refresh and try again\n\n" + up.Message, "Error!", MessageBoxButton.OK);
                textboxUsername.Clear();
                textboxEmail.Clear();
            }
        
        }

        private void textboxUsername_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                textboxEmail.Focus();
            }
        }

        private void textboxEmail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnSignUp_Click(sender, e);
            }
        }
    }
}
