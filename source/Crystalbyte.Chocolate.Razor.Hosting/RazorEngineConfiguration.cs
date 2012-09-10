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
using System.IO;
using System.Text;

#endregion

namespace Crystalbyte.Chocolate.Razor {
    /// <summary>
    ///   Configuration for the Host class. These settings determine some of the
    ///   operational parameters of the RazorHost class that can be changed at
    ///   runtime.
    /// </summary>
    public class RazorEngineConfiguration : MarshalByRefObject {
        /// <summary>
        ///   Determines if assemblies are compiled to disk or to memory.
        ///   If compiling to disk generated assemblies are not cleaned up
        /// </summary>
        public bool CompileToMemory {
            get { return _compileToMemory; }
            set { _compileToMemory = value; }
        }

        private bool _compileToMemory = true;

        /// <summary>
        ///   When compiling to disk use this Path to hold generated assemblies
        /// </summary>
        public string TempAssemblyPath {
            get {
                if (!string.IsNullOrEmpty(_tempAssemblyPath)) {
                    return _tempAssemblyPath;
                }

                return Path.GetTempPath();
            }
            set { _tempAssemblyPath = value; }
        }

        private string _tempAssemblyPath;

        /// <summary>
        ///   Encoding to be used when generating output to file
        /// </summary>
        public Encoding OutputEncoding {
            get { return _outputEncoding; }
            set { _outputEncoding = value; }
        }

        private Encoding _outputEncoding = Encoding.UTF8;


        /// <summary>
        ///   Buffer size for streamed template output when using filenames
        /// </summary>
        public int StreamBufferSize {
            get { return _streamBufferSize; }
            set { _streamBufferSize = value; }
        }

        private int _streamBufferSize = 2048;
    }
}