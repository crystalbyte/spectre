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

#endregion

namespace Crystalbyte.Spectre.UI {
    public sealed class ContextMenu : RefCountedCefTypeAdapter {
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
