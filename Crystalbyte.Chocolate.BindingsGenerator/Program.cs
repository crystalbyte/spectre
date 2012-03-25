using System.Collections.Generic;
using System.IO;

namespace Crystalbyte.Chocolate
{
    class Program
    {
        static void Main(string[] args) {

            var created = false;
            var dirs = new List<string> {"include/capi", "include/internal"};
            foreach (var dir in dirs) {
                
                var settings = new GeneratorSettings() {
                    RootDirectory = new DirectoryInfo(dir),
                    OutputDirectory = new DirectoryInfo("out/" + dir),
                    Namespace = "Crystalbyte.Chocolate.Bindings"
                };
                if (dir.EndsWith("internal")) {
                    settings.ClassNameSuffix = "Class";
                    settings.Namespace = "Crystalbyte.Chocolate.Bindings.Internal";
                }
                var generator = new BindingsGenerator(settings);
                generator.Generate();

                if (created) {
                    continue;
                }
                generator.GenerateAssemblyFile();
                created = true;
            }
        }
    }
}
