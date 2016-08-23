using System;
using McuData.DeviceInterface;
using System.Collections.Generic;

namespace McuData.DeviceInterface.Mocks
{   public class MockDiscovery : IDeviceDiscovery 
    {
        public IUniversalDevice Connect()
        {
            return new DeviceMock();
        }

        public IUniversalDevice Connect(int n)
        {
            return new DeviceMock();
        }

        public ConnectionState Discovery()
        {
            return ConnectionState.MultipleConnected;
        }

        public ConnectionState GetConnectionState()
        {
            return ConnectionState.MultipleConnected;
        }

        public List<DeviceInformation> GetDevInfo()
        {
            var l = new List<DeviceInformation> ();
            var e1 = new DeviceInformation();
            e1.deviceFirmware = "000";
            e1.deviceSerial = "22";
            e1.portDescription = "Mock Device 1";
            e1.portName = "COM1";
            l.Add(e1);
            var e2 = new DeviceInformation();
            e2.deviceFirmware = "00220";
            e2.deviceSerial = "2222";
            e2.portDescription = "Mock Device 2";
            e2.portName = "COM2";
            l.Add(e2);

            return l;

        }

        public DeviceInformation GetFirstDevInfo()
        {
            var e1 = new DeviceInformation();
            e1.deviceFirmware = "000";
            e1.deviceSerial = "22";
            e1.portDescription = "Mock Device 1";
            e1.portName = "COM1";
            return e1;
        }
    }
}
