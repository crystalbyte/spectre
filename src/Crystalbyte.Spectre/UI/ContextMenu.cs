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

#endregion

namespace Crystalbyte.Spectre.UI {
    public sealed class ContextMenu : RefCountedNativeTypeAdapter {
        private ContextMenu(IntPtr handle)
            : base(typeof (CefMenuModel)) {
            Handle = handle;
            _items = new ContextMenuItemCollection(this);
        }

        public static ContextMenu FromHandle(IntPtr handle) {
            return new ContextMenu(handle);
        }

        private readonly ContextMenuItemCollection _items;

        public ContextMenuItemCollection Items {
            get { return _items; }
        }

        internal void Clear() {
            var r = MarshalFromNative<CefMenuModel>();
            var action =
                (CefMenuModelCapiDelegates.ClearCallback)
                Marshal.GetDelegateForFunctionPointer(r.Clear, typeof (CefMenuModelCapiDelegates.ClearCallback));
            action(Handle);
        }

        internal void AddItem(int commandId, string label) {
            var s = new StringUtf16(label);
            var r = MarshalFromNative<CefMenuModel>();
            var action =
                (CefMenuModelCapiDelegates.AddItemCallback)
                Marshal.GetDelegateForFunctionPointer(r.AddItem, typeof (CefMenuModelCapiDelegates.AddItemCallback));
            action(Handle, commandId, s.Handle);
            s.Free();
        }

        internal int GetCount() {
            var r = MarshalFromNative<CefMenuModel>();
            var function =
                (CefMenuModelCapiDelegates.GetCountCallback)
                Marshal.GetDelegateForFunctionPointer(r.GetCount, typeof (CefMenuModelCapiDelegates.GetCountCallback));
            return function(Handle);
        }

        public bool Remove(int commandId) {
            var r = MarshalFromNative<CefMenuModel>();
            var function =
                (CefMenuModelCapiDelegates.RemoveCallback)
                Marshal.GetDelegateForFunctionPointer(r.Remove, typeof (CefMenuModelCapiDelegates.RemoveCallback));
            var result = function(Handle, commandId);
            return Convert.ToBoolean(result);
        }
    }
}
