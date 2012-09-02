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
using System.Diagnostics;
using System.Runtime.InteropServices;
using Crystalbyte.Chocolate.Bindings.Internal;

#endregion

namespace Crystalbyte.Chocolate {
    [DebuggerDisplay("Text = {Text}")]
    internal sealed class StringUtf16 : NativeObject {
        public StringUtf16(string value)
            : this() {
            Text = value;
        }

        public StringUtf16(IntPtr handle)
            : base(typeof (CefStringUtf16)) {
            NativeHandle = handle;
        }

        public StringUtf16()
            : this(CefStringTypesClass.CefStringUserfreeUtf16Alloc()) {}

        public string Text {
            get {
                var reflection = MarshalFromNative<CefStringUtf16>();
                return Marshal.PtrToStringUni(reflection.Str);
            }
            set {
                Clear();
                MarshalToNative(new CefStringUtf16 {
                    Length = value.Length,
                    Str = Marshal.StringToHGlobalUni(value)
                });
            }
        }

        public static void WriteString(string text, IntPtr handle) {
            new StringUtf16(handle) {
                Text = text
            };
        }

        public static string ReadString(IntPtr handle) {
            if (handle == IntPtr.Zero) {
                Debug.WriteLine("ReadString: handle is null");
                return string.Empty;
            }
            return new StringUtf16(handle).Text;
        }

        public static string ReadStringAndFree(IntPtr handle) {
            if (handle == IntPtr.Zero) {
                Debug.WriteLine("ReadStringAndFree: handle is null");
                return string.Empty;
            }
            var value = new StringUtf16(handle);
            var text = value.Text;
            value.Free();
            return text;
        }

        public void Free() {
            CefStringTypesClass.CefStringUserfreeUtf16Free(NativeHandle);
        }

        public void Clear() {
            CefStringTypesClass.CefStringUtf16Clear(NativeHandle);
        }
    }
}