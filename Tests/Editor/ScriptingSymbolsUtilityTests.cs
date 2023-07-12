using NUnit.Framework;
using UnityEditor;

namespace UniSharperEditor.Tests
{
    public class ScriptingSymbolsUtilityTests
    {
        private const string DefineForTest = "DEFINE_TEST";

        [Test, Order(1)]
        public void AddDefineTest()
        {
            ScriptingSymbolsUtility.AddDefine(DefineForTest);
            Assert.True(ScriptingSymbolsUtility.ContainsDefine(DefineForTest));
        }
        
        [Test, Order(2)]
        public void ScriptingSymbolsForActiveBuildTargetTest()
        {
            var scriptingSymbols = ScriptingSymbolsUtility.ScriptingSymbolsForActiveBuildTarget;
            Assert.True(scriptingSymbols.Equals(DefineForTest));
        }
        
        [Test, Order(3)]
        public void RemoveDefineTest()
        {
            ScriptingSymbolsUtility.RemoveDefine(DefineForTest);
            Assert.False(ScriptingSymbolsUtility.ContainsDefine(DefineForTest));
        }

        [Test, Order(4)]
        public void AddDefineTest2()
        {
            ScriptingSymbolsUtility.AddDefine(BuildTarget.Android, DefineForTest);
            Assert.True(ScriptingSymbolsUtility.ContainsDefine(BuildTarget.Android, DefineForTest));
        }
        
        [Test, Order(5)]
        public void GetScriptingSymbolsTest()
        {
            var scriptingSymbols = ScriptingSymbolsUtility.GetScriptingSymbols(BuildTarget.Android);
            Assert.True(scriptingSymbols.Equals(DefineForTest));
        }

        [Test, Order(6)]
        public void RemoveDefineTest2()
        {
            ScriptingSymbolsUtility.RemoveDefine(BuildTarget.Android, DefineForTest);
            Assert.False(ScriptingSymbolsUtility.ContainsDefine(BuildTarget.Android, DefineForTest));
        }
    }
}