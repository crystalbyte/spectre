#region Using directives

using System;
using System.Runtime.InteropServices;
using Crystalbyte.Spectre.Interop;
using Crystalbyte.Spectre.Projections;
using Crystalbyte.Spectre.UI;

#endregion

namespace Crystalbyte.Spectre.Web{
    public abstract class SchemeHandlerFactory : OwnedRefCountedNativeObject{
        private readonly CreateCallback _createCallback;

        protected SchemeHandlerFactory()
            : base(typeof (CefSchemeHandlerFactory)){
            _createCallback = Create;
            MarshalToNative(new CefSchemeHandlerFactory{
                                                           Base = DedicatedBase,
                                                           Create =
                                                               Marshal.GetFunctionPointerForDelegate(_createCallback)
                                                       });
        }

        private IntPtr Create(IntPtr self, IntPtr browser, IntPtr frame, IntPtr schemename, IntPtr request){
            var e = new CreateHandlerEventArgs{
                                                  Browser = Browser.FromHandle(browser),
                                                  Frame = Frame.FromHandle(frame),
                                                  Scheme = StringUtf16.ReadString(schemename)
                                              };
            return OnCreateHandler(this, e).NativeHandle;
        }

        protected abstract ResourceHandler OnCreateHandler(object sender, CreateHandlerEventArgs e);
    }
}