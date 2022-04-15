using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace DoctorFe.Executable
{
    class BefungeInterpreter
    {
        static void Main()
        {
            //
        }
    }

    public class Befunge
    {
        public string[] Data { get; private set; }
        private Vector2 Position = new(0, 0);
        private Vector2 Dir = new(1, 0);
        private Queue<int> Stack = new();

        public Befunge(string[] data)
        {
            Data = data;
        }

        public void Interpret()
        {
            char c = GetChar();
        }

        private char GetChar()
        {
            return Data[(int)Position.Y][(int)Position.X];
        }

        private void SetDirection(Direction d)
        {
            Position = d switch
            {
                Direction.Up => new(0, -1),
                Direction.Down => new(0, 1),
                Direction.Left => new(-1, 0),
                Direction.Right => new(1, 0),
                _ => Position,
            };
        }

        private void Replace(Vector2 p, int c)
        {
            //
        }
    }

    public enum Direction
    {
        Up,
        Down,
        Left,
        Right,
    }
}