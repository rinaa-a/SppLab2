using System;
using System.Collections.Generic;
using System.Text;

namespace Lab4Spp
{
    [DTO]
    class Foo
    {
        public int num;
        public string str;
        public List<int> list;
        public Bar bar;
        public List<int> values;
        public List<Bar> objects;
        public DateTime Date { get; set; }

        public Foo(int num, string str)
        {
            this.num = num;
            this.str = str;
        }
        private Foo()
        {
            this.num = 0;
            this.str = "Hello";
        }
    }
}
