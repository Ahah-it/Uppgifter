namespace Lektion9
{
    class Vec2D
    {
        private int x;
        private int y;

        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }

        public Vec2D(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static Vec2D operator +(Vec2D v1, Vec2D v2) => new Vec2D(v1.X + v2.X, v1.Y + v2.Y);
        public static bool operator ==(Vec2D v1, Vec2D v2) => v1.X == v2.X && v1.Y == v2.Y;
        public static bool operator !=(Vec2D v1, Vec2D v2) => v1.X != v2.X || v1.Y != v2.Y;

    }
}
