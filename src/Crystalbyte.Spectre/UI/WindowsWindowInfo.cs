#region Using directives

using System;
using System.Runtime.InteropServices;
using Crystalbyte.Spectre.Interop;
using Crystalbyte.Spectre.Projections.Internal;

#endregion

namespace Crystalbyte.Spectre.UI{
    public sealed class WindowsWindowInfo : NativeObject{
        private readonly bool _isOwned;

        public WindowsWindowInfo(IRenderTarget target)
            : base(typeof (WindowsCefWindowInfo)){
            NativeHandle = Marshal.AllocHGlobal(NativeSize);
            MarshalToNative(new WindowsCefWindowInfo{
                                                        ParentWindow = target.Handle,
                                                        Style = (uint) (WindowStyles.ChildWindow
                                                                        | WindowStyles.ClipChildren
                                                                        | WindowStyles.ClipSiblings
                                                                        | WindowStyles.TabStop
                                                                        | WindowStyles.Visible),
                                                        X = 0,
                                                        Y = 0,
                                                        Width = target.Size.Width,
                                                        Height = target.Size.Height
                                                    });
            _isOwned = true;
        }

        private WindowsWindowInfo(IntPtr handle)
            : base(typeof (WindowsCefWindowInfo)){
            NativeHandle = handle;
        }

        public IntPtr WindowHandle{
            get{
                var reflection = MarshalFromNative<WindowsCefWindowInfo>();
                return reflection.Window;
            }
        }

        public static WindowsWindowInfo FromHandle(IntPtr handle){
            return new WindowsWindowInfo(handle);
        }

        protected override void DisposeNative(){
            if (NativeHandle != IntPtr.Zero && _isOwned){
                Marshal.FreeHGlobal(NativeHandle);
            }
            base.DisposeNative();
        }
    }
}