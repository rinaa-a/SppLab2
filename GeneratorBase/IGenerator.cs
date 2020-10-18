using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Text;

namespace GeneratorBase
{
    public interface IGenerator
    {
        object Generate(Type type);
        bool CanGenerate(Type type);
    }
}
