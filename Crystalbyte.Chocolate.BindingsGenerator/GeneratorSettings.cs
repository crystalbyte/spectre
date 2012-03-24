using System.IO;

namespace Crystalbyte.Chocolate {
    public sealed class GeneratorSettings {
        public DirectoryInfo RootDirectory { get; set; }
        public DirectoryInfo OutputDirectory { get; set; }
        public string Namespace { get; set; }
        public string ExportSymbol { get; set; }
    }
}
