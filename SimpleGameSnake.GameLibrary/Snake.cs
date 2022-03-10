using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleGameSnake.GameLibrary
{
    public class Snake
    {
        private Point _head;
        public Point Head { get => _head; }
        public List<Point> Body { get; private set; }
        public Point PrevHead { get; private set; }
        public Point LastPart { get; private set; }
        public int TailLength { get; private set; }
        public Direction CurrentDirection { get; private set; }

        public Snake(int headX, int headY)
        {
            Body = new List<Point>();
            _head = new Point(headX, headY);

            TailLength = 2;
            //LastPart = new Point(headX - TailLength, Head.Y);

            Body.Add(new Point(headX - (TailLength - 1), Head.Y));
            Body.Add(new Point(headX - TailLength, Head.Y));

            CurrentDirection = Direction.Right;
        }

        public void Eat()
        {
            Body.Add(LastPart);
        }

        public void ChangeDirection(Direction newDirection)
        {
            if (!IsPossibleToChangeDirection(newDirection))
                return;

            CurrentDirection = newDirection;
        }

        private bool IsPossibleToChangeDirection(Direction newDirection)
        {
            return newDirection switch
            {
                Direction.Down when CurrentDirection == Direction.Up => false,
                Direction.Up when CurrentDirection == Direction.Down => false,
                Direction.Left when CurrentDirection == Direction.Right => false,
                Direction.Right when CurrentDirection == Direction.Left => false,
                _ => true
            };
        }

        public void MoveSnake()
        {
            PrevHead = Head;

            switch (CurrentDirection)
            {
                case Direction.Up:
                    _head.Y--;
                    break;
                case Direction.Down:
                    _head.Y++;
                    break;
                case Direction.Left:
                    _head.X--;
                    break;
                case Direction.Right:
                    _head.X++;
                    break;
            }

            LastPart = Body.Last();
            Body.RemoveAt(Body.Count - 1);
            Body.Insert(0, PrevHead);
        }
    }
}
