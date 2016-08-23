using System;
using System.Text;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Management;

namespace McuData.DeviceInterface
{
    /// <summary>
    /// Implements Class that make termorary connection, for make sure about it's our device
    /// </summary>
    public class ComDevice : IDisposable, IUniversalDevice
    {
        const int portTimeOut = 4000;
        const int defaultBaudRate = 115200;
        const Parity defaultParity = Parity.None;
        const StopBits defaultStopBits = StopBits.Two;
        const int defaultDataBits = 8;
        private SerialPort serialPort;
        /// <summary>
        /// Default constructor
        /// </summary>
        public ComDevice() {
            // do nothing
        }
        /// <summary>
        /// Create device Prober with specifited port name
        /// </summary>
        /// <param name="portName"> port like COM1, COM2 </param>
        public ComDevice(string portName) {
            serialPort = new SerialPort(portName, defaultBaudRate, defaultParity, defaultDataBits, defaultStopBits);
            serialPort.ReadTimeout = portTimeOut;
            serialPort.WriteTimeout = portTimeOut;
            serialPort.NewLine = "\x0D";
            serialPort.Handshake = Handshake.None;
            serialPort.Encoding = Encoding.GetEncoding(1252);
            serialPort.Open();

        }
        /// <summary>
        /// Perfom connection to device, if last connection not closed it will close last
        /// </summary>
        /// <param name="portName"></param>
        public IUniversalDevice Connect(string portName) {
            return new ComDevice(portName);
          
        }
        /// <summary>
        /// Requests firmware version string from device
        /// </summary>
        /// <returns>Firmware version string</returns>
        public string GetFwVersion() {
            if (checkPort())
            {
                return DeviceTextCommand(DeviceBasicCommandStrings.CmdFirmware);
            }
            throw new NotConnectedException();
        }
        /// <summary>
        /// Requests serial number from device
        /// </summary>
        /// <returns>Firmware version string</returns>
        public string GetSerial() {
            if (checkPort())
            {
                return DeviceTextCommand(DeviceBasicCommandStrings.CmdSerial);
            }
            throw new NotConnectedException();
        }
        /// <summary>
        /// Perfom simple command request to device
        /// </summary>
        /// <param name="command"> Command to the device</param>
        /// <returns> Device answer </returns>
        public string DeviceTextCommand(string command) {
            string res = string.Empty;
            serialPort.DiscardInBuffer();
            serialPort.DiscardOutBuffer();
            serialPort.WriteLine(command);
            //int attempt = 0;
            //while (res == string.Empty && attempt < 1)
            //{
            //   attempt--;
            //    try
            //    {
                    res = serialPort.ReadLine();
            //    }
            //    catch (TimeoutException e) {
            //        break;
            //    }
            //}
            //for debug
            //Console.WriteLine($"Device request {command} answer {res}");
            return res;
        }
        /// <summary>
        /// Perfoms checking serial port is open and valid
        /// </summary>
        /// <returns></returns>
        private bool checkPort() {
            if (serialPort != null)
                if (serialPort.IsOpen)
                    return true;
            return false;
        }
        /// <summary>
        /// Get information for all serial ports can be accessed
        /// maked non static for mocking
        /// </summary>
        /// <returns></returns>
        public List<PortInfromation> GetPortListing()
        {

            List<PortInfromation> devInfo = new List<PortInfromation>();
            var portNames = SerialPort.GetPortNames();
            foreach (string portName in portNames)
            {
                var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PnPEntity WHERE Caption like '%(" + portName + "%'");
                var ports = searcher.Get().Cast<ManagementBaseObject>().ToList().Select(p => p["Caption"].ToString());

                var di = new PortInfromation();
                di.portName = portName;
                di.portDescription = ports.First();
                devInfo.Add(di);
            }
            return devInfo;
        }

        #region IDisposable Support
        private bool disposedValue = false; // detect redundant calls
        /// <summary>
        /// C# Dispose patten
        /// </summary>
        public virtual void Dispose()
        {
            if (!disposedValue)
            {
                serialPort?.Close();
                serialPort?.Dispose();
                serialPort = null;

                disposedValue = true;
            }
        }

         ~ComDevice() {
           Dispose();
        }

        void IDisposable.Dispose()
        {
            Dispose();
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}