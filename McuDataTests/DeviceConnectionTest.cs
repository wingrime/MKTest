using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using McuData.DeviceInterface;

namespace McuData.Tests
{
    [TestClass]
    public class DeviceConnectionTest
    {
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void DeviceConnectionInvalidPortNameConstructor()
        {
            ComDevice prober = new ComDevice("ADN");

        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void DeviceConnectionDispose()
        {
            using (var prober = new ComDevice("ADN"))
            {
                // nothing to do here
            }
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void DeviceConnectionGetSerial()
        {
            using (var prober = new ComDevice("ADN"))
            {
                prober.GetSerial();
            }
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void DeviceConnectionGetFWVersion()
        {
            using (var prober = new ComDevice("ADN"))
            {
                prober.GetFwVersion();
            }
        }
       
    }
}