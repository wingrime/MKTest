using System.Collections.Generic;

namespace McuData.DeviceInterface
{
    public interface IDeviceDiscovery
    {
        IUniversalDevice Connect();
        IUniversalDevice Connect(int n);
        ConnectionState Discovery();
        ConnectionState GetConnectionState();
        List<DeviceInformation> GetDevInfo();
        DeviceInformation GetFirstDevInfo();
    }
}