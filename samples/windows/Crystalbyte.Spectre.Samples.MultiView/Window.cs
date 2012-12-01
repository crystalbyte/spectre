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
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Crystalbyte.Spectre.UI;

#endregion

namespace Crystalbyte.Spectre.Samples {
    public partial class Window : Form {
        public Window() {
            InitializeComponent();
        }

        public IEnumerable<IRenderTarget> GetRenderTargets() {
            return _layout.Controls.OfType<IRenderTarget>();
        }

        protected override void OnClosing(CancelEventArgs e) {
            var frames = _layout.Controls.OfType<Frame>().ToList();
            frames.ForEach(x => x.OnClosing(new CancelEventArgs()));
            base.OnClosing(e);
        }

        protected override void OnClosed(EventArgs e) {
            var frames = _layout.Controls.OfType<Frame>().ToList();
            frames.ForEach(x => x.OnClosed(EventArgs.Empty));
            base.OnClosed(e);
        }
    }
}
