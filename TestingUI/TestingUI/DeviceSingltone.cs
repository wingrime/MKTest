using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using McuData.DeviceInterface;
using McuData.DeviceInterface.Mocks;
using System.Threading;
namespace TestingUI
{
    public class DeviceSingltone
    {
        private static IDeviceDiscovery DiscoveryService;
        public static IUniversalDevice CurrentConnection { get; private set; }
        public DeviceSingltone() {
             //DiscoveryService = new ComDiscovery();
            DiscoveryService = new MockDiscovery();
        }
        public static ConnectionState DoScaning() {
            DropConnection();
            return DiscoveryService.Discovery();
        }
        public static void ConnectToFirst() {
            DropConnection();
            CurrentConnection = DiscoveryService?.Connect();
        }
        public static void DropConnection() {
            CurrentConnection?.Dispose();
            CurrentConnection = null;
        }
    }
}
