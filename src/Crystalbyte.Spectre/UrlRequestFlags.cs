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

#endregion

namespace Crystalbyte.Spectre {
    [Flags]
    public enum UrlRequestFlags {
        None = 0,
        SkipCache = 1 << 0,
        AllowCachedCredentials = 1 << 1,
        AllowCookies = 1 << 2,
        ReportUploadProgress = 1 << 3,
        ReportLoadTiming = 1 << 4,
        ReportRawHeaders = 1 << 5,
        NoDownloadData = 1 << 6,
        NoRetryOn5XX = 1 << 7
    }
}
