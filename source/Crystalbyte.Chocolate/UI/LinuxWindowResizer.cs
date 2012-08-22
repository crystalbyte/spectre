using System;
using Crystalbyte.Chocolate.UI;

namespace Crystalbyte.Chocolate
{
	public sealed class LinuxWindowResizer : IWindowResizer
	{
		public LinuxWindowResizer () { }

		#region IWindowResizer implementation

		public void Resize (IntPtr handle, Rectangle bounds)
		{

		}

		#endregion

	}
}

