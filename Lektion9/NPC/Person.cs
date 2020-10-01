namespace Lektion9
{
    class Person
    {
        Position position;
        Direction direction;

        public Person(Position pos, Direction dir)
        {
            this.Direction = dir;
            this.Position = pos;
        }

        public void Move(int width, int height)
        {
            if (Position.X + Direction.X < 0 || Position.X + Direction.X >= width)
            {

                if (Position.X + Direction.X < 0)
                    Position.X = width - 1;
                if (Position.X + Direction.X >= width)
                    Position.X = 0;
            }
            else
            {
                Position.X += Direction.X;
            }

            if (Position.Y + Direction.Y < 0 || Position.Y + Direction.Y >= height)
            {
                if (Position.Y + Direction.Y < 0)
                    Position.Y = height - 1;
                if (Position.Y + Direction.Y >= height)
                    Position.X = 0;
            }
            else
            {
                Position.Y += Direction.Y;
            }


        }

        public Position Position { get => position; set => position = value; }
        public Direction Direction { get => direction; set => direction = value; }
    }

}
