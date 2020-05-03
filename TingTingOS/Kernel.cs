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
            LoadFileSystem();

            ShowWelcomeMessage();

            if (!File.Exists(Reference.RootPath + "Installed.txt"))
            {
                Setup.Init();
            }

            Power.Init();
        }

        protected override void Run()
        {
            ColorConsole.WriteLine(ConsoleColor.Yellow, "Login to your user account.");

        Login:
            ColorConsole.Write(ConsoleColor.White, "Username");
            ColorConsole.Write(ConsoleColor.White, ": ");
            string user = Console.ReadLine();

            ColorConsole.Write(ConsoleColor.White, "Password");
            ColorConsole.Write(ConsoleColor.White, ": ");
            string pass = Console.ReadLine();

            if (AccountManager.Exist(user) && AccountManager.GetPassword(user, true).Equals(pass))
            {
                Reference.UserAccount = new Account(user, pass);
                // CmdMan.Init();
                while (true)
                {
                    EchoTest();
                }
            }
            else
            {
                ColorConsole.WriteLine(ConsoleColor.Red, "Login failed. Try again.");
                goto Login;
            }
        }

        void LoadFileSystem()
        {
            ColorConsole.WriteLine(ConsoleColor.Yellow, "Loading virtual file system...");
            Sys.FileSystem.VFS.VFSManager.RegisterVFS(Reference.FAT);

            if (Reference.FAT.GetVolumes().Count > 0)
            {
                ColorConsole.WriteLine(ConsoleColor.Green, "Sucessfully loaded the virtual file system.");
            }
            else
            {
                ColorConsole.WriteLine(ConsoleColor.Red, "Couldn't load the virtual file system...");
            }

            Console.WriteLine();
        }

        void ShowWelcomeMessage()
        {
            ColorConsole.WriteLine(ConsoleColor.Yellow, "TingTingOS booted successfully.");
            ColorConsole.WriteLine(ConsoleColor.Yellow, "Press any key to continue...");
            Console.ReadKey();

            Console.WriteLine();
        }

        void EchoTest()
        {
            ColorConsole.Write(ConsoleColor.White, Reference.DefaultAccessPrefix);
            var input = Console.ReadLine();
            ColorConsole.WriteLine(ConsoleColor.Magenta, input);
        }
    }
}
