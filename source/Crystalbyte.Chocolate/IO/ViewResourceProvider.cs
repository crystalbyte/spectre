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
using System.Text;

#endregion

namespace Crystalbyte.Chocolate.IO {
    public sealed class ViewResourceProvider : IResourceProvider {
        private Type _controllerType;
        private readonly Uri _requestUri;
        private BinaryReader _reader;

        public ViewResourceProvider(Uri requestUri) {
            _requestUri = requestUri;
        }

        public bool WriteDataBlock(BinaryWriter writer, int blockSize) {
            var bytes = new byte[blockSize];
            var readBytes = _reader.Read(bytes, 0, blockSize);
            writer.Write(bytes, 0, readBytes);
            return _reader.BaseStream.Position == _reader.BaseStream.Length - 1;
        }

        public ResourceState GetResourceState() {
            var success = RouteRegistrar.Current.TryGetController(_requestUri.AbsoluteUri, out _controllerType);
            if (!success) {
                return ResourceState.Missing;
            }
            return ResourceState.Valid;
        }

        public void Initialize() {
            var controller = (ViewController) Activator.CreateInstance(_controllerType);
            var view = controller.CreateView();
            var result = view.Compose();
            var bytes = Encoding.UTF8.GetBytes(result);
            _reader = new BinaryReader(new MemoryStream(bytes), Encoding.UTF8);
            _reader.BaseStream.Seek(0, SeekOrigin.Begin);
        }
    }
}