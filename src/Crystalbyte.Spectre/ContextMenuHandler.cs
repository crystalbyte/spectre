using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Crystalbyte.Chocolate.Projections;
using Crystalbyte.Chocolate.Projections.Internal;

namespace Crystalbyte.Chocolate.UI {
    public sealed class ContextMenuHandler : RetainedNativeObject {
        private readonly OnContextMenuCommandCallback _contextMenuCommandCallback;
        private readonly OnBeforeContextMenuCallback _beforeContextMenuCallback;
        private readonly OnContextMenuDismissedCallback _contextMenuDismissedCallback;
        private readonly BrowserDelegate _browserDelegate;

        public ContextMenuHandler(BrowserDelegate browserDelegate)
            : base(typeof(CefContextMenuHandler)) {

            _browserDelegate = browserDelegate;
            _contextMenuCommandCallback = OnContextMenuCommand;
            _beforeContextMenuCallback = OnBeforeContextMenu;
            _contextMenuDismissedCallback = OnContextMenuDismissed;

            MarshalToNative(new CefContextMenuHandler {
                Base = DedicatedBase,
                OnBeforeContextMenu = Marshal.GetFunctionPointerForDelegate(_beforeContextMenuCallback),
                OnContextMenuCommand = Marshal.GetFunctionPointerForDelegate(_contextMenuCommandCallback),
                OnContextMenuDismissed = Marshal.GetFunctionPointerForDelegate(_contextMenuDismissedCallback)
            });
        }

        private void OnContextMenuDismissed(IntPtr self, IntPtr browser, IntPtr frame) {
            var e = new ContextMenuClosedEventArgs {
                Browser = Browser.FromHandle(browser),
                Frame = Frame.FromHandle(frame)
            };

            _browserDelegate.OnContextMenuClosed(e);
        }

        private int OnContextMenuCommand(IntPtr self, IntPtr browser, IntPtr frame, IntPtr @params, int commandid, CefEventFlags eventflags) {
            var p = ContextMenuArgs.FromHandle(@params);
            var e = new ContextMenuCommandEventArgs {
                Browser = Browser.FromHandle(browser),
                Frame = Frame.FromHandle(frame)
            };

            _browserDelegate.OnContextMenuCommand(e);
            return Convert.ToInt32(false);
        }

        private void OnBeforeContextMenu(IntPtr self, IntPtr browser, IntPtr frame, IntPtr @params, IntPtr model) {
            var e = new ContextMenuOpeningEventArgs
            {
                Browser = Browser.FromHandle(browser),
                Frame = Frame.FromHandle(frame),
                Arguments = ContextMenuArgs.FromHandle(@params)
            };

            _browserDelegate.OnContextMenuOpening(e);
        }
    }
}
