using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using McuData.MK;
using McuData.DeviceInterface;
namespace TestingUI
{
    /// <summary>
    /// Interaction logic for ManualTesting.xaml
    /// </summary>
    public partial class ManualTesting : Page
    {
        bool btn1toggle = false;
        bool btn2toggle = false;

        bool btn1toggled = false;
        bool btn2toggled = false;
        public ManualTesting()
        {
            InitializeComponent();
            nextTest.IsEnabled = false;
        }
        private void toggle1Btn(bool status)
        {
            var mk = new MKDevice(DeviceSingltone.CurrentConnection);
            try
            {
                mk.CoilControl(MKCoil.Coil1, status ? LogicLevel.HIGH : LogicLevel.LOW);
            }
            catch (NotConnectedException)
            {
                this.NavigationService.Navigate(new Uri("Connection\\ConnectionError.xaml", UriKind.Relative));
            }
            }
        private void toggle2Btn(bool status)
        {
            var mk = new MKDevice(DeviceSingltone.CurrentConnection);
            try {
                mk.CoilControl(MKCoil.Coil2, status ? LogicLevel.HIGH : LogicLevel.LOW);
            }
            catch (NotConnectedException)
            {
                this.NavigationService.Navigate(new Uri("Connection\\ConnectionError.xaml", UriKind.Relative));
            }
        }
        private void updateNextButtonStatus() {
            if (btn1toggled && btn2toggled)
                nextTest.IsEnabled = true;
        }

        private void btnCoil1_Click(object sender, RoutedEventArgs e)
        {
            updateNextButtonStatus();
            btn1toggled = true;
            if (!btn1toggle)
            {
                btn1toggle = true;
                toggle1Btn(true);
                btnCoil1.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
            }
            else
            {
                btn1toggle = false;
                toggle1Btn(false);
                btnCoil1.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));
            }
        }

        private void btnCoil2_Click(object sender, RoutedEventArgs e)
        {
            updateNextButtonStatus();
            btn2toggled = true;
            if (!btn2toggle)
            {
                toggle2Btn(true);
                btn2toggle = true;
                btnCoil2.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
            }
            else
            {
                toggle2Btn(false);
                btn2toggle = false;
                btnCoil2.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));
            }
        }

        private void nextTest_Click(object sender, RoutedEventArgs e)
        {
            toggle1Btn(false);
            toggle2Btn(false);
            this.NavigationService.Navigate(new Uri("ManualTests\\ManualVoltageSensing_Step4.xaml", UriKind.Relative));
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }
    }
}
