#region Using directives

using System;
using System.Collections;
using System.Collections.Generic;

#endregion

namespace Crystalbyte.Spectre.UI{
    public sealed class ContextMenuItemCollection : ICollection<ContextMenuItem>{
        public ContextMenuItemCollection(ContextMenu menu){}

        #region ICollection<ContextMenuItem> Members

        public void Add(ContextMenuItem item){
            throw new NotImplementedException();
        }

        public void Clear(){
            throw new NotImplementedException();
        }

        public bool Contains(ContextMenuItem item){
            throw new NotImplementedException();
        }

        public void CopyTo(ContextMenuItem[] array, int arrayIndex){
            throw new NotImplementedException();
        }

        public int Count{
            get { throw new NotImplementedException(); }
        }

        public bool IsReadOnly{
            get { throw new NotImplementedException(); }
        }

        public bool Remove(ContextMenuItem item){
            throw new NotImplementedException();
        }

        public IEnumerator<ContextMenuItem> GetEnumerator(){
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator(){
            throw new NotImplementedException();
        }

        #endregion
    }
}