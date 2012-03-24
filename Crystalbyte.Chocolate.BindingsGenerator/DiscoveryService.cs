using System.Collections.Generic;
using System.IO;

namespace Crystalbyte.Chocolate
{
    public sealed class DiscoveryService {
        private readonly GeneratorSettings _settings;
        public DiscoveryService(GeneratorSettings settings) {
            _settings = settings;
        }

        public IEnumerable<FileInfo> Discover() {
            var files = new List<FileInfo>();
            TraverseDirectory(_settings.RootDirectory, files);
            return files;
        }

        private static void TraverseDirectory(DirectoryInfo directory, List<FileInfo> files) {
            files.AddRange(directory.GetFiles("*.h"));
            foreach (var child in directory.GetDirectories()) {
                TraverseDirectory(child, files);
            }
        }

    }
}
