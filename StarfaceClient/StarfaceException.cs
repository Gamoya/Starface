using System;
using System.Runtime.Serialization;

namespace Gamoya.Phone.Starface {
    [Serializable]
    public class StarfaceException : Exception {
        public StarfaceException() : base() { }
        public StarfaceException(string message) : base(message) { }
        protected StarfaceException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        public StarfaceException(string message, Exception innerException) : base(message, innerException) { }
    }
}
