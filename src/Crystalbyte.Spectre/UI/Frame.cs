#region Using directives

using System;
using System.Runtime.InteropServices;
using Crystalbyte.Spectre.Interop;
using Crystalbyte.Spectre.Projections;
using Crystalbyte.Spectre.Scripting;

#endregion

namespace Crystalbyte.Spectre.UI{
    public sealed class Frame : RefCountedNativeObject{
        private readonly StringUtf16 _aboutBlank;

        private Frame(IntPtr handle)
            : base(typeof (CefFrame)){
            NativeHandle = handle;
            _aboutBlank = new StringUtf16("about:blank");
        }

        public bool IsFocused{
            get{
                var r = MarshalFromNative<CefFrame>();
                var function = (IsFocusedCallback)
                               Marshal.GetDelegateForFunctionPointer(r.IsFocused, typeof (IsFocusedCallback));
                var value = function(NativeHandle);
                return Convert.ToBoolean(value);
            }
        }

        public bool IsMain{
            get{
                var r = MarshalFromNative<CefFrame>();
                var function = (IsMainCallback)
                               Marshal.GetDelegateForFunctionPointer(r.IsMain, typeof (IsMainCallback));
                var value = function(NativeHandle);
                return Convert.ToBoolean(value);
            }
        }

        public bool IsValid{
            get{
                var r = MarshalFromNative<CefFrame>();
                var function = (IsValidCallback)
                               Marshal.GetDelegateForFunctionPointer(r.IsValid, typeof (IsValidCallback));
                var value = function(NativeHandle);
                return Convert.ToBoolean(value);
            }
        }

        public long Id{
            get{
                var r = MarshalFromNative<CefFrame>();
                var function = (GetIdentifierCallback)
                               Marshal.GetDelegateForFunctionPointer(r.GetIdentifier,
                                                                     typeof (GetIdentifierCallback));
                return function(NativeHandle);
            }
        }

        public string Name{
            get{
                var r = MarshalFromNative<CefFrame>();
                var function = (GetNameCallback)
                               Marshal.GetDelegateForFunctionPointer(r.GetName, typeof (GetNameCallback));
                var handle = function(NativeHandle);
                return StringUtf16.ReadStringAndFree(handle);
            }
        }

        public Frame Parent{
            get{
                var r = MarshalFromNative<CefFrame>();
                var function = (GetParentCallback)
                               Marshal.GetDelegateForFunctionPointer(r.GetParent, typeof (GetParentCallback));
                var handle = function(NativeHandle);
                return handle == IntPtr.Zero ? null : FromHandle(handle);
            }
        }

        public string Url{
            get{
                var r = MarshalFromNative<CefFrame>();
                var function = (GetUrlCallback)
                               Marshal.GetDelegateForFunctionPointer(r.GetUrl, typeof (GetUrlCallback));
                var handle = function(NativeHandle);
                return handle == IntPtr.Zero ? string.Empty : StringUtf16.ReadStringAndFree(handle);
            }
        }

        public Browser Browser{
            get{
                var r = MarshalFromNative<CefFrame>();
                var function = (GetBrowserCallback)
                               Marshal.GetDelegateForFunctionPointer(r.GetBrowser, typeof (GetBrowserCallback));
                var handle = function(NativeHandle);
                return Browser.FromHandle(handle);
            }
        }

        public ScriptingContext Context{
            get{
                var r = MarshalFromNative<CefFrame>();
                var function = (GetV8contextCallback)
                               Marshal.GetDelegateForFunctionPointer(r.GetV8context,
                                                                     typeof (GetV8contextCallback));
                var handle = function(NativeHandle);
                return ScriptingContext.FromHandle(handle);
            }
        }

        protected override void DisposeNative(){
            if (_aboutBlank.NativeHandle != IntPtr.Zero){
                _aboutBlank.Free();
            }
            base.DisposeNative();
        }

        public static Frame FromHandle(IntPtr handle){
            return new Frame(handle);
        }

        public void Navigate(string address){
            var u = new StringUtf16(address);
            var r = MarshalFromNative<CefFrame>();
            var action = (LoadUrlCallback)
                         Marshal.GetDelegateForFunctionPointer(r.LoadUrl, typeof (LoadUrlCallback));
            action(NativeHandle, u.NativeHandle);
            u.Free();
        }

        public void Display(string source){
            var u = new StringUtf16(source);
            var reflection = MarshalFromNative<CefFrame>();
            var action = (LoadStringCallback)
                         Marshal.GetDelegateForFunctionPointer(reflection.LoadString, typeof (LoadStringCallback));
            action(NativeHandle, u.NativeHandle, _aboutBlank.NativeHandle);
            u.Free();
        }

        public void Navigate(Uri address){
            Navigate(address.AbsoluteUri);
        }

        public void SelectAll(){
            var r = MarshalFromNative<CefFrame>();
            var action = (SelectAllCallback)
                         Marshal.GetDelegateForFunctionPointer(r.SelectAll, typeof (SelectAllCallback));
            action(NativeHandle);
        }

        public void Copy(){
            var r = MarshalFromNative<CefFrame>();
            var action = (CopyCallback)
                         Marshal.GetDelegateForFunctionPointer(r.Copy, typeof (CopyCallback));
            action(NativeHandle);
        }

        public void Cut(){
            var r = MarshalFromNative<CefFrame>();
            var action = (CutCallback)
                         Marshal.GetDelegateForFunctionPointer(r.Cut, typeof (CutCallback));
            action(NativeHandle);
        }

        public void Undo(){
            var r = MarshalFromNative<CefFrame>();
            var action = (UndoCallback)
                         Marshal.GetDelegateForFunctionPointer(r.Undo, typeof (UndoCallback));
            action(NativeHandle);
        }

        public void Delete(){
            var r = MarshalFromNative<CefFrame>();
            var action = (DelCallback)
                         Marshal.GetDelegateForFunctionPointer(r.Del, typeof (DelCallback));
            action(NativeHandle);
        }

        public void Redo(){
            var r = MarshalFromNative<CefFrame>();
            var action = (RedoCallback)
                         Marshal.GetDelegateForFunctionPointer(r.Redo, typeof (RedoCallback));
            action(NativeHandle);
        }

        public void Paste(){
            var r = MarshalFromNative<CefFrame>();
            var action = (PasteCallback)
                         Marshal.GetDelegateForFunctionPointer(r.Paste, typeof (PasteCallback));
            action(NativeHandle);
        }

        public void ExecuteJavascript(string code){
            ExecuteJavascript(code, "about:blank", 0);
        }

        public void ExecuteJavascript(string code, string scriptUrl){
            ExecuteJavascript(code, scriptUrl, 0);
        }

        public void ExecuteJavascript(string code, string scriptUrl, int line){
            var s = new StringUtf16(scriptUrl);
            var c = new StringUtf16(code);
            var reflection = MarshalFromNative<CefFrame>();
            var action = (ExecuteJavaScriptCallback)
                         Marshal.GetDelegateForFunctionPointer(reflection.ExecuteJavaScript,
                                                               typeof (ExecuteJavaScriptCallback));
            action(NativeHandle, c.NativeHandle, s.NativeHandle, line);
            c.Free();
            s.Free();
        }
    }
}