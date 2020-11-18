using System;
using System.Runtime.Serialization;

namespace Server.Exceptions
{
    /// <summary>
    /// Custom snake exception
    /// </summary>
    [Serializable]
    public class SnakeException : Exception
    {
        public SnakeException()
        {
        }

        public SnakeException(string message) : base(message)
        {
        }

        public SnakeException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected SnakeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}