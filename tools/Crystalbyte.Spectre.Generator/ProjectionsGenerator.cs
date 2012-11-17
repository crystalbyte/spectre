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
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

#endregion

namespace Crystalbyte.Spectre
{
    public sealed class ProjectionsGenerator
    {
        private readonly Dictionary<string, string> _delegateArchive;
        private readonly GeneratorSettings _settings;

        public ProjectionsGenerator(GeneratorSettings settings)
        {
            _settings = settings;
            _delegateArchive = new Dictionary<string, string>();
        }

        public void Generate()
        {
            if (_settings.OutputDirectory.Exists) {
                foreach (var file in _settings.OutputDirectory.GetFiles("*.cs").Where(file => file.Name != "AssemblyInfo.cs")) {
                    File.Delete(file.FullName);
                }
                foreach (var dir in _settings.OutputDirectory.GetDirectories().Where(dir => dir.Name != "Properties")) {
                    Directory.Delete(dir.FullName, true);
                }
            }
            else
            {
                _settings.OutputDirectory.Create();
            }

            var service = new DiscoveryService(_settings);
            var files = service.Discover();
            foreach (var file in files)
            {
                Debug.WriteLine(file.Name);
                ParseFile(file);
            }
        }

        public void GenerateAssemblyFile()
        {
            using (var fs = GenerateOutputFile("CefAssembly.cs"))
            {
                using (var cs = new CSharpCodeWriter(fs))
                {
                    cs.WriteDefaultUsingDirectives();
                    cs.BeginNamespace(_settings.Namespace);
                    cs.BeginClass("CefAssembly", false);
                    cs.WriteLine("public const string Name = \"libcef.dll\";");
                    cs.EndClass();
                    cs.EndNamespace();
                }
            }
        }

        private void ParseFile(FileSystemInfo file)
        {
            string content;
            using (var f = File.OpenRead(file.FullName))
            {
                using (var sr = new StreamReader(f))
                {
                    content = sr.ReadToEnd();
                }
            }
            var name = CSharpCodeConverter.ConvertFileName(file.Name);
            using (var fs = GenerateOutputFile(name))
            {
                using (var cw = new CSharpCodeWriter(fs))
                {
                    cw.WriteDefaultUsingDirectives();
                    cw.BeginNamespace(_settings.Namespace);

                    var methods = FindNativeMethods(content);
                    if (methods.Count > 0)
                    {
                        GenerateClass(cw, methods, name, _settings.ClassNameSuffix);
                        cw.WriteLine(string.Empty);
                    }

                    var structs = FindStructures(content);
                    if (structs.Count > 0)
                    {
                        var prefix = DeterminePrefix(name);
                        GenerateStruct(cw, structs, prefix, name.Split('.').First());
                        cw.WriteLine(string.Empty);
                    }

                    var enums = FindEnums(content);
                    if (enums.Count > 0)
                    {
                        var prefix = DeterminePrefix(name);
                        GenerateEnum(cw, enums, prefix);
                        cw.WriteLine(string.Empty);
                    }

                    cw.EndNamespace();
                }
            }
        }

        private static string DeterminePrefix(string name)
        {
            if (name.ToLower().Contains("mac"))
            {
                return "Mac";
            }
            if (name.ToLower().Contains("linux"))
            {
                return "Linux";
            }
            return name.ToLower().Contains("win") ? "Windows" : string.Empty;
        }

        private static void GenerateEnum(CSharpCodeWriter cw, IEnumerable<string> enums, string prefix = "")
        {
            foreach (var @enum in enums)
            {
                var name = CSharpCodeConverter.ExtractEnumName(@enum);
                cw.BeginEnum(prefix + name);

                var entries = FindEnumEntries(@enum);
                foreach (var convert in entries.Select(CSharpCodeConverter.ConvertEnumEntry)) {
                    cw.WriteLine(convert);
                }

                cw.EndEnum();
            }
        }

        private static IEnumerable<string> FindEnumEntries(string @enum)
        {
            var splitByComma = @enum.Split('\n');
            for (var i = 1; i < splitByComma.Length - 1; i++)
            {
                var line = splitByComma[i];
                if (!line.Trim().StartsWith("//") && !string.IsNullOrWhiteSpace(line))
                {
                    yield return line.Trim(',');
                }
            }
        }

        private static IList<string> FindEnums(string content)
        {
            const string enumPattern =
                @"enum\s+\w+\s*{(\n|.)+?};";
            var matches = Regex.Matches(content, enumPattern, RegexOptions.Multiline);
            return (from Match match in matches select match.Value).ToList();
        }

        private void GenerateStruct(CSharpCodeWriter cw, IEnumerable<string> structs, string prefix = "", string className = "")
        {
            var delegates = new List<string>();
            foreach (var @struct in structs)
            {
                var name = CSharpCodeConverter.ExtractStructName(@struct);
                cw.BeginStruct(prefix + name);

                var members = FindStructMembers(@struct);
                foreach (var member in members)
                {
                    IEnumerable<string> attributes;
                    bool isFunctionPointer;
                    var convertedMember = CSharpCodeConverter.ConvertMember(member, out isFunctionPointer, out attributes);
                    foreach (var attribute in attributes)
                    {
                        cw.Write(attribute);
                    }
                    cw.WriteLine(convertedMember);
                    if (isFunctionPointer)
                    {
                        delegates.Add(member);
                    }
                }

                cw.EndStruct();
                cw.WriteLine(string.Empty);
            }

            var container = className + "Delegates";
            cw.BeginClass(container);

            foreach (var @delegate in delegates)
            {
                string name;
                var @d = CSharpCodeConverter.CreateDelegate(@delegate, out name).Trim();

                var original = name;

                var number = 1;
                while (_delegateArchive.ContainsKey(name))
                {
                    var c = number.ToString().First();
                    name = name.TrimEnd(c) + (number+=1);
                }

                if (_delegateArchive.ContainsKey(name)) 
                    continue;

                @d = @d.Replace(original, name);

                cw.WriteLine(@d);
                _delegateArchive.Add(name, @d);
            }

            cw.EndClass();
        }

        private static void GenerateClass(CSharpCodeWriter cw, IEnumerable<string> methods, string name, string suffix)
        {
            cw.BeginClass(name.Split('.')[0], true, suffix);
            var enumerable = methods as string[] ?? methods.ToArray();
            foreach (var method in enumerable)
            {
                cw.WriteMethod(method);
                if (enumerable.Last() != method)
                {
                    cw.WriteLine(string.Empty);
                }
            }
            cw.EndClass();
        }

        private Stream GenerateOutputFile(string name)
        {
            // sometimes this goes wrong, no clue as to why
            return File.Create(Path.Combine(_settings.OutputDirectory.FullName, name));
        }

        private static IEnumerable<string> FindStructMembers(string content)
        {
            const string memberPattern =
                @"(^\s*\w+\**\s+\w+;)|(^(\s*(\w|\*)+\s+)+\((\n|.)+?\)\s*\((\n|.)+?\)\s*;)";
            var matches = Regex.Matches(content, memberPattern, RegexOptions.Multiline);
            return (from Match match in matches select match.Value).ToList();
        }

        private static IList<string> FindStructures(string content)
        {
            const string structPattern =
                @"typedef(\s)+struct(\s)+\w+\s+\{(.|\n)*?\}\s+\w+;";
            var matches = Regex.Matches(content, structPattern, RegexOptions.Multiline);
            return (from Match match in matches select match.Value).ToList();
        }

        private static IList<string> FindNativeMethods(string content)
        {
            const string exportPattern =
                @"CEF_EXPORT(\s)+(\w|\*)+(\s)+(\w)+(\(\)|\((\s)*((const|struct)(\s)+)*(\w)+(\*)?(\s)+(\w)+(,(\s)+((const|struct)(\s)+)*(\w)+(\*)?(\s)+(\w)+)*\)(\s)*);";
            var matches = Regex.Matches(content, exportPattern, RegexOptions.Multiline);
            return (from Match match in matches select match.Value).ToList();
        }
    }
}