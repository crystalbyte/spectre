using System;
using MonoMac.AppKit;
using Crystalbyte.Chocolate.UI;
using MonoMac.Foundation;

namespace Crystalbyte.Chocolate.Application.Mac
{
	public class Program : NSApplication
	{
		private bool _isInitialized;
		
		public Program ()
		{
		}
		
		public override void Run ()
		{
			if (!_isInitialized) {
				InitChocolate();
				_isInitialized = true;
				return;
			}
			
			if (!Framework.IsRootProcess) {
				return;
			}
			
			Framework.Run();
			Framework.Shutdown();
		}
		
		private void InitChocolate() {
			var commandLine = NSProcessInfo.ProcessInfo.Arguments;
			Framework.Initialize(commandLine, new Chocolate.UI.AppDelegate());
		}
	}
}

