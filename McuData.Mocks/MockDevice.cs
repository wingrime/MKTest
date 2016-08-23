using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using McuData.DeviceInterface;
namespace McuData.DeviceInterface.Mocks
{
    public class DeviceMock : IUniversalDevice
    {
        public enum MockBehavior
        {
            DoNotConnected,
            DoCOM1Device,
            DoMultipleDevice,
            DoTimeOutException,
            DoNoAccessException
        }
        private MockBehavior mockBehavior = MockBehavior.DoNotConnected;
        private const string workingDevicePort1 = "COM1";
        private const string workingDevicePort2 = "COM2";
        private string connectedDevicePort = string.Empty;
        public DeviceMock()
        {
            mockBehavior = MockBehavior.DoMultipleDevice;
        }
        public DeviceMock(MockBehavior b)
        {
            mockBehavior = b;
        }
        public DeviceMock(string portName, MockBehavior b)
        {
            mockBehavior = b;
            connectedDevicePort = portName;
        }
        public void Dispose() { return; }
        public string GetFwVersion()
        {
            switch (mockBehavior)
            {
                case MockBehavior.DoCOM1Device:
                    if (workingDevicePort1 == connectedDevicePort)
                        return "ver0.1-rc-mk";
                    else
                        throw new NotConnectedException();
                case MockBehavior.DoMultipleDevice:
                    if (workingDevicePort1 == connectedDevicePort)
                        return "ver0.1-rc-mk";
                    else if (workingDevicePort2 == connectedDevicePort)
                        return "ver0.2-rc-mk2";
                    else
                        throw new NotConnectedException();
                case MockBehavior.DoNotConnected:
                    return string.Empty;
                case MockBehavior.DoTimeOutException:
                    throw new TimeoutException();
            }
            return string.Empty;
        }
        public string GetSerial()
        {
            switch (mockBehavior)
            {
                case MockBehavior.DoCOM1Device:
                    if (workingDevicePort1 == connectedDevicePort)
                        return "0000001"; //TODO: randomize ???
                    else
                        throw new NotConnectedException();
                case MockBehavior.DoMultipleDevice:
                    if (workingDevicePort1 == connectedDevicePort)
                        return "0000001";
                    else if (workingDevicePort2 == connectedDevicePort)
                        return "0000002";
                    else
                        throw new NotConnectedException();
                case MockBehavior.DoNotConnected:
                    return string.Empty;
                case MockBehavior.DoTimeOutException:
                    throw new TimeoutException();
            }
            return string.Empty;
        }
        public IUniversalDevice Connect(string portName)
        {
            return new DeviceMock(portName, mockBehavior);
        }
        public List<PortInfromation> GetPortListing()
        {
            var l = new List<PortInfromation>();
            switch (mockBehavior)
            {
                case MockBehavior.DoCOM1Device:
                    l.Add(new PortInfromation { portDescription = "Moked port interface 1", portName = "COM1" });
                    return l;
                case MockBehavior.DoMultipleDevice:
                    l.Add(new PortInfromation { portDescription = "Moked port interface 1", portName = "COM1" });
                    l.Add(new PortInfromation { portDescription = "Moked port interface 2", portName = "COM2" });
                    return l;
            }
            return l;
        }

        public string DeviceTextCommand(string command)
        {
            //throw new NotImplementedException();
            return "";
        }
    }
}
