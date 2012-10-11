#region Using directives

using System;
using System.Runtime.Serialization;

#endregion

namespace Crystalbyte.Spectre.Scripting{
    [Serializable]
    public class RuntimeException : Exception{
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public RuntimeException(){}

        public RuntimeException(string message) : base(message){}

        public RuntimeException(string message, Exception inner) : base(message, inner){}

        protected RuntimeException(
            SerializationInfo info,
            StreamingContext context) : base(info, context){}
    }
}