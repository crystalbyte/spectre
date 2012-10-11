#region Using directives

using System;

#endregion

namespace Crystalbyte.Spectre{
    [Flags]
    public enum UrlRequestFlags{
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