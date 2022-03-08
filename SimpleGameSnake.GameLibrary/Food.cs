using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleGameSnake.GameLibrary
{
    public class Food
    {
        public int Value { get; private set; }

        public Food(int value)
        {
            Value = value;
        }

    }
}
