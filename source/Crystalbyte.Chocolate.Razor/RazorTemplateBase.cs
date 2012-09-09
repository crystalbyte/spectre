#region Copyright notice

// Copyright (C) 2012 Alexander Wieser-Kuciel <alexander.wieser@crystalbyte.de>
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

#endregion

#region Namespace directives

using System;

#endregion

namespace Crystalbyte.Chocolate.Razor {
    /// <summary>
    ///   Base class used for Razor Page Templates - Razor generates
    ///   a class from the parsed Razor markup and this class is the
    ///   base class. Class must implement an Execute() method that 
    ///   is overridden by the parser and contains the code that generates
    ///   the markup.  Write() and WriteLiteral() must be implemented
    ///   to handle output generation inside of the Execute() generated
    ///   code.
    /// 
    ///   This class can be subclassed to provide custom functionality.
    ///   One common feature likely will be to provide Context style properties
    ///   that are application specific (ie. HelpBuilderContext) and strongly
    ///   typed and easily accesible in Razor markup code.
    /// </summary>
    public class RazorTemplateBase : MarshalByRefObject, IDisposable {
        /// <summary>
        ///   You can pass in a generic context object
        ///   to use in your template code
        /// </summary>
        public dynamic Context { get; set; }

        /// <summary>
        ///   Any optional result data that the template
        ///   might have to create and return to the caller
        /// </summary>
        public dynamic ResultData { get; set; }


        /// <summary>
        ///   Class that generates output. Currently ultra simple
        ///   with only Response.Write() implementation.
        /// </summary>
        public RazorResponse Response { get; set; }


        /// <summary>
        ///   Class that provides request specific information.
        ///   May or may not have its member data set.
        /// </summary>
        public RazorRequest Request { get; set; }


        /// <summary>
        ///   Instance of the HostContainer that is hosting
        ///   this Engine instance. Note that this may be null
        ///   if no HostContainer is used.
        /// 
        ///   Note this object needs to be cast to the 
        ///   the appropriate Host Container
        /// </summary>
        public object HostContainer { get; set; }

        /// <summary>
        ///   Instance of the RazorEngine object.
        /// </summary>
        public object Engine { get; set; }


        /// <summary>
        ///   This method is called upon instantiation
        ///   and allows passing custom configuration
        ///   data to the template from the Engine.
        /// 
        ///   This method can then be overridden
        /// </summary>
        /// <param name="configurationData"> </param>
        public virtual void InitializeTemplate(object context, object configurationData) {}


        public RazorTemplateBase() {
            Response = new RazorResponse();
            Request = new RazorRequest();
        }

        public virtual void Write(object value) {
            Response.Write(value);
        }

        public virtual void WriteString(object value) {
            Response.Write(value + "\r\n");
        }

        public virtual void WriteLiteral(object value) {
            Response.Write(value);
        }

        /// <summary>
        ///   Razor Parser overrides this method
        /// </summary>
        public virtual void Execute() {}


        public virtual void Dispose() {
            if (Response != null) {
                Response.Dispose();
                Response = null;
            }
        }
    }
}