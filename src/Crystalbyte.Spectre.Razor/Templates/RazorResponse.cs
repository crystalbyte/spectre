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
