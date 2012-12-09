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

namespace Crystalbyte.Spectre {
    public enum ErrorCode {
        ErrFailed = -2,
        ErrAborted = -3,
        ErrInvalidArgument = -4,
        ErrInvalidHandle = -5,
        ErrFileNotFound = -6,
        ErrTimedOut = -7,
        ErrFileTooBig = -8,
        ErrUnexpected = -9,
        ErrAccessDenied = -10,
        ErrNotImplemented = -11,
        ErrConnectionClosed = -100,
        ErrConnectionReset = -101,
        ErrConnectionRefused = -102,
        ErrConnectionAborted = -103,
        ErrConnectionFailed = -104,
        ErrNameNotResolved = -105,
        ErrInternetDisconnected = -106,
        ErrSslProtocolError = -107,
        ErrAddressInvalid = -108,
        ErrAddressUnreachable = -109,
        ErrSslClientAuthCertNeeded = -110,
        ErrTunnelConnectionFailed = -111,
        ErrNoSslVersionsEnabled = -112,
        ErrSslVersionOrCipherMismatch = -113,
        ErrSslRenegotiationRequested = -114,
        ErrCertCommonNameInvalid = -200,
        ErrCertDateInvalid = -201,
        ErrCertAuthorityInvalid = -202,
        ErrCertContainsErrors = -203,
        ErrCertNoRevocationMechanism = -204,
        ErrCertUnableToCheckRevocation = -205,
        ErrCertRevoked = -206,
        ErrCertInvalid = -207,
        ErrCertEnd = -208,
        ErrInvalidUrl = -300,
        ErrDisallowedUrlScheme = -301,
        ErrUnknownUrlScheme = -302,
        ErrTooManyRedirects = -310,
        ErrUnsafeRedirect = -311,
        ErrUnsafePort = -312,
        ErrInvalidResponse = -320,
        ErrInvalidChunkedEncoding = -321,
        ErrMethodNotSupported = -322,
        ErrUnexpectedProxyAuth = -323,
        ErrEmptyResponse = -324,
        ErrResponseHeadersTooBig = -325,
        ErrCacheMiss = -400,
        ErrInsecureResponse = -501
    }
}
