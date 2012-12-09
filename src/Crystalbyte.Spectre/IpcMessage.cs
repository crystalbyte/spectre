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
