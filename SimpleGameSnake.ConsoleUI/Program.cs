using SimpleGameSnake.GameLibrary;
using System;
using System.Text;

namespace SimpleGameSnake.ConsoleUI
{
    internal class Program
    {
        private const int UPDATEPERSECONDS = 150;
        private const int FIELD_WIDTH = 80;
        private const int FIELD_HEIGHT = 25;

        private const string VERTICAL_WALL_SYMBOL = "│";
        private const string HORIZONTAL_WALL_SYMBOL = "──";
        private const string SNAKE_HEAD_TOP_SYMBOL = "^";
        private const string SNAKE_HEAD_BOTTOM_SYMBOL = "v";
        private const string SNAKE_HEAD_LEFT_SYMBOL = "<";
        private const string SNAKE_HEAD_RIGHT_SYMBOL = ">";
        private const string SNAKE_BODY_SYMBOL = "o";

        static async Task Main(string[] args)
        {
            Snake snake = new Snake(FIELD_WIDTH / 2, FIELD_HEIGHT / 2);
            ConsoleSettingUp();
            Console.Clear();
            ShowField();
            ShowSnake(snake);

            Task.Run(() => ListenKeys(snake));

            while (true)
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

        private static void DisplaySymbol(int x, int y, string symbol)
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

        private static string SelectHeadSymbol(Direction direction)
        {
            return direction switch
            {
                Direction.Top => SNAKE_HEAD_TOP_SYMBOL,
                Direction.Bottom => SNAKE_HEAD_BOTTOM_SYMBOL,
                Direction.Left => SNAKE_HEAD_LEFT_SYMBOL,
                Direction.Right => SNAKE_HEAD_RIGHT_SYMBOL,
                _ => ".",
            };
        }

        private static void MoveSnake(Snake snake)
        {
            snake.MoveSnake();
            DisplaySymbol(snake.Head.X, snake.Head.Y, SelectHeadSymbol(snake.CurrentDirection));
            DisplaySymbol(snake.PrevHead.X, snake.PrevHead.Y, SNAKE_BODY_SYMBOL);
            DisplaySymbol(snake.LastPart.X, snake.LastPart.Y, " ");
        }

        private static Task ListenKeys(Snake snake)
        {
            while (true)
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
        }
    }
}