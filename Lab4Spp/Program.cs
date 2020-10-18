using System;

namespace Lab4Spp
{
    class Program
    {
        static void Main(string[] args)
        {
            var faker = new Faker();
            Foo foo = faker.Create<Foo>();
            Bar bar = faker.Create<Bar>();
            Console.WriteLine($"foo.num: {foo.num}");
            Console.WriteLine($"foo.str: {foo.str}");
            Console.WriteLine($"foo.bar.str: {foo.bar.str}");
            Console.WriteLine($"bar.str: {bar.str}");
            Console.WriteLine($"foo.Date: {foo.Date}");
            Console.WriteLine("foo.values:");
            foo.values.ForEach(v => Console.WriteLine(v));
            Console.WriteLine("foo.objects:");
            foo.objects.ForEach(o => Console.WriteLine(o));
            Console.ReadLine();
        }
    }
}
