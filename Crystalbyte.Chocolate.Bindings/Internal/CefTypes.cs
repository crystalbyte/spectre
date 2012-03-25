using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Security;
using Crystalbyte.Chocolate.Bindings.Internal;

namespace Crystalbyte.Chocolate.Bindings.Internal
{
	[StructLayout(LayoutKind.Sequential)]
	public struct CefSettings {
		int Size;
		bool SingleProcess;
		CefStringUtf8 BrowserSubprocessPath;
		bool MultiThreadedMessageLoop;
		bool CommandLineArgsDisabled;
		CefStringUtf8 CachePath;
		CefStringUtf8 UserAgent;
		CefStringUtf8 ProductVersion;
		CefStringUtf8 Locale;
		IntPtr ExtraPluginPaths;
		CefStringUtf8 LogFile;
		CefLogSeverity LogSeverity;
		CefGraphicsImplementation GraphicsImplementation;
		CefStringUtf8 JavascriptFlags;
		bool AutoDetectProxySettingsEnabled;
		CefStringUtf8 PackFilePath;
		CefStringUtf8 LocalesDirPath;
		bool PackLoadingDisabled;
	}
	
	[StructLayout(LayoutKind.Sequential)]
	public struct CefUrlparts {
		CefStringUtf8 Spec;
		CefStringUtf8 Scheme;
		CefStringUtf8 Username;
		CefStringUtf8 Password;
		CefStringUtf8 Host;
		CefStringUtf8 Port;
		CefStringUtf8 Path;
		CefStringUtf8 Query;
	}
	
	[StructLayout(LayoutKind.Sequential)]
	public struct CefCookie {
		CefStringUtf8 Name;
		CefStringUtf8 Value;
		CefStringUtf8 Domain;
		CefStringUtf8 Path;
		bool Secure;
		bool Httponly;
		CefTime Creation;
		CefTime LastAccess;
		bool HasExpires;
		CefTime Expires;
	}
	
	[StructLayout(LayoutKind.Sequential)]
	public struct CefRect {
		int X;
		int Y;
		int Width;
		int Height;
	}
	
	[StructLayout(LayoutKind.Sequential)]
	public struct CefPopupFeatures {
		int X;
		bool Xset;
		int Y;
		bool Yset;
		int Width;
		bool Widthset;
		int Height;
		bool Heightset;
		bool Menubarvisible;
		bool Statusbarvisible;
		bool Toolbarvisible;
		bool Locationbarvisible;
		bool Scrollbarsvisible;
		bool Resizable;
		bool Fullscreen;
		bool Dialog;
		IntPtr Additionalfeatures;
	}
	
	[StructLayout(LayoutKind.Sequential)]
	public struct CefProxyInfo {
		CefStringUtf8 Proxylist;
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
