using LogicLayer;
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
using LogicLayerInterfaces;

namespace PresentationLayer
{
    /// <summary>
    /// Interaction logic for LogInWindow.xaml
    /// </summary>
    public partial class LogInWindow : Window
    {

        // now i actually have to think this through step by step 😔
        private User _user = null;
        private UserManager _userManager = new UserManager();
        private bool _signUp = false;

        public LogInWindow(bool signUp = false)
        {
            InitializeComponent();
            _signUp = signUp;
        }

        public LogInWindow(User user)
        { 
            _user = user;
        }

        private void btnLogIn_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            string email = textboxEmail.Text;
            string password = textboxPassword.Password;

            if (email.Length < 6)
            {
                MessageBox.Show("Invalid email address.");
                textboxEmail.Text = "";
                textboxEmail.Focus();
                return;
            }

            if (password == "" || password == null)
            {
                MessageBox.Show("Don't forget to enter a password!");
                textboxPassword.Focus();
                return;
            }

            try
            {
                _user = _userManager.LoginUser(email, password);

                if (textboxPassword.Password == "newuser")
                {
                    var passwordWindow = new ChangePasswordWindow(_user, _userManager, true);


                    if ((bool)passwordWindow.ShowDialog())
                    {
                        MessageBox.Show("Password updated! 👍");
                        textboxPassword.Clear();
                        textboxPassword.Focus();
                        return;
                    }

                    else
                    {
                        // MessageBox.Show("No good. 🙁 Try again later, ok?");
                        // _user = null;
                        textboxEmail.Clear();
                        textboxPassword.Clear();
                        return;
                    }
                }
                else
                {
                    mainWindow._user = _user;
                    mainWindow.UpdateForLogin();
                    this.Close();
                }
            }
            catch (Exception up)
            {
                MessageBox.Show(up.Message + "\n\n" + up.InnerException.Message);
            }
        }

        private void textboxPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnLogIn_Click(sender,e);
            }
        }

        private void textboxEmail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            { 
                textboxPassword.Focus();
            }
        }

        private void btnSignUp_Click(object sender, RoutedEventArgs e)
        {
            var signUpWindow = new SignUpWindow();
            this.Close();
            signUpWindow.ShowDialog();

        }
    }
}
