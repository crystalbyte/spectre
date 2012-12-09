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
using System.Runtime.InteropServices;
using Crystalbyte.Spectre.Interop;
using Crystalbyte.Spectre.Projections;
using Crystalbyte.Spectre.Projections.Internal;
using Crystalbyte.Spectre.UI;

#endregion

namespace Crystalbyte.Spectre {
    public sealed class ContextMenuHandler : OwnedRefCountedCefTypeAdapter {
        private readonly CefContextMenuHandlerCapiDelegates.OnBeforeContextMenuCallback _beforeContextMenuCallback;
        private readonly BrowserDelegate _browserDelegate;
        private readonly CefContextMenuHandlerCapiDelegates.OnContextMenuCommandCallback _contextMenuCommandCallback;
        private readonly CefContextMenuHandlerCapiDelegates.OnContextMenuDismissedCallback _contextMenuDismissedCallback;

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
