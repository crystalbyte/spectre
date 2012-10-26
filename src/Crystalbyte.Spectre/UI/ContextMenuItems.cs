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
using System.Collections;
using System.Collections.Generic;

#endregion

namespace Crystalbyte.Spectre.UI {
    public sealed class ContextMenuItems {
        private readonly ContextMenu _menu;
        private readonly Dictionary<int, ContextMenuItem> _items;

        public ContextMenuItems(ContextMenu menu) {
            if (menu == null) 
                throw new ArgumentNullException("menu");
            _items = new Dictionary<int, ContextMenuItem>();
            _menu = menu;
        }

        #region ICollection<ContextMenuItem> Members

        public void Add(ContextMenuItem item) {
            _menu.AddItem(item.Command, item.Text);
        }

        public void Clear() {
            _menu.Clear();
        }

        public int Count {
            get { return _menu.GetCount(); }
        }

        public bool IsReadOnly {
            get { return false; }
        }

        public bool Remove(ContextMenuItem item) {
            return _menu.Remove(item.Command);
        }

        #endregion
    }
}
