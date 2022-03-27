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

        string GetRandomNumber(int length)
        {
            const string chars = "1234567890";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[new Random().Next(s.Length)]).ToArray());
        }

        public enum RenameVariant
        {
            TEMPPROTECT,
            ALPHABET
        }

        public string GetRenamed(RenameVariant variant, int index)
        {
	        switch (variant)
	        {
		        case RenameVariant.TEMPPROTECT:
		        {
			        return "TEMPPROTECT0" + index;
		        }
		        case RenameVariant.ALPHABET:
		        {
			        return IndexToColumn(index).ToLower();
		        }
                default:
	                return "";
	        }
        }

        private const int ColumnBase = 26;
        private const int DigitMax = 7; 
        private const string Digits = "abcdefghijklmnopqrstxyz";

        public static string IndexToColumn(int index)
        {
	        if (index <= 0)
		        throw new IndexOutOfRangeException("index must be a positive number");

	        if (index <= ColumnBase)
		        return Digits[index - 1].ToString();

	        var sb = new StringBuilder().Append(' ', DigitMax);
	        var current = index;
	        var offset = DigitMax;
	        while (current > 0)
	        {
		        sb[--offset] = Digits[--current % ColumnBase];
		        current /= ColumnBase;
	        }
	        return sb.ToString(offset, DigitMax - offset);
        }

        public override void Transform(ModuleDefMD module)
        {
            int typeIndex = 0, methodIndex = 0, fieldIndex = 0;
            foreach (var type in module.Types)
            {
                ++typeIndex;

                type.Namespace = "";
                type.Name = GetRenamed(RenameVariant.ALPHABET, typeIndex);

                foreach (var method in type.Methods)
                {
	                if (!method.IsRuntimeSpecialName && !method.DeclaringType.IsForwarder)
	                {
		                ++methodIndex;
		                method.Name = GetRenamed(RenameVariant.ALPHABET, methodIndex);
		                foreach (var param in method.Parameters)
		                {
			                param.Name = GetRandomNumber(5);
		                }
	                }
                }
                foreach (var field in type.Fields)
                {
                    ++fieldIndex;
                    field.Name = GetRenamed(RenameVariant.ALPHABET, fieldIndex);
                }
            }
        }
    }
}
