using SimpleGameSnake.GameLibrary;

namespace SimpleGameSnake.ConsoleUI
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            ConsoleSettings settings = new ConsoleSettings();
            ConsoleGraphics graphics = new ConsoleGraphics();
            Snake snake = new Snake(settings.FieldWidth / 2, settings.FieldHeight / 2);
            GameManager game = new GameManager();

            Console.WriteLine("Press any key to start...");
            Console.ReadKey();

            StartGame(game, settings, graphics, snake);
            await MainLoop(game, settings, graphics, snake);

            Console.WriteLine("Game Over");
            Console.ReadKey();
        }

        private static void StartGame(GameManager game, ConsoleSettings settings, ConsoleGraphics graphics, Snake snake)
        {
            Console.Clear();
            graphics.DisplayField(settings.FieldWidth, settings.FieldHeight);
            graphics.DisplaySnake(snake);
            Task.Run(() => PressedKeyListener.Instance.ListenKeys(game, snake));
        }

        private static async Task MainLoop(GameManager game, ConsoleSettings settings, ConsoleGraphics graphics, Snake snake)
        {
            while (!game.IsGameOver)
            {
                if (!game.IsFoodOnField)
                    game.GenerateFoodOnField(1, settings.FieldWidth - 1, 1, settings.FieldHeight - 1);

                graphics.DisplayFood(game.Food.Position.X, game.Food.Position.Y);
                game.MoveSnake(snake, settings.FieldWidth, settings.FieldHeight);
                graphics.DisplaySnakeStep(snake, game);
                graphics.DisplayScoreUnderTheField(game.Score, settings.FieldWidth, settings.FieldHeight);

                await Task.Delay(settings.ConsoleRefreshDelay);
            }
        }
    }
}