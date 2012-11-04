using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crystalbyte.Spectre.Razor {
    public static class ControllerRegistrar {
        private static readonly Dictionary<string, Type>
            _controllers = new Dictionary<string, Type>();

        public static void Register(string url, Type type) {
            if (url == null) {
                throw new ArgumentNullException("url");
            }

            if (type == null) {
                throw new ArgumentNullException("type");
            }

            if (!typeof(Controller).IsAssignableFrom(type)) {
                throw new InvalidOperationException("Type must be assignable from Controller.");
            }

            if (!type.Name.EndsWith("Controller")) {
                throw new InvalidOperationException("Controller's name must end with 'Controller'.");
            }

            var name = type.Name.Replace("Controller", string.Empty);
            _controllers.Add(name.ToLower(), type);
        }

        public static bool CanServe(string url) {
            return _controllers.ContainsKey(url.ToLower());
        }

        public static Controller CreateInstance(string url) {
            var type = _controllers[url.ToLower()];
            return Activator.CreateInstance(type) as Controller;
        }
    }
}
