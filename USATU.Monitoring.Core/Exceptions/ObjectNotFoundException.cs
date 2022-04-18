using System.Runtime.Serialization;
using System;

namespace USATU.Monitoring.Core.Exceptions
{
    public class ObjectNotFoundException : Exception
    {
        public ObjectNotFoundException() { }

        public ObjectNotFoundException(string message) : base(message) { }

        public ObjectNotFoundException(string message, Exception inner) : base(message, inner) { }

        protected ObjectNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }

    }
}