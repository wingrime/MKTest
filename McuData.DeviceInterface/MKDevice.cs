using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using McuData.DeviceInterface;
using System.Globalization;

namespace McuData.MK
{
    public enum MKCoil {
        Coil1 = 1,
        Coil2 = 2
    }
    public enum MKChannel {
        Channel8,
        Channel7,
        Channel6,
        Channel5,
        Channel4,
        Channel3,
        Channel2,
        Channel1,
        ChannelSelf = Channel8
    }
    public class MKDevice
    {
        IUniversalDevice deviceConnection;
        public MKDevice(IUniversalDevice devCon) {
            deviceConnection = devCon;
        }
        //private bool connectionStatus = false;

        public void CoilControl(MKCoil coilN,LogicLevel control) {
            string retVal;
            if (deviceConnection == null)
                throw new NotConnectedException();
                switch (coilN) {
                    case MKCoil.Coil1:
                        retVal = deviceConnection?.DeviceTextCommand((control == LogicLevel.HIGH)?MKCommandStrings.CmdOnCoil1: MKCommandStrings.CmdOffCoil1);
                    break;
                    case MKCoil.Coil2:
                        retVal = deviceConnection?.DeviceTextCommand((control == LogicLevel.HIGH) ? MKCommandStrings.CmdOnCoil2 : MKCommandStrings.CmdOffCoil2);
                    break;
                default:
                    throw new NotImplementedException();
            }
            //if (retVal != MKAnswerStrings.Done)
            //    throw new NotRecognizedAnswer();
        }
        public double ChannelVoltage(MKChannel channel) {
            string retStr = string.Empty;
            switch (channel) {
                case MKChannel.Channel1:
                    retStr = deviceConnection.DeviceTextCommand(MKCommandStrings.CmdVoltageMeasurmentChannel1);
                    break;
                case MKChannel.Channel2:
                    retStr = deviceConnection.DeviceTextCommand(MKCommandStrings.CmdVoltageMeasurmentChannel2);
                    break;
                case MKChannel.Channel3:
                    retStr = deviceConnection.DeviceTextCommand(MKCommandStrings.CmdVoltageMeasurmentChannel3);
                    break;
                case MKChannel.Channel4:
                    retStr = deviceConnection.DeviceTextCommand(MKCommandStrings.CmdVoltageMeasurmentChannel4);
                    break;
                case MKChannel.Channel5:
                    retStr = deviceConnection.DeviceTextCommand(MKCommandStrings.CmdVoltageMeasurmentChannel5);
                    break;
                case MKChannel.Channel6:
                    retStr = deviceConnection.DeviceTextCommand(MKCommandStrings.CmdVoltageMeasurmentChannel6);
                    break;
                case MKChannel.Channel7:
                    retStr = deviceConnection.DeviceTextCommand(MKCommandStrings.CmdVoltageMeasurmentChannel7);
                    break;
                case MKChannel.Channel8:
                    retStr = deviceConnection.DeviceTextCommand(MKCommandStrings.CmdVoltageMeasurmentChannel8);
                    break;
                default:
                    throw new NotImplementedException();
            }
            double result = -1;
            //FIXME:
            //if (!double.TryParse(retStr, out result)) {
            //    throw new NotRecognizedAnswer($"Expected result <double> got \"{retStr}\"");
            //}
            return result; 

        }
    }
}
