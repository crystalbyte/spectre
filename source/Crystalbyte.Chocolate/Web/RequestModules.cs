using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crystalbyte.Chocolate.Web
{
    public sealed class RequestModules {
        private readonly List<Type> _types;

        public RequestModules() {
            _types = new List<Type>();
        }

        public void Register(Type type) {
            if (!typeof(IRequestModule).IsAssignableFrom(type)) {
                throw new InvalidOperationException("Object must be assignable to IRequestModule.");
            }
            _types.Add(type);
        }

        public IEnumerable<Type> Types { get { return _types; } }
    }
}
