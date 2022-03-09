using SimpleGameSnake.GameLibrary;
using System;
using System.Runtime.InteropServices;
using System.Text;

namespace SimpleGameSnake.ConsoleUI
{
    internal class Program
    {
        private const int UPDATEPERSECONDS = 150;
        private const int FIELD_WIDTH = 80;
        private const int FIELD_HEIGHT = 25;

        private const char VERTICAL_WALL_SYMBOL = '│';
        private const char HORIZONTAL_WALL_SYMBOL = '─';
        private const char SNAKE_HEAD_TOP_SYMBOL = '^';
        private const char SNAKE_HEAD_BOTTOM_SYMBOL = 'v';
        private const char SNAKE_HEAD_LEFT_SYMBOL = '<';
        private const char SNAKE_HEAD_RIGHT_SYMBOL = '>';
        private const char SNAKE_BODY_SYMBOL = 'o';

        private static GameManager _game;
        static async Task Main(string[] args)
        {
            Snake snake = new Snake(FIELD_WIDTH / 2, FIELD_HEIGHT / 2);
            _game = new GameManager();
            ConsoleSettingUp();
            Console.Clear();
            ShowField();
            ShowSnake(snake);

            Task.Run(() => ListenKeys(snake));

            while (!_game.IsGameOver)
            {

                MoveSnake(snake);

                await Task.Delay(UPDATEPERSECONDS);
            }
        }

        private static void ConsoleSettingUp()
        {
            //Console.BackgroundColor = ConsoleColor.Green;
            //Console.ForegroundColor = ConsoleColor.Black;
            Console.OutputEncoding = Encoding.UTF8;
            Console.SetWindowSize(FIELD_WIDTH, FIELD_HEIGHT);
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

            //Console.SetCursorPosition(snake.Head.X, snake.Head.Y);
            //char nextSymbol = Convert.ToChar(Console.Read());
            //if (nextSymbol == VERTICAL_WALL_SYMBOL || nextSymbol == HORIZONTAL_WALL_SYMBOL)
            //    _game.StopGame();
            //if(snake.Head.X, snake.Head.Y,)

            DisplaySymbol(snake.Head.X, snake.Head.Y, SelectHeadSymbol(snake.CurrentDirection));
            DisplaySymbol(snake.PrevHead.X, snake.PrevHead.Y, SNAKE_BODY_SYMBOL);
            DisplaySymbol(snake.LastPart.X, snake.LastPart.Y, ' ');
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
            }

            return Task.CompletedTask;
        }
    }
}