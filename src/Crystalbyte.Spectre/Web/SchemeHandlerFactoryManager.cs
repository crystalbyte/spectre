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
using Crystalbyte.Spectre.Projections;

#endregion

namespace Crystalbyte.Spectre.Web {
    public sealed class SchemeHandlerFactoryManager {
        internal SchemeHandlerFactoryManager() {}

        public void Register(ISchemeHandlerFactoryDescriptor descriptor) {
            if (descriptor == null) {
                throw new ArgumentNullException("descriptor");
            }

            var s = new StringUtf16(descriptor.SchemeName);
            var d = new StringUtf16(descriptor.DomainName);

            CefSchemeCapi.CefRegisterSchemeHandlerFactory(s.Handle, d.Handle,
                                                          descriptor.Factory.Handle);

            d.Free();
            s.Free();
        }

        public static void Clear() {
            CefSchemeCapi.CefClearSchemeHandlerFactories();
        }
    }
}
