
using System.Dynamic;
using Assets.Scripts.Unity;
using Assets.Scripts.Unity.UI_New.InGame;
using BTD6.Script.Utils;
using IronPython.Hosting;

namespace BTD6.Script
{
    class PyEngine : DynamicObject
    {

        public static void RunScript(string s)
        {
            var engine = Python.CreateEngine();
            var source = engine.CreateScriptSourceFromString(s);
            var compiled = source.Compile();
            var scope = compiled.DefaultScope;
            scope.SetVariable("gameModel", Game.instance.model);
            scope.SetVariable("inGame", InGame.instance);
            scope.SetVariable("util", new Util());

            compiled.Execute(scope);
        }
    }
}
