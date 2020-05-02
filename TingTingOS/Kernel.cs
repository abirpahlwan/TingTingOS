using System;

using TingTingOS.Utils;
using Sys = Cosmos.System;

namespace TingTingOS
{
    public class Kernel : Sys.Kernel
    {
        protected override void BeforeRun()
        {
            Power.Init();

            ColorConsole.WriteLine(ConsoleColor.Yellow, "TingTingOS booted successfully");
            ColorConsole.WriteLine(ConsoleColor.Yellow, "Press any key to continue");

            if (Console.ReadKey().Key != ConsoleKey.Escape)
            {
                Console.Clear();
            }
            else
            {
                Power.Shutdown();
            }
        }

        protected override void Run()
        {
            ColorConsole.Write(ConsoleColor.White, "$ ");
            var input = Console.ReadLine();
            ColorConsole.WriteLine(ConsoleColor.Green, input);
        }
    }
}
