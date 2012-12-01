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
using Crystalbyte.Spectre.Projections.Internal;

#endregion

namespace Crystalbyte.Spectre.Projections {
    [StructLayout(LayoutKind.Sequential)]
    public struct CefXmlReader {
        public CefBase Base;
        public IntPtr MoveToNextNode;
        public IntPtr Close;
        public IntPtr HasError;
        public IntPtr GetError;
        public IntPtr GetElementType;
        public IntPtr GetDepth;
        public IntPtr GetLocalName;
        public IntPtr GetPrefix;
        public IntPtr GetQualifiedName;
        public IntPtr GetNamespaceUri;
        public IntPtr GetBaseUri;
        public IntPtr GetXmlLang;
        public IntPtr IsEmptyElement;
        public IntPtr HasValue;
        public IntPtr GetValue;
        public IntPtr HasAttributes;
        public IntPtr GetAttributeCount;
        public IntPtr GetAttributeByindex;
        public IntPtr GetAttributeByqname;
        public IntPtr GetAttributeBylname;
        public IntPtr GetInnerXml;
        public IntPtr GetOuterXml;
        public IntPtr GetLineNumber;
        public IntPtr MoveToAttributeByindex;
        public IntPtr MoveToAttributeByqname;
        public IntPtr MoveToAttributeBylname;
        public IntPtr MoveToFirstAttribute;
        public IntPtr MoveToNextAttribute;
        public IntPtr MoveToCarryingElement;
    }

    [SuppressUnmanagedCodeSecurity]
    public static class CefXmlReaderCapiDelegates {
        public delegate int MoveToNextNodeCallback(IntPtr self);

        public delegate int CloseCallback(IntPtr self);

        public delegate int HasErrorCallback(IntPtr self);

        public delegate IntPtr GetErrorCallback(IntPtr self);

        public delegate CefXmlNodeType GetTypeCallback8(IntPtr self);

        public delegate int GetDepthCallback(IntPtr self);

        public delegate IntPtr GetLocalNameCallback(IntPtr self);

        public delegate IntPtr GetPrefixCallback(IntPtr self);

        public delegate IntPtr GetQualifiedNameCallback(IntPtr self);

        public delegate IntPtr GetNamespaceUriCallback(IntPtr self);

        public delegate IntPtr GetBaseUriCallback(IntPtr self);

        public delegate IntPtr GetXmlLangCallback(IntPtr self);

        public delegate int IsEmptyElementCallback(IntPtr self);

        public delegate int HasValueCallback(IntPtr self);

        public delegate IntPtr GetValueCallback2(IntPtr self);

        public delegate int HasAttributesCallback(IntPtr self);

        public delegate int GetAttributeCountCallback(IntPtr self);

        public delegate IntPtr GetAttributeByindexCallback(IntPtr self, int index);

        public delegate IntPtr GetAttributeByqnameCallback(IntPtr self, IntPtr qualifiedname);

        public delegate IntPtr GetAttributeBylnameCallback(IntPtr self, IntPtr localname, IntPtr namespaceuri);

        public delegate IntPtr GetInnerXmlCallback(IntPtr self);

        public delegate IntPtr GetOuterXmlCallback(IntPtr self);

        public delegate int GetLineNumberCallback3(IntPtr self);

        public delegate int MoveToAttributeByindexCallback(IntPtr self, int index);

        public delegate int MoveToAttributeByqnameCallback(IntPtr self, IntPtr qualifiedname);

        public delegate int MoveToAttributeBylnameCallback(IntPtr self, IntPtr localname, IntPtr namespaceuri);

        public delegate int MoveToFirstAttributeCallback(IntPtr self);

        public delegate int MoveToNextAttributeCallback(IntPtr self);

        public delegate int MoveToCarryingElementCallback(IntPtr self);
    }
}
