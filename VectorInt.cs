namespace DoctorFe.Executable
{
    public struct VectorInt
    {
        public int X;
        public int Y;

        public VectorInt(int x, int y)
        {
            X = x;
            Y = y;
        }

        public VectorInt(VectorInt v) : this(v.X, v.Y) { }

        public VectorInt() : this(0, 0) { }

        public override string ToString()
        {
            return string.Format("({0}, {1})", X, Y);
        }

        public static VectorInt operator +(VectorInt x) => new(x);
        public static VectorInt operator -(VectorInt x) => new(-x.X, -x.Y);

        public static VectorInt operator +(VectorInt x, VectorInt y) => new(x.X + y.X, x.Y + y.Y);
        public static VectorInt operator -(VectorInt x, VectorInt y) => x + -y;

        public static explicit operator VectorInt(int x) => new(x, 0);

        public static explicit operator VectorInt(Direction d)
        {
            return d switch
            {
                Direction.Up => new(0, -1),
                Direction.Down => new(0, 1),
                Direction.Left => new(-1, 0),
                Direction.Right => new(1, 0),
                _ => new (0, 0),
            };
        }
    }
}