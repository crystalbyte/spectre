#region Licensing notice

// Copyright (C) 2012, Alexander Wieser-Kuciel <alexander.wieser@crystalbyte.de>
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//    http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

#endregion

#region Using directives

using System;
using System.IO;
using System.Text;

#endregion

namespace Crystalbyte.Spectre.Razor.Hosting {
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
            get { return _CompileToMemory; }
            set { _CompileToMemory = value; }
        }

        private bool _CompileToMemory = true;

        /// <summary>
        ///   When compiling to disk use this Path to hold generated assemblies
        /// </summary>
        public string TempAssemblyPath {
            get {
                if (!string.IsNullOrEmpty(_tempAssemblyPath))
                    return _tempAssemblyPath;

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
