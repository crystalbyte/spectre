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
using System.Linq;

#endregion

namespace Crystalbyte.Spectre.Web {
    public sealed class FileDataProvider : IDataProvider {
        private Stream _fileStream;
        private bool _isCompleted;
        private BinaryReader _reader;
        private Request _request;

        #region IDataProvider Members

        public void OnDataBlockReading(DataBlockReadingEventArgs e) {
            if (_isCompleted) {
                e.IsCompleted = true;
                if (_reader != null) {
                    _reader.Dispose();
                }
                return;
            }

            if (_reader == null) {
                _reader = new BinaryReader(_fileStream);
            }

            var bytes = _reader.ReadBytes(e.MaxBlockSize);
            e.ResponseWriter.Write(bytes);

            _isCompleted = _reader.BaseStream.Position == _reader.BaseStream.Length;
        }

        public void OnResponseHeadersReading(ResponseHeadersReadingEventArgs e) {
            var uri = new Uri(_request.Url, UriKind.RelativeOrAbsolute);
            var path = uri.LocalPath.TrimStart('/');

            if (!File.Exists(path)) {
                e.Response.MimeType = "text/plain";
                e.Response.StatusCode = 404;
                e.Response.StatusText = "Resource not found.";
                return;
            }

            try {
                _fileStream = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.Read);
            }
            catch (IOException ex) {
                e.Response.MimeType = "text/plain";
                e.Response.StatusCode = 500;
                e.Response.StatusText = string.Format("Access denied. {0}", ex);
            }
            catch (Exception ex) {
                e.Response.MimeType = "text/plain";
                e.Response.StatusCode = 505;
                e.Response.StatusText = string.Format("Internal error. {0}", ex);
            }

            var extension = uri.Segments.Last().ToFileExtension();
            e.Response.MimeType = MimeMapper.ResolveFromExtension(extension);
            e.Response.StatusCode = 200;
            e.Response.StatusText = "OK";
        }

        public bool OnRequestProcessing(Request request) {
            _request = request;
            var uri = new Uri(_request.Url, UriKind.RelativeOrAbsolute);
            var path = uri.LocalPath.TrimStart('/');
            return File.Exists(path);
        }

        #endregion
    }
}
