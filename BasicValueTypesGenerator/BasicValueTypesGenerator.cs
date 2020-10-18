using GeneratorBase;
using System;
using System.Linq;

namespace BasicValueTypesGenerator
{
    public class BasicValueTypesGenerator : IGenerator
    {
        private Random random = new Random();
        private Type[] possibleTypes =
            {
                typeof(byte),
                typeof(sbyte),
                typeof(short),
                typeof(ushort),
                typeof(int),
                typeof(uint),
                typeof(long),
                typeof(ulong),
                typeof(decimal),
                typeof(bool),
                typeof(char),
                typeof(float),
                typeof(double)
        };
        public bool CanGenerate(Type type)
        {
            if (possibleTypes.Contains(type))
                return true;
            else
                return false;
        }

        public object Generate(Type type)
        {
            switch(Type.GetTypeCode(type))
            {
                case TypeCode.Boolean: return random.Next(0, 2) == 0;    
                case TypeCode.Byte: return (byte)random.Next();
                case TypeCode.Char: return (char)random.Next('A', 'z');
                case TypeCode.Int16: return (short)random.Next();
                case TypeCode.UInt16: return (ushort)random.Next();
                case TypeCode.Int32: return (int)random.Next();
                case TypeCode.UInt32: return (uint)random.Next();
                case TypeCode.Int64: return (long)random.Next();
                case TypeCode.UInt64: return (ulong)random.Next();
                case TypeCode.Single: return (float)random.NextDouble();
                case TypeCode.Double: return (double)random.NextDouble();
                case TypeCode.Decimal: return (decimal)random.NextDouble();
                case TypeCode.SByte: return (sbyte)random.Next();
                default: return null;
            }
        }
    }
}
