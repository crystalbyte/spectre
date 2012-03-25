using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Security;
using Crystalbyte.Chocolate.Bindings.Internal;

namespace Crystalbyte.Chocolate.Bindings
{
	[StructLayout(LayoutKind.Sequential)]
	public struct CefFrame {
		CefBase Base;
		IntPtr IsValid;
		IntPtr Undo;
		IntPtr Redo;
		IntPtr Cut;
		IntPtr Copy;
		IntPtr Paste;
		IntPtr Del;
		IntPtr SelectAll;
		IntPtr Print;
		IntPtr ViewSource;
		IntPtr GetSource;
		IntPtr GetText;
		IntPtr LoadRequest;
		IntPtr LoadUrl;
		IntPtr LoadString;
		IntPtr ExecuteJavaScript;
		IntPtr IsMain;
		IntPtr IsFocused;
		IntPtr GetName;
		IntPtr GetIdentifier;
		IntPtr GetParent;
		IntPtr GetUrl;
		IntPtr GetBrowser;
	}
	
	public delegate int IsValidCallback(IntPtr self);
	public delegate void UndoCallback(IntPtr self);
	public delegate void RedoCallback(IntPtr self);
	public delegate void CutCallback(IntPtr self);
	public delegate void CopyCallback(IntPtr self);
	public delegate void PasteCallback(IntPtr self);
	public delegate void DelCallback(IntPtr self);
	public delegate void SelectAllCallback(IntPtr self);
	public delegate void PrintCallback(IntPtr self);
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
	public delegate long GetIdentifierCallback(IntPtr self);
	public delegate IntPtr GetParentCallback(IntPtr self);
	public delegate IntPtr GetUrlCallback(IntPtr self);
	public delegate IntPtr GetBrowserCallback(IntPtr self);
	
}
