#region Licensing notice

// Copyright (C) 2012, Alexander Wieser-Kuciel <alexander.wieser@crystalbyte.de>
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License version 3 as published by
// the Free Software Foundation.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

#endregion

#region Using directives

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Crystalbyte.Spectre.Interop;
using Crystalbyte.Spectre.Projections;
using Crystalbyte.Spectre.Projections.Internal;
using Crystalbyte.Spectre.UI;
using System.Collections;

#endregion

namespace Crystalbyte.Spectre {
    public sealed class ContextMenuHandler : OwnedRefCountedNativeObject {
        private readonly OnBeforeContextMenuCallback _beforeContextMenuCallback;
        private readonly BrowserDelegate _browserDelegate;
        private readonly OnContextMenuCommandCallback _contextMenuCommandCallback;
        private readonly OnContextMenuDismissedCallback _contextMenuDismissedCallback;

        public ContextMenuHandler(BrowserDelegate browserDelegate)
            : base(typeof (CefContextMenuHandler)) {
            _browserDelegate = browserDelegate;
            _contextMenuCommandCallback = OnContextMenuCommand;
            _beforeContextMenuCallback = OnBeforeContextMenu;
            _contextMenuDismissedCallback = OnContextMenuDismissed;

            MarshalToNative(new CefContextMenuHandler {
                Base = DedicatedBase,
                OnBeforeContextMenu =
                    Marshal.GetFunctionPointerForDelegate(
                        _beforeContextMenuCallback),
                OnContextMenuCommand =
                    Marshal.GetFunctionPointerForDelegate(
                        _contextMenuCommandCallback),
                OnContextMenuDismissed =
                    Marshal.GetFunctionPointerForDelegate(
                        _contextMenuDismissedCallback)
            });
        }

        private void OnContextMenuDismissed(IntPtr self, IntPtr browser, IntPtr frame) {
            var e = new ContextMenuClosedEventArgs {
                Browser = Browser.FromHandle(browser),
                Frame = Frame.FromHandle(frame)
            };

            _browserDelegate.OnContextMenuClosed(e);
        }

        private int OnContextMenuCommand(IntPtr self, IntPtr browser, IntPtr frame, IntPtr @params, int commandid,
                                         CefEventFlags eventflags) {
            var e = new ContextMenuCommandEventArgs {
                Browser = Browser.FromHandle(browser),
                Frame = Frame.FromHandle(frame),
                Arguments = ContextMenuArgs.FromHandle(@params),
                Command = commandid,
                Modifiers = (KeyModifiers) eventflags
            };

            _browserDelegate.OnContextMenuCommand(e);
            e.Arguments.Dispose();
            return Convert.ToInt32(false);
        }

        private void OnBeforeContextMenu(IntPtr self, IntPtr browser, IntPtr frame, IntPtr @params, IntPtr model) {
            var e = new ContextMenuOpeningEventArgs {
                Browser = Browser.FromHandle(browser),
                Frame = Frame.FromHandle(frame),
                Arguments = ContextMenuArgs.FromHandle(@params),
                Menu = ContextMenu.FromHandle(model)
            };
            
            _browserDelegate.OnContextMenuOpening(e);
            e.Arguments.Dispose();
            e.Menu.Dispose();
        }
    }
}
