#region Copyright notice

// Copyright (C) 2012 Alexander Wieser-Kuciel <alexander.wieser@crystalbyte.de>
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

#endregion

#region Namespace Directives

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

#endregion

namespace Crystalbyte.Chocolate
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            const string envVarName = "CHROMIUM_SRC";
            const string projectName = "Crystalbyte.Chocolate.Bindings";

            var created = false;
         
        
			var solutionDir = new FileInfo(typeof(Program).Assembly.Location).Directory.Parent.Parent.Parent.FullName;            
			var outputDir = Path.Combine(solutionDir, projectName);

            var chromiumPath = Environment.GetEnvironmentVariable(envVarName);
			chromiumPath = "/home/alexander/Development/Google/chromium/src/";
            if (string.IsNullOrWhiteSpace(chromiumPath))
            {
                MessageBox.Show(
                    "Environmental variable 'CHROMIUM_SRC' not set, unable to determine Chromium source path.");
                Application.Exit();
            }
            var sourceDir = Path.Combine(chromiumPath, "cef", "include");
            var dirs = new List<string> {Path.Combine(sourceDir, "capi"), Path.Combine(sourceDir, "internal")};

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