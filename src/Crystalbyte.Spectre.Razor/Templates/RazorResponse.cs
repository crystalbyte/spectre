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

#endregion

namespace Crystalbyte.Spectre.Razor.Templates {
    /// <summary>
    ///   Class that handles Response output generation inside of
    ///   RazorTemplateBase implementation.
    /// </summary>
    public class RazorResponse : IDisposable {
        /// <summary>
        ///   Internal text writer - default to StringWriter()
        /// </summary>
        public TextWriter Writer = new StringWriter();

        public virtual void Write(object value) {
            Writer.Write(value);
        }

        public virtual void WriteLine(object value) {
            Write(value);
            Write("\r\n");
        }

        public virtual void WriteFormat(string format, params object[] args) {
            Write(string.Format(format, args));
        }

        public override string ToString() {
            return Writer.ToString();
        }

        public virtual void Dispose() {
            Writer.Close();
        }

        /// <summary>
        ///   Allows overriding the TextWriter used write output to.
        ///   Note: This method MUST be called before any output has
        ///   been written to the Response to capture the entire response.
        /// </summary>
        /// <param name="writer"> </param>
        public virtual void SetTextWriter(TextWriter writer) {
            // Close original writer
            if (Writer != null)
                Writer.Close();

            Writer = writer;
        }
    }
}
