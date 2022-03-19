using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dnlib.DotNet;

namespace TempProtect.Transform
{
    internal abstract class Transformer
    {

        public ModuleDefMD Module { get; set; }
        public string Name { get; set; }
        public TransformType Type { get; set; }

        public Transformer(ModuleDefMD module, string name, TransformType type)
        {
            this.Module = module;
            this.Name = name;
            this.Type = type;
        }


        public abstract void Transform(ModuleDefMD module);

        public enum TransformType
        {
            RENAME,
            FLOW,
            OTHER
        }
    }
}
