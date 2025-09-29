using NUnit.Framework;

namespace UniSharper.Tests
{
    internal class PlayerEnvironmentTests
    {
        [Test]
        public void Is64BitProcessTest()
        {
            Assert.AreEqual(true, PlayerEnvironment.Is64BitProcess);
        }
    }
}