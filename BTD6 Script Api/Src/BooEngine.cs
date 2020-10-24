
using System;
using Boo.Lang.Compiler;
using Boo.Lang.Compiler.IO;
using System.Dynamic;
using MelonLoader;
using System.Reflection;
using Assets.Scripts.Models;
using Boo.Lang.Environments;
using Boo.Lang.Compiler.Steps;
using Boo.Lang.Compiler.TypeSystem.Services;
using Boo.Lang.Compiler.Pipelines;

namespace BTD6.Script
{
    class BooEngine : DynamicObject
    {
        public static void RunScript(string s)
        {
            CompileToMemory ctm = new CompileToMemory();
            BooCompiler compiler = new BooCompiler();
            compiler.Parameters.Input.Add(new StringInput("BooFile_Module", s));
            compiler.Parameters.Pipeline = ctm;
            compiler.Parameters.Environment = new ClosedEnvironment(ctm);
            compiler.Parameters.Ducky = true;
            compiler.Parameters.AddAssembly(typeof(GameModel).Assembly);

            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                compiler.Parameters.References.Add(assembly);
            }

            CompilerContext context = compiler.Run();
            if (context.GeneratedAssembly == null)
            {
                foreach (CompilerError error in context.Errors)
                    MelonLogger.LogError(error.ToString());
                return;
            }

            Type[] types = context.GeneratedAssembly.GetTypes();
            Type scriptModule = types[types.Length - 1];
            MethodInfo mainEntry = scriptModule.Assembly.EntryPoint;
            mainEntry.Invoke(null, new object[mainEntry.GetParameters().Length]);
        }
    }
}
