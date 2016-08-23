namespace McuData.MK
{
    /// <summary>
    /// All possible command strings for MK test firmware
    /// </summary>
    public static class MKCommandStrings 
    {
        /// <summary>
        /// Request serial number
        /// </summary>
        public const string CmdSerial = "SERIAL";
        /// <summary>
        /// Request fw version
        /// </summary>
        public const string CmdFirmware = "FWVERSION";
        /// <summary>
        /// Set kontact 1 on
        /// </summary>
        public const string CmdOnCoil1 = "COIL1_ON";
        /// <summary>
        /// Set kontact 1 off
        /// </summary>
        public const string CmdOffCoil1 = "COIL1_OFF";
        /// <summary>
        /// Set kontact 2 on
        /// </summary>
        public const string CmdOnCoil2 = "COIL2_ON";
        /// <summary>
        /// Set kontact 2 off
        /// </summary>
        public const string CmdOffCoil2 = "COIL2_OFF";
        /// <summary>
        /// Set For Voltage Measure on channel 8 (self power)
        /// </summary>
        public const string CmdVoltageMeasurmentChannel8 = "V8";
        /// <summary>
        /// Set For Voltage Measure on channel 7 
        /// </summary>
        public const string CmdVoltageMeasurmentChannel7 = "V7";
        /// <summary>
        /// Set For Voltage Measure on channel 6
        /// </summary>
        public const string CmdVoltageMeasurmentChannel6 = "V6";
        /// <summary>
        /// Set For Voltage Measure on channel 5
        /// </summary>
        public const string CmdVoltageMeasurmentChannel5 = "V5";
        /// <summary>
        /// Set For Voltage Measure on channel 4
        /// </summary>
        public const string CmdVoltageMeasurmentChannel4 = "V4";
        /// <summary>
        /// Set For Voltage Measure on channel 3
        /// </summary>
        public const string CmdVoltageMeasurmentChannel3 = "V3";
        /// <summary>
        /// Set For Voltage Measure on channel 2
        /// </summary>
        public const string CmdVoltageMeasurmentChannel2 = "V2";
        /// <summary>
        /// Set For Voltage Measure on channel 1
        /// </summary>
        public const string CmdVoltageMeasurmentChannel1 = "V1";
    }
}
