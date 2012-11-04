﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Crystalbyte.Spectre.Razor {
    public interface IView {
        void Render(ViewContext viewContext, TextWriter writer);
    }
}
