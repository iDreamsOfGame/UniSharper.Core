using System.IO;
using NUnit.Framework;
using UniSharper.UniSharper.Core.Runtime;

namespace UniSharper.Tests
{
    internal class PlayerPathTests
    {
        [Test]
        public void GetAssetPathTest()
        {
            const string folderName = "Test";
            var assetPath = PlayerPath.GetAssetPath(folderName);
            var expected = Path.Combine(PlayerEnvironment.AssetsFolderName, folderName);
            Assert.AreEqual(expected, assetPath);
        }

        [Test]
        public void GetPackagePathTest()
        {
            const string packageName = "com.xx.xxx";
            var assetPath = PlayerPath.GetPackagePath(packageName);
            var expected = Path.Combine(PlayerEnvironment.PackagesFolderName, packageName);
            Assert.AreEqual(expected, assetPath);
        }
    }
}