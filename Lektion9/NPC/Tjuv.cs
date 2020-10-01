using System;

namespace Lektion9
{
    class Tjuv : Person {
        Stöldgods items;
        bool fängelse;
        DateTime släpps;

        public Tjuv(Position pos, Direction dir) : base(pos, dir)
        {
            Items = new Stöldgods();
            Släpps = DateTime.Now;
            Fängelse = false;
        }

        public void Fängsla() {
            Släpps = DateTime.Now.AddSeconds(30.0);
            Fängelse = true;
        }

        public void SläppFri() {
            Fängelse = false;
        }

        internal Stöldgods Items { get => items; set => items = value; }
        public bool Fängelse { get => fängelse; set => fängelse = value; }
        public DateTime Släpps { get => släpps; set => släpps = value; }
    }
}
