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
    /// Interaction logic for DeviceSelect.xaml
    /// </summary>
    public partial class DeviceSelect : Page
    {
        public DeviceSelect()
        {
            InitializeComponent();
        }

        private void selectBlock2_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("Connection\\ModeSelection_Step2.xaml", UriKind.Relative));
        }

        private void selectBlock1_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("Connection\\ModeSelection_Step2.xaml", UriKind.Relative));
        }
    }
}
