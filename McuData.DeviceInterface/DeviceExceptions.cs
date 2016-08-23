using System;

namespace McuData.DeviceInterface
{
    public class NotConnectedException : Exception
    {
        public NotConnectedException()
        {
        }
        public NotConnectedException(string message)
            : base(message)
        {
        }
        public NotConnectedException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
    public class NotRecognizedAnswer : Exception
    {
        public NotRecognizedAnswer()
        {
        }
        public NotRecognizedAnswer(string message)
            : base(message)
        {
        }
        public NotRecognizedAnswer(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

}