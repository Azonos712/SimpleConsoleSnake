using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleGameSnake.ConsoleUI
{
    public class GameManager
    {
        public bool IsGameOver { get; private set; }

        public GameManager()
        {
            IsGameOver = false;
        }

        public void StopGame()
        {
            IsGameOver = true;
        }
    }
}
