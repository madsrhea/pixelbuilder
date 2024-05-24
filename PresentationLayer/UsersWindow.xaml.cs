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
    /// Interaction logic for UsersWindow.xaml
    /// </summary>
    public partial class UsersWindow : Window
    {
        private User _user = null;

        public UsersWindow(User user)
        {
            _user = user;
            InitializeComponent();
        }

        private void btnUser_Click(object sender, RoutedEventArgs e)
        {
            var userProfileWindow = new UserWindow(_user);
            userProfileWindow.ShowDialog();

        }

        private void datUserList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selected = (User)datUserList.SelectedItem;
            var userProfile = new UserWindow(_user, selected);
            userProfile.Show();
        }

        private void datUserList_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (datUserList.Items.Count == 0)
                {
                    var userManager = new UserManager();
                    datUserList.ItemsSource = userManager.RetrieveAllUsers();
                }
            }
            catch (Exception up)
            {
                throw up;
            }

            datUserList.Columns.RemoveAt(0); // userID
            datUserList.Columns.RemoveAt(2); // email
            datUserList.Columns.RemoveAt(3); // updated
            datUserList.Columns.RemoveAt(3); // active
            datUserList.Columns.RemoveAt(3); // roles

            datUserList.Columns[0].Header = "User";
            datUserList.Columns[1].Header = "Tagline";
            datUserList.Columns[2].Header = "Date Joined";
        }
    }
}
