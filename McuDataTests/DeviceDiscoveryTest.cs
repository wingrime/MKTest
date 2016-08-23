using McuData;
using System;

using McuData.DeviceInterface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using McuData.DeviceInterface.Mocks;
namespace McuData.Tests
{
    [TestClass]
    public class DeviceConnectionUnitTest
    {
        [TestMethod]
        public void DeviceConnectionDefaultConstructor()
        {
            var dev = new ComDiscovery();
            Assert.AreEqual(dev.GetConnectionState(), ConnectionState.Unconnected);
        }
        [TestMethod]
        public void DeviceConnectionInjectedConstructor()
        {
            var inj = new ComDevice();
            var dev = new ComDiscovery(inj);
            Assert.AreEqual(dev.GetConnectionState(), ConnectionState.Unconnected);
        }
        [TestMethod]
        public void DeviceConnectionDiscovey()
        {
            var dev = new ComDiscovery();
            var connectionState = dev.Discovery();
        }
        [TestMethod]
        public void DeviceConnectionDiscoveyInjectedReal()
        {
            var inj = new ComDevice();
            var dev = new ComDiscovery(inj);
            var connectionState = dev.Discovery();
        }
        [TestMethod]
        public void DeviceConnectionDiscoveyInjectedMock()
        {

            var dev = new ComDiscovery(new DeviceMock(DeviceMock.MockBehavior.DoNotConnected));
            var connectionState = dev.Discovery();
        }

        [TestMethod, ExpectedException(typeof(NotConnectedException))]
        public void DeviceConnectionGetAllDevices()
        {
            var dev = new ComDiscovery();
            var connectionState = dev.Discovery();
            var devicesInformation = dev.GetDevInfo();
        }
        [TestMethod, ExpectedException(typeof(NotConnectedException))]
        public void DeviceConnectionGetAllDevicesInjectedReal()
        {
            var inj = new ComDevice();
            var dev = new ComDiscovery(inj);
            var connectionState = dev.Discovery();
            var devicesInformation = dev.GetDevInfo();
        }
        [TestMethod]
        public void DeviceConnectionGetAllDevicesMockNormalDevice()
        {
            var dev = new ComDiscovery(new DeviceMock(DeviceMock.MockBehavior.DoCOM1Device));
            var connectionState = dev.Discovery();
            var devicesInformation = dev.GetDevInfo();
            Assert.AreEqual(1, devicesInformation.Capacity);
            Assert.AreEqual("COM1", devicesInformation[0].portName);
        }
        [TestMethod]
        public void DeviceConnectionGetAllDevicesMockMultipleDevice()
        {
            var dev = new ComDiscovery(new DeviceMock(DeviceMock.MockBehavior.DoMultipleDevice));
            var connectionState = dev.Discovery();
            var devicesInformation = dev.GetDevInfo();
            Assert.AreEqual(2, devicesInformation.Capacity);
            Assert.AreEqual("COM1", devicesInformation[0].portName);
            Assert.AreEqual("COM2", devicesInformation[1].portName);
        }
        [TestMethod, ExpectedException(typeof(NotConnectedException))]
        public void DeviceConnectionGetAllDevicesMockNoneDevices()
        {
            var dev = new ComDiscovery(new DeviceMock(DeviceMock.MockBehavior.DoNotConnected));
            var connectionState = dev.Discovery();
            var devicesInformation = dev.GetDevInfo();
        }
        [TestMethod, ExpectedException(typeof(NotConnectedException))]
        public void DeviceConnectionGetFirstDevice()
        {
            var dev = new ComDiscovery();
            var connectionState = dev.Discovery();
            var deviceInformation = dev.GetFirstDevInfo();
        }
        [TestMethod, ExpectedException(typeof(NotConnectedException))]
        public void DeviceConnectionGetFirstDeviceInjectedReal()
        {
            var inj = new ComDevice();
            var dev = new ComDiscovery(inj);
            var connectionState = dev.Discovery();
            var deviceInformation = dev.GetFirstDevInfo();
        }
        [TestMethod]
        public void DeviceConnectionGetFirstDeviceMockSingleDevice()
        {
            var dev = new ComDiscovery(new DeviceMock(DeviceMock.MockBehavior.DoCOM1Device));
            var connectionState = dev.Discovery();
            Assert.AreEqual(connectionState, ConnectionState.SigleConnected);
            var deviceInformation = dev.GetFirstDevInfo();
        }
        [TestMethod]
        public void DeviceConnectionGetFirstDeviceMockMultipleDevice()
        {
            var dev = new ComDiscovery(new DeviceMock(DeviceMock.MockBehavior.DoMultipleDevice));
            var connectionState = dev.Discovery();
            Assert.AreEqual(connectionState, ConnectionState.MultipleConnected);
            var deviceInformation = dev.GetFirstDevInfo();
            Assert.AreEqual( "COM1",deviceInformation.portName);
        }
        [TestMethod, ExpectedException(typeof(NotConnectedException))]
        public void DeviceConnectionGetFirstDeviceMockNoDevice()
        {
            var dev = new ComDiscovery(new DeviceMock(DeviceMock.MockBehavior.DoNotConnected));
            var connectionState = dev.Discovery();
            Assert.AreEqual(connectionState, ConnectionState.Unconnected);
            var deviceInformation = dev.GetFirstDevInfo();
        }
        [TestMethod]
        public void DeviceConnectionConnectToFirstMockSingleDevice()
        {
            var dev = new ComDiscovery(new DeviceMock(DeviceMock.MockBehavior.DoCOM1Device));
            var devCon = dev.Connect();
            string testFW = devCon.GetSerial();
            Assert.AreEqual("0000001", testFW); //moked FW should be same 

        }
        [TestMethod]
        public void DeviceConnectionConnectToMockMultipleDevice()
        {
            var dev = new ComDiscovery(new DeviceMock(DeviceMock.MockBehavior.DoMultipleDevice));
            var devCon1 = dev.Connect(0);
            string testFW1 = devCon1.GetSerial();
            Assert.AreEqual("0000001", testFW1); //moked FW should be same
            var devCon2 = dev.Connect(1);
            string testFW2 = devCon2.GetSerial();
            Assert.AreEqual("0000002", testFW2); //moked FW should be same 

        }
        [TestMethod]
        public void DeviceConnectionAutoIDisposeMock()
        {
            var dev = new ComDiscovery(new DeviceMock(DeviceMock.MockBehavior.DoMultipleDevice));
            using (var devCon = dev.Connect())
            {
                string testFW = devCon.GetSerial();
                Assert.AreEqual("0000001", testFW);
            }
        }
        [TestMethod]
        public void DeviceConnectionManualDisposeMock()
        {
            var dev = new ComDiscovery(new DeviceMock(DeviceMock.MockBehavior.DoMultipleDevice));
            var devCon = dev.Connect();
            string testFW = devCon.GetSerial();
            Assert.AreEqual("0000001", testFW);
            devCon.Dispose();
            //todo add code for handling assertions
        }
        [TestMethod,ExpectedException(typeof(NotConnectedException))]
        public void DeviceConnectionConnectToDeviceExceptionTestMock()
        {
            var dev = new ComDiscovery(new DeviceMock(DeviceMock.MockBehavior.DoMultipleDevice));
            var devCon1 = dev.Connect(4); //should trow
            var devCon2 = dev.Connect(-1);//should trow
        }
        [TestMethod, ExpectedException(typeof(NotConnectedException))]
        public void DeviceConnectionConnectToFirstDeviceExceptionTestMock()
        {
            var dev = new ComDiscovery(new DeviceMock(DeviceMock.MockBehavior.DoNotConnected));
            var devCon1 = dev.Connect();//should trow
        }
    }
}