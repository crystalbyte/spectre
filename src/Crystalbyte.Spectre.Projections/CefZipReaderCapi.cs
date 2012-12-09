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
    [SuppressUnmanagedCodeSecurity]
    public static class CefZipReaderCapi {
        [DllImport(CefAssembly.Name, EntryPoint = "cef_zip_reader_create", CallingConvention = CallingConvention.Cdecl,
            CharSet = CharSet.Unicode)]
        public static extern IntPtr CefZipReaderCreate(IntPtr stream);
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct CefZipReader {
        public CefBase Base;
        public IntPtr MoveToFirstFile;
        public IntPtr MoveToNextFile;
        public IntPtr MoveToFile;
        public IntPtr Close;
        public IntPtr GetFileName;
        public IntPtr GetFileSize;
        public IntPtr GetFileLastModified;
        public IntPtr OpenFile;
        public IntPtr CloseFile;
        public IntPtr ReadFile;
        public IntPtr Tell;
        public IntPtr Eof;
    }

    [SuppressUnmanagedCodeSecurity]
    public static class CefZipReaderCapiDelegates {
        public delegate int MoveToFirstFileCallback(IntPtr self);

        public delegate int MoveToNextFileCallback(IntPtr self);

        public delegate int MoveToFileCallback(IntPtr self, IntPtr filename, int casesensitive);

        public delegate int CloseCallback2(IntPtr self);

        public delegate IntPtr GetFileNameCallback(IntPtr self);

        public delegate long GetFileSizeCallback(IntPtr self);

        public delegate long GetFileLastModifiedCallback(IntPtr self);

        public delegate int OpenFileCallback(IntPtr self, IntPtr password);

        public delegate int CloseFileCallback(IntPtr self);

        public delegate int ReadFileCallback(IntPtr self, IntPtr buffer, int buffersize);

        public delegate long TellCallback5(IntPtr self);

        public delegate int EofCallback3(IntPtr self);
    }
}
