using System;
using System.IO;

namespace Crystalbyte.Chocolate
{
    public sealed class CSharpCodeWriter : IDisposable {
        private readonly StreamWriter _writer;
        private int _indent;

        public CSharpCodeWriter(Stream stream) {
            _indent = 0;
            _writer = new StreamWriter(stream);
        }

        public void Dispose() {
            _writer.Dispose();
        }

        public void WriteDefaultUsingDirectives() {
            WriteUsingDirective("System");
            WriteUsingDirective("System.Runtime.InteropServices");
            WriteUsingDirective("System.Collections.Generic");
            WriteUsingDirective("System.Security");
            WriteUsingDirective("Crystalbyte.Chocolate.Bindings.Internal");
            _writer.Write(Environment.NewLine);
        }

        public void WriteUsingDirective(string namepace) {
            _writer.WriteLine(string.Format("using {0};", namepace));   
        }

        public void BeginNamespace(string namepace) {
            _writer.WriteLine(string.Format("namespace {0}{1}{{", namepace, Environment.NewLine));
            _indent++;
        }

        public void EndNamespace() {
            _indent--;
            WriteLine("}");
        }

        public void BeginClass(string name, bool suppressSecurity = true, string suffix = "") {
            var line = string.Format("public static class {0}{1} {{", name, suffix);
            if (suppressSecurity) {
                WriteLine("[SuppressUnmanagedCodeSecurity]");    
            }
            WriteLine(line);
            _indent++;
        }

        public void EndClass() {
            _indent--;
            WriteLine("}");
        }

        public void Append(string text) {
            _writer.Write(text);
        }

        public void BeginStruct(string name) {
            WriteLine("[StructLayout(LayoutKind.Sequential)]");
            WriteLine(string.Format("public struct {0} {{", name));
            _indent++;
        }

        public void EndStruct() {
            _indent--;
            WriteLine("}");
        }

        public void WriteMethod(string method) {
            string name;
            var converted = CSharpCodeConverter.ConvertMethod(method, out name);
            var dllImport = string.Format("[DllImport(CefAssembly.Name, EntryPoint = \"{0}\", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]", name);
            WriteLine(dllImport);
            WriteLine(converted);
        }

        public void WriteLine(string input) {
            for (var i = 0; i < _indent; i++) {
                _writer.Write('\t');
            }
            _writer.WriteLine(input);
        }

        internal void BeginEnum(string name) {
            WriteLine(string.Format("public enum {0} {{", name));
            _indent++;
        }

        internal void EndEnum() {
            _indent--;
            WriteLine("}");
        }

        public void Write(string text) {
            for (var i = 0; i < _indent; i++) {
                _writer.Write('\t');
            }
            _writer.Write(text);
        }
    }
}
