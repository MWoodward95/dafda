using System;

namespace Dafda.Consuming
{
    /// <summary>
    /// Thrown when a transport level exception occurs
    /// </summary>
    public sealed class TransportLevelMessageException : Exception
    {
        /// <summary>
        /// Creates a new instance of the exception
        /// </summary>
        /// <param name="innerException"></param>
        public TransportLevelMessageException(Exception innerException) : base("Transport level exception occured when readling message.", innerException)
        {
        }
    }
}
