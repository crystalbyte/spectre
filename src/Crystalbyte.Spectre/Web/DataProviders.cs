#region Using directives

using System;
using System.Collections.Generic;

#endregion

namespace Crystalbyte.Spectre.Web{
    public sealed class DataProviders{
        private readonly List<Type> _types;

        public DataProviders(){
            _types = new List<Type>();
        }

        public IEnumerable<Type> Types{
            get { return _types; }
        }

        public void Register(Type type){
            if (!typeof (IDataProvider).IsAssignableFrom(type)){
                throw new InvalidOperationException("Object must be assignable to IDataProvider.");
            }
            _types.Add(type);
        }
    }
}