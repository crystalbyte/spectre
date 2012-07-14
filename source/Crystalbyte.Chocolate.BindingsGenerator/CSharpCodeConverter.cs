#region Namespace Directives

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

#endregion

namespace Crystalbyte.Chocolate {
    public static class CSharpCodeConverter {
        private static string StripUnnecessarySymbols(string input) {
            input = input.Trim(';').Trim();
            var noExport = input.Replace("CEF_EXPORT", string.Empty);
            var noConst = noExport.Replace("const", string.Empty);
            var noStruct = noConst.Replace("struct", string.Empty);
            var noNewLine = noStruct.Replace("\n", string.Empty).Replace("\r", string.Empty);
            var noEnum = noNewLine.Replace("enum", string.Empty);
            var noBrackets = noEnum.Replace("(", " ").Replace(")", " ").Replace(",", string.Empty).Trim();
            return noBrackets;
        }

        public static string ExtractStructName(string input) {
            input = input.Trim();
            var splitBySpace = input.Split(' ');
            var name = splitBySpace.Last().TrimEnd(';');
            return ConvertTypeName(name);
        }

        private static string AdjustArgumentName(string name) {
            name = name.FirstToLower();
            if (name == "object") {
                return "@object";
            }
            if (name == "string") {
                return "@string";
            }

            if (name == "event") {
                return "@event";
            }
            if (name == "params") {
                return "@params";
            }
            if (name == "checked") {
                return "@checked";
            }
            return name;
        }

        public static string ConvertMethod(string input, out string name) {
            input = StripUnnecessarySymbols(input);
            var splitBySpace = input.Split(new[] {" "}, StringSplitOptions.RemoveEmptyEntries);

            var returnValue = splitBySpace.First();
            name = splitBySpace[1];
            var args = new List<string>();

            if (splitBySpace.Length > 2) {
                for (var i = 2; i < splitBySpace.Length; i++) {
                    var argumentType = ConvertType(splitBySpace[i++]);
                    var argument = ConvertType(splitBySpace[i], true);
                    args.Add(argumentType + " " + AdjustArgumentName(argument));
                }
            }

            using (var sw = new StringWriter()) {
                sw.Write("public static extern ");
                sw.Write(ConvertType(returnValue));
                sw.Write(" ");
                sw.Write(ConvertTypeName(name));
                sw.Write("(");
                foreach (var arg in args) {
                    sw.Write(arg);
                    sw.Write(", ");
                }
                if (args.Count > 0) {
                    sw.GetStringBuilder().Remove(sw.GetStringBuilder().Length - 2, 2);
                }
                sw.Write(")");
                sw.Write(";");
                return sw.ToString();
            }
        }

        public static string ConvertTypeName(string typeName) {
            var name = string.Empty;
            var parts = typeName.Split('_');
            var result = parts.Where(part => part != "t").Aggregate(name,
                                                                    (current, part) => current + " " + part.ToLower());
            result = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(result.ToLower());
            return result.Replace(" ", string.Empty);
        }

        public static string ConvertFileName(string filename) {
            var name = string.Empty;
            var parts = filename.Split('_');
            var result = parts.Where(part => part != "t").Aggregate(name,
                                                                    (current, part) => current + " " + part.ToLower());
            result = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(result.ToLower());
            return result.Replace(" ", "").Replace(".H", ".cs");
        }

        private static string ConvertType(string type, bool isIdent = false) {
            type = type.ToLower();

            if (type.Contains("*")) {
                return "IntPtr";
            }

            if (type == "cef_window_handle_t") {
                return "IntPtr";
            }

            if (type == "dword") {
                return "uint";
            }

            if (type == "hinstance") {
                return "IntPtr";
            }

            if (type == "cef_string_userfree_t") {
                return "IntPtr";
            }

            if (type == "cef_string_map_t") {
                return "IntPtr";
            }

            if (type == "cef_string_multimap_t") {
                return "IntPtr";
            }

            if (type.StartsWith("cef_string_userfree_wide_t")) {
                return "IntPtr";
            }

            if (type.StartsWith("cef_string_userfree_utf8_t")) {
                return "IntPtr";
            }

            if (type.StartsWith("cef_string_userfree_utf16_t")) {
                return "IntPtr";
            }

            if (type == "bool") {
                return "bool";
            }

            if (type == "hmenu") {
                return "IntPtr";
            }

            if (type == "uint32") {
                return "uint";
            }

            if (type == "char16") {
                return "char";
            }

            if (type == "uint64") {
                return "ulong";
            }

            if (type == "cef_string_list_t") {
                return "IntPtr";
            }

            if (type == "cef_string_t") {
                return "CefStringUtf16";
            }

            if (type == "cef_string_list_t") {
                return "IntPtr";
            }

            if (type == "time_t") {
                return "long";
            }

            if (type == "size") {
                return isIdent ? "size" : "int";
            }

            if (type == "size_t") {
                return "int";
            }

            if (type == "cef_time_t") {
                return "IntPtr";
            }

            if (type == "bool") {
                return "bool";
            }

            if (type == "int") {
                return "int";
            }

            if (type == "int32") {
                return "int";
            }

            if (type == "int64") {
                return "long";
            }

            if (type == "void") {
                return "void";
            }

            if (type == "cef_event_handle_t") {
                return "IntPtr";
            }

            if (type == "cef_window_handle_t") {
                return "IntPtr";
            }

            return ConvertTypeName(type);
        }

        private static string AdjustFunctionName(string name) {
            return name == "GetType" ? "GetElementType" : name;
        }

        internal static string ConvertMember(string member, out bool isFunctionPointer) {
            if (member.Contains("(")) {
                isFunctionPointer = true;
                var name = ExtractFunctionPointerName(member);
                return string.Format("public IntPtr {0};", AdjustFunctionName(name));
            }

            isFunctionPointer = false;
            var splitBySpace = member.Trim().TrimEnd(';').Split(' ');
            if (splitBySpace.Length == 2) {
                var type = ConvertType(splitBySpace.First());
                var name = ConvertTypeName(splitBySpace.Last());
                return string.Format("public {0} {1};", type, AdjustFunctionName(name));
            }
            if (splitBySpace.Length == 3) {
                var prefix = splitBySpace[0] == "enum" ? string.Empty : "u";
                var type = ConvertType(splitBySpace[1]);
                var name = ConvertTypeName(splitBySpace[2]);
                return string.Format("public {0}{1} {2};", prefix, type, AdjustFunctionName(name));
            }

            throw new ApplicationException("unknown format");
        }

        private static string ExtractFunctionPointerName(string member) {
            var match = Regex.Match(member, @"\(CEF_CALLBACK(\s|\w|\*)*\)");
            if (!match.Success) {
                var matches = Regex.Matches(member, @"\((\s|\w|\*)*\)");
                var d = matches[0].Value.Replace("*", string.Empty).Trim(new[] {'(', ')'}).Trim();
                return ConvertTypeName(d);
            }
            var value = match.Value.Trim(new[] {'(', ')'});
            var splitBySpace = value.Split(' ');
            var pointer = splitBySpace.Last();
            var name = pointer.Replace("*", string.Empty);
            return ConvertTypeName(name);
        }

        internal static string CreateDelegate(string del, out string name) {
            del = StripUnnecessarySymbols(del);
            var splitBySpace = del.Split(new[] {" "}, StringSplitOptions.RemoveEmptyEntries);
            var returnValue = ConvertType(splitBySpace.First());
            name = ConvertTypeName(splitBySpace[2].Replace("*", string.Empty)) + "Callback";
            var args = new List<string>();

            var begin = del.Contains("CEF_CALLBACK") ? 3 : 2;

            if (splitBySpace.Length > begin) {
                for (var i = begin; i < splitBySpace.Length; i++) {
                    var argumentType = ConvertType(splitBySpace[i++]);
                    var token = splitBySpace[i];
                    if (token == "*") {
                        i++;
                    }
                    var argument = ConvertType(splitBySpace[i], true);
                    args.Add(argumentType + " " + AdjustArgumentName(argument));
                }
            }

            using (var sw = new StringWriter()) {
                sw.Write("public delegate ");
                sw.Write(returnValue);
                sw.Write(" ");
                sw.Write(name);
                sw.Write("(");
                foreach (var arg in args) {
                    sw.Write(arg);
                    sw.Write(", ");
                }
                if (args.Count > 0) {
                    sw.GetStringBuilder().Remove(sw.GetStringBuilder().Length - 2, 2);
                }
                sw.Write(")");
                sw.Write(";");
                return sw.ToString();
            }
        }

        internal static string ExtractEnumName(string @enum) {
            var splitBySpace = @enum.Split(' ');
            return ConvertTypeName(splitBySpace[1]);
        }

        internal static string ConvertEnumEntry(string entry) {
            if (!entry.Contains("=")) {
                return ConvertTypeName(entry) + ",";
            }
            var splitByEqual = entry.Split(new[] {"="}, StringSplitOptions.RemoveEmptyEntries);
            var name = splitByEqual.First().ToLower().Trim();
            name = ConvertTypeName(name).Replace("_", string.Empty);
            return string.Format("{0} = {1},", name, splitByEqual[1].Trim());
        }
    }
}