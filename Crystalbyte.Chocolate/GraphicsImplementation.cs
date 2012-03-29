using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crystalbyte.Chocolate
{
    public enum GraphicsImplementation {
        AngleInProcess = 0,
        AngleInProcessCommandBuffer,
        DesktopInProcess,
        DesktopInProcessCommandBuffer,
    }
}
