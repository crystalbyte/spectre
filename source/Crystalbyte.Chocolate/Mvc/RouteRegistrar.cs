using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crystalbyte.Chocolate.Mvc
{
    internal static class RouteRegistrar {
        private static readonly Dictionary<string, Type> Types;

        static RouteRegistrar() {
            Types = new Dictionary<string, Type>();
        }

        public static void Register(string route, Type controller) {
            Types.Add(route, controller);
        }

        public static Type GetController(string route) {
            return Types[route];
        }

        public static bool TryGetController(string route, out Type controller) {
            if (Types.ContainsKey(route)) {
                controller = Types[route];
                return true;
            }

            controller = null;
            return false;
        }
    }
}
