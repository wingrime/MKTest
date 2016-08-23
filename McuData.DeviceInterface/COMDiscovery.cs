using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
namespace McuData.DeviceInterface 
{
    /// <summary>
    /// This class control multiple serial devices connected to PC
    /// implements Factory for DeviceConnection
    /// </summary>
    public class ComDiscovery : IDisposable, IDeviceDiscovery
    {

        private Dictionary<string, DeviceInformation> connectedDevices = new Dictionary<string, DeviceInformation>();
        private ConnectionState lastConnectionState = ConnectionState.Unconnected;
        private IUniversalDevice devCon;
        /// <summary>
        /// Default constructor without Depedency Injection
        /// </summary>
        public ComDiscovery() {
            devCon = new ComDevice();
        }
        /// <summary>
        /// Constructor with using Depedency Injection
        /// </summary>
        /// <param name="dC">Injected class </param>
        /// <returns></returns>
        public ComDiscovery(IUniversalDevice dC) {
            devCon = dC;
        }
        /// <summary>
        /// Return current state information without update
        /// </summary>
        /// <returns></returns>
        public ConnectionState GetConnectionState()
        {
            return lastConnectionState;
        }
        /// <summary>
        /// Return list of all connected devices
        /// </summary>
        /// <returns></returns>
        public List<DeviceInformation> GetDevInfo()
        {
            if (connectedDevices.Count == 0)
                throw new NotConnectedException("Нет подключенных устройств");
            return connectedDevices.Values.ToList();
        }
        /// <summary>
        /// Connect to first device, and return Interface, performs automatic discovery
        /// </summary>
        /// <returns></returns>
        public IUniversalDevice Connect() {
            Discovery();
            if (connectedDevices.Count == 0)
                throw new NotConnectedException("Нет подключенных устройств");
           
            return devCon.Connect(connectedDevices.Values.First().portName);
        }
        /// <summary>
        /// Connect to the n'th device starts from 0
        /// </summary>
        /// <param name="n">n</param>
        /// <returns></returns>
        public IUniversalDevice Connect(int n)
        {
            Discovery();
            if (n > connectedDevices.Count || connectedDevices.Count <= 0)
                throw new NotConnectedException("Устройство не подключено");
            return devCon.Connect(connectedDevices.Values.ToList()[n].portName); ;
        }
        /// <summary>
        /// Get infromation about only first device connected
        /// </summary>
        /// <returns></returns>
        public DeviceInformation GetFirstDevInfo()
        {
            if (connectedDevices.Count == 0)
                throw new NotConnectedException("Нет подключенных устройств");
            return connectedDevices.Values.First();
        }
        /// <summary>
        /// Perform discovery procedure and return if there connections avaliable
        /// </summary>
        /// <returns> new connection state</returns>
        public ConnectionState Discovery()
        {
            var connDev = new Dictionary<string, DeviceInformation>();
            ConnectionState connection = ConnectionState.Unconnected;
            List<PortInfromation> portsAndNames = devCon.GetPortListing();
            foreach (var port in portsAndNames)
            {
                DeviceInformation devInfo = null;
                PortProbeResult r = ProbeDevicePort(port, out devInfo);
                // no access to device can means we already connected to it
                if (r == PortProbeResult.Valid || r== PortProbeResult.NoAccessToDevice)
                {
                    connDev.Add(port.portName, devInfo);
                    if (connection == ConnectionState.Unconnected)
                        connection = ConnectionState.SigleConnected;
                    else if (connection == ConnectionState.SigleConnected)
                        connection = ConnectionState.MultipleConnected;
                }
            }
            lastConnectionState = connection;
            connectedDevices = connDev;
            return connection;
        }
        private bool ValidateSerial(string serial) {
            //TODO Regexp!
            if (serial.Length > 10)
                return false;
            else
                return true;

        }
        private bool ValidateFirmware(string fimware) {
            //TODO Regexp!
            if (fimware.Length > 10)
                return false;
            else
                return true;
        }
        /// <summary>
        /// Perfom device probing
        /// </summary>
        /// <param name="portName">port to test</param>
        /// <param name="deviceInfo"> full probing info </param>
        /// <returns> port status </returns>
        private PortProbeResult ProbeDevicePort(PortInfromation port, out DeviceInformation deviceInfo)
        {
            var probeResult = PortProbeResult.Valid;
            deviceInfo = new DeviceInformation();
            deviceInfo.portName = port.portName;
            deviceInfo.portDescription = port.portDescription;
            try
            {
                using (var device = devCon.Connect(port.portName))
                {
                    deviceInfo.deviceFirmware = device.GetFwVersion();
                    deviceInfo.deviceSerial = device.GetSerial();
                }
            }
            catch (Exception e) when (e is NotConnectedException || e is UnauthorizedAccessException ||
            e is TimeoutException || e is IOException)
            {
                deviceInfo.deviceSerial = "<undefined>";
                deviceInfo.deviceFirmware = "<undefined>";
                probeResult = PortProbeResult.ConnectionError;
                if (e is TimeoutException)
                    probeResult = PortProbeResult.TimeOutAnswer;
                else if (e is UnauthorizedAccessException)
                    probeResult = PortProbeResult.NoAccessToDevice;
            }
            if (!ValidateFirmware(deviceInfo.deviceSerial))
                return PortProbeResult.InvalidFWVersion;
            else if (!ValidateSerial(deviceInfo.deviceSerial))
                return PortProbeResult.InvalidSerial;
            else
                return probeResult;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose()
        {
            if (!disposedValue)
            {
                devCon.Dispose();
                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
         ~ComDiscovery() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
           Dispose();
         }

        // This code added to correctly implement the disposable pattern.
        void IDisposable.Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose();
            // TODO: uncomment the following line if the finalizer is overridden above.
            GC.SuppressFinalize(this);
        }
        #endregion

    }


}
