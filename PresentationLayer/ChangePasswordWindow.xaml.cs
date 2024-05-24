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
    /// Interaction logic for ChangePasswordWindow.xaml
    /// </summary>
    public partial class ChangePasswordWindow : Window
    {
        User _user = null;
        UserManager _userManager = null;
        bool _newUser = false;

        public ChangePasswordWindow(User user, UserManager userManager, bool newUser = false)
        {
            _user = user;
            _userManager = userManager;
            _newUser = newUser;

            InitializeComponent();
        }

        public ChangePasswordWindow(User user)
        {
            _user = user;

            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            btnChangePassword.IsDefault = true;

            if (_newUser)
            {
                MessageBox.Show("Welcome to PixelBuilder!\nSince this is your first time logging in, you will be required to enter a new password!\n\nNote: you cannot offically log into your account until your password is changed.");
                textboxEmail.Text = _user.Email;
                textboxEmail.IsEnabled = false;
                textboxOldPassword.Password = "newuser";
                textboxOldPassword.IsEnabled = false;
                textboxNewPassword.Focus();

            }

        }

        private void btnChangePassword_Click(object sender, RoutedEventArgs e)
        {

            string email = textboxEmail.Text;
            string oldPassword = textboxOldPassword.Password;
            string newPassword = textboxNewPassword.Password;
            string confirmPassword = textboxConfirmPassword.Password;

            if (email == "")
            {
                MessageBox.Show("You need to enter your email.");
                textboxEmail.Focus();
                textboxEmail.SelectAll();
                return;
            }

            if (oldPassword == "")
            {
                MessageBox.Show("You need to enter your current password.");
                textboxOldPassword.Focus();
                textboxOldPassword.SelectAll();
                return;
            }

            if (newPassword == "")
            {
                MessageBox.Show("You need to choose a new password.");
                textboxNewPassword.Focus();
                textboxNewPassword.SelectAll();
                return;
            }

            if (confirmPassword == "")
            {
                MessageBox.Show("You'll need to type in your password again to confirm it!");
                textboxConfirmPassword.Focus();
                textboxConfirmPassword.SelectAll();
                return;
            }

            if (newPassword == "newuser" || newPassword == oldPassword)
            {
                MessageBox.Show("Haha, very clever.\nNo, I'm afraid the password has to be different.");
                textboxNewPassword.Focus();
                textboxNewPassword.SelectAll();
                return;
            }


            if (newPassword != confirmPassword)
            {
                MessageBox.Show("New & confirm password don't match!\nTry typing a little slower. ☺");
                textboxConfirmPassword.Clear();
                textboxNewPassword.Focus();                
                textboxNewPassword.Clear();
                return;
            }

            try
            {
                _userManager = new UserManager();

                if (_userManager.ResetPassword(_user, email, newPassword, oldPassword))
                {
                    MessageBox.Show("Password successfully updated!");
                }
                else
                {
                    MessageBox.Show("Bad email and / or password");
                }
            }
            catch (Exception up)
            {
                MessageBox.Show("Update failed 🙁\n\n" + up.InnerException.Message);
            }

            this.Close();
        }
    }
}
