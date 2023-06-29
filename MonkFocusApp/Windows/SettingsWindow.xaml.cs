using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using MonkFocusApp.ViewModels;

namespace MonkFocusApp.Windows
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public SettingsWindow(int userId)
        {
            InitializeComponent();
            DataContext = new SettingsViewModel(userId);
            ProjectRepoButton.Click += Button_Click;
            GitHubProfileButton.Click += Button_Click;

        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Retrieve the URL based on the clicked button
            Button button = (Button)sender;
            string url = GetUrlForButton(button.Name);

            // Launch the default browser with the URL
            //Process.Start(new ProcessStartInfo(url));
            var sInfo = new System.Diagnostics.ProcessStartInfo(url)
            {
                UseShellExecute = true,
            };
            System.Diagnostics.Process.Start(sInfo);
        }

        private string GetUrlForButton(string buttonName)
        {
            // Provide the URL for each button based on its name
            switch (buttonName)
            {
                case "ProjectRepoButton":
                    return "https://github.com/zoneel/MonkFocus";
                case "GitHubProfileButton":
                    return "https://github.com/zoneel";
                default:
                    return string.Empty;
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void TitleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void WakeUpTime_focus(object sender, MouseButtonEventArgs e)
        {
            WakeUp.Focus();
        }

        private void BedTime_focus(object sender, MouseButtonEventArgs e)
        {
            BedTime.Focus();
        }

        private void WorkTime_focus(object sender, MouseButtonEventArgs e)
        {
            WorkTime.Focus();
        }

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
    }
}
