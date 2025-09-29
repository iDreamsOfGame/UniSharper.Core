using System.IO;
using NUnit.Framework;
using UniSharper;

namespace UniSharperEditor.Tests
{
    public class EditorPathTests
    {
        private const string TestAssetFolderName = "UniSharper.Core";

        private const string TestAssetFileName = "README.md";
        
        private const string TestPackageName = "com.unity.modules.ui";

        private const string TestPackageFileName = "package.json";

        private const string FakePackageName = "com.xxxx.xxx.package1";

        private const string PackageCacheFolderName = "PackageCache";

        [Test]
        public void IsAssetPathTest1()
        {
            var result = EditorPath.IsAssetPath(Path.Combine(PlayerEnvironment.AssetsFolderName, FakePackageName));
            Assert.IsFalse(result);
        }
        
        [Test]
        public void IsAssetPathTest2()
        {
            var result = EditorPath.IsAssetPath(Path.Combine(PlayerEnvironment.AssetsFolderName, TestAssetFolderName, TestAssetFileName));
            Assert.IsTrue(result);
        }
        
        [Test]
        public void IsAssetPathTest3()
        {
            var result = EditorPath.IsAssetPath(Path.Combine(Directory.GetCurrentDirectory(), PlayerEnvironment.AssetsFolderName, TestAssetFolderName, TestAssetFileName));
            Assert.IsTrue(result);
        }

        [Test]
        public void GetFullPathTest1()
        {
            var fullPath = EditorPath.GetFullPath(PlayerEnvironment.AssetsFolderName, FakePackageName);
            var expected = Path.Combine(Directory.GetCurrentDirectory(), PlayerEnvironment.AssetsFolderName, FakePackageName);
            Assert.AreEqual(expected, fullPath);
        }
        
        [Test]
        public void GetFullPathTest2()
        {
            var assetPath = Path.Combine(PlayerEnvironment.AssetsFolderName, TestAssetFolderName, TestAssetFileName);
            var fullPath = EditorPath.GetFullPath(assetPath);
            var actual = Path.Combine(Directory.GetCurrentDirectory(), assetPath);
            Assert.AreEqual(actual, fullPath);
        }
        
        [Test]
        public void GetFullPathTest3()
        {
            var assetPath = Path.Combine(PlayerEnvironment.PackagesFolderName, TestPackageName, TestPackageFileName);
            var fullPath = EditorPath.GetFullPath(assetPath);
            var actual = Path.Combine(Directory.GetCurrentDirectory(), EditorEnvironment.LibraryFolderName, PackageCacheFolderName, $"{TestPackageName}@1.0.0", TestPackageFileName);
            Assert.AreEqual(actual, fullPath);
        }

        [Test]
        public void GetPhysicalPathTest1()
        {
            var fullPath = EditorPath.GetPhysicalPath(PlayerEnvironment.AssetsFolderName, FakePackageName);
            Assert.AreEqual(null, fullPath);
        }

        [Test]
        public void GetPhysicalPathTest2()
        {
            var assetPath = Path.Combine(PlayerEnvironment.AssetsFolderName, TestAssetFolderName, TestAssetFileName);
            var fullPath = EditorPath.GetPhysicalPath(assetPath);
            var actual = Path.Combine(Directory.GetCurrentDirectory(), assetPath);
            Assert.AreEqual(actual, fullPath);
        }

        [Test]
        public void GetPhysicalPathTest3()
        {
            var assetPath = Path.Combine(PlayerEnvironment.PackagesFolderName, TestPackageName, TestPackageFileName);
            var fullPath = EditorPath.GetPhysicalPath(assetPath);
            var actual = Path.Combine(Directory.GetCurrentDirectory(), EditorEnvironment.LibraryFolderName, PackageCacheFolderName, $"{TestPackageName}@1.0.0", TestPackageFileName);
            Assert.AreEqual(actual, fullPath);
        }

        [Test]
        public void GetAssetPathTest1()
        {
            var assetPath = Path.Combine(PlayerEnvironment.AssetsFolderName, TestAssetFolderName, TestAssetFileName);
            var actual = EditorPath.GetAssetPath(assetPath);
            Assert.AreEqual(assetPath, actual);
        }
        
        [Test]
        public void GetAssetPathTest2()
        {
            var absolutePath = Path.Combine(Directory.GetCurrentDirectory(), PlayerEnvironment.AssetsFolderName, TestAssetFolderName, TestAssetFileName);
            var actual = EditorPath.GetAssetPath(absolutePath);
            var expected = Path.Combine(PlayerEnvironment.AssetsFolderName, TestAssetFolderName, TestAssetFileName);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetAssetPathTest3()
        {
            var assetPath = Path.Combine(PlayerEnvironment.PackagesFolderName, TestPackageName, TestPackageFileName);
            var actual = EditorPath.GetAssetPath(assetPath);
            Assert.AreEqual(assetPath, actual);
        }
        
        [Test]
        public void GetAssetPathTest4()
        {
            var absolutePath = Path.Combine(EditorPath.GetPackageResolvedPath(TestPackageName), TestPackageFileName);
            var actual = EditorPath.GetAssetPath(absolutePath);
            var expected = Path.Combine(EditorPath.GetPackageAssetPath(TestPackageName), TestPackageFileName);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetAssetPathTest5()
        {
            var path = Path.Combine(PlayerEnvironment.PackagesFolderName, FakePackageName);
            var actual = EditorPath.GetAssetPath(path);
            Assert.AreEqual(null, actual);
        }
        
        [Test]
        public void TryGetAssetPathTest1()
        {
            var result = EditorPath.TryGetAssetPath(Path.Combine(PlayerEnvironment.AssetsFolderName, FakePackageName), out _);
            Assert.IsFalse(result);
        }
        
        [Test]
        public void TryGetAssetPathTest2()
        {
            var result = EditorPath.TryGetAssetPath(Path.Combine(PlayerEnvironment.AssetsFolderName, TestAssetFolderName, TestAssetFileName), out _);
            Assert.IsTrue(result);
        }
        
        [Test]
        public void TryGetAssetPathTest3()
        {
            var result = EditorPath.TryGetAssetPath(Path.Combine(Directory.GetCurrentDirectory(), PlayerEnvironment.AssetsFolderName, TestAssetFolderName, TestAssetFileName), out _);
            Assert.IsTrue(result);
        }

        [Test]
        public void GetPackageAssetPathTest1()
        {
            var assetPath = EditorPath.GetPackageAssetPath(TestPackageName);
            var expected = Path.Combine(PlayerEnvironment.PackagesFolderName, TestPackageName);
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
            var basePath = Path.Combine(Directory.GetCurrentDirectory(), EditorEnvironment.LibraryFolderName, PackageCacheFolderName);
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