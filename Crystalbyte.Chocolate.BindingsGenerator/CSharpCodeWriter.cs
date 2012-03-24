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
            WriteLineWithIndent("}");
        }

        public void BeginClass(string name, bool suppressSecurity = true) {
            var line = string.Format("public static class {0} {{", name);
            if (suppressSecurity) {
                WriteLineWithIndent("[SuppressUnmanagedCodeSecurity]");    
            }
            WriteLineWithIndent(line);
            _indent++;
        }

        public void EndClass() {
            _indent--;
            WriteLineWithIndent("}");
        }

        public void WriteLine(string text) {
            WriteLineWithIndent(text);
        }

        public void BeginStruct(string name) {
            WriteLineWithIndent("[StructLayout(LayoutKind.Sequential)]");
            WriteLineWithIndent(string.Format("public struct {0} {{", name));
            _indent++;
        }

        public void EndStruct() {
            _indent--;
            WriteLineWithIndent("}");
        }

        public void WriteMethod(string method) {
            string name;
            var converted = CSharpCodeConverter.ConvertMethod(method, out name);
            var dllImport = string.Format("[DllImport(CefAssembly.Name, EntryPoint = \"{0}\", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]", name);
            WriteLineWithIndent(dllImport);
            WriteLineWithIndent(converted);
        }

        private void WriteLineWithIndent(string input) {
            for (var i = 0; i < _indent; i++) {
                _writer.Write('\t');
            }
            _writer.WriteLine(input);
        }

        internal void BeginEnum(string name)
        {
            WriteLineWithIndent(string.Format("public enum {0} {{", name));
            _indent++;
        }

        internal void EndEnum()
        {
            _indent--;
            WriteLineWithIndent("}");
        }
    }
}
