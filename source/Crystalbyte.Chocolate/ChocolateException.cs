#region Namespace Directives

using System;
using System.Runtime.Serialization;

#endregion

namespace Crystalbyte.Chocolate {
    [Serializable]
    public class ChocolateException : Exception {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public ChocolateException() {}
        public ChocolateException(string message) : base(message) {}
        public ChocolateException(string message, Exception inner) : base(message, inner) {}

        protected ChocolateException(
            SerializationInfo info,
            StreamingContext context) : base(info, context) {}
    }
}