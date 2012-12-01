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

namespace Crystalbyte.Spectre.Scripting {
    public sealed class JavaScriptDialogCallback : CefTypeAdapter {
        private JavaScriptDialogCallback(IntPtr handle)
            : base(typeof (CefJsdialogCallback)) {
            Handle = handle;
        }

        public void Resume(bool success, string message = "") {
            var r = MarshalFromNative<CefJsdialogCallback>();
            var action = (CefJsdialogHandlerCapiDelegates.ContCallback6)
                         Marshal.GetDelegateForFunctionPointer(r.Cont,
                                                               typeof (CefJsdialogHandlerCapiDelegates.ContCallback6));
            var input = new StringUtf16(message);
            action(Handle, Convert.ToInt32(success), input.Handle);
            input.Free();
        }

        public static JavaScriptDialogCallback FromHandle(IntPtr handle) {
            return new JavaScriptDialogCallback(handle);
        }
    }
}
