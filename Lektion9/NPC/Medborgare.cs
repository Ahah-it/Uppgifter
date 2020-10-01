namespace Lektion9
{
    class Medborgare : Person {
       Behörigheter items;

        public Medborgare(Position pos, Direction dir) : base(pos, dir)
        {
            Items = new Behörigheter();
        }

        internal Behörigheter Items { get => items; set => items = value; }

    }
}
