using System;
using Crystalbyte.Chocolate.UI;

namespace Crystalbyte.Chocolate
{
	internal interface IWindowResizer
	{
		void Resize(IntPtr handle, Rectangle bounds);	
	}
}

