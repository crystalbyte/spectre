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

namespace Crystalbyte.Spectre.Scripting {
    public sealed class JavaScriptDialogHandler : OwnedRefCountedCefTypeAdapter {
        private readonly CefJsdialogHandlerCapiDelegates.OnBeforeUnloadDialogCallback _beforeUnloadDialogCallback;
        private readonly CefJsdialogHandlerCapiDelegates.OnJsdialogCallback _jsDialogCallback;
        private readonly CefJsdialogHandlerCapiDelegates.OnResetDialogStateCallback _resetDialogStateCallback;
        private readonly BrowserDelegate _browserDelegate;

        public JavaScriptDialogHandler(BrowserDelegate browserDelegate)
            : base(typeof (CefJsdialogHandler)) {
            _browserDelegate = browserDelegate;
            _jsDialogCallback = OnJsDialog;
            _resetDialogStateCallback = OnResetDialogState;
            _beforeUnloadDialogCallback = OnBeforeUnloadDialog;

            MarshalToNative(new CefJsdialogHandler {
                Base = DedicatedBase,
                OnJsdialog =
                    Marshal.GetFunctionPointerForDelegate(_jsDialogCallback),
                OnResetDialogState =
                    Marshal.GetFunctionPointerForDelegate(
                        _resetDialogStateCallback),
                OnBeforeUnloadDialog =
                    Marshal.GetFunctionPointerForDelegate(
                        _beforeUnloadDialogCallback)
            });
        }

        private int OnBeforeUnloadDialog(IntPtr self, IntPtr browser, IntPtr messagetext, int isreload, IntPtr callback) {
            var e = new PageChangeNotificationDialogOpeningEventArgs {
                Browser = Browser.FromHandle(browser),
                IsReload = Convert.ToBoolean(isreload),
                Message = StringUtf16.ReadString(messagetext),
                Callback =
                    JavaScriptDialogCallback.FromHandle(callback)
            };

            _browserDelegate.OnPageChangeNotificationDialogOpening(e);

            return e.IsHandled ? 0 : 1;
        }

        private static void OnResetDialogState(IntPtr self, IntPtr browser) {
            // TODO: Do we really need that ?
        }

        private int OnJsDialog(IntPtr self, IntPtr browser, IntPtr originurl, IntPtr acceptlang,
                               CefJsdialogType dialogtype, IntPtr messagetext, IntPtr defaultprompttext, IntPtr callback,
                               ref int suppressmessage) {
            var e = new JavaScriptDialogOpeningEventArgs {
                AcceptedLanguage = StringUtf16.ReadString(acceptlang),
                Browser = Browser.FromHandle(browser),
                Origin = StringUtf16.ReadString(originurl),
                DialogType = (DialogType) dialogtype,
                Message = StringUtf16.ReadString(messagetext),
                DefaultPrompt = StringUtf16.ReadString(defaultprompttext),
                Result = new DialogResult()
            };

            _browserDelegate.OnJavaScriptDialogOpening(e);

            if (e.IsCanceled) {
                suppressmessage = Convert.ToInt32(true);
                return Convert.ToInt32(false);
            }

            if (e.IsHandled) {
                var func = JavaScriptDialogCallback.FromHandle(callback);
                func.Resume(e.Result.IsSuccessful, e.Result.Message);
                suppressmessage = Convert.ToInt32(false);
                return Convert.ToInt32(true);
            }

            suppressmessage = Convert.ToInt32(false);
            return Convert.ToInt32(false);
        }
    }
}
