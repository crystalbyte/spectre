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

namespace Crystalbyte.Spectre.Projections.Internal {
    [StructLayout(LayoutKind.Sequential)]
    public struct CefSettings {
        public int Size;

        [MarshalAs(UnmanagedType.U1)]
        public bool SingleProcess;

        public CefStringUtf16 BrowserSubprocessPath;

        [MarshalAs(UnmanagedType.U1)]
        public bool MultiThreadedMessageLoop;

        [MarshalAs(UnmanagedType.U1)]
        public bool CommandLineArgsDisabled;

        public CefStringUtf16 CachePath;
        public CefStringUtf16 UserAgent;
        public CefStringUtf16 ProductVersion;
        public CefStringUtf16 Locale;
        public CefStringUtf16 LogFile;
        public CefLogSeverity LogSeverity;
        public CefStringUtf16 JavascriptFlags;

        [MarshalAs(UnmanagedType.U1)]
        public bool AutoDetectProxySettingsEnabled;

        public CefStringUtf16 ResourcesDirPath;
        public CefStringUtf16 LocalesDirPath;

        [MarshalAs(UnmanagedType.U1)]
        public bool PackLoadingDisabled;

        public int RemoteDebuggingPort;
        public int UncaughtExceptionStackSize;
        public int ContextSafetyImplementation;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct CefBrowserSettings {
        public int Size;
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

        [MarshalAs(UnmanagedType.U1)]
        public bool RemoteFontsDisabled;

        public CefStringUtf16 DefaultEncoding;

        [MarshalAs(UnmanagedType.U1)]
        public bool EncodingDetectorEnabled;

        [MarshalAs(UnmanagedType.U1)]
        public bool JavascriptDisabled;

        [MarshalAs(UnmanagedType.U1)]
        public bool JavascriptOpenWindowsDisallowed;

        [MarshalAs(UnmanagedType.U1)]
        public bool JavascriptCloseWindowsDisallowed;

        [MarshalAs(UnmanagedType.U1)]
        public bool JavascriptAccessClipboardDisallowed;

        [MarshalAs(UnmanagedType.U1)]
        public bool DomPasteDisabled;

        [MarshalAs(UnmanagedType.U1)]
        public bool CaretBrowsingEnabled;

        [MarshalAs(UnmanagedType.U1)]
        public bool JavaDisabled;

        [MarshalAs(UnmanagedType.U1)]
        public bool PluginsDisabled;

        [MarshalAs(UnmanagedType.U1)]
        public bool UniversalAccessFromFileUrlsAllowed;

        [MarshalAs(UnmanagedType.U1)]
        public bool FileAccessFromFileUrlsAllowed;

        [MarshalAs(UnmanagedType.U1)]
        public bool WebSecurityDisabled;

        [MarshalAs(UnmanagedType.U1)]
        public bool XssAuditorEnabled;

        [MarshalAs(UnmanagedType.U1)]
        public bool ImageLoadDisabled;

        [MarshalAs(UnmanagedType.U1)]
        public bool ShrinkStandaloneImagesToFit;

        [MarshalAs(UnmanagedType.U1)]
        public bool SiteSpecificQuirksDisabled;

        [MarshalAs(UnmanagedType.U1)]
        public bool TextAreaResizeDisabled;

        [MarshalAs(UnmanagedType.U1)]
        public bool PageCacheDisabled;

        [MarshalAs(UnmanagedType.U1)]
        public bool TabToLinksDisabled;

        [MarshalAs(UnmanagedType.U1)]
        public bool HyperlinkAuditingDisabled;

        [MarshalAs(UnmanagedType.U1)]
        public bool UserStyleSheetEnabled;

        public CefStringUtf16 UserStyleSheetLocation;

        [MarshalAs(UnmanagedType.U1)]
        public bool AuthorAndUserStylesDisabled;

        [MarshalAs(UnmanagedType.U1)]
        public bool LocalStorageDisabled;

        [MarshalAs(UnmanagedType.U1)]
        public bool DatabasesDisabled;

        [MarshalAs(UnmanagedType.U1)]
        public bool ApplicationCacheDisabled;

        [MarshalAs(UnmanagedType.U1)]
        public bool WebglDisabled;

        [MarshalAs(UnmanagedType.U1)]
        public bool AcceleratedCompositingDisabled;

        [MarshalAs(UnmanagedType.U1)]
        public bool AcceleratedLayersDisabled;

        [MarshalAs(UnmanagedType.U1)]
        public bool AcceleratedVideoDisabled;

        [MarshalAs(UnmanagedType.U1)]
        public bool Accelerated2DCanvasDisabled;

        [MarshalAs(UnmanagedType.U1)]
        public bool AcceleratedPaintingEnabled;

        [MarshalAs(UnmanagedType.U1)]
        public bool AcceleratedFiltersEnabled;

        [MarshalAs(UnmanagedType.U1)]
        public bool AcceleratedPluginsDisabled;

        [MarshalAs(UnmanagedType.U1)]
        public bool DeveloperToolsDisabled;

        [MarshalAs(UnmanagedType.U1)]
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

        [MarshalAs(UnmanagedType.U1)]
        public bool Secure;

        [MarshalAs(UnmanagedType.U1)]
        public bool Httponly;

        public IntPtr Creation;
        public IntPtr LastAccess;

        [MarshalAs(UnmanagedType.U1)]
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
    public struct CefKeyEvent {
        public CefKeyEventType Type;
        public int Modifiers;
        public int WindowsKeyCode;
        public int NativeKeyCode;

        [MarshalAs(UnmanagedType.U1)]
        public bool IsSystemKey;

        public char Character;
        public char UnmodifiedCharacter;

        [MarshalAs(UnmanagedType.U1)]
        public bool FocusOnEditableField;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct CefPopupFeatures {
        public int X;

        [MarshalAs(UnmanagedType.U1)]
        public bool Xset;

        public int Y;

        [MarshalAs(UnmanagedType.U1)]
        public bool Yset;

        public int Width;

        [MarshalAs(UnmanagedType.U1)]
        public bool Widthset;

        public int Height;

        [MarshalAs(UnmanagedType.U1)]
        public bool Heightset;

        [MarshalAs(UnmanagedType.U1)]
        public bool Menubarvisible;

        [MarshalAs(UnmanagedType.U1)]
        public bool Statusbarvisible;

        [MarshalAs(UnmanagedType.U1)]
        public bool Toolbarvisible;

        [MarshalAs(UnmanagedType.U1)]
        public bool Locationbarvisible;

        [MarshalAs(UnmanagedType.U1)]
        public bool Scrollbarsvisible;

        [MarshalAs(UnmanagedType.U1)]
        public bool Resizable;

        [MarshalAs(UnmanagedType.U1)]
        public bool Fullscreen;

        [MarshalAs(UnmanagedType.U1)]
        public bool Dialog;

        public IntPtr Additionalfeatures;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct CefProxyInfo {
        public CefStringUtf16 Proxylist;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct CefGeoposition {
        public Double Latitude;
        public Double Longitude;
        public Double Altitude;
        public Double Accuracy;
        public Double AltitudeAccuracy;
        public Double Heading;
        public Double Speed;
        public IntPtr Timestamp;
        public CefGeopositionErrorCode ErrorCode;
        public CefStringUtf16 ErrorMessage;
    }

    [SuppressUnmanagedCodeSecurity]
    public static class CefTypesDelegates {}

    public enum CefLogSeverity {
        LogseverityDefault,
        LogseverityVerbose,
        LogseverityInfo,
        LogseverityWarning,
        LogseverityError,
        LogseverityErrorReport,
        LogseverityDisable = 99,
    }

    public enum CefTerminationStatus {
        TsAbnormalTermination,
        TsProcessWasKilled,
        TsProcessCrashed,
    }

    public enum CefPathKey {
        PkDirCurrent,
        PkDirExe,
        PkDirModule,
        PkDirTemp,
        PkFileExe,
        PkFileModule,
    }

    public enum CefStorageType {
        StLocalstorage = 0,
        StSessionstorage,
    }

    public enum CefErrorcode {
        ErrNone = 0,
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
        V8PropertyAttributeNone = 0, // Writeable, Enumerable,
        V8PropertyAttributeReadonly = 1 << 0, // Not writeable,
        V8PropertyAttributeDontenum = 1 << 1, // Not enumerable,
        V8PropertyAttributeDontdelete = 1 << 2 // Not configurable,
    }

    public enum CefPostdataelementType {
        PdeTypeEmpty = 0,
        PdeTypeBytes,
        PdeTypeFile,
    }

    public enum CefUrlrequestFlags {
        UrFlagNone = 0,
        UrFlagSkipCache = 1 << 0,
        UrFlagAllowCachedCredentials = 1 << 1,
        UrFlagAllowCookies = 1 << 2,
        UrFlagReportUploadProgress = 1 << 3,
        UrFlagReportLoadTiming = 1 << 4,
        UrFlagReportRawHeaders = 1 << 5,
        UrFlagNoDownloadData = 1 << 6,
        UrFlagNoRetryOn5Xx = 1 << 7,
    }

    public enum CefUrlrequestStatus {
        UrUnknown = 0,
        UrSuccess,
        UrIoPending,
        UrCanceled,
        UrFailed,
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

    public enum CefJsdialogType {
        JsdialogtypeAlert = 0,
        JsdialogtypeConfirm,
        JsdialogtypePrompt,
    }

    public enum CefMenuId {
        MenuIdBack = 100,
        MenuIdForward = 101,
        MenuIdReload = 102,
        MenuIdReloadNocache = 103,
        MenuIdStopload = 104,
        MenuIdUndo = 110,
        MenuIdRedo = 111,
        MenuIdCut = 112,
        MenuIdCopy = 113,
        MenuIdPaste = 114,
        MenuIdDelete = 115,
        MenuIdSelectAll = 116,
        MenuIdFind = 130,
        MenuIdPrint = 131,
        MenuIdViewSource = 132,
        MenuIdUserFirst = 26500,
        MenuIdUserLast = 28500,
    }

    public enum CefEventFlags {
        EventflagNone = 0,
        EventflagCapsLockDown = 1 << 0,
        EventflagShiftDown = 1 << 1,
        EventflagControlDown = 1 << 2,
        EventflagAltDown = 1 << 3,
        EventflagLeftMouseButton = 1 << 4,
        EventflagMiddleMouseButton = 1 << 5,
        EventflagRightMouseButton = 1 << 6,
        EventflagCommandDown = 1 << 7,
        EventflagExtended = 1 << 8,
    }

    public enum CefMenuItemType {
        MenuitemtypeNone,
        MenuitemtypeCommand,
        MenuitemtypeCheck,
        MenuitemtypeRadio,
        MenuitemtypeSeparator,
        MenuitemtypeSubmenu,
    }

    public enum CefContextMenuTypeFlags {
        CmTypeflagNone = 0,
        CmTypeflagPage = 1 << 0,
        CmTypeflagFrame = 1 << 1,
        CmTypeflagLink = 1 << 2,
        CmTypeflagMedia = 1 << 3,
        CmTypeflagSelection = 1 << 4,
        CmTypeflagEditable = 1 << 5,
    }

    public enum CefContextMenuMediaType {
        CmMediatypeNone,
        CmMediatypeImage,
        CmMediatypeVideo,
        CmMediatypeAudio,
        CmMediatypeFile,
        CmMediatypePlugin,
    }

    public enum CefContextMenuMediaStateFlags {
        CmMediaflagNone = 0,
        CmMediaflagError = 1 << 0,
        CmMediaflagPaused = 1 << 1,
        CmMediaflagMuted = 1 << 2,
        CmMediaflagLoop = 1 << 3,
        CmMediaflagCanSave = 1 << 4,
        CmMediaflagHasAudio = 1 << 5,
        CmMediaflagHasVideo = 1 << 6,
        CmMediaflagControlRootElement = 1 << 7,
        CmMediaflagCanPrint = 1 << 8,
        CmMediaflagCanRotate = 1 << 9,
    }

    public enum CefContextMenuEditStateFlags {
        CmEditflagNone = 0,
        CmEditflagCanUndo = 1 << 0,
        CmEditflagCanRedo = 1 << 1,
        CmEditflagCanCut = 1 << 2,
        CmEditflagCanCopy = 1 << 3,
        CmEditflagCanPaste = 1 << 4,
        CmEditflagCanDelete = 1 << 5,
        CmEditflagCanSelectAll = 1 << 6,
        CmEditflagCanTranslate = 1 << 7,
    }

    public enum CefKeyEventType {
        KeyeventRawkeydown = 0,
        KeyeventKeydown,
        KeyeventKeyup,
        KeyeventChar,
    }

    public enum CefKeyEventModifiers {
        KeyShift = 1 << 0,
        KeyCtrl = 1 << 1,
        KeyAlt = 1 << 2,
        KeyMeta = 1 << 3,
        KeyKeypad = 1 << 4, // Only used on Mac OS-X,
    }

    public enum CefFocusSource {
        FocusSourceNavigation = 0,
        FocusSourceSystem,
    }

    public enum CefNavigationType {
        NavigationLinkClicked = 0,
        NavigationFormSubmitted,
        NavigationBackForward,
        NavigationReload,
        NavigationFormResubmitted,
        NavigationOther,
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

    public enum CefProxyType {
        CefProxyTypeDirect = 0,
        CefProxyTypeNamed,
        CefProxyTypePacString,
    }

    public enum CefDomDocumentType {
        DomDocumentTypeUnknown = 0,
        DomDocumentTypeHtml,
        DomDocumentTypeXhtml,
        DomDocumentTypePlugin,
    }

    public enum CefDomEventCategory {
        DomEventCategoryUnknown = 0x0,
        DomEventCategoryUi = 0x1,
        DomEventCategoryMouse = 0x2,
        DomEventCategoryMutation = 0x4,
        DomEventCategoryKeyboard = 0x8,
        DomEventCategoryText = 0x10,
        DomEventCategoryComposition = 0x20,
        DomEventCategoryDrag = 0x40,
        DomEventCategoryClipboard = 0x80,
        DomEventCategoryMessage = 0x100,
        DomEventCategoryWheel = 0x200,
        DomEventCategoryBeforeTextInserted = 0x400,
        DomEventCategoryOverflow = 0x800,
        DomEventCategoryPageTransition = 0x1000,
        DomEventCategoryPopstate = 0x2000,
        DomEventCategoryProgress = 0x4000,
        DomEventCategoryXmlhttprequestProgress = 0x8000,
        DomEventCategoryWebkitAnimation = 0x10000,
        DomEventCategoryWebkitTransition = 0x20000,
        DomEventCategoryBeforeLoad = 0x40000,
    }

    public enum CefDomEventPhase {
        DomEventPhaseUnknown = 0,
        DomEventPhaseCapturing,
        DomEventPhaseAtTarget,
        DomEventPhaseBubbling,
    }

    public enum CefDomNodeType {
        DomNodeTypeUnsupported = 0,
        DomNodeTypeElement,
        DomNodeTypeAttribute,
        DomNodeTypeText,
        DomNodeTypeCdataSection,
        DomNodeTypeEntityReference,
        DomNodeTypeEntity,
        DomNodeTypeProcessingInstructions,
        DomNodeTypeComment,
        DomNodeTypeDocument,
        DomNodeTypeDocumentType,
        DomNodeTypeDocumentFragment,
        DomNodeTypeNotation,
        DomNodeTypeXpathNamespace,
    }

    public enum CefFileDialogMode {
        FileDialogOpen = 0,
        FileDialogOpenMultiple,
        FileDialogSave,
    }

    public enum CefGeopositionErrorCode {
        GeopositonErrorNone = 0,
        GeopositonErrorPermissionDenied,
        GeopositonErrorPositionUnavailable,
        GeopositonErrorTimeout,
    }
}
