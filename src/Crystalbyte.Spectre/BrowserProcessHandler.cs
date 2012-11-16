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
using System.Reflection;
using System.Runtime.InteropServices;
using Crystalbyte.Spectre.Interop;
using Crystalbyte.Spectre.Projections;

#endregion

namespace Crystalbyte.Spectre {
    internal sealed class BrowserProcessHandler : OwnedRefCountedNativeTypeAdapter {
        private readonly AppDelegate _appDelegate;

        private readonly CefBrowserProcessHandlerCapiDelegates.OnBeforeChildProcessLaunchCallback
            _beforeChildProcessLaunchCallback;

        public BrowserProcessHandler(AppDelegate appDelegate)
            : base(typeof (CefBrowserProcessHandler)) {
            _appDelegate = appDelegate;

            _beforeChildProcessLaunchCallback = OnBeforeChildProcessLaunch;

            MarshalToNative(new CefBrowserProcessHandler {
                Base = DedicatedBase,
                OnBeforeChildProcessLaunch = Marshal.GetFunctionPointerForDelegate(_beforeChildProcessLaunchCallback)
            });
        }

        private void OnBeforeChildProcessLaunch(IntPtr self, IntPtr commandLine) {

			var cl = CommandLine.FromHandle(commandLine);

      		// .NET in Windows treat assemblies as native images, so no magic required.
            // Mono on any platform usually located far away from entry assembly, so we want prepare command line to call it correctly.
            if (Type.GetType("Mono.Runtime") != null)
            {
                if (!cl.HasSwitch("mono"))
                {
                    cl.Program = new Uri(Assembly.GetEntryAssembly().CodeBase).LocalPath;

                    var mono = (Platform.IsLinux || Platform.IsOsX)  
						? "/usr/bin/mono" 
							: @"C:\Program Files\Mono-2.10.8\bin\monow.exe";
                    cl.PrependWrapper(mono);
					cl.AppendSwitch("mono");
                }
            }
        }
    }
}
