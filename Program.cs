using System;
using System.Diagnostics;
using System.Threading;

namespace QuickShutDown
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.WriteLine(@"______________________________________________________________________");
            Console.WriteLine();
            Console.WriteLine(@"███████ ██   ██ ██    ██ ████████ ██████   ██████  ██     ██ ███    ██ ");
            Console.WriteLine(@"██      ██   ██ ██    ██    ██    ██   ██ ██    ██ ██     ██ ████   ██ ");
            Console.WriteLine(@"███████ ███████ ██    ██    ██    ██   ██ ██    ██ ██  █  ██ ██ ██  ██ ");
            Console.WriteLine(@"     ██ ██   ██ ██    ██    ██    ██   ██ ██    ██ ██ ███ ██ ██  ██ ██ ");
            Console.WriteLine(@"███████ ██   ██  ██████     ██    ██████   ██████   ███ ███  ██   ████ ");
            Console.WriteLine(@"______________________________________________________________________");
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.White;

            string timeout;
            int hours = 0, minutes = 0, seconds = 0;
            bool validInput = false;

            do
            {
                Console.WriteLine("Введите время до выключения или A для отмены.\nНапример, 1h 20m 50s.");
                Console.Write("Запрос: ");
                timeout = Console.ReadLine();
                string[] parts = timeout.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string part in parts)
                {
                    if (part.EndsWith("h"))
                    {
                        int.TryParse(part.TrimEnd('h'), out hours);
                    }
                    else if (part.EndsWith("m"))
                    {
                        int.TryParse(part.TrimEnd('m'), out minutes);
                    }
                    else if (part.EndsWith("s"))
                    {
                        int.TryParse(part.TrimEnd('s'), out seconds);
                    }
                    else if (timeout.ToUpper() == "A")
                    {
                        Process.Start($"shutdown", "/a");
                        Console.WriteLine("Выключение отменено.");
                        Console.WriteLine();
                        validInput = false;
                        break;
                    }
                    else
                    {
                        Console.WriteLine($"Некорректное значение: {part}");
                        Console.WriteLine();
                        validInput = false;
                        break;
                    }
                    validInput = true;
                }
            }
            while (!validInput);

            // Перевод времени в секунды
            int totalSeconds = hours * 3600 + minutes * 60 + seconds;
            Console.WriteLine($"Время до выключения компьютера: {totalSeconds} секунд.");

            Console.WriteLine("Нажмите ENTER для подтверждения или ESCAPE для отмены выключения.");
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.Enter:
                    Process.Start("shutdown", $"/f /s /t {totalSeconds}");

                    Environment.Exit(0);
                    break;
                case ConsoleKey.Escape:
                    Process.Start($"shutdown", "/a");
                    Console.WriteLine("Выключение отменено. Программа будет закрыта через 5 секунд.");
                    Thread.Sleep(5000);
                    Environment.Exit(0);
                    break;
            }
        }
    }
}
