#region Copyright notice

// Copyright (C) 2012 Alexander Wieser-Kuciel <alexander.wieser@crystalbyte.de>
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

#endregion

#region Namespace Directives

using System;
using System.IO;

#endregion

namespace Crystalbyte.Chocolate
{
    public sealed class CSharpCodeWriter : IDisposable
    {
        private readonly StreamWriter _writer;
        private int _indent;

        public CSharpCodeWriter(Stream stream)
        {
            _indent = 0;
            _writer = new StreamWriter(stream);
        }

        #region IDisposable Members

        public void Dispose()
        {
            _writer.Dispose();
        }

        #endregion

        public void WriteDefaultUsingDirectives()
        {
            WriteUsingDirective("System");
            WriteUsingDirective("System.Runtime.InteropServices");
            WriteUsingDirective("System.Collections.Generic");
            WriteUsingDirective("System.Security");
            WriteUsingDirective("Crystalbyte.Chocolate.Projections.Internal");
            _writer.Write(Environment.NewLine);
        }

        public void WriteUsingDirective(string namepace)
        {
            _writer.WriteLine(string.Format("using {0};", namepace));
        }

        public void BeginNamespace(string namepace)
        {
            _writer.WriteLine(string.Format("namespace {0}{1}{{", namepace, Environment.NewLine));
            _indent++;
        }

        public void EndNamespace()
        {
            _indent--;
            WriteLine("}");
        }

        public void BeginClass(string name, bool suppressSecurity = true, string suffix = "")
        {
            var line = string.Format("public static class {0}{1} {{", name, suffix);
            if (suppressSecurity)
            {
                WriteLine("[SuppressUnmanagedCodeSecurity]");
            }
            WriteLine(line);
            _indent++;
        }

        public void EndClass()
        {
            _indent--;
            WriteLine("}");
        }

        public void Append(string text)
        {
            _writer.Write(text);
        }

        public void BeginStruct(string name)
        {
            WriteLine("[StructLayout(LayoutKind.Sequential)]");
            WriteLine(string.Format("public struct {0} {{", name));
            _indent++;
        }

        public void EndStruct()
        {
            _indent--;
            WriteLine("}");
        }

        public void WriteMethod(string method)
        {
            string name;
            var converted = CSharpCodeConverter.ConvertMethod(method, out name);
            var dllImport =
                string.Format(
                    "[DllImport(CefAssembly.Name, EntryPoint = \"{0}\", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]",
                    name);
            WriteLine(dllImport);
            WriteLine(converted);
        }

        public void WriteLine(string input)
        {
            for (var i = 0; i < _indent; i++)
            {
                _writer.Write('\t');
            }
            _writer.WriteLine(input);
        }

        internal void BeginEnum(string name)
        {
            WriteLine(string.Format("public enum {0} {{", name));
            _indent++;
        }

        internal void EndEnum()
        {
            _indent--;
            WriteLine("}");
        }

        public void Write(string text)
        {
            for (var i = 0; i < _indent; i++)
            {
                _writer.Write('\t');
            }
            _writer.Write(text);
        }
    }
}