using System;
using System.IO;

using TingTingOS.Utils;

namespace TingTingOS.Programs
{
    public static class Textpad
    {
        public static void Run(string filepath = null)
        {
            // Console.Clear();

        FileReader:
            string fullFilepath = Reference.CurrentDirectory;

            if (string.IsNullOrEmpty(filepath))
            {
                ColorConsole.Write(ConsoleColor.Yellow, "Filename: ");
                string filename = Console.ReadLine();

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

            // fullFilepath = filepath.Contains(Reference.RootPath) ? filepath : Reference.CurrentDirectory + @"\" + filepath;

            if (!File.Exists(fullFilepath))
            {
                ColorConsole.WriteLine(ConsoleColor.Red, "File doesn't exist");
                // filepath = string.Empty;

                return;
                // goto FileReader;
            }

            string[] lines = File.ReadAllLines(fullFilepath);

            // Console.Clear();
            for (int i = 0; i < lines.Length; i++)
            {
                ColorConsole.WriteLine(ConsoleColor.Cyan, lines[i]);
            }

            //Console.ReadKey(true);

        }
    }
}
