using NUnit.Framework;
using UnityEditor;

namespace UniSharperEditor.Tests
{
    public class ScriptingSymbolsUtilityTests
    {
        private const string DefineForTest = "DEFINE_TEST";

        private const string DefineForTest2 = "DEFINE_TEST_2";

        private BuildTarget TestBuildTarget => BuildTarget.Android;

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
        public void AddDefinesTest()
        {
            ScriptingSymbolsUtility.AddDefines(new[] { DefineForTest, DefineForTest2 });
            var expected = $"{DefineForTest}{ScriptingSymbolsUtility.DefinesSeparator}{DefineForTest2}";
            Assert.AreEqual(expected, ScriptingSymbolsUtility.ScriptingSymbolsForActiveBuildTarget);
        }

        [Test, Order(5)]
        public void RemoveDefinesTest()
        {
            ScriptingSymbolsUtility.RemoveDefines(new[] { DefineForTest, DefineForTest2 });
            Assert.AreEqual(string.Empty, ScriptingSymbolsUtility.ScriptingSymbolsForActiveBuildTarget);
        }

        [Test, Order(6)]
        public void ClearAllDefinesTest()
        {
            ScriptingSymbolsUtility.AddDefines(new[] { DefineForTest, DefineForTest2 });
            ScriptingSymbolsUtility.ClearAllDefines();
            Assert.AreEqual(string.Empty, ScriptingSymbolsUtility.ScriptingSymbolsForActiveBuildTarget);
        }

        [Test, Order(7)]
        public void AddDefineTest2()
        {
            ScriptingSymbolsUtility.AddDefine(TestBuildTarget, DefineForTest);
            Assert.True(ScriptingSymbolsUtility.ContainsDefine(TestBuildTarget, DefineForTest));
        }

        [Test, Order(8)]
        public void GetScriptingSymbolsTest()
        {
            var scriptingSymbols = ScriptingSymbolsUtility.GetScriptingSymbols(TestBuildTarget);
            Assert.True(scriptingSymbols.Equals(DefineForTest));
        }

        [Test, Order(9)]
        public void RemoveDefineTest2()
        {
            ScriptingSymbolsUtility.RemoveDefine(TestBuildTarget, DefineForTest);
            Assert.False(ScriptingSymbolsUtility.ContainsDefine(TestBuildTarget, DefineForTest));
        }

        [Test, Order(10)]
        public void AddDefinesTest2()
        {
            ScriptingSymbolsUtility.AddDefines(TestBuildTarget, new[] { DefineForTest, DefineForTest2 });
            var expected = $"{DefineForTest}{ScriptingSymbolsUtility.DefinesSeparator}{DefineForTest2}";
            Assert.AreEqual(expected, ScriptingSymbolsUtility.GetScriptingSymbols(TestBuildTarget));
        }

        [Test, Order(11)]
        public void RemoveDefinesTest2()
        {
            ScriptingSymbolsUtility.RemoveDefines(TestBuildTarget, new[] { DefineForTest, DefineForTest2 });
            Assert.AreEqual(string.Empty, ScriptingSymbolsUtility.GetScriptingSymbols(TestBuildTarget));
        }

        [Test, Order(12)]
        public void ClearAllDefinesTest2()
        {
            ScriptingSymbolsUtility.ClearAllDefines(TestBuildTarget);
            Assert.AreEqual(string.Empty, ScriptingSymbolsUtility.GetScriptingSymbols(TestBuildTarget));
        }
    }
}