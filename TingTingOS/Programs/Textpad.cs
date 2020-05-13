using System;
using System.IO;

using TingTingOS.Utils;

namespace TingTingOS.Programs
{
    public static class Textpad
    {
        private const int SAFELINE = 22;
        private const int SAFEWIDTH = 72;

        public static void Init(string filepath = null)
        {
            string fullFilepath = Reference.CurrentDirectory;
            string filename = string.Empty;

            if (string.IsNullOrEmpty(filepath))
            {
                ColorConsole.Write(ConsoleColor.Yellow, "Filename: ");
                filename = Console.ReadLine();

                if (filename.Contains(Reference.RootPath))
                {
                    fullFilepath = filename;
                }
                else
                {
                    fullFilepath += @"\" + filename;
                }
            }
            else
            {
                if (filepath.Contains(Reference.RootPath))
                {
                    fullFilepath = filepath;
                }
                else
                {
                    fullFilepath += @"\" + filepath;
                }
            }

            try
            {
                if (!File.Exists(fullFilepath))
                {
                    File.Create(fullFilepath);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            string text = string.Empty;
            text = Run(File.ReadAllText(fullFilepath));

            Console.Clear();

            if (text != null)
            {
                File.WriteAllText(fullFilepath, text);
                ColorConsole.WriteLine(ConsoleColor.Yellow, "File saved");
            }

            Console.WriteLine();
        }

        public static string Run(string text)
        {
            char[] chars = new char[2000];
            int pointer = 0;
            string infoBar = string.Empty;
            bool editMode = false;

            pointer = text.Length;

            for (int i = 0; i < text.Length; i++)
            {
                chars[i] = text[i];
            }

            PrintScreen(chars, pointer, infoBar, editMode);

            ConsoleKeyInfo keyInfo;

            do
            {
                keyInfo = Console.ReadKey(true);

                if (IsForbiddenKey(keyInfo.Key)) continue;

                else if (!editMode && keyInfo.KeyChar == ':')
                {
                    infoBar = ":";
                    PrintScreen(chars, pointer, infoBar, editMode);
                    do
                    {
                        keyInfo = Console.ReadKey(true);
                        if (keyInfo.Key == ConsoleKey.Enter)
                        {
                            if (infoBar == ":s")
                            {
                                string returnString = string.Empty;
                                for (int i = 0; i < pointer; i++)
                                {
                                    returnString += chars[i];
                                }
                                return returnString;
                            }
                            else if (infoBar == ":q")
                            {
                                return null;

                            }
                            else if (infoBar == ":help")
                            {
                                // printMIVStartScreen();
                                break;
                            }
                            else
                            {
                                infoBar = "Command Error";
                                PrintScreen(chars, pointer, infoBar, editMode);
                                break;
                            }
                        }
                        else if (keyInfo.Key == ConsoleKey.Backspace)
                        {
                            infoBar = StringCopy(infoBar);
                            PrintScreen(chars, pointer, infoBar, editMode);
                        }
                        else if (keyInfo.KeyChar == 'q')
                        {
                            infoBar += "q";
                        }
                        else if (keyInfo.KeyChar == ':')
                        {
                            infoBar += ":";
                        }
                        else if (keyInfo.KeyChar == 's')
                        {
                            infoBar += "s";
                        }
                        else if (keyInfo.KeyChar == 'h')
                        {
                            infoBar += "h";
                        }
                        else if (keyInfo.KeyChar == 'e')
                        {
                            infoBar += "e";
                        }
                        else if (keyInfo.KeyChar == 'l')
                        {
                            infoBar += "l";
                        }
                        else if (keyInfo.KeyChar == 'p')
                        {
                            infoBar += "p";
                        }
                        else
                        {
                            continue;
                        }
                        PrintScreen(chars, pointer, infoBar, editMode);



                    } while (keyInfo.Key != ConsoleKey.Escape);
                }

                else if (keyInfo.Key == ConsoleKey.Escape)
                {
                    editMode = false;
                    infoBar = string.Empty;
                    PrintScreen(chars, pointer, infoBar, editMode);
                    continue;
                }

                else if (keyInfo.Key == ConsoleKey.I && !editMode)
                {
                    editMode = true;
                    infoBar = "EDIT";
                    PrintScreen(chars, pointer, infoBar, editMode);
                    continue;
                }

                else if (keyInfo.Key == ConsoleKey.Enter && editMode && pointer >= 0)
                {
                    chars[pointer++] = '\n';
                    PrintScreen(chars, pointer, infoBar, editMode);
                    continue;
                }
                else if (keyInfo.Key == ConsoleKey.Backspace && editMode && pointer >= 0)
                {
                    if (pointer > 0) pointer--;

                    chars[pointer] = '\0';

                    PrintScreen(chars, pointer, infoBar, editMode);
                    continue;
                }

                if (editMode && pointer >= 0)
                {
                    chars[pointer++] = keyInfo.KeyChar;
                    PrintScreen(chars, pointer, infoBar, editMode);
                }

            } while (true);
        }

        public static void PrintScreen(char[] chars, int pointer, string infoBar, bool editMode)
        {
            int lineCount = 0;
            int characterCount = 0;

            Console.Clear();

            for (int i = 0; i < pointer; i++)
            {
                if (chars[i] == '\n')
                {
                    Console.WriteLine();

                    lineCount++;
                    characterCount = 0;
                }
                else
                {
                    Console.Write(chars[i]);

                    characterCount++;
                    if (characterCount % 80 == 79)
                    {
                        lineCount++;
                    }
                }
            }

            //Console.Write(" _");

            // Print virtual lines
            for (int i = 0; i < SAFELINE - lineCount; i++)
            {
                Console.WriteLine();
            }

            // Print instructions
            Console.WriteLine();

            for (int i = 0; i < SAFEWIDTH; i++)
            {
                if (i < infoBar.Length)
                {
                    ColorConsole.Write(ConsoleColor.Yellow, infoBar[i]);
                }
                else
                {
                    ColorConsole.Write(ConsoleColor.Yellow, " ");
                }
            }

            if (editMode)
            {
                ColorConsole.Write(ConsoleColor.Yellow, (lineCount+1) + "," + characterCount);
            }

            Console.WriteLine();
        }

        static bool IsForbiddenKey(ConsoleKey key)
        {
            ConsoleKey[] forbiddenKeys = { ConsoleKey.Print, ConsoleKey.PrintScreen, ConsoleKey.Pause, ConsoleKey.Home, ConsoleKey.PageUp, ConsoleKey.PageDown, ConsoleKey.End, ConsoleKey.NumPad0, ConsoleKey.NumPad1, ConsoleKey.NumPad2, ConsoleKey.NumPad3, ConsoleKey.NumPad4, ConsoleKey.NumPad5, ConsoleKey.NumPad6, ConsoleKey.NumPad7, ConsoleKey.NumPad8, ConsoleKey.NumPad9, ConsoleKey.Insert, ConsoleKey.F1, ConsoleKey.F2, ConsoleKey.F3, ConsoleKey.F4, ConsoleKey.F5, ConsoleKey.F6, ConsoleKey.F7, ConsoleKey.F8, ConsoleKey.F9, ConsoleKey.F10, ConsoleKey.F11, ConsoleKey.F12, ConsoleKey.Add, ConsoleKey.Divide, ConsoleKey.Multiply, ConsoleKey.Subtract, ConsoleKey.LeftWindows, ConsoleKey.RightWindows };
            
            for (int i = 0; i < forbiddenKeys.Length; i++)
            {
                if (key == forbiddenKeys[i])
                {
                    return true;
                }
            }

            return false;
        }

        static string StringCopy(string value)
        {
            string newString = string.Empty;

            for (int i = 0; i < value.Length - 1; i++)
            {
                newString += value[i];
            }

            return newString;
        }
    }
}
