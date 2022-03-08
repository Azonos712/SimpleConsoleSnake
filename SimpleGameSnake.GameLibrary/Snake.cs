using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleGameSnake.GameLibrary
{
    public class Snake
    {
        public Point Head { get; private set; }
        public List<Point> Body { get; private set; }
        public Point PrevHead { get; private set; }
        public Point LastPart { get; private set; }
        public int TailLength { get; private set; }
        public Direction CurrentDirection { get; private set; }

        public Snake(int headX, int headY)
        {
            Body = new List<Point>();
            Head = new Point(headX, headY);

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
            //TODO Проверка на противоположную сторону
            CurrentDirection = newDirection;
        }

        public void MoveSnake()
        {
            PrevHead = Head;
            var temp = Head;
            switch (CurrentDirection)
            {
                case Direction.Top:
                    temp.Y--;
                    break;
                case Direction.Bottom:
                    temp.Y++;
                    break;
                case Direction.Left:
                    temp.X--;
                    break;
                case Direction.Right:
                    temp.X++;
                    break;
            }
            Head = temp;

            LastPart = Body.Last();
            Body.RemoveAt(Body.Count - 1);
            Body.Insert(0, PrevHead);
        }
    }
}
