using System;
using System.IO;
using System.Xml.Linq;

namespace IKriv.XamlMerge
{
    class XamlFile
    {
        public XamlFile(string assembly = null)
        {
            Assembly = assembly;
        }

        public string Path { get; private set; }
        public XDocument Xml { get; private set; }
        public string Assembly { get; private set; }

        public XamlFile Read(string path)
        {
            try
            {
                Path = path;

                if (Assembly == null)
                {
                    Xml = XDocument.Load(path);
                }
                else
                {
                    var content = File.ReadAllText(path);
                    var normalized = new ClrNamespaceHelper().NormalizeLocalNamespaces(content, Assembly);
                    Xml = XDocument.Parse(normalized);
                }

                return this;

            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error reading XAML from '{path}': {ex.Message}", ex);
            }
        }
    }
}
