using System;
using System.Collections.Generic;
using System.Linq;

namespace DoctorFe.Executable
{
    public class Befunge
    {
        public char[,] Data { get; private set; }
        public bool IsFinished { get; private set; }

        private VectorInt Position = new(0, 0);
        private VectorInt Dir = new(1, 0);
        private VectorInt Size;

        private readonly Stack<int> Stack = new();
        private readonly Random rnd = new();

        private bool IsCharPushing = false;

        public Befunge(params string[] data)
        {
            Size = new VectorInt(data.Max(a => a.Length), data.Length);
            Data = new char[Size.X,Size.Y];
            for (int i = 0; i < Size.Y; i++)
            {
                for (int j = 0; j < data[i].Length; j++)
                {
                    Data[j, i] = data[i][j];
                }
            }
        }

        public void InterpretToEnd()
        {
            while (!IsFinished)
            {
                Interpret();
            }
        }

        public void Interpret()
        {
            char c = GetChar();
            if (IsCharPushing)
            {
                if (c == '"')
                {
                    IsCharPushing = false;
                } else
                {
                    Stack.Push(c);
                }
            }
            else
            {
                int tmp;
                switch (c)
                {
                    //---- プログラム制御 ----
                    case '@':
                        IsFinished = true;
                        return;
                    case '#':
                        Position += Dir;
                        break;
                    //---- 条件分岐 ----
                    case '|':
                        tmp = Pop();
                        if (tmp == 0)
                        {
                            goto case 'v';
                        }
                        goto case '^';
                    case '_':
                        tmp = Pop();
                        if (tmp == 0)
                        {
                            goto case '>';
                        }
                        goto case '<';
                    //---- 方向 ----
                    case '^':
                        SetDirection(Direction.Up);
                        break;
                    case 'v':
                        SetDirection(Direction.Down);
                        break;
                    case '<':
                        SetDirection(Direction.Left);
                        break;
                    case '>':
                        SetDirection(Direction.Right);
                        break;
                    case '?':
                        SetDirection((Direction)rnd.Next(0, 4));
                        break;
                    //---- 加減乗除ほか ----
                    case '+':
                        Compute((b, a) => a + b);
                        break;
                    case '-':
                        Compute((b, a) => a - b);
                        break;
                    case '*':
                        Compute((b, a) => a * b);
                        break;
                    case '/':
                        Compute((b, a) => a / b);
                        break;
                    case '%':
                        Compute((b, a) => a % b);
                        break;
                    case '`':
                        Compute((b, a) => a > b ? 1 : 0);
                        break;
                    case '!':
                        tmp = Pop();
                        Stack.Push(tmp == 0 ? 1 : 0);
                        break;
                    //---- 入出力
                    case '&':
                        bool r = int.TryParse(Console.ReadLine(), out int s);
                        Stack.Push(r ? s : 0);
                        break;
                    case '~':
                        Stack.Push((byte)Console.ReadKey().KeyChar);
                        break;
                    case '.':
                        Console.Write("{0} ", Pop());
                        break;
                    case ',':
                        Console.Write("{0}", (char)Pop());
                        break;
                    //---- スタック操作 ----
                    case ':':
                        Stack.Push(Stack.Count == 0 ? 0 : Stack.Peek());
                        break;
                    case '\\':
                        Compute((a, b) => { Stack.Push(a); return b; });
                        break;
                    case '$':
                        _ = Pop();
                        break;
                    //---- メモリ操作 ----
                    case 'g':
                        {
                            int y = Pop();
                            int x = Pop();
                            Stack.Push(Data[x, y]);
                        }
                        break;
                    case 'p':
                        {
                            int y = Pop();
                            int x = Pop();
                            int v = Pop();
                            Replace(new(x, y), (char)v);
                        }
                        break;
                    //---- その他 ----
                    case '"':
                        IsCharPushing = true;
                        break;
                    default:
                        if ('0' <= c && c <= '9')
                        {
                            Stack.Push(c - 48);
                        }
                        break;
                }
            }
            Position += Dir;
        }

        private int Pop()
        {
            return Stack.Count == 0 ? 0 : Stack.Pop();
        }

        private void Compute(Func<int, int, int> f)
        {
            int a = Pop();
            int b = Pop();
            Stack.Push(f(b, a));
        }

        private char GetChar()
        {
            Position.X %= Size.X;
            Position.Y %= Size.Y;
            return Data[Position.X, Position.Y];
        }

        private void SetDirection(Direction d)
        {
            Dir = (VectorInt)d;
        }

        private void Replace(VectorInt p, char c)
        {
            Data[p.X, p.Y] = c;
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