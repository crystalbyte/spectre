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

namespace Crystalbyte.Spectre {
    internal sealed class BrowserProcessHandler : OwnedRefCountedCefTypeAdapter {
        private readonly RenderDelegate _appDelegate;

        private readonly CefBrowserProcessHandlerCapiDelegates.OnBeforeChildProcessLaunchCallback
            _beforeChildProcessLaunchCallback;

        public BrowserProcessHandler(RenderDelegate appDelegate)
            : base(typeof (CefBrowserProcessHandler)) {
            _appDelegate = appDelegate;

            _beforeChildProcessLaunchCallback = OnBeforeChildProcessLaunch;

            MarshalToNative(new CefBrowserProcessHandler {
                Base = DedicatedBase,
                OnBeforeChildProcessLaunch = Marshal.GetFunctionPointerForDelegate(_beforeChildProcessLaunchCallback)
            });
        }

        private void OnBeforeChildProcessLaunch(IntPtr self, IntPtr commandLine) {
            var e = new ProcessLaunchingEventArgs {
                CommandLine = CommandLine.FromHandle(commandLine)
            };

            _appDelegate.OnChildProcessLaunching(e);
        }
    }
}
