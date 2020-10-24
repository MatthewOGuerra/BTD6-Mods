
using Il2CppSystem;
using System.Collections.Generic;
using Il2CppSystem.IO;
using Assets.Scripts.Unity;
using MelonLoader;
using System.Net;
using System.Threading;
using BTD6.Script.Utils;
using RuFramework;
using UnityEngine;

namespace BTD6.Script
{
    public class Main__ : MelonMod
    {
        public static bool hasRunInit = false;
        public static Dictionary<string, bool> pyMods = new Dictionary<string, bool>();
        public static Dictionary<string, bool> luaMods = new Dictionary<string, bool>();
        public static Dictionary<string, bool> booMods = new Dictionary<string, bool>();

        private static List<string> requiredDependancies = new List<string>
        {
            "zIronPython.dll", "zIronPython.Modules.dll", "Microsoft.Scripting.dll", "Microsoft.Dynamic.dll", "zNeo.Lua.dll"
        };

        public override void OnApplicationStart()
        {
            if (!Directory.Exists("ScriptMods/Python"))
                Directory.CreateDirectory("ScriptMods/Python");

            if (!Directory.Exists("ScriptMods/Lua"))
                Directory.CreateDirectory("ScriptMods/Lua");

            if (!Directory.Exists("ScriptMods/Boo"))
                Directory.CreateDirectory("ScriptMods/Boo");

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

            foreach (var tmp in requiredDependancies)
            {
                if (!File.Exists(tmp))
                {
                    MelonLogger.Log("DOWNLOADING FILES! AFTER THEY ARE DONE, A MESSAGE WILL APPEAR WITH FURTHER INSTRUCTIONS!");
                    Download();
                    Thread.Sleep(10000);
                    Application.Quit(0);
                    return;
                }
            }


            MelonLogger.Log("Python Mods");
            foreach (var moduleFile in Directory.GetFiles(@"ScriptMods\Python", "*.py"))
            {
                pyMods.Add(File.ReadAllText(moduleFile), moduleFile.Contains("init"));
                MelonLogger.Log(moduleFile + " loaded!");
            }

            MelonLogger.Log("Lua Mods");
            foreach (var moduleFile in Directory.GetFiles(@"ScriptMods\Lua", "*.lua"))
            {
                luaMods.Add(File.ReadAllText(moduleFile), moduleFile.Contains("init"));
                MelonLogger.Log(moduleFile + " loaded!");
            }

            MelonLogger.Log("Boo Mods");
            foreach (var moduleFile in Directory.GetFiles(@"ScriptMods\Boo", "*.boo"))
            {
                booMods.Add(File.ReadAllText(moduleFile), moduleFile.Contains("init"));
                MelonLogger.Log(moduleFile + " loaded!");
            }
        }

        private void Download()
        {

            RuProgressBar ruProgressBar = new RuProgressBar(Text: "Downloading Files");
            ThreadPool.QueueUserWorkItem(new WaitCallback(Downloader), ruProgressBar);
            ruProgressBar.ShowDialog();
        }

        public void Downloader(object status)
        {
            try
            {
                IProgressCallback callback = status as IProgressCallback;
                callback.Begin(0, requiredDependancies.Count);
                WebClient web = new WebClient();
                for (int i = 0; i < requiredDependancies.Count; i++)
                {
                    if (!File.Exists("Mods/" + requiredDependancies[i]))
                    {
                        MelonLogger.Log("Downloading " + requiredDependancies[i]);
                        web.DownloadFileTaskAsync("https://raw.githubusercontent.com/KosmicShovel/BTD6-Mods/master/BTD6.py/DLL/" + requiredDependancies[i], "Mods/" + requiredDependancies[i]).GetAwaiter().GetResult();
                        MelonLogger.Log("Downloaded " + requiredDependancies[i] + "!");
                        Console.WriteLine("");
                    }
                    callback.StepTo(i);
                }
                callback.End();
                MelonLogger.Log("THE WIZARD WILL NOW RESTART SO YOU CAN USE THE SCRIPT MODS!");
            }
            catch (System.FormatException) {}
        }

        public override void OnUpdate()
        {
            if (Game.instance == null)
                return;
            if (Game.instance.model == null)
                return;
            if (Game.instance.model.towers == null)
                return;
            if (Util.instance.gameModel == null)
                MelonLogger.LogError("Utils gameModel is null while the game isnt! This isnt good!");

            foreach (var moduleFile in pyMods)
            {
                if (!moduleFile.Value)
                {
                    PyEngine.RunScript(moduleFile.Key);
                }
            }

            foreach (var moduleFile in luaMods)
            {
                if (!moduleFile.Value)
                {
                    LuaEngine.RunScript(moduleFile.Key);
                }
            }

            foreach (var moduleFile in booMods)
            {
                if (!moduleFile.Value)
                {
                    BooEngine.RunScript(moduleFile.Key);
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

            foreach (var moduleFile in luaMods)
            {
                if (moduleFile.Value)
                {
                    LuaEngine.RunScript(moduleFile.Key);
                }
            }

            foreach (var moduleFile in booMods)
            {
                if (moduleFile.Value)
                {
                    BooEngine.RunScript(moduleFile.Key);
                }
            }

            hasRunInit = true;
        }
    }
}
