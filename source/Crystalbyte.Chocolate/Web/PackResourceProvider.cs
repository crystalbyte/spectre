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

#endregion

namespace Crystalbyte.Chocolate.IO {
    public sealed class FileResourceProvider : IResourceProvider {
        private readonly Uri _uri;
        private BinaryReader _reader;

        public FileResourceProvider(Uri uri) {
            _uri = uri;
        }

        public ResourceState GetResourceState() {
            //TODO: Implement
            return ResourceState.Valid;
        }

        public bool WriteDataBlock(BinaryWriter writer, int blockSize) {
            var bytes = new byte[blockSize];
            var bytesRead = _reader.Read(bytes, 0, blockSize);
            writer.Write(bytes, 0, bytesRead);
            return _reader.BaseStream.Position == _reader.BaseStream.Length - 1;
        }

        public void Initialize() {
            var info = Framework.GetResourceStream(_uri);
            _reader = new BinaryReader(info.Stream);
            _reader.BaseStream.Seek(0, SeekOrigin.Begin);
        }
    }
}