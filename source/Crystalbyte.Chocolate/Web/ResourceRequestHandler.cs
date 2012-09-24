#region Copyright notice

// Copyright (C) 2012 Alexander Wieser-Kuciel <alexander.wieser@crystalbyte.de>
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

#endregion

#region Namespace directives

using System;
using System.IO;
using System.Linq;

#endregion

namespace Crystalbyte.Chocolate.Web
{
    public sealed class ResourceRequestHandler : IRequestHandler
    {
        private Uri _uri;
        private bool _isCompleted;
        private Stream _fileStream;
        private BinaryReader _reader;

        public bool CanHandle(Request request)
        {
            _uri = new Uri(request.Url, UriKind.RelativeOrAbsolute);
            // This handler is the last to be called if all others fail.
            return true;
        }

        public void OnDataBlockReading(DataBlockReadingEventArgs e)
        {
            if (_isCompleted)
            {
                e.IsCompleted = true;
                if (_reader != null)
                {
                    _reader.Dispose();
                }
                return;
            }
                
            if (_reader == null)
            {
                _reader = new BinaryReader(_fileStream);
            }

            var bytes = _reader.ReadBytes(e.MaxBlockSize);
            e.ResponseWriter.Write(bytes);

            _isCompleted = _reader.BaseStream.Position == _reader.BaseStream.Length;
        }

        public void OnResponseHeadersReading(ResponseHeadersReadingEventArgs e)
        {
            var path = _uri.LocalPath.TrimStart('/');
            if (!File.Exists(path))
            {
                e.Response.MimeType = "text/plain";
                e.Response.StatusCode = 404;
                e.Response.StatusText = "Resource not found.";
                return;
            }

            try
            {
                _fileStream = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.Read);
            }
            catch (IOException ex) {
                e.Response.MimeType = "text/plain";
                e.Response.StatusCode = 500;
                e.Response.StatusText = ex.ToString();
            }
            catch(Exception ex) {
                e.Response.MimeType = "text/plain";
                e.Response.StatusCode = 505;
                e.Response.StatusText = string.Format("Internal error. {0}", ex);
            }

            var extension = _uri.Segments.Last().ToFileExtension();
            e.Response.MimeType = MimeMapper.ResolveFromExtension(extension);
            e.Response.StatusCode = 200;
            e.Response.StatusText = "OK";
        }
    }
}