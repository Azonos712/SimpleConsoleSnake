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

        public void Eat(Food food)
        {

        }

        public void ChangeDirection(Direction newDirection)
        {
            if (CurrentDirection == Direction.Top && newDirection == Direction.Bottom)
                return;

            if (CurrentDirection == Direction.Bottom && newDirection == Direction.Top)
                return;

            if (CurrentDirection == Direction.Right && newDirection == Direction.Left)
                return;

            if (CurrentDirection == Direction.Left && newDirection == Direction.Right)
                return;

            CurrentDirection = newDirection;
        }

        public void MoveSnake()
        {
            PrevHead = Head;

            switch (CurrentDirection)
            {
                case Direction.Top:
                    _head.Y--;
                    break;
                case Direction.Bottom:
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
