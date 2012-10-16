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

    public delegate int MoveToNextNodeCallback(IntPtr self);

    public delegate int CloseCallback(IntPtr self);

    public delegate int HasErrorCallback(IntPtr self);

    public delegate IntPtr GetErrorCallback(IntPtr self);

    public delegate int GetDepthCallback(IntPtr self);

    public delegate IntPtr GetLocalNameCallback(IntPtr self);

    public delegate IntPtr GetPrefixCallback(IntPtr self);

    public delegate IntPtr GetQualifiedNameCallback(IntPtr self);

    public delegate IntPtr GetNamespaceUriCallback(IntPtr self);

    public delegate IntPtr GetBaseUriCallback(IntPtr self);

    public delegate IntPtr GetXmlLangCallback(IntPtr self);

    public delegate int IsEmptyElementCallback(IntPtr self);

    public delegate int HasValueCallback(IntPtr self);

    public delegate int HasAttributesCallback(IntPtr self);

    public delegate int GetAttributeCountCallback(IntPtr self);

    public delegate IntPtr GetAttributeByindexCallback(IntPtr self, int index);

    public delegate IntPtr GetAttributeByqnameCallback(IntPtr self, IntPtr qualifiedname);

    public delegate IntPtr GetAttributeBylnameCallback(IntPtr self, IntPtr localname, IntPtr namespaceuri);

    public delegate IntPtr GetInnerXmlCallback(IntPtr self);

    public delegate IntPtr GetOuterXmlCallback(IntPtr self);

    public delegate int MoveToAttributeByindexCallback(IntPtr self, int index);

    public delegate int MoveToAttributeByqnameCallback(IntPtr self, IntPtr qualifiedname);

    public delegate int MoveToAttributeBylnameCallback(IntPtr self, IntPtr localname, IntPtr namespaceuri);

    public delegate int MoveToFirstAttributeCallback(IntPtr self);

    public delegate int MoveToNextAttributeCallback(IntPtr self);

    public delegate int MoveToCarryingElementCallback(IntPtr self);
}
