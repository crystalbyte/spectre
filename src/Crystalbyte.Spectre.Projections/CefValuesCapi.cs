using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Security;
using Crystalbyte.Spectre.Projections.Internal;

namespace Crystalbyte.Spectre.Projections
{
	[SuppressUnmanagedCodeSecurity]
	public static class CefValuesCapi {
		[DllImport(CefAssembly.Name, EntryPoint = "cef_binary_value_create", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern IntPtr CefBinaryValueCreate(IntPtr data, int dataSize);
		
		[DllImport(CefAssembly.Name, EntryPoint = "cef_dictionary_value_create", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern IntPtr CefDictionaryValueCreate();
		
		[DllImport(CefAssembly.Name, EntryPoint = "cef_list_value_create", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern IntPtr CefListValueCreate();
	}
	
	[StructLayout(LayoutKind.Sequential)]
	public struct CefBinaryValue {
		public CefBase Base;
		public IntPtr IsValid;
		public IntPtr IsOwned;
		public IntPtr Copy;
		public IntPtr GetSize;
		public IntPtr GetData;
	}
	
	[StructLayout(LayoutKind.Sequential)]
	public struct CefDictionaryValue {
		public CefBase Base;
		public IntPtr IsValid;
		public IntPtr IsOwned;
		public IntPtr IsReadOnly;
		public IntPtr Copy;
		public IntPtr GetSize;
		public IntPtr Clear;
		public IntPtr HasKey;
		public IntPtr GetKeys;
		public IntPtr Remove;
		public IntPtr GetElementType;
		public IntPtr GetBool;
		public IntPtr GetInt;
		public IntPtr GetDouble;
		public IntPtr GetString;
		public IntPtr GetBinary;
		public IntPtr GetDictionary;
		public IntPtr GetList;
		public IntPtr SetNull;
		public IntPtr SetBool;
		public IntPtr SetInt;
		public IntPtr SetDouble;
		public IntPtr SetString;
		public IntPtr SetBinary;
		public IntPtr SetDictionary;
		public IntPtr SetList;
	}
	
	[StructLayout(LayoutKind.Sequential)]
	public struct CefListValue {
		public CefBase Base;
		public IntPtr IsValid;
		public IntPtr IsOwned;
		public IntPtr IsReadOnly;
		public IntPtr Copy;
		public IntPtr SetSize;
		public IntPtr GetSize;
		public IntPtr Clear;
		public IntPtr Remove;
		public IntPtr GetElementType;
		public IntPtr GetBool;
		public IntPtr GetInt;
		public IntPtr GetDouble;
		public IntPtr GetString;
		public IntPtr GetBinary;
		public IntPtr GetDictionary;
		public IntPtr GetList;
		public IntPtr SetNull;
		public IntPtr SetBool;
		public IntPtr SetInt;
		public IntPtr SetDouble;
		public IntPtr SetString;
		public IntPtr SetBinary;
		public IntPtr SetDictionary;
		public IntPtr SetList;
	}
	
	[SuppressUnmanagedCodeSecurity]
	public static class CefValuesCapiDelegates {
		public delegate int IsOwnedCallback(IntPtr self);
		public delegate int GetSizeCallback(IntPtr self);
		public delegate int GetDataCallback(IntPtr self, IntPtr buffer, int bufferSize, int dataOffset);
		public delegate IntPtr CopyCallback3(IntPtr self, int excludeEmptyChildren);
		public delegate int HasKeyCallback(IntPtr self, IntPtr key);
		public delegate int RemoveCallback2(IntPtr self, IntPtr key);
		public delegate CefValueType GetTypeCallback6(IntPtr self, IntPtr key);
		public delegate int GetBoolCallback(IntPtr self, IntPtr key);
		public delegate int GetIntCallback(IntPtr self, IntPtr key);
		public delegate Double GetDoubleCallback(IntPtr self, IntPtr key);
		public delegate IntPtr GetStringCallback(IntPtr self, IntPtr key);
		public delegate IntPtr GetBinaryCallback(IntPtr self, IntPtr key);
		public delegate IntPtr GetDictionaryCallback(IntPtr self, IntPtr key);
		public delegate IntPtr GetListCallback(IntPtr self, IntPtr key);
		public delegate int SetNullCallback(IntPtr self, IntPtr key);
		public delegate int SetBoolCallback(IntPtr self, IntPtr key, int value);
		public delegate int SetIntCallback(IntPtr self, IntPtr key, int value);
		public delegate int SetDoubleCallback(IntPtr self, IntPtr key, Double value);
		public delegate int SetStringCallback(IntPtr self, IntPtr key, IntPtr value);
		public delegate int SetBinaryCallback(IntPtr self, IntPtr key, IntPtr value);
		public delegate int SetDictionaryCallback(IntPtr self, IntPtr key, IntPtr value);
		public delegate int SetListCallback(IntPtr self, IntPtr key, IntPtr value);
		public delegate int SetSizeCallback(IntPtr self, int size);
		public delegate int RemoveCallback3(IntPtr self, int index);
		public delegate CefValueType GetTypeCallback7(IntPtr self, int index);
		public delegate int GetBoolCallback2(IntPtr self, int index);
		public delegate int GetIntCallback2(IntPtr self, int index);
		public delegate Double GetDoubleCallback2(IntPtr self, int index);
		public delegate IntPtr GetStringCallback2(IntPtr self, int index);
		public delegate IntPtr GetBinaryCallback2(IntPtr self, int index);
		public delegate IntPtr GetDictionaryCallback2(IntPtr self, int index);
		public delegate IntPtr GetListCallback2(IntPtr self, int index);
		public delegate int SetNullCallback2(IntPtr self, int index);
		public delegate int SetBoolCallback2(IntPtr self, int index, int value);
		public delegate int SetIntCallback2(IntPtr self, int index, int value);
		public delegate int SetDoubleCallback2(IntPtr self, int index, Double value);
		public delegate int SetStringCallback2(IntPtr self, int index, IntPtr value);
		public delegate int SetBinaryCallback2(IntPtr self, int index, IntPtr value);
		public delegate int SetDictionaryCallback2(IntPtr self, int index, IntPtr value);
		public delegate int SetListCallback2(IntPtr self, int index, IntPtr value);
	}
	
}
