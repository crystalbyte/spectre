using System;
using System.Runtime.InteropServices;
using Crystalbyte.Chocolate.Projections;
using Crystalbyte.Chocolate.Projections.Internal;
using Crystalbyte.Chocolate.UI;

namespace Crystalbyte.Chocolate.Scripting {
    public sealed class JavaScriptDialogHandler : RefCountedNativeObject {
        private readonly OnJsdialogCallback _jsDialogCallback;
        private readonly OnResetDialogStateCallback _resetDialogStateCallback;
        private readonly OnBeforeUnloadDialogCallback _beforeUnloadDialogCallback;
        private readonly BrowserDelegate _browserDelegate;

        public JavaScriptDialogHandler(BrowserDelegate browserDelegate)
            : base(typeof(CefJsdialogHandler)) {

            _browserDelegate = browserDelegate;
            _jsDialogCallback = OnJsDialog;
            _resetDialogStateCallback = OnResetDialogState;
            _beforeUnloadDialogCallback = OnBeforeUnloadDialog;

            MarshalToNative(new CefJsdialogHandler {
                Base = DedicatedBase,
                OnJsdialog = Marshal.GetFunctionPointerForDelegate(_jsDialogCallback),
                OnResetDialogState = Marshal.GetFunctionPointerForDelegate(_resetDialogStateCallback),
                OnBeforeUnloadDialog = Marshal.GetFunctionPointerForDelegate(_beforeUnloadDialogCallback)
            });
        }

        private int OnBeforeUnloadDialog(IntPtr self, IntPtr browser, IntPtr messagetext, int isreload, IntPtr callback) {
            var e = new PageChangeNotificationDialogOpeningEventArgs {
                Browser = Browser.FromHandle(browser),
                IsReload = Convert.ToBoolean(isreload),
                Message = StringUtf16.ReadString(messagetext),
                Callback = JavaScriptDialogCallback.FromHandle(callback)
            };

            _browserDelegate.OnPageChangeNotificationDialogOpening(e);

            return e.IsHandled ? 0 : 1;
        }

        private static void OnResetDialogState(IntPtr self, IntPtr browser) {
            // TODO: Do we really need that ?
        }

        private int OnJsDialog(IntPtr self, IntPtr browser, IntPtr originurl, IntPtr acceptlang, CefJsdialogType dialogtype, IntPtr messagetext, IntPtr defaultprompttext, IntPtr callback, out int suppressmessage) {
            var e = new JavaScriptDialogOpeningEventArgs {
                AcceptedLanguage = StringUtf16.ReadString(acceptlang),
                Browser = Browser.FromHandle(browser),
                Origin = StringUtf16.ReadString(originurl),
                DialogType = (DialogType) dialogtype,
                Message = StringUtf16.ReadString(messagetext),
                DefaultPrompt = StringUtf16.ReadString(defaultprompttext),
                Result= new DialogResult()
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
