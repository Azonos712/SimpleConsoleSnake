using SimpleGameSnake.GameLibrary;
using System;
using System.Runtime.InteropServices;
using System.Text;

namespace SimpleGameSnake.ConsoleUI
{
    internal class Program
    {
        private const int UPDATEPERSECONDS = 100;

        private static GameManager _game;

        static async Task Main(string[] args)
        {
            ConsoleSettings settings = new ConsoleSettings();
            ConsoleGraphics graphics = new ConsoleGraphics();
            Snake snake = new Snake(settings.FieldWidth / 2, settings.FieldHeight / 2);
            _game = new GameManager();

            Console.WriteLine("Press any key to start...");
            Console.ReadKey();

            StartGame(settings, graphics, snake);
            await MainLoop(settings, graphics, snake);

            Console.WriteLine("Game Over");
            Console.ReadKey();
        }

        private static void StartGame(ConsoleSettings settings, ConsoleGraphics graphics, Snake snake)
        {
            Console.Clear();
            graphics.DisplayField(settings.FieldWidth, settings.FieldHeight);
            graphics.DisplaySnake(snake);
            Task.Run(() => PressedKeyListener.Instance.ListenKeys(_game, snake));
        }

        private static async Task MainLoop(ConsoleSettings settings, ConsoleGraphics graphics, Snake snake)
        {
            while (!_game.IsGameOver)
            {
                if (!_game.IsFoodOnField)
                    GenerateFoodOnField(settings.FieldWidth, settings.FieldHeight);

                graphics.DisplayFood(_game.Food.Position.X, _game.Food.Position.Y);
                MoveSnake(snake, settings.FieldWidth, settings.FieldHeight);
                graphics.DisplaySnakeStep(snake, _game);
                graphics.DisplayScoreUnderTheField(_game.Score, settings.FieldWidth, settings.FieldHeight);

                await Task.Delay(UPDATEPERSECONDS);
            }
        }

        private static void GenerateFoodOnField(int width, int height)
        {
            _game.CreateFood(1, width - 1, 1, height - 1);
        }

        private static void MoveSnake(Snake snake, int width, int height)
        {
            snake.MoveSnake();

            ScreenEdgeCheck(snake, width, height);

            SelfBodyCheck(snake);

            FoodCheck(snake);
        }

        private static void ScreenEdgeCheck(Snake snake, int width, int height)
        {
            if (snake.Head.X == 0 || snake.Head.X == width - 1)
                _game.StopGame();

            if (snake.Head.Y == 0 || snake.Head.Y == height - 1)
                _game.StopGame();
        }

        private static void SelfBodyCheck(Snake snake)
        {
            if (snake.Body.Contains(snake.Head))
                _game.StopGame();
        }

        private static void FoodCheck(Snake snake)
        {
            if (snake.Head.X == _game.Food.Position.X && snake.Head.Y == _game.Food.Position.Y)
            {
                _game.PutAwayFood();
                snake.Eat();
            }
        }
    }
}