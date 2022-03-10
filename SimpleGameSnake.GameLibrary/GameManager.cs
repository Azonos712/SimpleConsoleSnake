namespace SimpleGameSnake.GameLibrary
{
    public class GameManager
    {
        public bool IsGameOver { get; private set; }
        public bool IsFoodOnField { get; private set; }
        public int Score { get; private set; }
        public Food Food { get; init; }
        public Snake Snake { get; init; }

        public GameManager(int x, int y)
        {
            Snake = new Snake(x, y);
            IsGameOver = false;
            Score = 0;
            Food = new Food(1);
        }

        public void MoveSnake(int width, int height)
        {
            Snake.MoveSnake();

            ScreenEdgeCheck(width, height);

            SelfBodyCheck();

            FoodCheck();
        }

        private void ScreenEdgeCheck(int width, int height)
        {
            if (Snake.Head.X == 0 || Snake.Head.X == width - 1)
                StopGame();

            if (Snake.Head.Y == 0 || Snake.Head.Y == height - 1)
                StopGame();
        }

        private void StopGame()
        {
            IsGameOver = true;
        }

        private void SelfBodyCheck()
        {
            if (Snake.Body.Contains(Snake.Head))
                StopGame();
        }

        private void FoodCheck()
        {
            if (Snake.Head.X == Food.Position.X && Snake.Head.Y == Food.Position.Y)
            {
                PutAwayFoodFromField();
                Snake.Eat();
            }
        }

        private void PutAwayFoodFromField()
        {
            IsFoodOnField = false;
            Score += Food.Value;
        }

        public void GenerateFoodOnField(int minX, int maxX, int minY, int maxY)
        {
            Food.CreateNewFood(minX, maxX, minY, maxY);
            IsFoodOnField = true;
        }
    }
}