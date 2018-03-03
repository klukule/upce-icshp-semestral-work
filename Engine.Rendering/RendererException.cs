using System;

namespace Engine.Rendering
{
    /// <summary>
    /// Represents errors that occur in the Rendering library.
    /// </summary>
    public class RendererException : MeteorException
    {
        /// <summary>
        /// Constructs a new <see cref="RendererException"/>.
        /// </summary>
        public RendererException()
        {
        }

        /// <summary>
        /// Constructs a new <see cref="RendererException"/> with the given message.
        /// </summary>
        /// <param name="message">The exception message.</param>
        public RendererException(string message) : base(message)
        {
        }

        /// <summary>
        /// Constructs a new <see cref="RendererException"/> with the given message and inner exception.
        /// </summary>
        /// <param name="message">The exception message.</param>
        /// <param name="innerException">The inner exception.</param>
        public RendererException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}