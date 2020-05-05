using System;
using System.IO;

using Sys = Cosmos.System;

using TingTingOS.Accounts;
using TingTingOS.Programs;
using TingTingOS.Utils;

namespace TingTingOS
{
    public class Kernel : Sys.Kernel
    {
        protected override void BeforeRun()
        {
            FileSystem.LoadFileSystem();

            Power.ShowWelcomeMessage();

            if (!File.Exists(Reference.RootPath + "Installed.txt"))
            {
                Setup.Init();
            }

            Power.Init();
        }

        protected override void Run()
        {
        Login:
            ColorConsole.Write(ConsoleColor.White, "Username: ");
            string user = Console.ReadLine();

            ColorConsole.Write(ConsoleColor.White, "Password: ");
            string pass = Console.ReadLine();

            if (AccountManager.Exist(user) && AccountManager.GetPassword(user, true).Equals(pass))
            {
                Reference.UserAccount = new Account(user, pass);

                goto Terminal;
            }
            else
            {
                ColorConsole.WriteLine(ConsoleColor.Red, "Login failed");

                goto Login;
            }

        Terminal:
            Power.Init();

            while (true)
            {
               CommandManager.Init();
            }
        }

        void EchoTest()
        {
            ColorConsole.Write(ConsoleColor.White, Reference.DefaultAccessPrefix);
            var input = Console.ReadLine();
            ColorConsole.WriteLine(ConsoleColor.White, input);
        }
    }
}
