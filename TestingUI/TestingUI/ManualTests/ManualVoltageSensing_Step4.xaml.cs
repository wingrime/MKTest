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

using McuData.DeviceInterface;
using McuData.MK;
namespace TestingUI
{
    /// <summary>
    /// Interaction logic for ManualVoltageSensing_Step4.xaml
    /// </summary>
    public partial class ManualVoltageSensing_Step4 : Page
    {
        private string setTaskText(int channelIndex) {
            return $"Установите переключатель \"S1\" в положение \"Канал {channelIndex+1}\" и нажимите \"Измерить\"; проверте соответствие измеренного напряжения заданному диапазону";

        }
        private int channelIdx = 0;
        readonly private MKChannel[] channelList = new MKChannel[] {MKChannel.Channel8,MKChannel.Channel7,MKChannel.Channel6,MKChannel.Channel5,
                                                MKChannel.Channel4, MKChannel.Channel3, MKChannel.Channel2, MKChannel.Channel1 }; 
        public ManualVoltageSensing_Step4()
        {
            InitializeComponent();
            taskText.Text = setTaskText(channelIdx);

        }

        private void btnMeasure_Click(object sender, RoutedEventArgs e)
        {
            var mk = new MKDevice(DeviceSingltone.CurrentConnection);
            double result = mk.ChannelVoltage(channelList[channelIdx]);
            blockVoltage.Text += $"U{channelIdx+1}={result}\n";
            if (channelIdx < channelList.Length-1)
            {
                channelIdx++;
                taskText.Text = setTaskText(channelIdx);

            }
            else
            {
                //DONE!
                btnMeasure.IsEnabled = false;
            }
            //TODO add here limits

        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        private void nextTest_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("Connection\\ModeSelection_Step2.xaml", UriKind.Relative));
        }
    }
}
