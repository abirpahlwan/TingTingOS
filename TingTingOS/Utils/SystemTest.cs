using System;

namespace TingTingOS.Utils
{
    public static class SystemTest
    {
        public static void EchoTest()
        {
            ColorConsole.Write(ConsoleColor.White, Reference.DefaultAccessPrefix);
            var input = Console.ReadLine();
            ColorConsole.WriteLine(ConsoleColor.White, input);
        }
    }
}
