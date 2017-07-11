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
            _fs[XamlFile] = TestApps.EmptyApp;
            _fs[AssembliesFile] = String.Empty;
            bool success = CreateObject().Run();
            Assert.IsTrue(success);
            Assert.AreEqual(TestApps.EmptyAppProcessed, _fs[MergedXamlFile]);
        }

        [Test]
        public void EmptyAppXaml_CannotOutputAsResOnly()
        {
            _fs[XamlFile] = TestApps.EmptyApp;
            _fs[AssembliesFile] = String.Empty;
            _options.OutputResourcesOnly = true;
            bool success = CreateObject().Run();
            Assert.IsFalse(success);
        }


        [Test]
        public void AppWithoutRdElement_ShouldBeAlmostUnchanged()
        {
            _fs[XamlFile] = TestApps.AppWithoutRdElement;
            _fs[AssembliesFile] = String.Empty;
            bool success = CreateObject().Run();
            Assert.IsTrue(success);
            Assert.AreEqual(TestApps.AppWithoutRdElementProcessed, _fs[MergedXamlFile]);
        }

        [Test]
        public void AppWithoutRdElement_CannotOutputAsResOnly()
        {
            _fs[XamlFile] = TestApps.AppWithoutRdElement;
            _fs[AssembliesFile] = String.Empty;
            _options.OutputResourcesOnly = true;
            bool success = CreateObject().Run();
            Assert.IsFalse(success);
        }

        [Test]
        public void AppWithoutMdElement_ShouldBeAlmostUnchanged()
        {
            _fs[XamlFile] = TestApps.AppWithoutMdElement;
            _fs[AssembliesFile] = String.Empty;
            bool success = CreateObject().Run();
            Assert.IsTrue(success);
            Assert.AreEqual(TestApps.AppWithoutMdElementProcessed, _fs[MergedXamlFile]);
        }

        [Test]
        public void AppWithoutMdElement_AsResOnly()
        {
            _fs[XamlFile] = TestApps.AppWithoutMdElement;
            _fs[AssembliesFile] = String.Empty;
            _options.OutputResourcesOnly = true;
            bool success = CreateObject().Run();
            Assert.IsTrue(success);
            Assert.AreEqual(TestApps.AppWithoutMdElementAsResOnly, _fs[MergedXamlFile]);
        }

        [Test]
        public void AppWithEmptyMdElement_ShouldBeAlmostUnchanged()
        {
            _fs[XamlFile] = TestApps.AppWithoutMdElement;
            _fs[AssembliesFile] = String.Empty;
            bool success = CreateObject().Run();
            Assert.IsTrue(success);
            Assert.AreEqual(TestApps.AppWithoutMdElementProcessed, _fs[MergedXamlFile]);
        }

        [Test]
        public void AppWithEmptyMdElement_AsResOnly()
        {
            _fs[XamlFile] = TestApps.AppWithoutMdElement;
            _fs[AssembliesFile] = String.Empty;
            _options.OutputResourcesOnly = true;
            bool success = CreateObject().Run();
            Assert.IsTrue(success);
            Assert.AreEqual(TestApps.AppWithoutMdElementAsResOnly, _fs[MergedXamlFile]);
        }

        [Test]
        public void AppWithLocalMd_MdIsMerged()
        {
            _fs[XamlFile] = TestApps.AppWithLocalMd;
            _fs["Local.xaml"] = TestDitionaries.SimpleMergedDictionary;
            _fs[AssembliesFile] = String.Empty;
            bool success = CreateObject().Run();
            Assert.IsTrue(success);
            Assert.AreEqual(TestApps.AppWithLocalMdProcessed, _fs[MergedXamlFile]);
        }

        [Test]
        public void AppWithLocalMd_MdIsMerged_ResOnly()
        {
            _fs[XamlFile] = TestApps.AppWithLocalMd;
            _fs["Local.xaml"] = TestDitionaries.SimpleMergedDictionary;
            _fs[AssembliesFile] = String.Empty;
            _options.OutputResourcesOnly = true;
            bool success = CreateObject().Run();
            Assert.IsTrue(success);
            Assert.AreEqual(TestApps.AppWithLocalMdAsResOnly, _fs[MergedXamlFile]);
        }

        [Test]
        public void AppWithExternalMd_ExternalMdRefRetained()
        {
            _fs[XamlFile] = TestApps.AppWithExternalMd;
            _fs[AssembliesFile] = "External.Assembly=@extern";
            bool success = CreateObject().Run();
            Assert.IsTrue(success);
            Assert.AreEqual(TestApps.AppWithExternalMdProcessed, _fs[MergedXamlFile]);
        }

        [Test]
        public void AppWithExternalMd_AsResOnly()
        {
            _fs[XamlFile] = TestApps.AppWithExternalMd;
            _fs[AssembliesFile] = "External.Assembly=@extern";
            _options.OutputResourcesOnly = true;
            bool success = CreateObject().Run();
            Assert.IsTrue(success);
            Assert.AreEqual(TestApps.AppWithExternalMdAsResOnly, _fs[MergedXamlFile]);
        }

        [Test]
        public void AppWithOurAssemblyMd_MdMerged()
        {
            _fs[XamlFile] = TestApps.AppWithOurAssemblyMd;
            _fs[@"Our.Assembly\Resources\Stuff.xaml"] = TestDitionaries.SimpleMergedDictionary;
            _fs[AssembliesFile] = "Our.Assembly";
            bool success = CreateObject().Run();
            Assert.IsTrue(success);
            Assert.AreEqual(TestApps.AppWithOurAssemblyMdProcessed, _fs[MergedXamlFile]);
        }

        [Test]
        public void AppWithOurAssemblyMd_ResOnly()
        {
            _fs[XamlFile] = TestApps.AppWithOurAssemblyMd;
            _fs[@"Our.Assembly\Resources\Stuff.xaml"] = TestDitionaries.SimpleMergedDictionary;
            _fs[AssembliesFile] = "Our.Assembly";
            _options.OutputResourcesOnly = true;
            bool success = CreateObject().Run();
            Assert.IsTrue(success);
            Assert.AreEqual(TestApps.AppWithOurAssemblyMdAsResOnly, _fs[MergedXamlFile]);
        }

        [Test]
        public void AppWithRecursiveMd_MergedRecursively()
        {
            _fs[XamlFile] = TestApps.AppWithRecursiveMd;
            _fs[@"Our.Assembly\Resources\Recursive.xaml"] = TestDitionaries.RecursiveDictionary;
            _fs[@"Another.Assembly\Resources\Stuff.xaml"] = TestDitionaries.SimpleMergedDictionary;
            _fs[AssembliesFile] = 
@"Our.Assembly
Another.Assembly
External.Assembly=@extern
Some.Assembly=@extern
UX.Themes=@extern";
            bool success = CreateObject().Run();
            Assert.IsTrue(success);
            Assert.AreEqual(TestApps.AppWithRecursiveMdProcessed, _fs[MergedXamlFile]);
        }

        [Test]
        public void AppWithRecursiveMd_ResOnly()
        {
            _fs[XamlFile] = TestApps.AppWithRecursiveMd;
            _fs[@"Our.Assembly\Resources\Recursive.xaml"] = TestDitionaries.RecursiveDictionary;
            _fs[@"Another.Assembly\Resources\Stuff.xaml"] = TestDitionaries.SimpleMergedDictionary;
            _fs[AssembliesFile] =
                @"Our.Assembly
Another.Assembly
External.Assembly=@extern
Some.Assembly=@extern
UX.Themes=@extern";
            _options.OutputResourcesOnly = true;
            bool success = CreateObject().Run();
            Assert.IsTrue(success);
            Assert.AreEqual(TestApps.AppWithRecursiveMdResOnly, _fs[MergedXamlFile]);
        }


        private XamlMerger CreateObject()
        {
            var assemblyList = new AssemblyList(_fs).Readfile(AssembliesFile);
            return new XamlMerger(_options, assemblyList, _log, _fs);
        }
    }
}
