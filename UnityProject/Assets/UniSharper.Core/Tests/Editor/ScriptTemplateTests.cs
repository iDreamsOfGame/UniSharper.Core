using NUnit.Framework;

namespace UniSharperEditor.Tests
{
    public class ScriptTemplateTests
    {
        [Test]
        public void LoadScriptTemplateFileTest()
        {
            const string packageName = "io.github.idreamsofgame.resharp.core";
            var content = ScriptTemplate.LoadScriptTemplateFile("package", packageName);
            Assert.AreNotEqual(string.Empty, content);
        }
    }
}