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
using System.Windows.Threading;
using McuData.DeviceInterface;

namespace TestingUI
{
    /// <summary>
    /// Interaction logic for WaitingConnection.xaml
    /// </summary>
    public partial class WaitingConnection : Page
    {
        private DispatcherTimer timerScanning;
        private DispatcherTimer timerTimeOutWarning;

        public WaitingConnection()
        {
            InitializeComponent();
            
            //
            timerScanning = new DispatcherTimer();
            timerScanning.Tick += new EventHandler(timerScanHandler);
            timerScanning.Interval = new TimeSpan(0, 0, 0, 4);
            timerScanning.Start();
            //
            timerTimeOutWarning = new DispatcherTimer();
            timerTimeOutWarning.Tick += new EventHandler(timerTimeoutHandler);
            timerTimeOutWarning.Interval = new TimeSpan(0, 0, 0, 30);
            timerTimeOutWarning.Start();
        }

        private void timerTimeoutHandler(object sender, EventArgs e)
        {
            (sender as DispatcherTimer).Stop();
            warningLabel.Visibility = Visibility.Visible;
        }

        private void timerScanHandler(object sender, EventArgs e)
        {
            switch (DeviceSingltone.DoScaning()) {
                case ConnectionState.Unconnected:
                    //contine waiting
                    break;
                case ConnectionState.SigleConnected:
                    DeviceSingltone.ConnectToFirst();
                    this.NavigationService.Navigate(new Uri("Connection\\ModeSelection_Step2.xaml", UriKind.Relative));
                    timerScanning.Stop();
                    timerScanning = null;
                    break;
                case ConnectionState.MultipleConnected:
                    //TODO
                    //this.NavigationService.Navigate(new Uri("DeviceSelect.xaml", UriKind.Relative));
                    DeviceSingltone.ConnectToFirst();
                    this.NavigationService.Navigate(new Uri("Connection\\ModeSelection_Step2.xaml", UriKind.Relative));

                    timerScanning.Stop();
                    timerScanning = null;
                    break;

            }
        }
    }
}
