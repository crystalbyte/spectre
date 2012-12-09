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
using System.Security;

#endregion

namespace Crystalbyte.Spectre.Projections {
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
        public IntPtr VisitDom;
    }

    [SuppressUnmanagedCodeSecurity]
    public static class CefFrameCapiDelegates {
        public delegate int IsValidCallback3(IntPtr self);

        public delegate void UndoCallback(IntPtr self);

        public delegate void RedoCallback(IntPtr self);

        public delegate void CutCallback(IntPtr self);

        public delegate void CopyCallback2(IntPtr self);

        public delegate void PasteCallback(IntPtr self);

        public delegate void DelCallback(IntPtr self);

        public delegate void SelectAllCallback(IntPtr self);

        public delegate void ViewSourceCallback(IntPtr self);

        public delegate void GetSourceCallback(IntPtr self, IntPtr visitor);

        public delegate void GetTextCallback(IntPtr self, IntPtr visitor);

        public delegate void LoadRequestCallback(IntPtr self, IntPtr request);

        public delegate void LoadUrlCallback(IntPtr self, IntPtr url);

        public delegate void LoadStringCallback(IntPtr self, IntPtr stringVal, IntPtr url);

        public delegate void ExecuteJavaScriptCallback(IntPtr self, IntPtr code, IntPtr scriptUrl, int startLine);

        public delegate int IsMainCallback(IntPtr self);

        public delegate int IsFocusedCallback(IntPtr self);

        public delegate IntPtr GetNameCallback2(IntPtr self);

        public delegate long GetIdentifierCallback2(IntPtr self);

        public delegate IntPtr GetParentCallback2(IntPtr self);

        public delegate IntPtr GetUrlCallback2(IntPtr self);

        public delegate IntPtr GetBrowserCallback2(IntPtr self);

        public delegate IntPtr GetV8contextCallback(IntPtr self);

        public delegate void VisitDomCallback(IntPtr self, IntPtr visitor);
    }
}
