using SimpleGameSnake.GameLibrary;

namespace SimpleGameSnake.ConsoleUI
{
    public class PressedKeyListener
    {
        private static PressedKeyListener _instance;

        private PressedKeyListener() { }

        public static PressedKeyListener Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new PressedKeyListener();
                return _instance;
            }
        }

        public Task ListenKeys(GameManager game)
        {
            while (!game.IsGameOver)
            {
                var pressedKey = Console.ReadKey();
                game.Snake.ChangeDirection(GetDirectionByConsoleKey(pressedKey.Key, game.Snake.CurrentDirection));
                Task.Delay(120).Wait();
            }

            return Task.CompletedTask;
        }

        private Direction GetDirectionByConsoleKey(ConsoleKey key, Direction prevDirection)
        {
            return key switch
            {
                ConsoleKey.UpArrow => Direction.Up,
                ConsoleKey.DownArrow => Direction.Down,
                ConsoleKey.LeftArrow => Direction.Left,
                ConsoleKey.RightArrow => Direction.Right,
                _ => prevDirection
            };
        }
    }
}