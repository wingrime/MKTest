using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using McuData.DeviceInterface;
using McuData.MK;

namespace ConUI
{
    class CLI
    {
        static void Main(string[] args)
        {
            Console.WriteLine("---------Console UI for MCU----------------\n");
            ComDiscovery dev = new ComDiscovery();
            ConnectionState state = dev.Discovery();
            Console.WriteLine($"Currect connection state is {state} \n");
            if (state != ConnectionState.Unconnected)
            {
                var info = dev.GetDevInfo();
                foreach (var singleDevice in info)
                {
                    Console.WriteLine($"Device FW:{singleDevice.deviceFirmware}, SER:{singleDevice.deviceSerial} , PORT:{singleDevice.portDescription} ");

                }
                var mk = new MKDevice(dev.Connect());

                for (int i = 0; i < 1000; i++)
                {
                    foreach (MKChannel channel in Enum.GetValues(typeof(MKChannel)))
                    {
                        Console.WriteLine($"Voltage on{ Enum.GetName(typeof(MKChannel), channel)} is { mk.ChannelVoltage(channel)} V");
                    }
                }
            }


            Console.WriteLine("-------------------------------------------\n");

            Console.ReadKey();
        }
    }
}
