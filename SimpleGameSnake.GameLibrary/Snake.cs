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
        public Point PrevHead { get; private set; }
        public Point LastPart { get; private set; }
        public int TailLength { get; private set; }
        public Direction CurrentDirection { get; private set; }

        public Snake(int headX, int headY)
        {
            Head.X = headX;
            Head.Y = headY;
            TailLength = 2;
            LastPart.X = headX - TailLength;
            LastPart.Y = Head.Y;
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
    }
}
