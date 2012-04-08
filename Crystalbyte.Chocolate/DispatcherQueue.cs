using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crystalbyte.Chocolate
{
    public enum DispatcherQueue {
        UI,
        DB,
        File,
        FileUserBlocking,
        ProcessLauncher,
        Cache,
        IO,
        Renderer,
    }
}
