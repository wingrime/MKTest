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

namespace TestingUI
{
    /// <summary>
    /// Interaction logic for ModeSelection.xaml
    /// </summary>
    public partial class ModeSelection : Page
    {
        public ModeSelection()
        {
            InitializeComponent();
        }

        private void btnManual_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("ManualTests\\ManualTesting_Step3.xaml", UriKind.Relative));
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        private void btnAutomatic_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("AutoTests\\AutoTestUI.xaml", UriKind.Relative));
        }
    }
}
