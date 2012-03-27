using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Crystalbyte.Chocolate
{
    public sealed class BindingsGenerator {
        private readonly GeneratorSettings _settings;
        public BindingsGenerator(GeneratorSettings settings) {
            _settings = settings;
            _delegeteArchive = new Dictionary<string, string>();
        }

        public void Generate() {
            if (_settings.OutputDirectory.Exists) {
                _settings.OutputDirectory.Delete(true);
            }
            _settings.OutputDirectory.Create();
            var service = new DiscoveryService(_settings);
            var files = service.Discover();
            foreach (var file in files) {
                ParseFile(file);
            }
        }

        public void GenerateAssemblyFile()
        {
            using (var fs = GenerateOutputFile("CefAssembly.cs")) {
                using (var cs = new CSharpCodeWriter(fs)) {
                    cs.WriteDefaultUsingDirectives();
                    cs.BeginNamespace(_settings.Namespace);
                    cs.BeginClass("CefAssembly", false);
                    cs.WriteLine("public const string Name = \"libcef.dll\";");
                    cs.EndClass();
                    cs.EndNamespace();
                }
            }
        }

        private void ParseFile(FileInfo file) {
            var content = string.Empty;
            using (var f = File.OpenRead(file.FullName)) {
                using (var sr = new StreamReader(f)) {
                    content = sr.ReadToEnd();
                }
            }
            var name = CSharpCodeConverter.ConvertFileName(file.Name);
            using (var fs = GenerateOutputFile(name)) {
                using (var cw = new CSharpCodeWriter(fs)) {
                    cw.WriteDefaultUsingDirectives();
                    cw.BeginNamespace(_settings.Namespace);

                    var methods = FindNativeMethods(content);
                    if (methods.Count > 0) {
                        GenerateClass(cw, methods, name, _settings.ClassNameSuffix);
                        cw.WriteLine(string.Empty);
                    }

                    var structs = FindStructures(content);
                    if (structs.Count > 0) {
                        GenerateStruct(cw, structs);
                        cw.WriteLine(string.Empty);
                    }

                    var enums = FindEnums(content);
                    if (enums.Count > 0) {
                        GenerateEnum(cw, enums);
                        cw.WriteLine(string.Empty);
                    }

                    cw.EndNamespace();
                }
            }
        }

        private static void GenerateEnum(CSharpCodeWriter cw, IList<string> enums)
        {
            foreach (var @enum in enums) {
                var name = CSharpCodeConverter.ExtractEnumName(@enum);
                cw.BeginEnum(name);

                var entries = FindEnumEntries(@enum);
                foreach (var entry in entries) {
                    var convert = CSharpCodeConverter.ConvertEnumEntry(entry);
                    cw.WriteLine(convert);
                }

                cw.EndEnum();
            }
        }

        private static IEnumerable<string> FindEnumEntries(string @enum) {
            var splitByComma = @enum.Split('\n');
            for (var i = 1; i < splitByComma.Length - 1; i++) {
                var line = splitByComma[i];
                if (!line.Trim().StartsWith("//") && !string.IsNullOrWhiteSpace(line)) {
                    yield return line.Trim(',');
                }
            }
        }

        private static IList<string> FindEnums(string content)
        {
            const string enumPattern =
                @"enum\s+\w+\s*{(\w+|[=\-_,/\.]|\s)*};";
            var matches = Regex.Matches(content, enumPattern, RegexOptions.Multiline);
            return (from Match match in matches select match.Value).ToList();
        }

        private readonly Dictionary<string,string> _delegeteArchive;

        private void GenerateStruct(CSharpCodeWriter cw, IEnumerable<string> structs) {
            var delegates = new List<string>();
            foreach (var @struct in structs) {
                var name = CSharpCodeConverter.ExtractStructName(@struct);
                cw.BeginStruct(name);

                var members = FindStructMembers(@struct);
                foreach (var member in members) {
                    bool isFunctionPointer;
                    var convertedMember = CSharpCodeConverter.ConvertMember(member, out isFunctionPointer);
                    cw.WriteLine(convertedMember);
                    if (isFunctionPointer) {
                        delegates.Add(member);
                    }
                }

                cw.EndStruct();
                cw.WriteLine(string.Empty);
            }

            foreach (var @delegate in delegates) {
                string name;
                var @d = CSharpCodeConverter.CreateDelegate(@delegate, out name);
                if (_delegeteArchive.ContainsKey(name)) {
                    continue;
                }
                cw.WriteLine(@d);
                _delegeteArchive.Add(name, @delegate);
            }
        }

        private static void GenerateClass(CSharpCodeWriter cw, IEnumerable<string> methods, string name, string suffix) {
            cw.BeginClass(name.Split('.')[0], true, suffix);
            foreach (var method in methods) {
                cw.WriteMethod(method);
                if (methods.Last() != method) {
                    cw.WriteLine(string.Empty);
                }
            }
            cw.EndClass();
        }

        private Stream GenerateOutputFile(string name) {
            return File.Create(Path.Combine(_settings.OutputDirectory.FullName, name));
        }

        private static IEnumerable<string> FindStructMembers(string content) {
            const string memberPattern =
                @"(^\s*\w+\**\s+\w+;)|(^(\s*\w+\**\s+)*\((CEF_CALLBACK)*\s*\*\s*\w+\)\(\s*((const|struct)\s+)*\s*\w+\**\s*\w+(,\s*((const|struct)\s+)*\s*\w+\**\s+\w+)*\);)";
            var matches = Regex.Matches(content, memberPattern, RegexOptions.Multiline);
            return (from Match match in matches select match.Value).ToList();
        }

        private static IList<string> FindStructures(string content) {
            const string structPattern =
                @"typedef(\s)+struct(\s)+\w+\s+\{(\w|\n|\s|[\.&:; /-\|-\(\*\),'""#=!])+}\s+\w+;";
            var matches = Regex.Matches(content, structPattern, RegexOptions.Multiline);
            return (from Match match in matches select match.Value).ToList();
        }

        private static IList<string> FindNativeMethods(string content) {
            const string exportPattern =
                @"CEF_EXPORT(\s)+(\w|\*)+(\s)+(\w)+(\(\)|\((\s)*((const|struct)(\s)+)*(\w)+(\*)?(\s)+(\w)+(,(\s)+((const|struct)(\s)+)*(\w)+(\*)?(\s)+(\w)+)*\)(\s)*);";
            var matches = Regex.Matches(content, exportPattern, RegexOptions.Multiline);
            return (from Match match in matches select match.Value).ToList();
        }
    }
}
