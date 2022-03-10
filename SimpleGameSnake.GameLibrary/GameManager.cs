namespace SimpleGameSnake.GameLibrary
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

        public void MoveSnake(Snake snake, int width, int height)
        {
            snake.MoveSnake();

            ScreenEdgeCheck(snake, width, height);

            SelfBodyCheck(snake);

            FoodCheck(snake);
        }

        private void ScreenEdgeCheck(Snake snake, int width, int height)
        {
            if (snake.Head.X == 0 || snake.Head.X == width - 1)
                StopGame();

            if (snake.Head.Y == 0 || snake.Head.Y == height - 1)
                StopGame();
        }

        private void StopGame()
        {
            IsGameOver = true;
        }

        private void SelfBodyCheck(Snake snake)
        {
            if (snake.Body.Contains(snake.Head))
                StopGame();
        }

        private void FoodCheck(Snake snake)
        {
            if (snake.Head.X == Food.Position.X && snake.Head.Y == Food.Position.Y)
            {
                PutAwayFoodFromField();
                snake.Eat();
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