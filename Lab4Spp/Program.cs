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
            Console.WriteLine(foo.num);
            Console.WriteLine(foo.str);
            Console.WriteLine(bar.str);
            Console.ReadLine();
        }
    }
}
