using System.Windows.Forms;
using Crystalbyte.Chocolate.Scripting;
using System;

namespace Crystalbyte.Chocolate.Extensions {
    internal sealed class GetSystemInformationExtension : RuntimeExtension {

        public override string PrototypeCode {
            get {
                return "if (!chocolate) " +
                       "    var chocolate = {};" +
                       "chocolate.getSystemInfo = function() {" +
                       "    native function __getSystemInfo ();" +
                       "    return __getSystemInfo ();" +
                       "}";
            }
        }

        protected override void OnExecuted(ExecutedEventArgs e) {
            var info = new JavaScriptObject {
                                                {"processorCount", new JavaScriptObject(Environment.ProcessorCount)},
                                                {"is64Bit", new JavaScriptObject(Environment.Is64BitOperatingSystem)},
                                                {"osVersion", new JavaScriptObject(Environment.OSVersion.ToString())},
                                                {"computerName", new JavaScriptObject(SystemInformation.ComputerName)},
                                                {"monitorCount", new JavaScriptObject(SystemInformation.MonitorCount)}
                                            };
            e.Result = info;
            e.IsHandled = true;
        }
    }
}
