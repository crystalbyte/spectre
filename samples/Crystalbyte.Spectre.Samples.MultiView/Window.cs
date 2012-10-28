#region Licensing notice

// Copyright (C) 2012, Alexander Wieser-Kuciel <alexander.wieser@crystalbyte.de>
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License version 3 as published by
// the Free Software Foundation.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

#endregion

#region Using directives

using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Windows.Forms;
using Crystalbyte.Spectre.UI;
using Crystalbyte.Spectre;

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
            GetRenderTargets().OfType<Frame>().ForEach(x => x.OnClosing(e));
            base.OnClosing(e);
        }

        protected override void OnClosed(EventArgs e) {
            GetRenderTargets().OfType<Frame>().ForEach(x => x.OnClosed(e));
            base.OnClosed(e);
        }
    }
}
