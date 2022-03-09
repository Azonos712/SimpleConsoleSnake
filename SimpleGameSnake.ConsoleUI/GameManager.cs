using SimpleGameSnake.GameLibrary;
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
        public bool IsFoodOnField { get; private set; }
        public int Score { get; private set; }

        public Food Food { get; private set; }

        public GameManager()
        {
            IsGameOver = false;
            Score = 0;
            Food = new Food(1);
        }

        public void StopGame()
        {
            IsGameOver = true;
        }

        public void CreateFood(int minX, int maxX, int minY, int maxY)
        {
            Food.CreateNewFood(minX, maxX, minY, maxY);
            IsFoodOnField = true;
        }

        public void PutAwayFood()
        {
            IsFoodOnField = false;
            Score += Food.Value;
        }
    }
}
