using System.IO;
using NUnit.Framework;

namespace UniSharperEditor.Tests
{
    public class EditorPathTests
    {
        private const string TestPackageName = "io.github.idreamsofgame.resharp.core";

        private const string FakePackageName = "com.xxxx.xxx.package1";

        [Test]
        public void ConvertToAssetPathTest1()
        {
            var assetPath = Path.Combine(EditorEnvironment.AssetsFolderName, "UniSharper.Core", "README.md");
            var actual = EditorPath.ConvertToAssetPath(assetPath);
            Assert.AreEqual(assetPath, actual);
        }
        
        [Test]
        public void ConvertToAssetPathTest2()
        {
            var absolutePath = Path.Combine(Directory.GetCurrentDirectory(), EditorEnvironment.AssetsFolderName, "UniSharper.Core", "README.md");
            var actual = EditorPath.ConvertToAssetPath(absolutePath);
            var expected = Path.Combine(EditorEnvironment.AssetsFolderName, "UniSharper.Core", "README.md");
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ConvertToAssetPathTest3()
        {
            var assetPath = Path.Combine(EditorEnvironment.PackagesFolderName, TestPackageName, "Plugins", "ReSharp.Core.xml");
            var actual = EditorPath.ConvertToAssetPath(assetPath);
            Assert.AreEqual(assetPath, actual);
        }
        
        [Test]
        public void ConvertToAssetPathTest4()
        {
            var absolutePath = Path.Combine(EditorPath.GetPackageResolvedPath(TestPackageName), "Plugins", "ReSharp.Core.xml");
            var actual = EditorPath.ConvertToAssetPath(absolutePath);
            var expected = Path.Combine(EditorPath.GetPackageAssetPath(TestPackageName), "Plugins", "ReSharp.Core.xml");
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ConvertToAssetPathTest5()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), EditorEnvironment.AssetsFolderName, FakePackageName);
            var actual = EditorPath.ConvertToAssetPath(path);
            Assert.AreEqual(null, actual);
        }

        [Test]
        public void GetPackageAssetPathTest1()
        {
            var assetPath = EditorPath.GetPackageAssetPath(TestPackageName);
            var expected = Path.Combine(EditorEnvironment.PackagesFolderName, TestPackageName);
            Assert.AreEqual(expected, assetPath);
        }

        [Test]
        public void GetPackageAssetPathTest2()
        {
            var assetPath = EditorPath.GetPackageAssetPath(FakePackageName);
            Assert.AreEqual(null, assetPath);
        }

        [Test]
        public void GetPackageResolvedPathTest1()
        {
            var resolvedPath = EditorPath.GetPackageResolvedPath(TestPackageName);
            var basePath = Path.Combine(Directory.GetCurrentDirectory(), EditorEnvironment.LibraryFolderName, "PackageCache");
            Assert.IsTrue(resolvedPath.StartsWith(basePath));
        }

        [Test]
        public void GetPackageResolvedPathTest2()
        {
            var resolvedPath = EditorPath.GetPackageResolvedPath(FakePackageName);
            Assert.AreEqual(null, resolvedPath);
        }
    }
}