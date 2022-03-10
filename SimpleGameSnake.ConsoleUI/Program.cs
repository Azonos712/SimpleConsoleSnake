using SimpleGameSnake.GameLibrary;
using System;
using System.Runtime.InteropServices;
using System.Text;

namespace SimpleGameSnake.ConsoleUI
{
    internal class Program
    {
        private const int UPDATEPERSECONDS = 100;
        private const int FIELD_WIDTH = 80;
        private const int FIELD_HEIGHT = 25;

        private const char VERTICAL_WALL_SYMBOL = '│';
        private const char HORIZONTAL_WALL_SYMBOL = '─';
        private const char SNAKE_HEAD_TOP_SYMBOL = '^';
        private const char SNAKE_HEAD_BOTTOM_SYMBOL = 'v';
        private const char SNAKE_HEAD_LEFT_SYMBOL = '<';
        private const char SNAKE_HEAD_RIGHT_SYMBOL = '>';
        private const char SNAKE_BODY_SYMBOL = 'o';
        private const char FOOD_SYMBOL = '@';

        private static GameManager _game;

        static async Task Main(string[] args)
        {
            Snake snake = new Snake(FIELD_WIDTH / 2, FIELD_HEIGHT / 2);
            _game = new GameManager();
            ConsoleSettingUp();

            Console.WriteLine("Press any key to start...");
            Console.ReadKey();

            Console.Clear();
            ShowField();
            ShowSnake(snake);

            Task.Run(() => ListenKeys(snake));

            while (!_game.IsGameOver)
            {
                if (_game.IsFoodOnField == false)
                    GenerateFood();

                DisplaySymbol(_game.Food.Position.X, _game.Food.Position.Y, FOOD_SYMBOL);

                MoveSnake(snake);

                ShowScore();

                await Task.Delay(UPDATEPERSECONDS);
            }
        }

        private static void GenerateFood()
        {
            _game.CreateFood(1, FIELD_WIDTH - 1, 1, FIELD_HEIGHT - 1);
        }

        private static void ConsoleSettingUp()
        {
            //Console.BackgroundColor = ConsoleColor.Green;
            //Console.ForegroundColor = ConsoleColor.Black;
            Console.OutputEncoding = Encoding.UTF8;
            Console.SetWindowSize(FIELD_WIDTH, FIELD_HEIGHT + 4);
            Console.CursorVisible = false;
        }

        private static void ShowField()
        {
            for (int i = 0; i < FIELD_HEIGHT; i++)
            {
                for (int j = 0; j < FIELD_WIDTH; j++)
                {
                    if (i == 0 || i == FIELD_HEIGHT - 1)
                        DisplaySymbol(j, i, HORIZONTAL_WALL_SYMBOL);

                    if (j == 0 || j == FIELD_WIDTH - 1)
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
            DisplaySymbol(snake.Head.X, snake.Head.Y, SelectHeadSymbol(snake.CurrentDirection));

            for (int i = 1; i <= snake.TailLength; i++)
                DisplaySymbol(snake.Head.X - i, snake.Head.Y, SNAKE_BODY_SYMBOL);
        }

        private static char SelectHeadSymbol(Direction direction)
        {
            return direction switch
            {
                Direction.Top => SNAKE_HEAD_TOP_SYMBOL,
                Direction.Bottom => SNAKE_HEAD_BOTTOM_SYMBOL,
                Direction.Left => SNAKE_HEAD_LEFT_SYMBOL,
                Direction.Right => SNAKE_HEAD_RIGHT_SYMBOL,
                _ => '.',
            };
        }

        private static void MoveSnake(Snake snake)
        {
            snake.MoveSnake();

            ScreenEdgeCheck(snake);

            SelfBodyCheck(snake);

            var foodHasBeenEaten = FoodCheck(snake);


            if (_game.IsGameOver)
            {
                DisplaySymbol(snake.PrevHead.X, snake.PrevHead.Y, SelectHeadSymbol(snake.CurrentDirection));
                return;
            }

            DisplaySymbol(snake.Head.X, snake.Head.Y, SelectHeadSymbol(snake.CurrentDirection));
            DisplaySymbol(snake.PrevHead.X, snake.PrevHead.Y, SNAKE_BODY_SYMBOL);
            if (!foodHasBeenEaten)
                DisplaySymbol(snake.LastPart.X, snake.LastPart.Y, ' ');
        }

        private static void ScreenEdgeCheck(Snake snake)
        {
            if (snake.Head.X == 0 || snake.Head.X == FIELD_WIDTH - 1)
                _game.StopGame();

            if (snake.Head.Y == 0 || snake.Head.Y == FIELD_HEIGHT - 1)
                _game.StopGame();
        }

        private static void SelfBodyCheck(Snake snake)
        {
            //var headPosition = snake.Head;
            //snake.Body.Contains(headPosition);
            if (snake.Body.Contains(snake.Head))
                _game.StopGame();

            //if (snake.Head.Y == 0 || snake.Head.Y == FIELD_HEIGHT - 1)
            // _game.StopGame();
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

        private static void ShowScore()
        {
            Console.SetCursorPosition(3, FIELD_HEIGHT + 1);
            Console.WriteLine($"Score - {_game.Score}");
        }

        private static Task ListenKeys(Snake snake)
        {
            while (!_game.IsGameOver)
            {
                var key = Console.ReadKey();

                if (key.Key == ConsoleKey.UpArrow)
                    snake.ChangeDirection(Direction.Top);

                if (key.Key == ConsoleKey.DownArrow)
                    snake.ChangeDirection(Direction.Bottom);

                if (key.Key == ConsoleKey.LeftArrow)
                    snake.ChangeDirection(Direction.Left);

                if (key.Key == ConsoleKey.RightArrow)
                    snake.ChangeDirection(Direction.Right);

                Task.Delay(120).Wait();
            }

            return Task.CompletedTask;
        }
    }
}