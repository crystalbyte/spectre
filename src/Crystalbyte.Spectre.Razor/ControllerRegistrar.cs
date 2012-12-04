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
