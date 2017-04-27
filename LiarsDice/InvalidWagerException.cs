using System;
using System.Runtime.Serialization;

namespace LiarsDice
{
    [Serializable]
    internal class InvalidWagerException : Exception
    {
        public InvalidWagerException()
        {
        }

        public InvalidWagerException(string message) : base(message)
        {
        }

        public InvalidWagerException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidWagerException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}