using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleGameSnake.GameLibrary
{
    public class Food
    {
        public Point Position { get; private set; }
        public int Value { get; private set; }
        private readonly Random _random;

        public Food(int value)
        {
            _random = new Random();
            Position = new Point(0, 0);
            Value = value;
        }

        public void CreateNewFood(int minX, int maxX, int minY, int maxY)
        {
            Position = new Point(_random.Next(minX, maxX), _random.Next(minY, maxY));
        }
    }
}
