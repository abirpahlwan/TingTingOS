using System;

using Sys = Cosmos.System;

namespace TingTingOS.Utils
{
    public static class Power
    {
        public static void Init()
        {
            Console.Clear();
            ColorConsole.WriteLine(ConsoleColor.Green, "TingTingOS v" + Reference.Version);
            Console.WriteLine();
        }

        public static void Shutdown()
        {
            ColorConsole.WriteLine(ConsoleColor.Yellow, "Shutting down computer...");
            Sys.Power.Shutdown();
        }

        public static void Restart()
        {
            ColorConsole.WriteLine(ConsoleColor.Yellow, "Rebooting computer...");
            Sys.Power.Reboot();
        }

        public static void ShowWelcomeMessage()
        {
            ColorConsole.WriteLine(ConsoleColor.Yellow, "TingTingOS booted successfully.");
            ColorConsole.WriteLine(ConsoleColor.Yellow, "Press any key to continue...");
            Console.ReadKey();

            Console.WriteLine();
        }
    }
}
