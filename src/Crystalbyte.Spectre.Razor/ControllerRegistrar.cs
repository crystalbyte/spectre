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

#endregion

namespace Crystalbyte.Spectre.Razor {
    public static class ControllerRegistrar {
        private static readonly Dictionary<string, Type>
            _controllers = new Dictionary<string, Type>();

        public static void Register(Type type) {
            if (type == null) {
                throw new ArgumentNullException("type");
            }

            if (!typeof (Controller).IsAssignableFrom(type)) {
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
