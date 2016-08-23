using McuData.DeviceInterface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McuData.Tests
{
    [TestClass]
    public class NotConnectedExceptionTest
    {
        [TestMethod]
        public void NotConnectedExceptionDefault()
        {
            var nc =  new NotConnectedException();
        }
        [TestMethod]
        public void NotConnectedExceptionString()
        {
            const string msg = "Нет подключенных устройств";
            var nc = new NotConnectedException(msg);
            Assert.AreEqual(msg, nc.Message);
        }
        [TestMethod]
        public void NotConnectedExceptionInner()
        {
            const string msg = "Нет подключенных устройств";
            var nc = new NotConnectedException(msg, new TimeoutException());
            Assert.AreEqual(msg, nc.Message);
        }
    }
}
