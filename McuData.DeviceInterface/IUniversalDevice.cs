using System;
using System.Collections.Generic;

namespace McuData.DeviceInterface
{
    /// <summary>
    /// Device Connection Interface used for dependency injection
    /// </summary>
    public interface IUniversalDevice :IDisposable
    {
        /// <summary>
        /// Get current firmware version
        /// </summary>
        /// <returns></returns>
        string GetFwVersion();
        /// <summary>
        /// Get serial number
        /// </summary>
        /// <returns></returns>
        string GetSerial();
        /// <summary>
        /// Connect to specified port
        /// </summary>
        /// <param name="portName"> 'COM1', 'COM2' , 'COM3' </param>
        IUniversalDevice Connect(string portName);
        /// <summary>
        /// Get Avaliable command listing
        /// </summary>
        /// <returns></returns>
        List<PortInfromation> GetPortListing();
        /// <summary>
        /// Simple command interface, for string <-> string communication
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        string DeviceTextCommand(string command);
    }
}