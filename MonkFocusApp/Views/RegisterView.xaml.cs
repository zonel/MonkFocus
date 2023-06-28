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
using MonkFocusApp.ViewModels;

namespace MonkFocusApp.Views
{
    /// <summary>
    /// Interaction logic for RegisterView.xaml
    /// </summary>
    public partial class RegisterView : UserControl
    {
        public RegisterView()
        {
            InitializeComponent();
            DataContext = new RegisterViewModel();
        }


        private void UsernameTextBox_LostFocus(object sender, RoutedEventArgs e) //TODO REMOVE
        {
        }

        #region TextBox Focus Helper
        private void Username_focus(object sender, MouseButtonEventArgs e)
        {
            Username.Focus();
        }
        private void name_focus(object sender, MouseButtonEventArgs e)
        {
            Name.Focus();
        }
        private void Password_focus(object sender, MouseButtonEventArgs e)
        {
            Password.Focus();
        }
        #endregion

        #region ButtonCosmetics
        private void Border_MouseEnter(object sender, MouseEventArgs e)
        {
            Border border = (Border)sender;
            border.Background = new SolidColorBrush(Color.FromArgb(255,33,84,95)); // Change to your desired hover color
        }
        private void Border_MouseLeave(object sender, MouseEventArgs e)
        {
            Border border = (Border)sender;
            border.Background = new SolidColorBrush(Color.FromArgb(255,60,155,176)); // Change to your original color
        }
        #endregion
        private void City_focus(object sender, MouseButtonEventArgs e)
        {
            City.Focus();
        }

        private void Email_focus(object sender, MouseButtonEventArgs e)
        {
            Email.Focus();
        }
    }
}
