using System;
using MonoMac;
using MonoMac.Foundation;
using MonoMac.AppKit;
using MonoMac.ObjCRuntime;
using Crystalbyte.Chocolate.UI;

namespace Crystalbyte.Chocolate.Application
{
	[Adopts("CefAppProtocol")]
	public sealed class ChocApplication : NSApplication
	{
		private bool _isFrameworkLoaded;
		private bool _isHandlingSendEvent;
		
		public ChocApplication () { }
		
		[Export("isHandlingSendEvent")]
		public bool IsHandlingSendEvent() {
			return _isHandlingSendEvent;
		}
		
		[Export("sendEvent:")]
		public override void SendEvent(NSEvent @event) {
			var isHandling = _isHandlingSendEvent;
			_isHandlingSendEvent = true;
			base.SendEvent(@event);
			_isHandlingSendEvent = isHandling;
		}
		
		[Export("setHandlingSendEvent:")]
		public void SetHandlingSendEvent(bool isHandlingSendEvent) {
			_isHandlingSendEvent = isHandlingSendEvent;
		}
		
		public override bool ConformsToProtocol(IntPtr protocol) {
			return base.ConformsToProtocol(protocol);
		}
		
		public override void Run() {
			if (_isFrameworkLoaded) {
				base.Run();
				return;
			}
			
			using(var pool = new NSAutoreleasePool()) {
				_isFrameworkLoaded = true;
				
				var argv = NSProcessInfo.ProcessInfo.Arguments;	
				Framework.Initialize(argv, new Crystalbyte.Chocolate.UI.AppDelegate());
				if (!Framework.IsRootProcess) {
					return;
				}
				Framework.Run();
				Framework.Shutdown();
			}
		} 
	}
}

