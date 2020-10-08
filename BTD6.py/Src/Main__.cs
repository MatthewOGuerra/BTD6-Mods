
using System;
using System.IO;
using System.Net;
using Assets.Scripts.Unity;
using MelonLoader;

namespace BTD6.py
{
    public class Main__ : MelonMod
    {
        public static bool hasRunInit = false;
        public override void OnApplicationStart()
        {
            if (!Directory.Exists("PyMods"))
                Directory.CreateDirectory("PyMods");

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

            foreach (var moduleFile in Directory.GetFiles("PyMods", "*.py"))
            {
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

            foreach (var moduleFile in Directory.GetFiles("PyMods", "*.py"))
            {
                if (moduleFile.Contains("init") && !hasRunInit)
                {
                    PyEngine.RunScript(moduleFile, Game.instance.model);
                }
                else if (!moduleFile.Contains("init"))
                {
                    PyEngine.RunScript(moduleFile, Game.instance.model);
                }
            }

            hasRunInit = true;
        }
    }
}
