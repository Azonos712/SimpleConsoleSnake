using SimpleGameSnake.GameLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SimpleGameSnake.ConsoleUI
{
    public class ConsoleGraphics
    {
        public readonly char VerticalWallSymbol = '│';
        public readonly char HorizontalWallSymbol = '─';
        public readonly char SnakeHeadUpSymbol = '^';
        public readonly char SnakeHeadDownSymbol = 'v';
        public readonly char SnakeHeadLeftSymbol = '<';
        public readonly char SnakeHeadRightSymbol = '>';
        public readonly char SnakeBodySymbol = 'o';
        public readonly char FoodSymbol = '@';

        public void ShowField(int width, int height)
        {
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (i == 0 || i == height - 1)
                        DisplaySymbol(j, i, HorizontalWallSymbol);

                    if (j == 0 || j == width - 1)
                        DisplaySymbol(j, i, VerticalWallSymbol);
                }
            }
        }

        private void DisplaySymbol(int x, int y, char symbol)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(symbol);
        }

        public void ShowSnake(Snake snake)
        {
            DisplaySymbol(snake.Head.X, snake.Head.Y, GetHeadSymbolByDirection(snake.CurrentDirection));

            for (int i = 1; i <= snake.TailLength; i++)
                DisplaySymbol(snake.Head.X - i, snake.Head.Y, SnakeBodySymbol);
        }

        private char GetHeadSymbolByDirection(Direction direction)
        {
            return direction switch
            {
                Direction.Up => SnakeHeadUpSymbol,
                Direction.Down => SnakeHeadDownSymbol,
                Direction.Left => SnakeHeadLeftSymbol,
                Direction.Right => SnakeHeadRightSymbol,
                _ => throw new Exception(),
            };
        }

        public void ShowFood(int x, int y)
        {
            DisplaySymbol(x, y, FoodSymbol);
        }

        public void ShowSnakeStep(Snake snake, GameManager game)
        {
            if (game.IsGameOver)
            {
                DisplaySymbol(snake.PrevHead.X, snake.PrevHead.Y, GetHeadSymbolByDirection(snake.CurrentDirection));
                return;
            }

            DisplaySymbol(snake.Head.X, snake.Head.Y, GetHeadSymbolByDirection(snake.CurrentDirection));
            DisplaySymbol(snake.PrevHead.X, snake.PrevHead.Y, SnakeBodySymbol);
            if (game.IsFoodOnField)
                DisplaySymbol(snake.LastPart.X, snake.LastPart.Y, ' ');
        }

        public void ShowScoreUnderTheField(int score, int width, int height)
        {
            Console.SetCursorPosition(3, height + 1);
            Console.WriteLine($"Score - {score}");
        }
    }
}
