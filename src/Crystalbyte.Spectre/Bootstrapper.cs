#region Using directives

using System;
using System.Collections.Generic;
using Crystalbyte.Spectre.Scripting;
using Crystalbyte.Spectre.UI;
using Crystalbyte.Spectre.Web;

#endregion

namespace Crystalbyte.Spectre{
    public abstract class Bootstrapper{

        protected abstract IRenderTarget CreateRenderTarget();

        protected virtual AppDelegate CreateAppDelegate(){
            return new AppDelegate();
        }

        protected virtual BrowserDelegate CreateBrowserDelegate(IRenderTarget target){
            return new BrowserDelegate();
        }

        public virtual void Run(){
            var app = CreateAppDelegate();
            app.CustomSchemesRegistering += OnCustomSchemesRegistering;
            app.Initialized += OnFrameworkInitialized;

            ConfigureSettings(Application.Current.Settings);
            Application.Current.Initialize(app);

            if (!Application.Current.IsRootProcess){
                return;
            }

            var factories = RegisterSchemeHandlerFactories();
            factories.ForEach(Application.Current.SchemeFactories.Register);

            var target = CreateRenderTarget();
            var browserDelegate = CreateBrowserDelegate(target);

            Application.Current.Run(new Viewport(target, browserDelegate));
            Application.Current.Shutdown();
        }

        private void OnCustomSchemesRegistering(object sender, CustomSchemesRegisteringEventArgs e){
            var descriptors = RegisterSchemeHandlers();
            e.SchemeDescriptors.AddRange(descriptors);
        }

        protected virtual void ConfigureSettings(ApplicationSettings settings){
#if DEBUG
            settings.LogSeverity = LogSeverity.LogseverityVerbose;
#else
            settings.LogSeverity = LogSeverity.LogseverityInfo;
#endif
        }

        protected virtual IList<ISchemeHandlerFactoryDescriptor> RegisterSchemeHandlerFactories(){
            return new List<ISchemeHandlerFactoryDescriptor>{
                                                                new SpectreSchemeHandlerFactoryDescriptor()
                                                            };
        }

        protected virtual IList<ISchemeDescriptor> RegisterSchemeHandlers(){
            return new List<ISchemeDescriptor>{
                                                  new SpectreSchemeDescriptor()
                                              };
        }

        protected virtual IList<ScriptingCommand> RegisterRuntimeCommands(){
            return new List<ScriptingCommand>();
        }

        private void OnFrameworkInitialized(object sender, EventArgs e){
            var extensions = RegisterRuntimeCommands();
            if (extensions != null){
                extensions.ForEach(RegisterRuntimeCommand);
            }
        }

        private static void RegisterRuntimeCommand(ScriptingCommand extension){
            var name = Guid.NewGuid().ToString();
            ScriptingRuntime.RegisterCommand(name, extension);
        }
    }
}