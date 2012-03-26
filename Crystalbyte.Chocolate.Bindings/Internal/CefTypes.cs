using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Security;
using Crystalbyte.Chocolate.Bindings.Internal;

namespace Crystalbyte.Chocolate.Bindings.Internal
{
	[StructLayout(LayoutKind.Sequential)]
	public struct CefSettings {
		public int Size;
		public bool SingleProcess;
		public CefStringUtf8 BrowserSubprocessPath;
		public bool MultiThreadedMessageLoop;
		public bool CommandLineArgsDisabled;
		public CefStringUtf8 CachePath;
		public CefStringUtf8 UserAgent;
		public CefStringUtf8 ProductVersion;
		public CefStringUtf8 Locale;
		public IntPtr ExtraPluginPaths;
		public CefStringUtf8 LogFile;
		public CefLogSeverity LogSeverity;
		public CefGraphicsImplementation GraphicsImplementation;
		public CefStringUtf8 JavascriptFlags;
		public bool AutoDetectProxySettingsEnabled;
		public CefStringUtf8 PackFilePath;
		public CefStringUtf8 LocalesDirPath;
		public bool PackLoadingDisabled;
	}
	
	[StructLayout(LayoutKind.Sequential)]
	public struct CefUrlparts {
		public CefStringUtf8 Spec;
		public CefStringUtf8 Scheme;
		public CefStringUtf8 Username;
		public CefStringUtf8 Password;
		public CefStringUtf8 Host;
		public CefStringUtf8 Port;
		public CefStringUtf8 Path;
		public CefStringUtf8 Query;
	}
	
	[StructLayout(LayoutKind.Sequential)]
	public struct CefCookie {
		public CefStringUtf8 Name;
		public CefStringUtf8 Value;
		public CefStringUtf8 Domain;
		public CefStringUtf8 Path;
		public bool Secure;
		public bool Httponly;
		public CefTime Creation;
		public CefTime LastAccess;
		public bool HasExpires;
		public CefTime Expires;
	}
	
	[StructLayout(LayoutKind.Sequential)]
	public struct CefRect {
		public int X;
		public int Y;
		public int Width;
		public int Height;
	}
	
	[StructLayout(LayoutKind.Sequential)]
	public struct CefPopupFeatures {
		public int X;
		public bool Xset;
		public int Y;
		public bool Yset;
		public int Width;
		public bool Widthset;
		public int Height;
		public bool Heightset;
		public bool Menubarvisible;
		public bool Statusbarvisible;
		public bool Toolbarvisible;
		public bool Locationbarvisible;
		public bool Scrollbarsvisible;
		public bool Resizable;
		public bool Fullscreen;
		public bool Dialog;
		public IntPtr Additionalfeatures;
	}
	
	[StructLayout(LayoutKind.Sequential)]
	public struct CefProxyInfo {
		public CefStringUtf8 Proxylist;
	}
	
	
	public enum CefLogSeverity {
		LogseverityVerbose = -1,
		LogseverityInfo,
		LogseverityWarning,
		LogseverityError,
		LogseverityErrorReport,
		LogseverityDisable = 99,
	}
	public enum CefStorageType {
		StLocalstorage = 0,
		StSessionstorage,
	}
	public enum CefHandlerErrorcode {
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
	public enum CefPostdataelementType {
		PdeTypeEmpty = 0,
		PdeTypeBytes,
		PdeTypeFile,
	}
	public enum CefWeburlrequestFlags {
		WurFlagNone = 0,
		WurFlagSkipCache = 0x1,
		WurFlagAllowCachedCredentials = 0x2,
		WurFlagAllowCookies = 0x4,
		WurFlagReportUploadProgress = 0x8,
		WurFlagReportLoadTiming = 0x10,
		WurFlagReportRawHeaders = 0x20,
	}
	public enum CefThreadId {
		TidUi,
		TidDb,
		TidFile,
		TidFileUserBlocking,
		TidProcessLauncher,
		TidCache,
		TidIo,
	}
	public enum CefXmlEncodingType {
		XmlEncodingNone = 0,
		XmlEncodingUtf8,
		XmlEncodingUtf16le,
		XmlEncodingUtf16be,
		XmlEncodingAscii,
	}
	public enum CefXmlNodeType {
		XmlNodeUnsupported = 0,
		XmlNodeProcessingInstruction,
		XmlNodeDocumentType,
		XmlNodeElementStart,
		XmlNodeElementEnd,
		XmlNodeAttribute,
		XmlNodeText,
		XmlNodeCdata,
		XmlNodeEntityReference,
		XmlNodeWhitespace,
		XmlNodeComment,
	}
	public enum CefHandlerStatustype {
		StatustypeText = 0,
		StatustypeMouseoverUrl,
		StatustypeKeyboardFocusUrl,
	}
	public enum CefProxyType {
		ProxyTypeDirect = 0,
		ProxyTypeNamed,
		ProxyTypePacString,
	}
	
}
