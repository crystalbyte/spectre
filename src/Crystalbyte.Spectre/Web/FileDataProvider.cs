﻿#region Licensing notice

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