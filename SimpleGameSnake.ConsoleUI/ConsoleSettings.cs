using System.Text;

namespace SimpleGameSnake.ConsoleUI
{
    public class ConsoleSettings
    {
        private static ConsoleSettings _instance;

        private ConsoleSettings() { }

        public static ConsoleSettings Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ConsoleSettings();
                return _instance;
            }
        }

        public int FieldWidth { get; private set; } = 80;
        public int FieldHeight { get; private set; } = 25;

        public void SettingUpConsole()
        {
            //Console.BackgroundColor = ConsoleColor.Green;
            //Console.ForegroundColor = ConsoleColor.Black;
            Console.OutputEncoding = Encoding.UTF8;
            Console.SetWindowSize(FieldWidth, FieldHeight + 4);
            Console.CursorVisible = false;
        }
    }
}
