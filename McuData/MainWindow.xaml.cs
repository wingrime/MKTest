using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using System.Windows.Threading;
using McuData.DeviceInterface;
using McuData.MK;
namespace McuData
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer timer;
        private ComDiscovery discovery;
        private MKDevice mk;

        public MainWindow()
        {
            InitializeComponent();
            discovery = new ComDiscovery();
        }
        private void startTimer() {
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timerTick);
            timer.Interval = new TimeSpan(0, 0, 0, 4);
            timer.Start();
        }
        private void timerTick(object sender, EventArgs e) {
            UpdateConnectionStatus();
        }
        private static string PrintableDeviceName(DeviceInformation devInfo) {
            return $"Подключен к {devInfo.portDescription}";

        }
        private void UpdateConnectionStatus() {
            portSelection.Items.Clear();
            
            var state  = discovery.Discovery();
            switch (state) {
                case ConnectionState.Unconnected:
                    dataTransmissionPanel.Visibility = Visibility.Collapsed;
                    portSelection.IsEnabled = false;
                    portSelection.Text = "Не подключен";
                    if (mk != null)
                        {
                        mk = null;

                    }
                    
                    return;
                case ConnectionState.SigleConnected:
                    dataTransmissionPanel.Visibility = Visibility.Visible;
                    portSelection.IsEnabled = false;
                    portSelection.Text = PrintableDeviceName(discovery.GetFirstDevInfo());
                    if (mk == null)
                        mk = new MKDevice(discovery.Connect());
                    return;
                case ConnectionState.MultipleConnected:
                    dataTransmissionPanel.Visibility = Visibility.Visible;
                    portSelection.IsEnabled = true;
                    var ports = discovery.GetDevInfo().Select(p => PrintableDeviceName(p));
                    foreach (var port in ports) {
                        var item = new ComboBoxItem();
                        item.Content = port;
                        portSelection.Items.Add(item);
                        if (mk == null)
                            mk = new MKDevice(discovery.Connect());
                    }
                    break;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateConnectionStatus();
            startTimer();
        }


        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            //if (serialPort != null && serialPort.IsOpen)
            //    serialPort.Close();
        }

        private void portSelection_DropDownOpened(object sender, EventArgs e)
        {
            UpdateConnectionStatus();
            
        }

        private void portSelection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if user selected connection different that before do reconnect;
            //if (portSelection.SelectedItem == null)
            //    return;
            //dataTransmissionPanel.Visibility = Visibility.Collapsed;
            //string currentPortName = ((ComboBoxItem)portSelection.SelectedItem).Content.ToString().Split(' ')[0];
            //if (serialPort != null && serialPort.IsOpen)
            //    serialPort.Close();
            //try
            //{
            //    serialPort = new SerialPort(currentPortName);
            //    serialPort.Open();
            //}
            //catch
            //{
            //    portStatus.Content = "Ошибка открытия порта " + (currentPortName);
            //    portStatus.Foreground = Brushes.Red;
            //    return;
            //}

            //if (serialPort.IsOpen)
            //    dataTransmissionPanel.Visibility = Visibility.Visible;
            //portStatus.Content = "Порт \"" + currentPortName + "\" подключен";
            //portStatus.Foreground = Brushes.Green;  

        }




    }
}
