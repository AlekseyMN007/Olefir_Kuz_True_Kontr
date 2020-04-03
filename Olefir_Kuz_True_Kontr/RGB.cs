using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Olefir_Kuz_True_Kontr
{
    class RGB
    {

        private int _r;
        private int _g;
        private int _b;

        public int R { get => _r; set => _r = value; }
        public int G { get => _g; set => _g = value; }
        public int B { get => _b; set => _b = value; }

        public RGB (int b,int g, int r)
        {
            _r = r;
            _b = b;
            _g = g;
        }

    }
}
