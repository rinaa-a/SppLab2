using System;
using System.Collections.Generic;
using System.Text;

namespace Lab4Spp
{
    [DTO]
    class Bar
    {
        public int num;
        public double x;
        public string str;
        public Foo foo;
        private Bar()
        {
            this.num = 0;
            this.x = 1.0;
        }
    }
}
