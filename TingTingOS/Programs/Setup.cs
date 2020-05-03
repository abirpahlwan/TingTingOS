using System;
using System.IO;
using System.Text;

using TingTingOS.Accounts;
using TingTingOS.Utils;

namespace TingTingOS.Programs
{
    public static class Setup
    {
        public static void Init()
        {
            Console.Clear();

            ColorConsole.WriteLine(ConsoleColor.Yellow, "Initiating setup...");


            ColorConsole.WriteLine(ConsoleColor.Yellow, "Create your user account.");

            ColorConsole.Write(ConsoleColor.White, "Username");
            ColorConsole.Write(ConsoleColor.White, ": ");
            string user = Console.ReadLine();

            ColorConsole.Write(ConsoleColor.White, "Password");
            ColorConsole.Write(ConsoleColor.White, ": ");
            string pass = Console.ReadLine();

            ColorConsole.WriteLine(ConsoleColor.Yellow, "Creating user account...");
            Reference.UserAccount = new Account(user, pass);
            Reference.UserAccount.CreateAccount();

            ColorConsole.WriteLine(ConsoleColor.Yellow, "Finishing installation...");
            File.WriteAllText(Reference.RootPath + "Installed.txt", "IsInstalled:True");

            ColorConsole.WriteLine(ConsoleColor.Yellow, "Press any key to restart...");
            Console.ReadKey();

            Power.Restart();
        }

        public static void Reset()
        {
            if (File.Exists(Reference.RootPath + "Installed.txt"))
            {
                ColorConsole.WriteLine(ConsoleColor.Yellow, "Finishing reset operation...");
                File.Delete(Reference.RootPath + "Installed.txt");
            }
        }
    }
}
