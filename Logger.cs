namespace OllamAgent
{
    internal class Logger
    {
        public enum Type
        {
            GOOD,
            BAD,
            INFO,
            WARN,
            NORMAL
        }

        public static void Log(string message, Type t)
        {
            var bg_actual = Console.ForegroundColor;

            switch (t)
            {
                case Type.GOOD:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case Type.BAD:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case Type.INFO:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                case Type.WARN:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case Type.NORMAL:
                    Console.ForegroundColor = bg_actual;
                    break;
                default:
                    Console.ForegroundColor = bg_actual;
                    break;

            }
            DateTime now = DateTime.Now;
            string time = now.ToString("HH:mm");
            Console.WriteLine($"[{time}] {message}");
            Console.ForegroundColor = bg_actual;
        }
    }
}