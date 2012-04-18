using System;
using Crystalbyte.Chocolate.UI;

namespace Crystalbyte.Chocolate.UI
{
	public sealed class MacWindowResizer : IWindowResizer
	{
		public MacWindowResizer ()
		{
		}

		#region IWindowResizer implementation
		public void Resize (IntPtr handle, Rectangle bounds)
		{
			// no need to resize manually on os x
		}
		#endregion
	}
}

