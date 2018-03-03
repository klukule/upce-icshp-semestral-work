using System;

namespace Engine
{
    /// <summary>
    /// Represents errors that occur during loading
    /// </summary>
    public sealed class LoadException : MeteorException
    {
        /// <summary>
        /// Constructs a new <see cref="LoadException"/>.
        /// </summary>
        public LoadException() : base("Data loading exception.")
        {
        }

        /// <summary>
        /// Constructs a new <see cref="LoadException"/> with the given message.
        /// </summary>
        /// <param name="message">The exception message.</param>
        public LoadException(string message) : base(message)
        {
        }

        /// <summary>
        /// Constructs a new <see cref="LoadException"/> with the given message and inner exception.
        /// </summary>
        /// <param name="message">The exception message.</param>
        /// <param name="innerException">The inner exception.</param>
        public LoadException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}