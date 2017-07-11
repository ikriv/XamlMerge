using System;
using System.IO;
using NUnit.Framework;


namespace IKriv.XamlMerge.Tests
{
    [TestFixture]
    public class XamlMergerTests
    {
        private const string RootDir = @"c:\some\dir";
        private const string XamlFile = RootDir + @"\app.xaml";
        private const string MergedXamlFile = RootDir + @"\app.merged.xaml";
        private const string AssembliesFile = RootDir + @"\assemblies.txt";

        private FakeFileSystem _fs;
        private Options _options;
        private StringWriter _log;

        [SetUp]
        public void Setup()
        {
            _fs = new FakeFileSystem(RootDir);
            _options = new Options
            {
                XamlPath = XamlFile,
                AssembliesListPath = AssembliesFile,
                OutPath = MergedXamlFile
            };
            _log = new StringWriter();
        }

        [TearDown]
        public void Teardown()
        {
            Console.WriteLine("Log:");
            Console.WriteLine(_log.ToString());
        }

        [Test]
        public void EmptyAppXaml_ShouldBeAlmostUnchanged()
        {
            _fs[XamlFile] = TestFiles.EmptyApp;
            _fs[AssembliesFile] = String.Empty;
            bool success = CreateObject().Run();
            Assert.IsTrue(success);
            Assert.AreEqual(TestFiles.EmptyAppProcessed, _fs[MergedXamlFile]);
        }

        [Test]
        public void EmptyAppXaml_CannotOutputAsResOnly()
        {
            _fs[XamlFile] = TestFiles.EmptyApp;
            _fs[AssembliesFile] = String.Empty;
            _options.OutputResourcesOnly = true;
            bool success = CreateObject().Run();
            Assert.IsFalse(success);
        }


        [Test]
        public void AppWithoutRdElement_ShouldBeAlmostUnchanged()
        {
            _fs[XamlFile] = TestFiles.AppWithoutRdElement;
            _fs[AssembliesFile] = String.Empty;
            bool success = CreateObject().Run();
            Assert.IsTrue(success);
            Assert.AreEqual(TestFiles.AppWithoutRdElementProcessed, _fs[MergedXamlFile]);
        }

        [Test]
        public void AppWithoutRdElement_CannotOutputAsResOnly()
        {
            _fs[XamlFile] = TestFiles.AppWithoutRdElement;
            _fs[AssembliesFile] = String.Empty;
            _options.OutputResourcesOnly = true;
            bool success = CreateObject().Run();
            Assert.IsFalse(success);
        }

        [Test]
        public void AppWithoutMdElement_ShouldBeAlmostUnchanged()
        {
            _fs[XamlFile] = TestFiles.AppWithoutMdElement;
            _fs[AssembliesFile] = String.Empty;
            bool success = CreateObject().Run();
            Assert.IsTrue(success);
            Assert.AreEqual(TestFiles.AppWithoutMdElementProcessed, _fs[MergedXamlFile]);
        }

        [Test]
        public void AppWithoutMdElement_AsResOnly()
        {
            _fs[XamlFile] = TestFiles.AppWithoutMdElement;
            _fs[AssembliesFile] = String.Empty;
            _options.OutputResourcesOnly = true;
            bool success = CreateObject().Run();
            Assert.IsTrue(success);
            Assert.AreEqual(TestFiles.AppWithoutMdElementAsResOnly, _fs[MergedXamlFile]);
        }

        private XamlMerger CreateObject()
        {
            var assemblyList = new AssemblyList(_fs).Readfile(AssembliesFile);
            return new XamlMerger(_options, assemblyList, _log, _fs);
        }
    }
}
