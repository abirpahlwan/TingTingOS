using System;

using Sys = Cosmos.System;

namespace TingTingOS.Utils
{
    public static class FileSystem
    {
        public static void LoadFileSystem()
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
    }
}
