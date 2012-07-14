using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Security;
using Crystalbyte.Chocolate.Bindings.Internal;

namespace Crystalbyte.Chocolate.Bindings
{
	[SuppressUnmanagedCodeSecurity]
	public static class CefV8Capi {
		[DllImport(CefAssembly.Name, EntryPoint = "cef_register_extension", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern int CefRegisterExtension(IntPtr extensionName, IntPtr javascriptCode, IntPtr handler);
		
		[DllImport(CefAssembly.Name, EntryPoint = "cef_v8context_get_current_context", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern IntPtr CefV8contextGetCurrentContext();
		
		[DllImport(CefAssembly.Name, EntryPoint = "cef_v8context_get_entered_context", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern IntPtr CefV8contextGetEnteredContext();
		
		[DllImport(CefAssembly.Name, EntryPoint = "cef_v8context_in_context", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern int CefV8contextInContext();
		
		[DllImport(CefAssembly.Name, EntryPoint = "cef_v8value_create_undefined", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern IntPtr CefV8valueCreateUndefined();
		
		[DllImport(CefAssembly.Name, EntryPoint = "cef_v8value_create_null", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern IntPtr CefV8valueCreateNull();
		
		[DllImport(CefAssembly.Name, EntryPoint = "cef_v8value_create_bool", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern IntPtr CefV8valueCreateBool(int value);
		
		[DllImport(CefAssembly.Name, EntryPoint = "cef_v8value_create_int", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern IntPtr CefV8valueCreateInt(int value);
		
		[DllImport(CefAssembly.Name, EntryPoint = "cef_v8value_create_uint", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern IntPtr CefV8valueCreateUint(uint value);
		
		[DllImport(CefAssembly.Name, EntryPoint = "cef_v8value_create_double", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern IntPtr CefV8valueCreateDouble(Double value);
		
		[DllImport(CefAssembly.Name, EntryPoint = "cef_v8value_create_date", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern IntPtr CefV8valueCreateDate(IntPtr date);
		
		[DllImport(CefAssembly.Name, EntryPoint = "cef_v8value_create_string", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern IntPtr CefV8valueCreateString(IntPtr value);
		
		[DllImport(CefAssembly.Name, EntryPoint = "cef_v8value_create_object", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern IntPtr CefV8valueCreateObject(IntPtr accessor);
		
		[DllImport(CefAssembly.Name, EntryPoint = "cef_v8value_create_array", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern IntPtr CefV8valueCreateArray(int length);
		
		[DllImport(CefAssembly.Name, EntryPoint = "cef_v8value_create_function", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern IntPtr CefV8valueCreateFunction(IntPtr name, IntPtr handler);
	}
	
	[StructLayout(LayoutKind.Sequential)]
	public struct CefV8context {
		public CefBase Base;
		public IntPtr GetBrowser;
		public IntPtr GetFrame;
		public IntPtr GetGlobal;
		public IntPtr Enter;
		public IntPtr Exit;
		public IntPtr IsSame;
		public IntPtr Eval;
	}
	
	[StructLayout(LayoutKind.Sequential)]
	public struct CefV8handler {
		public CefBase Base;
		public IntPtr Execute;
	}
	
	[StructLayout(LayoutKind.Sequential)]
	public struct CefV8accessor {
		public CefBase Base;
		public IntPtr Get;
		public IntPtr Set;
	}
	
	[StructLayout(LayoutKind.Sequential)]
	public struct CefV8exception {
		public CefBase Base;
		public IntPtr GetMessage;
		public IntPtr GetSourceLine;
		public IntPtr GetScriptResourceName;
		public IntPtr GetLineNumber;
		public IntPtr GetStartPosition;
		public IntPtr GetEndPosition;
		public IntPtr GetStartColumn;
		public IntPtr GetEndColumn;
	}
	
	[StructLayout(LayoutKind.Sequential)]
	public struct CefV8value {
		public CefBase Base;
		public IntPtr IsUndefined;
		public IntPtr IsNull;
		public IntPtr IsBool;
		public IntPtr IsInt;
		public IntPtr IsUint;
		public IntPtr IsDouble;
		public IntPtr IsDate;
		public IntPtr IsString;
		public IntPtr IsObject;
		public IntPtr IsArray;
		public IntPtr IsFunction;
		public IntPtr IsSame;
		public IntPtr GetBoolValue;
		public IntPtr GetIntValue;
		public IntPtr GetUintValue;
		public IntPtr GetDoubleValue;
		public IntPtr GetDateValue;
		public IntPtr GetStringValue;
		public IntPtr IsUserCreated;
		public IntPtr HasException;
		public IntPtr GetException;
		public IntPtr ClearException;
		public IntPtr WillRethrowExceptions;
		public IntPtr SetRethrowExceptions;
		public IntPtr HasValueBykey;
		public IntPtr HasValueByindex;
		public IntPtr DeleteValueBykey;
		public IntPtr DeleteValueByindex;
		public IntPtr GetValueBykey;
		public IntPtr GetValueByindex;
		public IntPtr SetValueBykey;
		public IntPtr SetValueByindex;
		public IntPtr SetValueByaccessor;
		public IntPtr GetKeys;
		public IntPtr SetUserData;
		public IntPtr GetUserData;
		public IntPtr GetExternallyAllocatedMemory;
		public IntPtr AdjustExternallyAllocatedMemory;
		public IntPtr GetArrayLength;
		public IntPtr GetFunctionName;
		public IntPtr GetFunctionHandler;
		public IntPtr ExecuteFunction;
		public IntPtr ExecuteFunctionWithContext;
	}
	
	public delegate IntPtr GetGlobalCallback(IntPtr self);
	public delegate int EnterCallback(IntPtr self);
	public delegate int ExitCallback(IntPtr self);
	public delegate int EvalCallback(IntPtr self, IntPtr code, IntPtr retval, IntPtr exception);
	public delegate int GetCallback(IntPtr self, IntPtr name, IntPtr @object, IntPtr retval, IntPtr exception);
	public delegate IntPtr GetMessageCallback(IntPtr self);
	public delegate IntPtr GetSourceLineCallback(IntPtr self);
	public delegate IntPtr GetScriptResourceNameCallback(IntPtr self);
	public delegate int GetLineNumberCallback(IntPtr self);
	public delegate int GetStartPositionCallback(IntPtr self);
	public delegate int GetEndPositionCallback(IntPtr self);
	public delegate int GetStartColumnCallback(IntPtr self);
	public delegate int GetEndColumnCallback(IntPtr self);
	public delegate int IsUndefinedCallback(IntPtr self);
	public delegate int IsNullCallback(IntPtr self);
	public delegate int IsBoolCallback(IntPtr self);
	public delegate int IsIntCallback(IntPtr self);
	public delegate int IsUintCallback(IntPtr self);
	public delegate int IsDoubleCallback(IntPtr self);
	public delegate int IsDateCallback(IntPtr self);
	public delegate int IsStringCallback(IntPtr self);
	public delegate int IsObjectCallback(IntPtr self);
	public delegate int IsArrayCallback(IntPtr self);
	public delegate int IsFunctionCallback(IntPtr self);
	public delegate int GetBoolValueCallback(IntPtr self);
	public delegate int GetIntValueCallback(IntPtr self);
	public delegate uint GetUintValueCallback(IntPtr self);
	public delegate Double GetDoubleValueCallback(IntPtr self);
	public delegate IntPtr GetDateValueCallback(IntPtr self);
	public delegate IntPtr GetStringValueCallback(IntPtr self);
	public delegate int IsUserCreatedCallback(IntPtr self);
	public delegate int HasExceptionCallback(IntPtr self);
	public delegate IntPtr GetExceptionCallback(IntPtr self);
	public delegate int ClearExceptionCallback(IntPtr self);
	public delegate int WillRethrowExceptionsCallback(IntPtr self);
	public delegate int SetRethrowExceptionsCallback(IntPtr self, int rethrow);
	public delegate int HasValueBykeyCallback(IntPtr self, IntPtr key);
	public delegate int HasValueByindexCallback(IntPtr self, int index);
	public delegate int DeleteValueBykeyCallback(IntPtr self, IntPtr key);
	public delegate int DeleteValueByindexCallback(IntPtr self, int index);
	public delegate IntPtr GetValueBykeyCallback(IntPtr self, IntPtr key);
	public delegate IntPtr GetValueByindexCallback(IntPtr self, int index);
	public delegate int SetValueBykeyCallback(IntPtr self, IntPtr key, IntPtr value, CefV8Propertyattribute attribute);
	public delegate int SetValueByindexCallback(IntPtr self, int index, IntPtr value);
	public delegate int SetValueByaccessorCallback(IntPtr self, IntPtr key, CefV8Accesscontrol settings, CefV8Propertyattribute attribute);
	public delegate int GetKeysCallback(IntPtr self, IntPtr keys);
	public delegate int SetUserDataCallback(IntPtr self, IntPtr userData);
	public delegate IntPtr GetUserDataCallback(IntPtr self);
	public delegate int GetExternallyAllocatedMemoryCallback(IntPtr self);
	public delegate int AdjustExternallyAllocatedMemoryCallback(IntPtr self, int changeInBytes);
	public delegate int GetArrayLengthCallback(IntPtr self);
	public delegate IntPtr GetFunctionNameCallback(IntPtr self);
	public delegate IntPtr GetFunctionHandlerCallback(IntPtr self);
	public delegate IntPtr ExecuteFunctionCallback(IntPtr self, IntPtr @object, int argumentscount, IntPtr arguments);
	public delegate IntPtr ExecuteFunctionWithContextCallback(IntPtr self, IntPtr context, IntPtr @object, int argumentscount, IntPtr arguments);
	
}
