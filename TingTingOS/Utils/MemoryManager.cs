using System;

using Cosmos.Core;

namespace TingTingOS.Utils
{
    public static class MemoryManager
    {
        public static void ShowMemoryUsage()
        {
            ColorConsole.Write(ConsoleColor.Yellow, UsedMemory().ToString() + " MB");
            ColorConsole.Write(ConsoleColor.Yellow, " / ");
            ColorConsole.Write(ConsoleColor.Yellow, TotalMemory().ToString() + " MB");
            ColorConsole.Write(ConsoleColor.Yellow, " (used / total) ");

            Console.WriteLine();
            Console.WriteLine();
        }

        public static uint UsedMemory()
        {
            return (CPU.GetEndOfKernel() + 1024) / 1048576;
        }
        public static uint TotalMemory()
        {
            return CPU.GetAmountOfRAM();
        }
    }
}
