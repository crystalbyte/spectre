#region Using directives

using System;
using System.Runtime.InteropServices;
using Crystalbyte.Spectre.Interop;
using Crystalbyte.Spectre.Projections;

#endregion

namespace Crystalbyte.Spectre{
    public sealed class PostElementCollection : NativeObject{
        public PostElementCollection()
            : base(typeof (CefPostData)){
            NativeHandle = CefRequestCapi.CefPostDataCreate();
        }

        private PostElementCollection(IntPtr handle)
            : base(typeof (CefPostData)){
            NativeHandle = handle;
        }

        public bool IsReadOnly{
            get{
                var r = MarshalFromNative<CefPostData>();

                var function =
                    (IsReadOnlyCallback)
                    Marshal.GetDelegateForFunctionPointer(r.IsReadOnly, typeof (IsReadOnlyCallback));
                var value = function(NativeHandle);
                return Convert.ToBoolean(value);
            }
        }

        internal static PostElementCollection FromHandle(IntPtr handle){
            return new PostElementCollection(handle);
        }
    }
}