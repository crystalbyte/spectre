#region Namespace Directives

using System;
using System.Runtime.InteropServices;
using Crystalbyte.Chocolate.Bindings.Internal;

#endregion

namespace Crystalbyte.Chocolate.UI {
    public sealed class FrameworkSettings : Adapter {
        public FrameworkSettings()
            : base(typeof (CefSettings)) {
            NativeHandle = Marshal.AllocHGlobal(NativeSize);
            MarshalToNative(new CefSettings {
                Size = NativeSize,
                LogSeverity = CefLogSeverity.LogseverityVerbose
            });
        }

        public GraphicsImplementation Graphics {
            get {
                var reflection = MarshalFromNative<CefSettings>();
                return (GraphicsImplementation) reflection.GraphicsImplementation;
            }
            set {
                var reflection = MarshalFromNative<CefSettings>();
                reflection.GraphicsImplementation = (CefGraphicsImplementation) value;
                MarshalToNative(reflection);
            }
        }

        public LogSeverity LogSeverity {
            get {
                var reflection = MarshalFromNative<CefSettings>();
                return (LogSeverity) reflection.LogSeverity;
            }
            set {
                var reflection = MarshalFromNative<CefSettings>();
                reflection.LogSeverity = (CefLogSeverity) value;
                MarshalToNative(reflection);
            }
        }

        public bool IsMessageLoopMultiThreaded {
            get {
                var reflection = MarshalFromNative<CefSettings>();
                return reflection.MultiThreadedMessageLoop;
            }
            set {
                var reflection = MarshalFromNative<CefSettings>();
                reflection.MultiThreadedMessageLoop = value;
                MarshalToNative(reflection);
            }
        }

        public bool IsSingleProcess {
            get {
                var reflection = MarshalFromNative<CefSettings>();
                return reflection.SingleProcess;
            }
            set {
                var reflection = MarshalFromNative<CefSettings>();
                reflection.SingleProcess = value;
                MarshalToNative(reflection);
            }
        }

        protected override void DisposeNative() {
            if (NativeHandle != IntPtr.Zero) {
                Marshal.FreeHGlobal(NativeHandle);
            }
            base.DisposeNative();
        }
    }
}