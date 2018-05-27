// Copyright (c) 2012-2018 Wojciech Figat. All rights reserved.

using System;
using System.Runtime.Serialization;

namespace FlaxEngine
{
    /// <summary>
    /// Flax Engine exception object.
    /// </summary>
    /// <seealso cref="System.SystemException" />
    [Serializable]
    public class FlaxException : SystemException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FlaxException"/> class.
        /// </summary>
        public FlaxException()
        : base("A Flax Runtime error occurred!")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FlaxException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public FlaxException(string message)
        : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FlaxException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not a null reference (Nothing in Visual Basic), the current exception is raised in a catch block that handles the inner exception.</param>
        public FlaxException(string message, Exception innerException)
        : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FlaxException"/> class.
        /// </summary>
        /// <param name="info">The object that holds the serialized object data.</param>
        /// <param name="context">The contextual information about the source or destination.</param>
        protected FlaxException(SerializationInfo info, StreamingContext context)
        : base(info, context)
        {
        }
    }
}
