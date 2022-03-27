using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using dnlib.DotNet;
using TempProtect.Transform;
using TempProtect.Transform.Impl;

namespace TempProtect
{
    internal class Program
    {
	    private static TransformerManager managerInstance ;

        static void Main(string[] args)
        {
	        
	        ModuleDefMD baseModule;

            Console.WriteLine("Temp protect");
            Console.Write("Path to the file you want to obfuscate: ");
            string pathToAssembly = Console.ReadLine();
            baseModule = ModuleDefMD.Load(pathToAssembly);
            managerInstance = new TransformerManager(baseModule);
            managerInstance.GetTransformers().ForEach(transformer => transformer.Transform(baseModule));
            baseModule.Write(pathToAssembly + "-protected.exe");

        }
    }
}
