using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Crystalbyte.Spectre.Projections
{
	[SuppressUnmanagedCodeSecurity]
	public static class CefCommandLineCapi {
		[DllImport(CefAssembly.Name, EntryPoint = "cef_command_line_create", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern IntPtr CefCommandLineCreate();
		
		[DllImport(CefAssembly.Name, EntryPoint = "cef_command_line_get_global", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern IntPtr CefCommandLineGetGlobal();
	}
	
	[StructLayout(LayoutKind.Sequential)]
	public struct CefCommandLine {
		public CefBase Base;
		public IntPtr IsValid;
		public IntPtr IsReadOnly;
		public IntPtr Copy;
		public IntPtr InitFromArgv;
		public IntPtr InitFromString;
		public IntPtr Reset;
		public IntPtr GetArgv;
		public IntPtr GetCommandLineString;
		public IntPtr GetProgram;
		public IntPtr SetProgram;
		public IntPtr HasSwitches;
		public IntPtr HasSwitch;
		public IntPtr GetSwitchValue;
		public IntPtr GetSwitches;
		public IntPtr AppendSwitch;
		public IntPtr AppendSwitchWithValue;
		public IntPtr HasArguments;
		public IntPtr GetArguments;
		public IntPtr AppendArgument;
		public IntPtr PrependWrapper;
	}
	
	public delegate int IsValidCallback(IntPtr self);
	public delegate int IsReadOnlyCallback(IntPtr self);
	public delegate IntPtr CopyCallback(IntPtr self);
	public delegate void InitFromArgvCallback(IntPtr self, int argc, IntPtr argv);
	public delegate void InitFromStringCallback(IntPtr self, IntPtr commandLine);
	public delegate void ResetCallback(IntPtr self);
	public delegate void GetArgvCallback(IntPtr self, IntPtr argv);
	public delegate IntPtr GetCommandLineStringCallback(IntPtr self);
	public delegate IntPtr GetProgramCallback(IntPtr self);
	public delegate void SetProgramCallback(IntPtr self, IntPtr program);
	public delegate int HasSwitchesCallback(IntPtr self);
	public delegate int HasSwitchCallback(IntPtr self, IntPtr name);
	public delegate IntPtr GetSwitchValueCallback(IntPtr self, IntPtr name);
	public delegate void GetSwitchesCallback(IntPtr self, IntPtr switches);
	public delegate void AppendSwitchCallback(IntPtr self, IntPtr name);
	public delegate void AppendSwitchWithValueCallback(IntPtr self, IntPtr name, IntPtr value);
	public delegate int HasArgumentsCallback(IntPtr self);
	public delegate void GetArgumentsCallback(IntPtr self, IntPtr arguments);
	public delegate void AppendArgumentCallback(IntPtr self, IntPtr argument);
	public delegate void PrependWrapperCallback(IntPtr self, IntPtr wrapper);
	
}
