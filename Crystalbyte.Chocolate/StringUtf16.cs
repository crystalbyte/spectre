#region Namespace Directives

using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Crystalbyte.Chocolate.Bindings.Internal;

#endregion

namespace Crystalbyte.Chocolate {
    [DebuggerDisplay("Text = {Text}")]
    internal sealed class StringUtf16 : Adapter {
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
            new StringUtf16(handle) {Text = text};
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
                Debug.WriteLine("ReadStringAndRelease: handle is null");
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