namespace McuData.DeviceInterface
{
    public static class DeviceBasicCommandStrings {
        public const string CmdSerial = "SERIAL";
        public const string CmdFirmware = "FWVERSION";
        public const string CmdBoard = "BOARD";
    }
    /// <summary>
    /// State of connection to device
    /// </summary>
    public enum ConnectionState
    {
        /// <summary> Single device detected</summary>
        SigleConnected,
        /// <summary> No device detected </summary>
        Unconnected,
        /// <summary>Detected multiple devices connected </summary>
        MultipleConnected
    }
    /// <summary>
    /// Device information from auto discovery
    /// </summary>
    public class DeviceInformation
    {
        /// <summary>
        /// connection port for example 'COM1', 'COM2'
        /// </summary>
        public string portName;
        /// <summary>
        /// full name of port
        /// </summary>
        public string portDescription;
        /// <summary>
        /// connected device serial number string
        /// </summary>
        public string deviceSerial;
        /// <summary>
        /// connected device firmware version string
        /// </summary>
        public string deviceFirmware;
    }
    /// <summary>
    /// Information about ports
    /// </summary>
    public class PortInfromation
    {
        /// <summary>
        /// short port name 'com1', 'com2'
        /// </summary>
        public string portName;
        /// <summary>
        /// full port name description
        /// </summary>
        public string portDescription;

    }
    public enum PortProbeResult
    {
        /// <summary>
        /// it's our device
        /// </summary>
        Valid,
        /// <summary>
        /// FWVersion is not recongnized
        /// </summary>
        InvalidFWVersion,
        /// <summary>
        ///  Serial is not recongnized
        /// </summary>
        InvalidSerial,
        /// <summary>
        ///  Answer TimeOut
        /// </summary>
        TimeOutAnswer,
        /// <summary>
        /// Answer is invalid, it's not our device
        /// </summary>
        InvalidAnswer,
        /// <summary>
        /// Can't connect to device (generic)
        /// </summary>
        ConnectionError,
        /// <summary>
        /// Can't connect, no access exception!
        /// </summary>
        NoAccessToDevice
    }
    public enum LogicLevel
    {
        LOW = 0,
        HIGH = 1
    }
}