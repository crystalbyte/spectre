using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Security;
using Crystalbyte.Spectre.Projections.Internal;

namespace Crystalbyte.Spectre.Projections
{
	[StructLayout(LayoutKind.Sequential)]
	public struct CefMenuModel {
		public CefBase Base;
		public IntPtr Clear;
		public IntPtr GetCount;
		public IntPtr AddSeparator;
		public IntPtr AddItem;
		public IntPtr AddCheckItem;
		public IntPtr AddRadioItem;
		public IntPtr AddSubMenu;
		public IntPtr InsertSeparatorAt;
		public IntPtr InsertItemAt;
		public IntPtr InsertCheckItemAt;
		public IntPtr InsertRadioItemAt;
		public IntPtr InsertSubMenuAt;
		public IntPtr Remove;
		public IntPtr RemoveAt;
		public IntPtr GetIndexOf;
		public IntPtr GetCommandIdAt;
		public IntPtr SetCommandIdAt;
		public IntPtr GetLabel;
		public IntPtr GetLabelAt;
		public IntPtr SetLabel;
		public IntPtr SetLabelAt;
		public IntPtr GetElementType;
		public IntPtr GetTypeAt;
		public IntPtr GetGroupId;
		public IntPtr GetGroupIdAt;
		public IntPtr SetGroupId;
		public IntPtr SetGroupIdAt;
		public IntPtr GetSubMenu;
		public IntPtr GetSubMenuAt;
		public IntPtr IsVisible;
		public IntPtr IsVisibleAt;
		public IntPtr SetVisible;
		public IntPtr SetVisibleAt;
		public IntPtr IsEnabled;
		public IntPtr IsEnabledAt;
		public IntPtr SetEnabled;
		public IntPtr SetEnabledAt;
		public IntPtr IsChecked;
		public IntPtr IsCheckedAt;
		public IntPtr SetChecked;
		public IntPtr SetCheckedAt;
		public IntPtr HasAccelerator;
		public IntPtr HasAcceleratorAt;
		public IntPtr SetAccelerator;
		public IntPtr SetAcceleratorAt;
		public IntPtr RemoveAccelerator;
		public IntPtr RemoveAcceleratorAt;
		public IntPtr GetAccelerator;
		public IntPtr GetAcceleratorAt;
	}
	
	[SuppressUnmanagedCodeSecurity]
	public static class CefMenuModelCapiDelegates {
		public delegate int ClearCallback(IntPtr self);
		public delegate int GetCountCallback(IntPtr self);
		public delegate int AddSeparatorCallback(IntPtr self);
		public delegate int AddItemCallback(IntPtr self, int commandId, IntPtr label);
		public delegate int AddCheckItemCallback(IntPtr self, int commandId, IntPtr label);
		public delegate int AddRadioItemCallback(IntPtr self, int commandId, IntPtr label, int groupId);
		public delegate IntPtr AddSubMenuCallback(IntPtr self, int commandId, IntPtr label);
		public delegate int InsertSeparatorAtCallback(IntPtr self, int index);
		public delegate int InsertItemAtCallback(IntPtr self, int index, int commandId, IntPtr label);
		public delegate int InsertCheckItemAtCallback(IntPtr self, int index, int commandId, IntPtr label);
		public delegate int InsertRadioItemAtCallback(IntPtr self, int index, int commandId, IntPtr label, int groupId);
		public delegate IntPtr InsertSubMenuAtCallback(IntPtr self, int index, int commandId, IntPtr label);
		public delegate int RemoveCallback(IntPtr self, int commandId);
		public delegate int RemoveAtCallback(IntPtr self, int index);
		public delegate int GetIndexOfCallback(IntPtr self, int commandId);
		public delegate int GetCommandIdAtCallback(IntPtr self, int index);
		public delegate int SetCommandIdAtCallback(IntPtr self, int index, int commandId);
		public delegate IntPtr GetLabelCallback(IntPtr self, int commandId);
		public delegate IntPtr GetLabelAtCallback(IntPtr self, int index);
		public delegate int SetLabelCallback(IntPtr self, int commandId, IntPtr label);
		public delegate int SetLabelAtCallback(IntPtr self, int index, IntPtr label);
		public delegate CefMenuItemType GetTypeCallback4(IntPtr self, int commandId);
		public delegate CefMenuItemType GetTypeAtCallback(IntPtr self, int index);
		public delegate int GetGroupIdCallback(IntPtr self, int commandId);
		public delegate int GetGroupIdAtCallback(IntPtr self, int index);
		public delegate int SetGroupIdCallback(IntPtr self, int commandId, int groupId);
		public delegate int SetGroupIdAtCallback(IntPtr self, int index, int groupId);
		public delegate IntPtr GetSubMenuCallback(IntPtr self, int commandId);
		public delegate IntPtr GetSubMenuAtCallback(IntPtr self, int index);
		public delegate int IsVisibleCallback(IntPtr self, int commandId);
		public delegate int IsVisibleAtCallback(IntPtr self, int index);
		public delegate int SetVisibleCallback(IntPtr self, int commandId, int visible);
		public delegate int SetVisibleAtCallback(IntPtr self, int index, int visible);
		public delegate int IsEnabledCallback(IntPtr self, int commandId);
		public delegate int IsEnabledAtCallback(IntPtr self, int index);
		public delegate int SetEnabledCallback(IntPtr self, int commandId, int enabled);
		public delegate int SetEnabledAtCallback(IntPtr self, int index, int enabled);
		public delegate int IsCheckedCallback(IntPtr self, int commandId);
		public delegate int IsCheckedAtCallback(IntPtr self, int index);
		public delegate int SetCheckedCallback(IntPtr self, int commandId, int @checked);
		public delegate int SetCheckedAtCallback(IntPtr self, int index, int @checked);
		public delegate int HasAcceleratorCallback(IntPtr self, int commandId);
		public delegate int HasAcceleratorAtCallback(IntPtr self, int index);
		public delegate int SetAcceleratorCallback(IntPtr self, int commandId, int keyCode, int shiftPressed, int ctrlPressed, int altPressed);
		public delegate int SetAcceleratorAtCallback(IntPtr self, int index, int keyCode, int shiftPressed, int ctrlPressed, int altPressed);
		public delegate int RemoveAcceleratorCallback(IntPtr self, int commandId);
		public delegate int RemoveAcceleratorAtCallback(IntPtr self, int index);
		public delegate int GetAcceleratorCallback(IntPtr self, int commandId, ref int keyCode, ref int shiftPressed, ref int ctrlPressed, ref int altPressed);
		public delegate int GetAcceleratorAtCallback(IntPtr self, int index, ref int keyCode, ref int shiftPressed, ref int ctrlPressed, ref int altPressed);
	}
	
}
