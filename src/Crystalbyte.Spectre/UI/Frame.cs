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
using System.Runtime.InteropServices;
using Crystalbyte.Spectre.Interop;
using Crystalbyte.Spectre.Projections;
using Crystalbyte.Spectre.Scripting;

#endregion

namespace Crystalbyte.Spectre.UI {
    public sealed class Frame : RefCountedNativeTypeAdapter {
        private readonly StringUtf16 _aboutBlank;

        private Frame(IntPtr handle)
            : base(typeof (CefFrame)) {
            Handle = handle;
            _aboutBlank = new StringUtf16("about:blank");
        }

        public bool IsFocused {
            get {
                var r = MarshalFromNative<CefFrame>();
                var function = (CefFrameCapiDelegates.IsFocusedCallback)
                               Marshal.GetDelegateForFunctionPointer(r.IsFocused,
                                                                     typeof (CefFrameCapiDelegates.IsFocusedCallback));
                var value = function(Handle);
                return Convert.ToBoolean(value);
            }
        }

        public bool IsMain {
            get {
                var r = MarshalFromNative<CefFrame>();
                var function = (CefFrameCapiDelegates.IsMainCallback)
                               Marshal.GetDelegateForFunctionPointer(r.IsMain,
                                                                     typeof (CefFrameCapiDelegates.IsMainCallback));
                var value = function(Handle);
                return Convert.ToBoolean(value);
            }
        }

        public bool IsValid {
            get {
                var r = MarshalFromNative<CefFrame>();
                var function = (CefCommandLineCapiDelegates.IsValidCallback)
                               Marshal.GetDelegateForFunctionPointer(r.IsValid,
                                                                     typeof (CefCommandLineCapiDelegates.IsValidCallback
                                                                         ));
                var value = function(Handle);
                return Convert.ToBoolean(value);
            }
        }

        public long Id {
            get {
                var r = MarshalFromNative<CefFrame>();
                var function = (CefBrowserCapiDelegates.GetIdentifierCallback)
                               Marshal.GetDelegateForFunctionPointer(r.GetIdentifier,
                                                                     typeof (
                                                                         CefBrowserCapiDelegates.GetIdentifierCallback));
                return function(Handle);
            }
        }

        public string Name {
            get {
                var r = MarshalFromNative<CefFrame>();
                var function = (CefDomCapiDelegates.GetNameCallback)
                               Marshal.GetDelegateForFunctionPointer(r.GetName,
                                                                     typeof (CefDomCapiDelegates.GetNameCallback));
                var handle = function(Handle);
                return StringUtf16.ReadStringAndFree(handle);
            }
        }

        public Frame Parent {
            get {
                var r = MarshalFromNative<CefFrame>();
                var function = (CefDomCapiDelegates.GetParentCallback)
                               Marshal.GetDelegateForFunctionPointer(r.GetParent,
                                                                     typeof (CefDomCapiDelegates.GetParentCallback));
                var handle = function(Handle);
                return handle == IntPtr.Zero ? null : FromHandle(handle);
            }
        }

        public string Url {
            get {
                var r = MarshalFromNative<CefFrame>();
                var function = (CefDownloadItemCapiDelegates.GetUrlCallback)
                               Marshal.GetDelegateForFunctionPointer(r.GetUrl,
                                                                     typeof (CefDownloadItemCapiDelegates.GetUrlCallback
                                                                         ));
                var handle = function(Handle);
                return handle == IntPtr.Zero ? string.Empty : StringUtf16.ReadStringAndFree(handle);
            }
        }

        public Browser Browser {
            get {
                var r = MarshalFromNative<CefFrame>();
                var function = (CefBrowserCapiDelegates.GetBrowserCallback)
                               Marshal.GetDelegateForFunctionPointer(r.GetBrowser,
                                                                     typeof (CefBrowserCapiDelegates.GetBrowserCallback));
                var handle = function(Handle);
                return Browser.FromHandle(handle);
            }
        }

        public ScriptingContext Context {
            get {
                var r = MarshalFromNative<CefFrame>();
                var function = (CefFrameCapiDelegates.GetV8contextCallback)
                               Marshal.GetDelegateForFunctionPointer(r.GetV8context,
                                                                     typeof (CefFrameCapiDelegates.GetV8contextCallback));
                var handle = function(Handle);
                return ScriptingContext.FromHandle(handle);
            }
        }

        protected override void DisposeNative() {
            if (_aboutBlank.Handle != IntPtr.Zero) {
                _aboutBlank.Free();
            }
            base.DisposeNative();
        }

        public static Frame FromHandle(IntPtr handle) {
            return new Frame(handle);
        }

        public void Navigate(string address) {
            var u = new StringUtf16(address);
            var r = MarshalFromNative<CefFrame>();
            var action = (CefFrameCapiDelegates.LoadUrlCallback)
                         Marshal.GetDelegateForFunctionPointer(r.LoadUrl, typeof (CefFrameCapiDelegates.LoadUrlCallback));
            action(Handle, u.Handle);
            u.Free();
        }

        public void Display(string source) {
            var u = new StringUtf16(source);
            var reflection = MarshalFromNative<CefFrame>();
            var action = (CefFrameCapiDelegates.LoadStringCallback)
                         Marshal.GetDelegateForFunctionPointer(reflection.LoadString,
                                                               typeof (CefFrameCapiDelegates.LoadStringCallback));
            action(Handle, u.Handle, _aboutBlank.Handle);
            u.Free();
        }

        public void Navigate(Uri address) {
            Navigate(address.AbsoluteUri);
        }

        public void SelectAll() {
            var r = MarshalFromNative<CefFrame>();
            var action = (CefFrameCapiDelegates.SelectAllCallback)
                         Marshal.GetDelegateForFunctionPointer(r.SelectAll,
                                                               typeof (CefFrameCapiDelegates.SelectAllCallback));
            action(Handle);
        }

        public void Copy() {
            var r = MarshalFromNative<CefFrame>();
            var action = (CefFrameCapiDelegates.CopyCallback2)
                         Marshal.GetDelegateForFunctionPointer(r.Copy, typeof (CefFrameCapiDelegates.CopyCallback2));
            action(Handle);
        }

        public void Cut() {
            var r = MarshalFromNative<CefFrame>();
            var action = (CefFrameCapiDelegates.CutCallback)
                         Marshal.GetDelegateForFunctionPointer(r.Cut, typeof (CefFrameCapiDelegates.CutCallback));
            action(Handle);
        }

        public void Undo() {
            var r = MarshalFromNative<CefFrame>();
            var action = (CefFrameCapiDelegates.UndoCallback)
                         Marshal.GetDelegateForFunctionPointer(r.Undo, typeof (CefFrameCapiDelegates.UndoCallback));
            action(Handle);
        }

        public void Delete() {
            var r = MarshalFromNative<CefFrame>();
            var action = (CefFrameCapiDelegates.DelCallback)
                         Marshal.GetDelegateForFunctionPointer(r.Del, typeof (CefFrameCapiDelegates.DelCallback));
            action(Handle);
        }

        public void Redo() {
            var r = MarshalFromNative<CefFrame>();
            var action = (CefFrameCapiDelegates.RedoCallback)
                         Marshal.GetDelegateForFunctionPointer(r.Redo, typeof (CefFrameCapiDelegates.RedoCallback));
            action(Handle);
        }

        public void Paste() {
            var r = MarshalFromNative<CefFrame>();
            var action = (CefFrameCapiDelegates.PasteCallback)
                         Marshal.GetDelegateForFunctionPointer(r.Paste, typeof (CefFrameCapiDelegates.PasteCallback));
            action(Handle);
        }

        public void ExecuteJavascript(string code) {
            ExecuteJavascript(code, "about:blank", 0);
        }

        public void ExecuteJavascript(string code, string scriptUrl) {
            ExecuteJavascript(code, scriptUrl, 0);
        }

        public void ExecuteJavascript(string code, string scriptUrl, int line) {
            var s = new StringUtf16(scriptUrl);
            var c = new StringUtf16(code);
            var reflection = MarshalFromNative<CefFrame>();
            var action = (CefFrameCapiDelegates.ExecuteJavaScriptCallback)
                         Marshal.GetDelegateForFunctionPointer(reflection.ExecuteJavaScript,
                                                               typeof (CefFrameCapiDelegates.ExecuteJavaScriptCallback));
            action(Handle, c.Handle, s.Handle, line);
            c.Free();
            s.Free();
        }
    }
}
