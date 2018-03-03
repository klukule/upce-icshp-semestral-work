using System;

namespace Engine
{
    /// <summary>
    /// Represents errors that occur in the Meteor engine.
    /// </summary>
    public class MeteorException : Exception
    {
        /// <summary>
        /// Constructs a new <see cref="MeteorException"/>.
        /// </summary>
        public MeteorException()
        {
        }

        /// <summary>
        /// Constructs a new <see cref="MeteorException"/> with the given message.
        /// </summary>
        /// <param name="message">The exception message.</param>
        public MeteorException(string message) : base(message)
        {
        }

        /// <summary>
        /// Constructs a new <see cref="MeteorException"/> with the given message and inner exception.
        /// </summary>
        /// <param name="message">The exception message.</param>
        /// <param name="innerException">The inner exception.</param>
        public MeteorException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}