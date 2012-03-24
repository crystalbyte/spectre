using System.IO;

namespace Crystalbyte.Chocolate
{
    class Program
    {
        static void Main(string[] args) {
            var settings = new GeneratorSettings() {
                RootDirectory = new DirectoryInfo("include/capi"),
                OutputDirectory = new DirectoryInfo("bindings"),
                Namespace = "Crystalbyte.Chocolate.Bindings"
            }; 
            var generator = new BindingsGenerator(settings);
            generator.Generate();

            var settings2 = new GeneratorSettings() {
                RootDirectory = new DirectoryInfo("include/internal"),
                OutputDirectory = new DirectoryInfo("bindings/internal"),
                Namespace = "Crystalbyte.Chocolate.Bindings"
            };
            var generator2 = new BindingsGenerator(settings2);
            generator2.Generate();
        }
    }
}
