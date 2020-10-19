using NUnit.Framework;
using Lab4Spp;
using System;

namespace NUnitTest
{
    public class A { }
    [DTO]
    public class C 
    {
        public B b;
    }
    [DTO]
    public class B
    {
        public A a;
        public C c;
    }
    public class Tests
    {
        private Faker faker;
        [SetUp]
        public void Setup()
        {
            faker = new Faker(); 
        }

        [Test]
        public void TestCreate()
        {
            var obj = faker.Create<B>();
            Assert.IsNotNull(obj);
            Assert.IsNull(obj.a);
            Assert.IsNull(faker.Create<String>());
            Assert.IsNotNull(obj.c);
            Assert.AreNotEqual(obj.c.b, obj);
        }
    }
}