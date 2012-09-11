// PackUriParser.cs created with MonoDevelop
// User: alan at 14:50 31/10/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

#region Namespace directives

using System;
using System.Text;

#endregion

namespace Crystalbyte.Chocolate.IO {
    internal sealed class PackUriParser : GenericUriParser {
        private const string SchemaName = "pack";

        private readonly StringBuilder _builder = new StringBuilder();

        public PackUriParser()
            : base(GenericUriParserOptions.Default) {}

        protected override string GetComponents(Uri uri, UriComponents components, UriFormat format) {
            var s = uri.OriginalString;
            _builder.Remove(0, _builder.Length);

            if ((components & UriComponents.Scheme) == UriComponents.Scheme) {
                var start = 0;
                var end = s.IndexOf(':');
                _builder.Append(s, start, end - start);
            }

            if ((components & UriComponents.Host) == UriComponents.Host) {
                // Skip past pack://
                var start = 7;
                var end = s.IndexOf('/', start);
                if (end == -1) {
                    end = s.Length;
                }

                if (_builder.Length > 0) {
                    _builder.Append("://");
                }

                _builder.Append(s, start, end - start);
            }

            // Port is always -1, so i think i can ignore both Port and StrongPort
            // Normally they'd get parsed here

            if ((components & UriComponents.Path) == UriComponents.Path) {
                // Skip past pack://
                var start = s.IndexOf('/', 7);
                var end = s.IndexOf('?');
                if (end == -1) {
                    end = s.IndexOf('#');
                }
                if (end == -1) {
                    end = s.Length;
                }

                if ((components & UriComponents.KeepDelimiter) != UriComponents.KeepDelimiter &&
                    _builder.Length == 0) {
                    start++;
                }

                _builder.Append(s, start, end - start);
            }

            if ((components & UriComponents.Query) == UriComponents.Query) {
                var index = s.IndexOf('?');
                if (index == -1) {
                    return null;
                }

                if ((components & UriComponents.KeepDelimiter) != UriComponents.KeepDelimiter &&
                    _builder.Length == 0) {
                    index++;
                }

                var fragIndex = s.IndexOf('#');
                var end = fragIndex == -1 ? s.Length : fragIndex;
                _builder.Append(s, index, end - index);
            }

            if ((components & UriComponents.Fragment) == UriComponents.Fragment) {
                var index = s.IndexOf('#');
                if (index == -1) {
                    return null;
                }

                if ((components & UriComponents.KeepDelimiter) != UriComponents.KeepDelimiter &&
                    _builder.Length == 0) {
                    index++;
                }

                _builder.Append(s, index, s.Length - index);
            }

            return _builder.ToString();
        }

        protected override void InitializeAndValidate(Uri uri, out UriFormatException parsingError) {
            parsingError = null;
        }

        protected override UriParser OnNewUri() {
            return new PackUriParser();
        }
    }
}
