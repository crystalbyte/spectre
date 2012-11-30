﻿#region Licensing notice

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
using System.Reflection;
using System.Runtime.InteropServices;
using Crystalbyte.Spectre.Interop;
using Crystalbyte.Spectre.Projections;

#endregion

namespace Crystalbyte.Spectre {
    internal sealed class BrowserProcessHandler : OwnedRefCountedCefTypeAdapter {
        private readonly RendererDelegate _appDelegate;

        private readonly CefBrowserProcessHandlerCapiDelegates.OnBeforeChildProcessLaunchCallback
            _beforeChildProcessLaunchCallback;

        public BrowserProcessHandler(RendererDelegate appDelegate)
            : base(typeof(CefBrowserProcessHandler)) {
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