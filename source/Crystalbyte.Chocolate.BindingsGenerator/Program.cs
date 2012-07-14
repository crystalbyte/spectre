#region Namespace Directives

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using EnvDTE80;

#endregion

namespace Crystalbyte.Chocolate
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            const string envVarName = "CHROMIUM_SRC";
            const string envDteProgId10 = "VisualStudio.DTE.10.0";
            const string envDteProgId11 = "VisualStudio.DTE.11.0";
            const string projectName = "Crystalbyte.Chocolate.Bindings";

            var created = false;
            DTE2 dte = null;

            try
            {
                dte = (DTE2) Marshal.GetActiveObject(envDteProgId10);
            }
            catch (COMException)
            {
                Debug.WriteLine("No Visual Studio 2010 instance running.");
            }

            try
            {
                dte = dte ?? (DTE2) Marshal.GetActiveObject(envDteProgId11);
            }
            catch (COMException)
            {
                Debug.WriteLine("No Visual Studio 2012 instance running.");
            }

            if (dte == null)
            {
                MessageBox.Show("No supported Visual Studio instance running. Unable to progress.");
                Application.Exit();
            }

            var solutionName = dte.Solution.FullName;
            if (string.IsNullOrWhiteSpace(solutionName))
            {
                MessageBox.Show(
                    "No active solution found, make sure no second Visual Studio instance is running in addition to this.");
                Application.Exit();
            }

            var solutionDir = new FileInfo(solutionName).DirectoryName;
            var outputDir = Path.Combine(solutionDir, projectName);

            var chromiumPath = Environment.GetEnvironmentVariable(envVarName);
            if (string.IsNullOrWhiteSpace(chromiumPath))
            {
                MessageBox.Show(
                    "Environmental variable 'CHROMIUM_SRC' not set, unable to determine Chromium source path.");
                Application.Exit();
            }
            var sourceDir = Path.Combine(chromiumPath, "cef", "include");
            var dirs = new List<string> { Path.Combine(sourceDir, "capi"), Path.Combine(sourceDir, "internal") };

            foreach (var dir in dirs)
            {
                var settings = new GeneratorSettings
                {
                    RootDirectory = new DirectoryInfo(dir),
                    OutputDirectory = new DirectoryInfo(outputDir),
                    Namespace = projectName
                };
                if (dir.EndsWith("internal"))
                {
                    settings.ClassNameSuffix = "Class";
                    settings.Namespace = projectName + ".Internal";
                    settings.OutputDirectory = new DirectoryInfo(Path.Combine(outputDir, "Internal"));
                }
                var generator = new BindingsGenerator(settings);
                generator.Generate();

                if (created)
                {
                    continue;
                }
                generator.GenerateAssemblyFile();
                created = true;
            }
        }
    }
}