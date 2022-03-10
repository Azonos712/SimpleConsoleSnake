using SimpleGameSnake.GameLibrary;

namespace SimpleGameSnake.ConsoleUI
{
    internal class Program
    {
        static async Task Main()
        {
            ConsoleSettings settings = new();
            ConsoleGraphics graphics = new();
            GameManager game = new(settings.FieldWidth, settings.FieldHeight);

            Console.WriteLine("Press any key to start...");
            Console.ReadKey();

            StartGame(game, settings, graphics);
            await MainLoop(game, settings, graphics);

            Console.WriteLine("Game Over");
            Console.ReadKey();
        }

        private static void StartGame(GameManager game, ConsoleSettings settings, ConsoleGraphics graphics)
        {
            Console.Clear();
            graphics.DisplayField(settings.FieldWidth, settings.FieldHeight);
            graphics.DisplaySnake(game.Snake);
            Task.Run(() => PressedKeyListener.Instance.ListenKeys(game));
        }

        private static async Task MainLoop(GameManager game, ConsoleSettings settings, ConsoleGraphics graphics)
        {
            while (!game.IsGameOver)
            {
                game.CheckFoodOnField();
                graphics.DisplayFood(game.Food.Position.X, game.Food.Position.Y);

                game.MoveSnake();
                graphics.DisplaySnakeStep(game);
                graphics.DisplayScoreUnderTheField(game.Score, settings.FieldWidth, settings.FieldHeight);

                await Task.Delay(settings.ConsoleRefreshDelay);
            }
        }
    }
}