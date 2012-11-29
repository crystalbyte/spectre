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
using System.IO;
using System.Runtime.InteropServices;
using Crystalbyte.Spectre.Interop;
using Crystalbyte.Spectre.Projections;

#endregion

namespace Crystalbyte.Spectre {
    public sealed class IpcMessage : RefCountedCefTypeAdapter {
        public IpcMessage(string name)
            : base(typeof (CefProcessMessage)) {
            var s = new StringUtf16(name);
            Handle = CefProcessMessageCapi.CefProcessMessageCreate(s.Handle);
            s.Free();
        }

        private IpcMessage(IntPtr handle)
            : base(typeof (CefProcessMessage)) {
            Handle = handle;
        }

        public bool IsReadOnly {
            get {
                var r = MarshalFromNative<CefProcessMessage>();
                var function = (CefProcessMessageCapiDelegates.IsReadOnlyCallback2)
                               Marshal.GetDelegateForFunctionPointer(r.IsReadOnly,
                                                                     typeof (
                                                                         CefProcessMessageCapiDelegates.
                                                                         IsReadOnlyCallback2));
                var value = function(Handle);
                return Convert.ToBoolean(value);
            }
        }

        public bool IsValid {
            get {
                var r = MarshalFromNative<CefProcessMessage>();
                var function = (CefProcessMessageCapiDelegates.IsValidCallback4)
                               Marshal.GetDelegateForFunctionPointer(r.IsValid,
                                                                     typeof (
                                                                         CefProcessMessageCapiDelegates.IsValidCallback4
                                                                         ));
                var value = function(Handle);
                return Convert.ToBoolean(value);
            }
        }

        public string Name {
            get {
                var r = MarshalFromNative<CefProcessMessage>();
                var function = (CefProcessMessageCapiDelegates.GetNameCallback3)
                               Marshal.GetDelegateForFunctionPointer(r.GetName,
                                                                     typeof (
                                                                         CefProcessMessageCapiDelegates.GetNameCallback3
                                                                         ));
                var handle = function(Handle);
                return StringUtf16.ReadStringAndFree(handle);
            }
        }

        private ListValue Arguments {
            get {
                var r = MarshalFromNative<CefProcessMessage>();
                var function =
                    (CefProcessMessageCapiDelegates.GetArgumentListCallback)
                    Marshal.GetDelegateForFunctionPointer(r.GetArgumentList,
                                                          typeof (CefProcessMessageCapiDelegates.GetArgumentListCallback
                                                              ));
                var handle = function(Handle);
                return ListValue.FromHandle(handle);
            }
        }

        public Stream Payload {
            get {
                var bin = Arguments.GetBinary(0);
                return bin.Data;
            }
            set {
                var a = Arguments;
                if (a.Count < 1) {
                    a.SetSize(1);
                }
                Arguments.SetBinary(0, new BinaryObject(value));
            }
        }

        public static IpcMessage FromHandle(IntPtr handle) {
            return new IpcMessage(handle);
        }

        protected override void DisposeNative() {
            // FIXME: Disposing throws AccessViolationException, object must be disposed internally. 
            // http://www.magpcss.org/ceforum/viewtopic.php?f=6&t=766
            //base.DisposeNative();
        }
    }
}
