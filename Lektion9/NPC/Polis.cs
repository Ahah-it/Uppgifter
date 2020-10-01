namespace Lektion9
{
    class Polis : Person {
       Beslagtaget items;

        public Polis(Position pos, Direction dir) : base(pos, dir)
        {
           Items = new Beslagtaget();

        }

        internal Beslagtaget Items { get => items; set => items = value; }
    }
}
