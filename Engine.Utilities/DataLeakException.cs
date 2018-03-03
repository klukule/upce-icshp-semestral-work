using System;

namespace Engine
{
    /// <summary>
    /// Represents errors that occur when data are not in expected format or memory gets corrupted
    /// </summary>
    public sealed class DataLeakException : MeteorException
    {
        /// <summary>
        /// Constructs a new <see cref="DataLeakException"/>.
        /// </summary>
        public DataLeakException() : base("Data leak.")
        {
        }

        /// <summary>
        /// Constructs a new <see cref="DataLeakException"/> with the given message.
        /// </summary>
        /// <param name="message">The exception message.</param>
        public DataLeakException(string message) : base(message)
        {
        }

        /// <summary>
        /// Constructs a new <see cref="LoadException"/> with the given message and inner exception.
        /// </summary>
        /// <param name="message">The exception message.</param>
        /// <param name="innerException">The inner exception.</param>
        public DataLeakException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}