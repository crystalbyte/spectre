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

namespace Crystalbyte.Spectre {
    public sealed class PostElementCollection : CefTypeAdapter {
        public PostElementCollection()
            : base(typeof (CefPostData)) {
            Handle = CefRequestCapi.CefPostDataCreate();
        }

        private PostElementCollection(IntPtr handle)
            : base(typeof (CefPostData)) {
            Handle = handle;
        }

        public bool IsReadOnly {
            get {
                var r = MarshalFromNative<CefPostData>();

                var function =
                    (CefRequestCapiDelegates.IsReadOnlyCallback3)
                    Marshal.GetDelegateForFunctionPointer(r.IsReadOnly,
                                                          typeof (CefRequestCapiDelegates.IsReadOnlyCallback3));
                var value = function(Handle);
                return Convert.ToBoolean(value);
            }
        }

        internal static PostElementCollection FromHandle(IntPtr handle) {
            return new PostElementCollection(handle);
        }
    }
}
