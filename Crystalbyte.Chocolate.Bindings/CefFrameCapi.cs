using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Security;
using Crystalbyte.Chocolate.Bindings.Internal;

namespace Crystalbyte.Chocolate.Bindings
{
	[StructLayout(LayoutKind.Sequential)]
	public struct CefFrame {
		public CefBase Base;
		public IntPtr IsValid;
		public IntPtr Undo;
		public IntPtr Redo;
		public IntPtr Cut;
		public IntPtr Copy;
		public IntPtr Paste;
		public IntPtr Del;
		public IntPtr SelectAll;
		public IntPtr ViewSource;
		public IntPtr GetSource;
		public IntPtr GetText;
		public IntPtr LoadRequest;
		public IntPtr LoadUrl;
		public IntPtr LoadString;
		public IntPtr ExecuteJavaScript;
		public IntPtr IsMain;
		public IntPtr IsFocused;
		public IntPtr GetName;
		public IntPtr GetIdentifier;
		public IntPtr GetParent;
		public IntPtr GetUrl;
		public IntPtr GetBrowser;
		public IntPtr GetV8context;
	}
	
	public delegate void UndoCallback(IntPtr self);
	public delegate void RedoCallback(IntPtr self);
	public delegate void CutCallback(IntPtr self);
	public delegate void PasteCallback(IntPtr self);
	public delegate void DelCallback(IntPtr self);
	public delegate void SelectAllCallback(IntPtr self);
	public delegate void ViewSourceCallback(IntPtr self);
	public delegate void GetSourceCallback(IntPtr self, IntPtr visitor);
	public delegate void GetTextCallback(IntPtr self, IntPtr visitor);
	public delegate void LoadRequestCallback(IntPtr self, IntPtr request);
	public delegate void LoadUrlCallback(IntPtr self, IntPtr url);
	public delegate void LoadStringCallback(IntPtr self, IntPtr stringVal, IntPtr url);
	public delegate void ExecuteJavaScriptCallback(IntPtr self, IntPtr jscode, IntPtr scripturl, int startline);
	public delegate int IsMainCallback(IntPtr self);
	public delegate int IsFocusedCallback(IntPtr self);
	public delegate IntPtr GetNameCallback(IntPtr self);
	public delegate IntPtr GetParentCallback(IntPtr self);
	public delegate IntPtr GetUrlCallback(IntPtr self);
	public delegate IntPtr GetV8contextCallback(IntPtr self);
	
}
