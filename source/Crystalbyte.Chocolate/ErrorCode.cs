#region Copyright notice

// Copyright (C) 2012 Alexander Wieser-Kuciel <alexander.wieser@crystalbyte.de>
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

#endregion

namespace Crystalbyte.Chocolate {
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
        ErrInsecureResponse = -501,
    }
}