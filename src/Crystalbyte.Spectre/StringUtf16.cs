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
using System.Diagnostics;
using System.Runtime.InteropServices;
using Crystalbyte.Spectre.Interop;
using Crystalbyte.Spectre.Projections.Internal;

#endregion

namespace Crystalbyte.Spectre {
    [DebuggerDisplay("Text = {Text}")]
    internal sealed class StringUtf16 : CefTypeAdapter {
        public static readonly CefStringTypesDelegates.Char16Callback FreeCallback;

        static StringUtf16() {
            FreeCallback = Marshal.FreeHGlobal;
        }

        public StringUtf16(string value)
            : this() {
            Text = value;
        }

        public StringUtf16(IntPtr handle)
            : base(typeof (CefStringUtf16)) {
            Handle = handle;
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
                    // TODO: Investigate why this breaks the application
                    //Dtor = Marshal.GetFunctionPointerForDelegate(FreeCallback),
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
            CefStringTypesClass.CefStringUserfreeUtf16Free(Handle);
        }

        public void Clear() {
            CefStringTypesClass.CefStringUtf16Clear(Handle);
        }
    }
}
