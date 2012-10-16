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

            CefSchemeCapi.CefRegisterSchemeHandlerFactory(s.NativeHandle, d.NativeHandle,
                                                          descriptor.Factory.NativeHandle);

            d.Free();
            s.Free();
        }

        public static void Clear() {
            CefSchemeCapi.CefClearSchemeHandlerFactories();
        }
    }
}
