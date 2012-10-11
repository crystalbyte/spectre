#region Using directives

using System;
using System.Runtime.InteropServices;
using Crystalbyte.Spectre.Interop;
using Crystalbyte.Spectre.Projections;
using Crystalbyte.Spectre.Projections.Internal;
using Crystalbyte.Spectre.Scripting;
using Crystalbyte.Spectre.UI;

#endregion

namespace Crystalbyte.Spectre{
    public sealed class RenderProcessHandler : OwnedRefCountedNativeObject{
        private readonly OnBrowserCreatedCallback _browserCreatedCallback;
        private readonly OnBrowserDestroyedCallback _browserDestroyedCallback;
        private readonly OnContextCreatedCallback _contextCreatedCallback;
        private readonly OnContextReleasedCallback _contextReleasedCallback;
        private readonly AppDelegate _delegate;
        private readonly OnProcessMessageReceivedCallback _processMessageReceivedCallback;
        private readonly OnRenderThreadCreatedCallback _renderThreadCreatedCallback;
        private readonly OnWebKitInitializedCallback _webkitInitializedCallback;

        public RenderProcessHandler(AppDelegate @delegate)
            : base(typeof (CefRenderProcessHandler)){
            _delegate = @delegate;
            _contextCreatedCallback = OnContextCreated;
            _browserCreatedCallback = OnBrowserCreated;
            _browserDestroyedCallback = OnBrowserDestroyed;
            _renderThreadCreatedCallback = OnRenderThreadCreated;
            _webkitInitializedCallback = OnWebKitInitialized;
            _contextReleasedCallback = OnContextReleased;
            _processMessageReceivedCallback = OnProcessMessageReceived;

            MarshalToNative(new CefRenderProcessHandler{
                                                           Base = DedicatedBase,
                                                           OnBrowserDestroyed =
                                                               Marshal.GetFunctionPointerForDelegate(
                                                                   _browserDestroyedCallback),
                                                           OnContextCreated =
                                                               Marshal.GetFunctionPointerForDelegate(
                                                                   _contextCreatedCallback),
                                                           OnRenderThreadCreated =
                                                               Marshal.GetFunctionPointerForDelegate(
                                                                   _renderThreadCreatedCallback),
                                                           OnWebKitInitialized =
                                                               Marshal.GetFunctionPointerForDelegate(
                                                                   _webkitInitializedCallback),
                                                           OnBrowserCreated =
                                                               Marshal.GetFunctionPointerForDelegate(
                                                                   _browserCreatedCallback),
                                                           OnContextReleased =
                                                               Marshal.GetFunctionPointerForDelegate(
                                                                   _contextReleasedCallback),
                                                           OnProcessMessageReceived =
                                                               Marshal.GetFunctionPointerForDelegate(
                                                                   _processMessageReceivedCallback)
                                                       });
        }

        private int OnProcessMessageReceived(IntPtr self, IntPtr browser, CefProcessId sourceprocess, IntPtr message){
            var e = new IpcMessageReceivedEventArgs{
                                                       SourceProcess = (ProcessType) sourceprocess,
                                                       Browser = Browser.FromHandle(browser),
                                                       Message = IpcMessage.FromHandle(message)
                                                   };
            _delegate.OnIpcMessageReceived(e);
            return e.IsHandled ? 1 : 0;
        }

        private void OnContextReleased(IntPtr self, IntPtr browser, IntPtr frame, IntPtr context){
            var e = new ContextEventArgs{
                                            Browser = Browser.FromHandle(browser),
                                            Frame = Frame.FromHandle(frame),
                                            Context = ScriptingContext.FromHandle(context)
                                        };
            _delegate.OnScriptingContextReleased(e);
        }

        private void OnBrowserCreated(IntPtr self, IntPtr browser){
            var e = new BrowserEventArgs{
                                            Browser = Browser.FromHandle(browser)
                                        };
            _delegate.OnBrowserCreated(e);
        }

        private void OnWebKitInitialized(IntPtr self){
            _delegate.OnInitialized(EventArgs.Empty);
        }

        private void OnRenderThreadCreated(IntPtr self){
            _delegate.OnRenderThreadCreated(EventArgs.Empty);
        }

        private void OnContextCreated(IntPtr self, IntPtr browser, IntPtr frame, IntPtr context){
            var e = new ContextEventArgs{
                                            Browser = Browser.FromHandle(browser),
                                            Frame = Frame.FromHandle(frame),
                                            Context = ScriptingContext.FromHandle(context)
                                        };
            _delegate.OnScriptingContextCreated(e);
        }

        private void OnBrowserDestroyed(IntPtr self, IntPtr browser){
            var b = Browser.FromHandle(browser);
            var e = new BrowserEventArgs(b);
            _delegate.OnBrowserDestroyed(e);
        }
    }
}