using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppRangeException
{
    [Serializable]
    public class InvalidRangeException<T>:Exception
    {
        private static readonly string DefaultMessage = "Invalid range";
        public T StartRangeInterval { get; set; }
        public T StopRangeInterval { get; set; }

        /// <summary>
        /// first 3 contructors are required
        /// </summary>
        public InvalidRangeException() : base(DefaultMessage) { }
        public InvalidRangeException(string message) : base(message) { }
        public InvalidRangeException(string message, System.Exception innerException)
        : base(message, innerException) { }

        /// <summary>
        /// constructor of custom exceptio that set range ends
        /// </summary>
        /// <param name="message"></param>
        /// <param name="valueStart"></param>
        /// <param name="valueStop"></param>
        public InvalidRangeException(string message, T valueStart, T valueStop)
        : base(message)
        {
            StartRangeInterval = valueStart;
            StopRangeInterval = valueStop;
        }
        /// <summary>
        /// other constructor that set ranges as innerexception
        /// </summary>
        /// <param name="message"></param>
        /// <param name="valueStart"></param>
        /// <param name="valueStop"></param>
        /// <param name="innerException"></param>
        public InvalidRangeException(string message, T valueStart, T valueStop, System.Exception innerException)
        : base(message, innerException)
        {
            StartRangeInterval = valueStart;
            StopRangeInterval = valueStop;
        }

        protected InvalidRangeException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
