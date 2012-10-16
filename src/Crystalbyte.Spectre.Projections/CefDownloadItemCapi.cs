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
    public struct CefDownloadItem {
        public CefBase Base;
        public IntPtr IsValid;
        public IntPtr IsInProgress;
        public IntPtr IsComplete;
        public IntPtr IsCanceled;
        public IntPtr GetCurrentSpeed;
        public IntPtr GetPercentComplete;
        public IntPtr GetTotalBytes;
        public IntPtr GetReceivedBytes;
        public IntPtr GetStartTime;
        public IntPtr GetEndTime;
        public IntPtr GetFullPath;
        public IntPtr GetId;
        public IntPtr GetUrl;
        public IntPtr GetSuggestedFileName;
        public IntPtr GetContentDisposition;
        public IntPtr GetMimeType;
        public IntPtr GetReferrerCharset;
    }

    public delegate int IsInProgressCallback(IntPtr self);

    public delegate int IsCompleteCallback(IntPtr self);

    public delegate int IsCanceledCallback(IntPtr self);

    public delegate long GetCurrentSpeedCallback(IntPtr self);

    public delegate int GetPercentCompleteCallback(IntPtr self);

    public delegate long GetTotalBytesCallback(IntPtr self);

    public delegate long GetReceivedBytesCallback(IntPtr self);

    public delegate IntPtr GetStartTimeCallback(IntPtr self);

    public delegate IntPtr GetEndTimeCallback(IntPtr self);

    public delegate IntPtr GetFullPathCallback(IntPtr self);

    public delegate int GetIdCallback(IntPtr self);

    public delegate IntPtr GetUrlCallback(IntPtr self);

    public delegate IntPtr GetSuggestedFileNameCallback(IntPtr self);

    public delegate IntPtr GetContentDispositionCallback(IntPtr self);

    public delegate IntPtr GetMimeTypeCallback(IntPtr self);

    public delegate IntPtr GetReferrerCharsetCallback(IntPtr self);
}
