﻿using System;
using System.IO;

using Cosmos.Core;

using TingTingOS.Utils;

namespace TingTingOS.Programs
{
    public static class CommandManager
    {
        public static void Init()
        {
            try
            {
            Commands:
                ColorConsole.Write(ConsoleColor.Green, Reference.UserAccount.GetUsername());
                ColorConsole.Write(ConsoleColor.White, ":");
                ColorConsole.Write(ConsoleColor.Blue, Directory.GetCurrentDirectory());    // TODO Double check this
                ColorConsole.Write(ConsoleColor.White, Reference.DefaultAccessPrefix);

                string input = Console.ReadLine();
                string command = input.Split(" ")[0];

                switch (command)
                {
                    // TODO Refactor all the commands
                    case "cd":
                        string newDir = input.Split(" ")[1];
                        if (Directory.Exists(newDir))
                        {
                            if (newDir.Contains(Reference.RootPath))
                            {
                                Reference.CurrentDir = newDir;
                            }
                            else
                            {
                                Reference.CurrentDir += newDir;
                            }
                        }
                        else
                        {
                            ColorConsole.WriteLine(ConsoleColor.Red, "The specified directory doesn't exist");
                        }

                        break;

                    case "ls":
                        string[] directories = Directory.GetDirectories(Reference.CurrentDir);
                        for (int i = 0; i < directories.Length; i++)
                        {
                            ColorConsole.WriteLine(ConsoleColor.Cyan, directories[i]);
                        }

                        string[] files = Directory.GetFiles(Reference.CurrentDir);
                        for (int i = 0; i < files.Length; i++)
                        {
                            ColorConsole.WriteLine(ConsoleColor.Cyan, files[i]);
                        }

                        break;

                    case "mkdir":
                        Directory.CreateDirectory(input.Split(" ")[1]);

                        break;

                    case "touch":
                        File.Create(input.Split(" ")[1]);

                        break;

                    case "rm":
                        string file = input.Split(" ")[1];
                        if (File.Exists(file))
                        {
                            File.Delete(file);
                        }
                        else
                        {
                            ColorConsole.WriteLine(ConsoleColor.Red, "The specified file doesn't exist");
                        }

                        break;

                    case "rmdir":
                        string directory = input.Split(" ")[1];
                        if (Directory.Exists(directory))
                        {
                            Directory.Delete(directory, true);
                        }
                        else
                        {
                            ColorConsole.WriteLine(ConsoleColor.Red, "The specified directory doesn't exist");
                        }

                        break;

                    case "cp":
                        File.Copy(input.Split(" ")[1], input.Split(" ")[2], true);

                        break;

                    case "mv":
                        string move = input.Split(" ")[1];
                        if (Directory.Exists(move) || File.Exists(move))
                        {
                            Directory.Move(move, input.Split(" ")[2]);
                        }
                        else
                        {
                            ColorConsole.WriteLine(ConsoleColor.Red, "The source directory/file doesn't exist");
                        }

                        break;

                    case "cls":
                        Console.Clear();

                        break;

                    default:
                        ColorConsole.WriteLine(ConsoleColor.Red, "Invalid command");

                        break;
                }

                goto Commands;
            }
            catch (Exception e)
            {
                Global.mDebugger.Send("ERROR : " + e.Message + e.HResult.ToString());
            }
        }
    }
}
