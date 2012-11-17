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
    public sealed class CommandLine : RefCountedCefTypeAdapter {
        public CommandLine()
            : base(typeof(CefCommandLine)) {
            Handle = CefCommandLineCapi.CefCommandLineCreate();
        }

        private CommandLine(IntPtr handle)
            : base(typeof(CefCommandLine)) {
            Handle = handle;
        }

        public static CommandLine Current {
            get {
                var handle = CefCommandLineCapi.CefCommandLineGetGlobal();
                return FromHandle(handle);
            }
        }

        public static CommandLine FromHandle(IntPtr handle) {
            return new CommandLine(handle);
        }

        public bool IsValid {
            get {
                var r = MarshalFromNative<CefCommandLine>();
                var function = (CefCommandLineCapiDelegates.IsValidCallback)
                    Marshal.GetDelegateForFunctionPointer(r.IsValid, typeof(CefCommandLineCapiDelegates.IsValidCallback));
                var isValid = function(Handle);
                return Convert.ToBoolean(isValid);
            }
        }

        public bool IsReadOnly {
            get {
                var r = MarshalFromNative<CefCommandLine>();
                var function = (CefCommandLineCapiDelegates.IsReadOnlyCallback)
                    Marshal.GetDelegateForFunctionPointer(r.IsReadOnly, typeof(CefCommandLineCapiDelegates.IsReadOnlyCallback));
                var isReadOnly = function(Handle);
                return Convert.ToBoolean(isReadOnly);
            }
        }

        public CommandLine CopyDeep() {
            var r = MarshalFromNative<CefCommandLine>();
            var function = (CefCommandLineCapiDelegates.CopyCallback)
                Marshal.GetDelegateForFunctionPointer(r.Copy, typeof(CefCommandLineCapiDelegates.CopyCallback));
            var handle = function(Handle);
            return FromHandle(handle);
        }

        public void Reset() {
            var r = MarshalFromNative<CefCommandLine>();
            var action = (CefCommandLineCapiDelegates.ResetCallback)
                Marshal.GetDelegateForFunctionPointer(r.Reset, typeof(CefCommandLineCapiDelegates.ResetCallback));
            action(Handle);
        }

        public override string ToString() {
            var r = MarshalFromNative<CefCommandLine>();
            var function = (CefCommandLineCapiDelegates.GetCommandLineStringCallback)
                Marshal.GetDelegateForFunctionPointer(r.GetCommandLineString, typeof(CefCommandLineCapiDelegates.GetCommandLineStringCallback));
            var handle = function(Handle);
            return StringUtf16.ReadStringAndFree(handle);
        }

        public string Program {
            get {
                var r = MarshalFromNative<CefCommandLine>();
                var function = (CefCommandLineCapiDelegates.GetProgramCallback)
                    Marshal.GetDelegateForFunctionPointer(r.GetProgram, typeof(CefCommandLineCapiDelegates.GetProgramCallback));
                var handle = function(Handle);
                return StringUtf16.ReadStringAndFree(handle);
            }
            set {
                var r = MarshalFromNative<CefCommandLine>();
                var action = (CefCommandLineCapiDelegates.SetProgramCallback)
                    Marshal.GetDelegateForFunctionPointer(r.SetProgram, typeof(CefCommandLineCapiDelegates.SetProgramCallback));
                action(Handle, new StringUtf16(value).Handle);
            }
        }

        public bool HasSwitches {
            get {
                var r = MarshalFromNative<CefCommandLine>();
                var function = (CefCommandLineCapiDelegates.HasSwitchesCallback)
                    Marshal.GetDelegateForFunctionPointer(r.HasSwitches, typeof(CefCommandLineCapiDelegates.HasSwitchesCallback));
                var hasSwitches = function(Handle);
                return Convert.ToBoolean(hasSwitches);
            }
        }

        public StringUtf16Map GetSwitches() {
            var r = MarshalFromNative<CefCommandLine>();
            var action = (CefCommandLineCapiDelegates.GetSwitchesCallback)
                Marshal.GetDelegateForFunctionPointer(r.GetSwitches, typeof(CefCommandLineCapiDelegates.GetSwitchesCallback));
            var map = new StringUtf16Map();
            action(Handle, map.Handle);
            return map;
        }

        public bool HasSwitch(string name) {
            var r = MarshalFromNative<CefCommandLine>();
            var function = (CefCommandLineCapiDelegates.HasSwitchCallback)
                Marshal.GetDelegateForFunctionPointer(r.HasSwitch, typeof(CefCommandLineCapiDelegates.HasSwitchCallback));
            var s = new StringUtf16(name);
            var hasSwitch = function(Handle, s.Handle);
            s.Free();
            return Convert.ToBoolean(hasSwitch);
        }

        public string GetSwitchValue(string name) {
            var r = MarshalFromNative<CefCommandLine>();
            var function = (CefCommandLineCapiDelegates.GetSwitchValueCallback)
                Marshal.GetDelegateForFunctionPointer(r.GetSwitchValue, typeof(CefCommandLineCapiDelegates.GetSwitchValueCallback));
            var value = function(Handle, new StringUtf16(name).Handle);
            return StringUtf16.ReadStringAndFree(value);
        }

        public void AppendSwitch(string name) {
            var r = MarshalFromNative<CefCommandLine>();
            var action = (CefCommandLineCapiDelegates.AppendSwitchCallback)
                Marshal.GetDelegateForFunctionPointer(r.AppendSwitch, typeof(CefCommandLineCapiDelegates.AppendSwitchCallback));
            action(Handle, new StringUtf16(name).Handle);
        }

        public void AppendSwitchWithValue(string name, string value) {
            var r = MarshalFromNative<CefCommandLine>();
            var action = (CefCommandLineCapiDelegates.AppendSwitchWithValueCallback)
                Marshal.GetDelegateForFunctionPointer(r.AppendSwitchWithValue, typeof(CefCommandLineCapiDelegates.AppendSwitchWithValueCallback));
            action(Handle, new StringUtf16(name).Handle, new StringUtf16(value).Handle);
        }

        public bool HasArgument {
            get {
                var r = MarshalFromNative<CefCommandLine>();
                var function = (CefCommandLineCapiDelegates.HasArgumentsCallback)
                    Marshal.GetDelegateForFunctionPointer(r.HasArguments, typeof(CefCommandLineCapiDelegates.HasArgumentsCallback));
                var hasSwitches = function(Handle);
                return Convert.ToBoolean(hasSwitches);
            }
        }

        public StringUtf16List GetArguments() {
            var r = MarshalFromNative<CefCommandLine>();
            var action = (CefCommandLineCapiDelegates.GetArgumentsCallback)
                Marshal.GetDelegateForFunctionPointer(r.GetArguments, typeof(CefCommandLineCapiDelegates.GetArgumentsCallback));

            var list = new StringUtf16List();
            action(Handle, list.Handle);
            return list;
        }

        public void AppendArgument(string value) {
            var r = MarshalFromNative<CefCommandLine>();
            var action = (CefCommandLineCapiDelegates.GetArgumentsCallback)
                Marshal.GetDelegateForFunctionPointer(r.AppendArgument, typeof(CefCommandLineCapiDelegates.GetArgumentsCallback));
            action(Handle, new StringUtf16(value).Handle);
        }

        public void PrependWrapper(string wrapper) {
            var r = MarshalFromNative<CefCommandLine>();
            var action = (CefCommandLineCapiDelegates.PrependWrapperCallback)
                Marshal.GetDelegateForFunctionPointer(r.PrependWrapper, typeof(CefCommandLineCapiDelegates.PrependWrapperCallback));
            action(Handle, new StringUtf16(wrapper).Handle);
        }
    }
}
