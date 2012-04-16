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
		public CefStringUtf16 BrowserSubprocessPath;
		public bool MultiThreadedMessageLoop;
		public bool CommandLineArgsDisabled;
		public CefStringUtf16 CachePath;
		public CefStringUtf16 UserAgent;
		public CefStringUtf16 ProductVersion;
		public CefStringUtf16 Locale;
		public IntPtr ExtraPluginPaths;
		public CefStringUtf16 LogFile;
		public CefLogSeverity LogSeverity;
		public int GraphicsImplementation;
		public CefStringUtf16 JavascriptFlags;
		public bool AutoDetectProxySettingsEnabled;
		public CefStringUtf16 PackFilePath;
		public CefStringUtf16 LocalesDirPath;
		public bool PackLoadingDisabled;
	}
	
	[StructLayout(LayoutKind.Sequential)]
	public struct CefBrowserSettings {
		public int Size;
		public bool DragDropDisabled;
		public bool LoadDropsDisabled;
		public bool HistoryDisabled;
		public CefStringUtf16 StandardFontFamily;
		public CefStringUtf16 FixedFontFamily;
		public CefStringUtf16 SerifFontFamily;
		public CefStringUtf16 SansSerifFontFamily;
		public CefStringUtf16 CursiveFontFamily;
		public CefStringUtf16 FantasyFontFamily;
		public int DefaultFontSize;
		public int DefaultFixedFontSize;
		public int MinimumFontSize;
		public int MinimumLogicalFontSize;
		public bool RemoteFontsDisabled;
		public CefStringUtf16 DefaultEncoding;
		public bool EncodingDetectorEnabled;
		public bool JavascriptDisabled;
		public bool JavascriptOpenWindowsDisallowed;
		public bool JavascriptCloseWindowsDisallowed;
		public bool JavascriptAccessClipboardDisallowed;
		public bool DomPasteDisabled;
		public bool CaretBrowsingEnabled;
		public bool JavaDisabled;
		public bool PluginsDisabled;
		public bool UniversalAccessFromFileUrlsAllowed;
		public bool FileAccessFromFileUrlsAllowed;
		public bool WebSecurityDisabled;
		public bool XssAuditorEnabled;
		public bool ImageLoadDisabled;
		public bool ShrinkStandaloneImagesToFit;
		public bool SiteSpecificQuirksDisabled;
		public bool TextAreaResizeDisabled;
		public bool PageCacheDisabled;
		public bool TabToLinksDisabled;
		public bool HyperlinkAuditingDisabled;
		public bool UserStyleSheetEnabled;
		public CefStringUtf16 UserStyleSheetLocation;
		public bool AuthorAndUserStylesDisabled;
		public bool LocalStorageDisabled;
		public bool DatabasesDisabled;
		public bool ApplicationCacheDisabled;
		public bool WebglDisabled;
		public bool AcceleratedCompositingDisabled;
		public bool AcceleratedLayersDisabled;
		public bool AcceleratedVideoDisabled;
		public bool Accelerated2DCanvasDisabled;
		public bool AcceleratedPaintingEnabled;
		public bool AcceleratedFiltersEnabled;
		public bool AcceleratedPluginsDisabled;
		public bool DeveloperToolsDisabled;
		public bool FullscreenEnabled;
	}
	
	[StructLayout(LayoutKind.Sequential)]
	public struct CefUrlparts {
		public CefStringUtf16 Spec;
		public CefStringUtf16 Scheme;
		public CefStringUtf16 Username;
		public CefStringUtf16 Password;
		public CefStringUtf16 Host;
		public CefStringUtf16 Port;
		public CefStringUtf16 Path;
		public CefStringUtf16 Query;
	}
	
	[StructLayout(LayoutKind.Sequential)]
	public struct CefCookie {
		public CefStringUtf16 Name;
		public CefStringUtf16 Value;
		public CefStringUtf16 Domain;
		public CefStringUtf16 Path;
		public bool Secure;
		public bool Httponly;
		public IntPtr Creation;
		public IntPtr LastAccess;
		public bool HasExpires;
		public IntPtr Expires;
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
		public CefStringUtf16 Proxylist;
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
	public enum CefV8Accesscontrol {
		V8AccessControlDefault = 0,
		V8AccessControlAllCanRead = 1,
		V8AccessControlAllCanWrite = 1 << 1,
		V8AccessControlProhibitsOverwriting = 1 << 2,
	}
	public enum CefV8Propertyattribute {
		V8PropertyAttributeNone = 0,       // Writeable, Enumerable,
		V8PropertyAttributeReadonly = 1 << 0,  // Not writeable,
		V8PropertyAttributeDontenum = 1 << 1,  // Not enumerable,
		V8PropertyAttributeDontdelete = 1 << 2   // Not configurable,
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
	public enum CefProcessId {
		PidBrowser,
		PidRenderer,
	}
	public enum CefThreadId {
		TidUi,
		TidDb,
		TidFile,
		TidFileUserBlocking,
		TidProcessLauncher,
		TidCache,
		TidIo,
		TidRenderer,
	}
	public enum CefValueType {
		VtypeInvalid = 0,
		VtypeNull,
		VtypeBool,
		VtypeInt,
		VtypeDouble,
		VtypeString,
		VtypeBinary,
		VtypeDictionary,
		VtypeList,
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
