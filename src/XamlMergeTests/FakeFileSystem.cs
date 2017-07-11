using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace IKriv.XamlMerge.Tests
{
    class FakeFileSystem : IFileSystem
    {
        private readonly Dictionary<string, string> _files = new Dictionary<string, string>();
        private readonly string _rootPath;

        public FakeFileSystem(string rootPath)
        {
            _rootPath = rootPath;
        }

        public string this[string path]
        {
            get
            {
                if (_files.TryGetValue(path, out string result)) return result;
                return null;
            }

            set => Add(path, value);
        }

        public FakeFileSystem Add(string path, string text)
        {
            _files[Path.Combine(_rootPath, path)] = text;
            return this;
        }

        public string ReadAllText(string path)
        {
            var result = this[path];
            if (result == null) throw new FileNotFoundException("File not found", path);
            return result;
        }

        public string[] ReadAllLines(string path)
        {
            return ReadAllText(path).Replace("\r\n", "\n").Split('\n');
        }

        public XDocument ReadXml(string path)
        {
            return XDocument.Parse(ReadAllText(path));
        }

        public void WriteAllText(string path, string text)
        {
            _files[path] = text;
        }
    }
}
