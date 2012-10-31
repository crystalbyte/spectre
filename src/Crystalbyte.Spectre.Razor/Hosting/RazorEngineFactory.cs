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
using System.IO;
using System.Reflection;
using Crystalbyte.Spectre.Razor.Templates;

#endregion

namespace Crystalbyte.Spectre.Razor.Hosting {
    /// <summary>
    ///   Factory that creates a RazorHost instance in a remote 
    ///   AppDomain that can be unloaded. This allows unloading of
    ///   assemblies created through scripting.
    /// 
    ///   Both static and instance loader methods are available. For
    ///   AppDomain created hosts.
    /// </summary>
    /// <typeparam name="TBaseTemplateType"> RazorTemplateBase based type </typeparam>
    public class RazorEngineFactory<TBaseTemplateType>
        where TBaseTemplateType : RazorTemplateBase {
        /// <summary>
        ///   Internal instance of the AppDomain to hang onto when
        ///   running in a separate AppDomain. Ensures the AppDomain
        ///   stays alive.
        /// </summary>
        private AppDomain _localAppDomain;

        /// <summary>
        ///   Internally managed instance of the HostFactory
        ///   that ensures that the AppDomain stays alive and
        ///   that it can be unloaded manually using the static
        ///   methods.
        /// </summary>
        private static RazorEngineFactory<TBaseTemplateType> _current;

        public string ErrorMessage { get; set; }

        /// <summary>
        ///   Create an instance of the RazorHost in the current
        ///   AppDomain. No special handling...
        /// </summary>
        /// <returns> </returns>
        public static RazorEngine<TBaseTemplateType> CreateRazorHost() {
            return new RazorEngine<TBaseTemplateType>();
        }

        /// <summary>
        ///   Creates an instance of the RazorHost in a new AppDomain. This 
        ///   version creates a static singleton that that is cached and you
        ///   can call UnloadRazorHostInAppDomain to unload it.
        /// </summary>
        /// <returns> </returns>
        public static RazorEngine<TBaseTemplateType> CreateRazorHostInAppDomain() {
            if (_current == null)
                _current = new RazorEngineFactory<TBaseTemplateType>();

            return _current.GetRazorHostInAppDomain();
        }

        /// <summary>
        /// </summary>
        public static void UnloadRazorHostInAppDomain() {
            if (_current != null)
                _current.UnloadHost();
            _current = null;
        }

        /// <summary>
        ///   Create a new instance of Razor Host in the current AppDomain.
        /// </summary>
        /// <returns> </returns>
        public RazorEngine<TBaseTemplateType> GetRazorHost() {
            return new RazorEngine<TBaseTemplateType>();
        }

        /// <summary>
        ///   Instance method that creates a RazorHost in a new AppDomain.
        ///   This method requires that you keep the Factory around in
        ///   order to keep the AppDomain alive and be able to unload it.
        /// </summary>
        /// <returns> </returns>
        public RazorEngine<TBaseTemplateType> GetRazorHostInAppDomain() {
            _localAppDomain = CreateAppDomain(null);
            if (_localAppDomain == null)
                return null;

            RazorEngine<TBaseTemplateType> host = null;

            try {
                var ass = Assembly.GetExecutingAssembly();

                var assemblyPath = ass.Location;

                var fullName = typeof (RazorEngine<TBaseTemplateType>).FullName;
                if (fullName != null)
                    host = (RazorEngine<TBaseTemplateType>) 
                           _localAppDomain.CreateInstanceFrom(assemblyPath, fullName).Unwrap();
            }
            catch (Exception ex) {
                ErrorMessage = ex.Message;
                return null;
            }

            return host;
        }

        /// <summary>
        ///   Internally creates a new AppDomain in which Razor templates can
        ///   be run.
        /// </summary>
        /// <param name="appDomainName"> </param>
        /// <returns> </returns>
        private AppDomain CreateAppDomain(string appDomainName) {
            if (appDomainName == null)
                appDomainName = "RazorHost_" + Guid.NewGuid().ToString("n");

            var setup = new AppDomainSetup();

            // *** Point at current directory
            setup.ApplicationBase = AppDomain.CurrentDomain.BaseDirectory;

            var localDomain = AppDomain.CreateDomain(appDomainName, null, setup);

            // *** Need a custom resolver so we can load assembly from non current path
            //AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);

            return localDomain;
        }

        /// <summary>
        ///   Allow for custom assembly resolution to local file paths for signed dependency
        ///   assemblies.
        /// </summary>
        /// <param name="sender"> </param>
        /// <param name="args"> </param>
        /// <returns> </returns>
        private System.Reflection.Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args) {
            try {
                var assembly = System.Reflection.Assembly.Load(args.Name);
                if (assembly != null)
                    return assembly;
            }
            catch {}

            // Try to load by filename - split out the filename of the full assembly name
            // and append the base path of the original assembly (ie. look in the same dir)
            // NOTE: this doesn't account for special search paths but then that never
            //       worked before either.
            var Parts = args.Name.Split(',');
            var File = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\" + Parts[0].Trim() + ".dll";

            return System.Reflection.Assembly.LoadFrom(File);
        }

        /// <summary>
        ///   Allow unloading of the created AppDomain to release resources
        ///   All internal resources in the AppDomain are released including
        ///   in memory compiled Razor assemblies.
        /// </summary>
        public void UnloadHost() {
            if (_localAppDomain != null) {
                AppDomain.Unload(_localAppDomain);
                _localAppDomain = null;
            }
        }
        }
}
