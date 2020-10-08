
using System.Dynamic;
using Assets.Scripts.Models;
using Assets.Scripts.Unity.UI_New.InGame;
using BTD6.py.Utils;
using IronPython.Hosting;

namespace BTD6.py
{
    class PyEngine : DynamicObject
    {
        public static void RunScript(string s, GameModel model)
        {
            var engine = Python.CreateEngine();
            var scope = engine.CreateScope();
            scope.SetVariable("gameModel", model);
            scope.SetVariable("inGame", InGame.instance);
            scope.SetVariable("util", new Util());

            var source = engine.CreateScriptSourceFromFile(s);
            var compiled = source.Compile();

            compiled.Execute(scope);
        }
    }
}
