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
    public struct CefDomvisitor {
        public CefBase Base;
        public IntPtr Visit;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct CefDomdocument {
        public CefBase Base;
        public IntPtr GetElementType;
        public IntPtr GetDocument;
        public IntPtr GetBody;
        public IntPtr GetHead;
        public IntPtr GetTitle;
        public IntPtr GetElementById;
        public IntPtr GetFocusedNode;
        public IntPtr HasSelection;
        public IntPtr GetSelectionStartNode;
        public IntPtr GetSelectionStartOffset;
        public IntPtr GetSelectionEndNode;
        public IntPtr GetSelectionEndOffset;
        public IntPtr GetSelectionAsMarkup;
        public IntPtr GetSelectionAsText;
        public IntPtr GetBaseUrl;
        public IntPtr GetCompleteUrl;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct CefDomnode {
        public CefBase Base;
        public IntPtr GetElementType;
        public IntPtr IsText;
        public IntPtr IsElement;
        public IntPtr IsEditable;
        public IntPtr IsFormControlElement;
        public IntPtr GetFormControlElementType;
        public IntPtr IsSame;
        public IntPtr GetName;
        public IntPtr GetValue;
        public IntPtr SetValue;
        public IntPtr GetAsMarkup;
        public IntPtr GetDocument;
        public IntPtr GetParent;
        public IntPtr GetPreviousSibling;
        public IntPtr GetNextSibling;
        public IntPtr HasChildren;
        public IntPtr GetFirstChild;
        public IntPtr GetLastChild;
        public IntPtr AddEventListener;
        public IntPtr GetElementTagName;
        public IntPtr HasElementAttributes;
        public IntPtr HasElementAttribute;
        public IntPtr GetElementAttribute;
        public IntPtr GetElementAttributes;
        public IntPtr SetElementAttribute;
        public IntPtr GetElementInnerText;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct CefDomevent {
        public CefBase Base;
        public IntPtr GetElementType;
        public IntPtr GetCategory;
        public IntPtr GetPhase;
        public IntPtr CanBubble;
        public IntPtr CanCancel;
        public IntPtr GetDocument;
        public IntPtr GetTarget;
        public IntPtr GetCurrentTarget;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct CefDomeventListener {
        public CefBase Base;
        public IntPtr HandleEvent;
    }

    [SuppressUnmanagedCodeSecurity]
    public static class CefDomCapiDelegates {
        public delegate void VisitCallback2(IntPtr self, IntPtr document);

        public delegate CefDomDocumentType GetTypeCallback(IntPtr self);

        public delegate IntPtr GetDocumentCallback(IntPtr self);

        public delegate IntPtr GetBodyCallback(IntPtr self);

        public delegate IntPtr GetHeadCallback(IntPtr self);

        public delegate IntPtr GetTitleCallback(IntPtr self);

        public delegate IntPtr GetElementByIdCallback(IntPtr self, IntPtr id);

        public delegate IntPtr GetFocusedNodeCallback(IntPtr self);

        public delegate int HasSelectionCallback(IntPtr self);

        public delegate IntPtr GetSelectionStartNodeCallback(IntPtr self);

        public delegate int GetSelectionStartOffsetCallback(IntPtr self);

        public delegate IntPtr GetSelectionEndNodeCallback(IntPtr self);

        public delegate int GetSelectionEndOffsetCallback(IntPtr self);

        public delegate IntPtr GetSelectionAsMarkupCallback(IntPtr self);

        public delegate IntPtr GetSelectionAsTextCallback(IntPtr self);

        public delegate IntPtr GetBaseUrlCallback(IntPtr self);

        public delegate IntPtr GetCompleteUrlCallback(IntPtr self, IntPtr partialurl);

        public delegate CefDomNodeType GetTypeCallback2(IntPtr self);

        public delegate int IsTextCallback(IntPtr self);

        public delegate int IsElementCallback(IntPtr self);

        public delegate int IsEditableCallback2(IntPtr self);

        public delegate int IsFormControlElementCallback(IntPtr self);

        public delegate IntPtr GetFormControlElementTypeCallback(IntPtr self);

        public delegate int IsSameCallback(IntPtr self, IntPtr that);

        public delegate IntPtr GetNameCallback(IntPtr self);

        public delegate IntPtr GetValueCallback(IntPtr self);

        public delegate int SetValueCallback(IntPtr self, IntPtr value);

        public delegate IntPtr GetAsMarkupCallback(IntPtr self);

        public delegate IntPtr GetDocumentCallback2(IntPtr self);

        public delegate IntPtr GetParentCallback(IntPtr self);

        public delegate IntPtr GetPreviousSiblingCallback(IntPtr self);

        public delegate IntPtr GetNextSiblingCallback(IntPtr self);

        public delegate int HasChildrenCallback(IntPtr self);

        public delegate IntPtr GetFirstChildCallback(IntPtr self);

        public delegate IntPtr GetLastChildCallback(IntPtr self);

        public delegate void AddEventListenerCallback(IntPtr self, IntPtr eventtype, IntPtr listener, int usecapture);

        public delegate IntPtr GetElementTagNameCallback(IntPtr self);

        public delegate int HasElementAttributesCallback(IntPtr self);

        public delegate int HasElementAttributeCallback(IntPtr self, IntPtr attrname);

        public delegate IntPtr GetElementAttributeCallback(IntPtr self, IntPtr attrname);

        public delegate void GetElementAttributesCallback(IntPtr self, IntPtr attrmap);

        public delegate int SetElementAttributeCallback(IntPtr self, IntPtr attrname, IntPtr value);

        public delegate IntPtr GetElementInnerTextCallback(IntPtr self);

        public delegate IntPtr GetTypeCallback3(IntPtr self);

        public delegate CefDomEventCategory GetCategoryCallback(IntPtr self);

        public delegate CefDomEventPhase GetPhaseCallback(IntPtr self);

        public delegate int CanBubbleCallback(IntPtr self);

        public delegate int CanCancelCallback(IntPtr self);

        public delegate IntPtr GetDocumentCallback3(IntPtr self);

        public delegate IntPtr GetTargetCallback(IntPtr self);

        public delegate IntPtr GetCurrentTargetCallback(IntPtr self);

        public delegate void HandleEventCallback(IntPtr self, IntPtr @event);
    }
}
