using Crystalbyte.Spectre.Interop;
using Crystalbyte.Spectre.Projections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Crystalbyte.Spectre.UI {
    public sealed class ContextMenu : RefCountedNativeObject {
        private ContextMenu(IntPtr handle)
            : base(typeof(CefMenuModel)) {
            NativeHandle = handle;
            _items = new ContextMenuItems(this);
        }

        public static ContextMenu FromHandle(IntPtr handle) {
            return new ContextMenu(handle);
        }

        private readonly ContextMenuItems _items;
        public ContextMenuItems Items { get { return _items; } }

        internal void Clear() {
            var r = MarshalFromNative<CefMenuModel>();
            var action = (ClearCallback) Marshal.GetDelegateForFunctionPointer(r.Clear, typeof (ClearCallback));
            action(NativeHandle);
        }

        internal void AddItem(int commandId, string label) {
            var s = new StringUtf16(label);
            var r = MarshalFromNative<CefMenuModel>();
            var action = (AddItemCallback)Marshal.GetDelegateForFunctionPointer(r.AddItem, typeof(AddItemCallback));
            action(NativeHandle, commandId, s.NativeHandle);
            s.Free();
        }

        internal int GetCount() {
            var r = MarshalFromNative<CefMenuModel>();
            var function =
                (GetCountCallback) Marshal.GetDelegateForFunctionPointer(r.GetCount, typeof (GetCountCallback));
            return function(NativeHandle);
        }

        public bool Remove(int commandId) {
            var r = MarshalFromNative<CefMenuModel>();
            var function =
                (RemoveCallback)Marshal.GetDelegateForFunctionPointer(r.Remove, typeof(RemoveCallback));
            var result = function(NativeHandle, commandId);
            return Convert.ToBoolean(result);
        }
    }
}
