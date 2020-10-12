
using Il2CppSystem;
using Il2CppSystem.Collections.Generic;
using Il2CppSystem.IO;
using Il2CppSystem.Net;
using Assets.Scripts.Unity;
using MelonLoader;

namespace BTD6.Script
{
    public class Main__ : MelonMod
    {
        public static bool hasRunInit = false;
        public static Dictionary<string, bool> pyMods = new Dictionary<string, bool>();
        public static Dictionary<string, bool> jsMods = new Dictionary<string, bool>(); // Not functional


        public override void OnApplicationStart()
        {
            if (!Directory.Exists("ScriptMods/Python"))
                Directory.CreateDirectory("ScriptMods/Python");

            if (Directory.Exists("PyMods"))
            {
                foreach (var moduleFile in Directory.GetFiles("PyMods", "*.py"))
                {
                    File.Create("ScriptMods/Python/" + moduleFile.Replace("PyMods/", ""));
                    File.WriteAllBytes("ScriptMods/Python/" + moduleFile.Replace("PyMods/", ""), File.ReadAllBytes(moduleFile));
                    File.Delete(moduleFile);
                }
                Directory.Delete("PyMods");
            }



            MelonLogger.Log("");

            if (!File.Exists("Mods/zIronPython.dll") || !File.Exists("Mods/zIronPython.Modules.dll") || !File.Exists("Mods/Microsoft.Scripting.dll") || !File.Exists("Mods/Microsoft.Dynamic.dll"))
            {
                Console.WriteLine("DOWNLOADING FILES! AFTER THEY ARE DONE, A MESSAGE WILL APPEAR WITH FURTHER INSTRUCTIONS!");
                WebClient web = new WebClient();
                web.DownloadFile("https://raw.githubusercontent.com/KosmicShovel/BTD6-Mods/master/BTD6.py/DLL/zIronPython.dll", "Mods/zIronPython.dll");
                web.DownloadFile("https://raw.githubusercontent.com/KosmicShovel/BTD6-Mods/master/BTD6.py/DLL/zIronPython.Modules.dll", "Mods/zIronPython.Modules.dll");
                web.DownloadFile("https://raw.githubusercontent.com/KosmicShovel/BTD6-Mods/master/BTD6.py/DLL/Microsoft.Scripting.dll", "Mods/Microsoft.Scripting.dll");
                web.DownloadFile("https://raw.githubusercontent.com/KosmicShovel/BTD6-Mods/master/BTD6.py/DLL/Microsoft.Dynamic.dll", "Mods/Microsoft.Dynamic.dll");
                Console.WriteLine("YOU MUST RESTART FOR PYMODS TO LOAD!");
                return;
            }


            MelonLogger.Log("Python Mods");
            foreach (var moduleFile in Directory.GetFiles(@"ScriptMods\Python", "*.py"))
            {
                pyMods.Add(File.ReadAllText(moduleFile), moduleFile.Contains("init"));
                MelonLogger.Log(moduleFile + " loaded!");
            }
        }

        public override void OnUpdate()
        {
            if (Game.instance == null)
                return;
            if (Game.instance.model == null)
                return;
            if (Game.instance.model.towers == null)
                return;

            foreach (var moduleFile in pyMods)
            {
                if (!moduleFile.Value)
                {
                    PyEngine.RunScript(moduleFile.Key);
                }
            }

            if (hasRunInit)
                return;

            foreach (var moduleFile in pyMods)
            {
                if (moduleFile.Value)
                {
                    PyEngine.RunScript(moduleFile.Key);
                }
            }

            hasRunInit = true;
        }
    }
}
