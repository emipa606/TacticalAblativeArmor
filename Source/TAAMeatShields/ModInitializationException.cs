// MeatShieldMod.cs created by Iron Wolf for TAAMeatShields on 08/21/2020 7:26 AM
// last updated 08/21/2020  7:26 AM


using System;
using System.Runtime.Serialization;
using JetBrains.Annotations;

namespace TAAMeatShields
{
    /// <summary>
    ///     exception for when something goes very wrong during mod initialization
    /// </summary>
    /// <seealso cref="System.Exception" />
    [Serializable] //Resharper wants this for some reason 
    public class ModInitializationException : Exception
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ModInitializationException" /> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public ModInitializationException(string message) : base(message)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ModInitializationException" /> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">
        ///     The exception that is the cause of the current exception, or a null reference (Nothing in
        ///     Visual Basic) if no inner exception is specified.
        /// </param>
        public ModInitializationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ModInitializationException" /> class.
        /// </summary>
        /// <param name="serializationInfo">The serialization information.</param>
        /// <param name="streamingContext">The streaming context.</param>
        protected ModInitializationException([NotNull] SerializationInfo serializationInfo,
            StreamingContext streamingContext) :
            base(serializationInfo, streamingContext)
        {
        }
    }
}