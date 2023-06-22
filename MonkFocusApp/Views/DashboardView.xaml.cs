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
using MonkFocusDataAccess;

namespace MonkFocusApp.Views
{
    /// <summary>
    /// Interaction logic for DashboardView.xaml
    /// </summary>
    public partial class DashboardView : UserControl
    {
        public DashboardView(int userId, MonkFocusDbContext context)
        {
            InitializeComponent();
            DataContext = new DashboardViewModel(userId, context);
        }

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
    }
}
