using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dnlib.DotNet;

namespace TempProtect.Transform.Impl
{
    internal sealed class RenamerTransformer : Transformer 
    {
        public RenamerTransformer(ModuleDefMD module) : base(module, "Renamer", TransformType.RENAME)
        {
            this.Module = module;
        }

        public override void Transform(ModuleDefMD module)
        {
            int index = 0;
            foreach (var type in module.Types)
            {
                ++index;

                type.Namespace = "";
                type.Name = "TEMPPROTECT0" + index;
            }
        }
    }
}
