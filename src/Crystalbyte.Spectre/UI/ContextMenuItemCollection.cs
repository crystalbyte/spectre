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
    public sealed class ContextMenuItemCollection : ICollection<ContextMenuItem> {
        public ContextMenuItemCollection(ContextMenu menu) {}

        #region ICollection<ContextMenuItem> Members

        public void Add(ContextMenuItem item) {
            throw new NotImplementedException();
        }

        public void Clear() {
            throw new NotImplementedException();
        }

        public bool Contains(ContextMenuItem item) {
            throw new NotImplementedException();
        }

        public void CopyTo(ContextMenuItem[] array, int arrayIndex) {
            throw new NotImplementedException();
        }

        public int Count {
            get { throw new NotImplementedException(); }
        }

        public bool IsReadOnly {
            get { throw new NotImplementedException(); }
        }

        public bool Remove(ContextMenuItem item) {
            throw new NotImplementedException();
        }

        public IEnumerator<ContextMenuItem> GetEnumerator() {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            throw new NotImplementedException();
        }

        #endregion
    }
}
