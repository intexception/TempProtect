using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dnlib.DotNet;
using TempProtect.Transform.Impl;

namespace TempProtect.Transform
{
    internal class TransformerManager
    {
        private List<Transformer> Transformers = new List<Transformer>();

        public TransformerManager(ModuleDefMD moduleBase)
        {
            Transformers.Add(new RenamerTransformer(moduleBase));
        }

        public List<Transformer> GetTransformers()
        {
            return Transformers;
        }
    }
}
