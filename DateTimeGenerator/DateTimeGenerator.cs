using GeneratorBase;
using System;

namespace DateTimeGenerator
{
    public class DateTimeGenerator : IGenerator
    {
        private Random random = new Random();
        public bool CanGenerate(Type type)
        {
            return type == typeof(DateTime);
        }

        public object Generate(Type type)
        {
            DateTime start = new DateTime(1995, 1, 1);
            int range = (DateTime.Today - start).Days;
            start.AddDays(random.Next(range));
            return start;
        }
    }
}
