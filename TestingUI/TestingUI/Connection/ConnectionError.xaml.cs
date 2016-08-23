using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TestingUI.Connection
{
    /// <summary>
    /// Interaction logic for ConnectionError.xaml
    /// </summary>
    public partial class ConnectionError : Page
    {
        public ConnectionError()
        {
            InitializeComponent();
        }

        private void resetBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("Connection\\WaitingConnection_Step0.xaml", UriKind.Relative));
        }
    }
}
