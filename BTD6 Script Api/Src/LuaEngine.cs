using System.Dynamic;
using BTD6.Script.Utils;
using System;
using Neo.IronLua;
using Console = System.Console;
using Random = System.Random;

namespace BTD6.Script
{
    class LuaEngine : DynamicObject
    {
        public static void RunScript(string s)
        {
            Lua lua = new Lua();

            var luaGlobal = lua.CreateEnvironment();
            dynamic dg = luaGlobal;
            dg.util = new Util();
            dg.print = new Func<string, bool>((a) =>
            {
                Console.WriteLine(a);
                return true;
            });
            var r = luaGlobal.DoChunk(s.Replace("\n", " ").Replace("\r", ""), "file_" + new Random().Next() + ".lua");
            Console.WriteLine(r[0]);
        }
    }
}
