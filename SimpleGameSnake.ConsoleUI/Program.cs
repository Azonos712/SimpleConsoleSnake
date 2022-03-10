using SimpleGameSnake.GameLibrary;
using System;
using System.Runtime.InteropServices;
using System.Text;

namespace SimpleGameSnake.ConsoleUI
{
    internal class Program
    {
        private const int UPDATEPERSECONDS = 100;

        private const char VERTICAL_WALL_SYMBOL = '│';
        private const char HORIZONTAL_WALL_SYMBOL = '─';
        private const char SNAKE_HEAD_UP_SYMBOL = '^';
        private const char SNAKE_HEAD_DOWN_SYMBOL = 'v';
        private const char SNAKE_HEAD_LEFT_SYMBOL = '<';
        private const char SNAKE_HEAD_RIGHT_SYMBOL = '>';
        private const char SNAKE_BODY_SYMBOL = 'o';
        private const char FOOD_SYMBOL = '@';

        private static GameManager _game;

        static async Task Main(string[] args)
        {
            ConsoleSettings settings = new ConsoleSettings();
            Snake snake = new Snake(settings.FieldWidth / 2, settings.FieldHeight / 2);
            _game = new GameManager();

            Console.WriteLine("Press any key to start...");
            Console.ReadKey();

            StartGame(settings, snake);
            await MainLoop(settings, snake);

            Console.WriteLine("Game Over");
            Console.ReadKey();
        }

        private static void StartGame(ConsoleSettings settings, Snake snake)
        {
            Console.Clear();
            ShowField(settings.FieldWidth, settings.FieldHeight);
            ShowSnake(snake);
            Task.Run(() => PressedKeyListener.Instance.ListenKeys(_game, snake));
        }

        private static void ShowField(int width, int height)
        {
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (i == 0 || i == height - 1)
                        DisplaySymbol(j, i, HORIZONTAL_WALL_SYMBOL);

                    if (j == 0 || j == width - 1)
                        DisplaySymbol(j, i, VERTICAL_WALL_SYMBOL);
                }
            }
        }

        private static void DisplaySymbol(int x, int y, char symbol)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(symbol);
        }

        private static void ShowSnake(Snake snake)
        {
            DisplaySymbol(snake.Head.X, snake.Head.Y, GetHeadSymbolByDirection(snake.CurrentDirection));

            for (int i = 1; i <= snake.TailLength; i++)
                DisplaySymbol(snake.Head.X - i, snake.Head.Y, SNAKE_BODY_SYMBOL);
        }

        private static async Task MainLoop(ConsoleSettings settings, Snake snake)
        {
            while (!_game.IsGameOver)
            {
                if (!_game.IsFoodOnField)
                    GenerateFoodOnField(settings.FieldWidth, settings.FieldHeight);

                DisplaySymbol(_game.Food.Position.X, _game.Food.Position.Y, FOOD_SYMBOL);
                MoveSnake(snake, settings.FieldWidth, settings.FieldHeight);
                ShowScoreUnderTheField(settings.FieldWidth, settings.FieldHeight);

                await Task.Delay(UPDATEPERSECONDS);
            }
        }

        private static void GenerateFoodOnField(int width, int height)
        {
            _game.CreateFood(1, width - 1, 1, height - 1);
        }

        private static char GetHeadSymbolByDirection(Direction direction)
        {
            return direction switch
            {
                Direction.Up => SNAKE_HEAD_UP_SYMBOL,
                Direction.Down => SNAKE_HEAD_DOWN_SYMBOL,
                Direction.Left => SNAKE_HEAD_LEFT_SYMBOL,
                Direction.Right => SNAKE_HEAD_RIGHT_SYMBOL,
                _ => throw new Exception(),
            };
        }

        private static void MoveSnake(Snake snake, int width, int height)
        {
            snake.MoveSnake();

            ScreenEdgeCheck(snake, width, height);

            SelfBodyCheck(snake);

            var foodHasBeenEaten = FoodCheck(snake);

            if (_game.IsGameOver)
            {
                DisplaySymbol(snake.PrevHead.X, snake.PrevHead.Y, GetHeadSymbolByDirection(snake.CurrentDirection));
                return;
            }

            DisplaySymbol(snake.Head.X, snake.Head.Y, GetHeadSymbolByDirection(snake.CurrentDirection));
            DisplaySymbol(snake.PrevHead.X, snake.PrevHead.Y, SNAKE_BODY_SYMBOL);
            if (!foodHasBeenEaten)
                DisplaySymbol(snake.LastPart.X, snake.LastPart.Y, ' ');
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

        private static bool FoodCheck(Snake snake)
        {
            if (snake.Head.X == _game.Food.Position.X && snake.Head.Y == _game.Food.Position.Y)
            {
                _game.PutAwayFood();
                snake.Eat();

                return true;
            }

            return false;
        }

        private static void ShowScoreUnderTheField(int width, int height)
        {
            Console.SetCursorPosition(3, height + 1);
            Console.WriteLine($"Score - {_game.Score}");
        }
    }
}