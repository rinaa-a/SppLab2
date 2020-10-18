using GeneratorBase;
using System;
using System.Linq;

namespace BasicValueTypesGenerator
{
    public class BasicValueTypesGenerator : IGenerator
    {
        private Random random = new Random();
        long LongRandom(long min, long max, Random rand)
        {
            byte[] buf = new byte[8];
            rand.NextBytes(buf);
            long longRand = BitConverter.ToInt64(buf, 0);
            return (Math.Abs(longRand % (max - min)) + min);
        }
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
            return possibleTypes.Contains(type);
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
                case TypeCode.Int32: return random.Next();
                case TypeCode.UInt32: return (uint)random.Next();
                case TypeCode.Int64: return LongRandom(long.MinValue, long.MaxValue, random);
                case TypeCode.UInt64: return (ulong)LongRandom(long.MinValue, long.MaxValue, random);
                case TypeCode.Single: return (float)random.NextDouble();
                case TypeCode.Double: return (double)random.NextDouble();
                case TypeCode.Decimal: return new decimal(random.Next(), random.Next(), random.Next(0x204FCE5E), false, 0); 
                case TypeCode.SByte: return (sbyte)random.Next();
                default: return null;
            }
        }
    }
}
