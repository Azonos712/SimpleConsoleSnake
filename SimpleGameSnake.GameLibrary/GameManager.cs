using System.Threading;

namespace SimpleGameSnake.GameLibrary
{
    public class GameManager
    {
        public int FieldWidth { get; init; }
        public int FieldHeight { get; init; }
        public bool IsGameOver { get; private set; }
        public bool IsFoodOnField { get; private set; }
        public int Score { get; private set; }
        public Food Food { get; init; }
        public Snake Snake { get; init; }

        public GameManager(int width, int height)
        {
            FieldWidth = width;
            FieldHeight = height;
            Snake = new Snake(FieldWidth / 2, FieldHeight / 2);
            IsGameOver = false;
            Score = 0;
            Food = new Food(1);
        }

        public void CheckFoodOnField()
        {
            if (!IsFoodOnField)
                GenerateFoodOnField(1, FieldWidth - 1, 1, FieldHeight - 1);
        }

        private void GenerateFoodOnField(int minX, int maxX, int minY, int maxY)
        {
            Food.CreateNewFood(minX, maxX, minY, maxY);
            IsFoodOnField = true;
        }

        public void MoveSnake()
        {
            Snake.MoveSnake();

            GameChecks();
        }

        private void GameChecks()
        {
            if (Snake.IsReachTheEndOfField(FieldWidth, FieldHeight))
                StopGame();

            if (Snake.IsCrashIntoBody())
                StopGame();

            if (Snake.IsFoodAhead(Food.Position.X, Food.Position.Y))
                PutAwayFoodFromField();
        }

        private void StopGame()
        {
            IsGameOver = true;
        }

        private void PutAwayFoodFromField()
        {
            IsFoodOnField = false;
            Score += Food.Value;
        }
    }
}