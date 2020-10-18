using GeneratorBase;
using System;

namespace StringGenerator
{
    public class StringGenerator : IGenerator
    {
        private Random random = new Random();
        public bool CanGenerate(Type type)
        {
            return type == typeof(string);
        }

        public object Generate(Type type)
        {
            string str = "";
            byte count = (byte)random.Next(0, 50);
            for (int i = 0; i < count; i++)
            {
                str += (char)random.Next('A', 'z');
            }
            return str;
        }
    }
}
