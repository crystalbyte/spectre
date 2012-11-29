#region Licensing notice

// Copyright (C) 2012, Alexander Wieser-Kuciel <alexander.wieser@crystalbyte.de>
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License version 3 as published by
// the Free Software Foundation.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

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
