using System;
using System.Collections.Generic;
using System.Text;

namespace TingTingOS.Utils
{
    public static class Reference
    {
        public static string Version = "0.0.1";

        public static string RootPath = @"0:\";
        public static string CurrentDir = RootPath;

        public static string DefaultAccess = "$"; // Means the user has root (or admin) permissions
        public static string RootAccess = "#"; // Means the user has root (or admin) permissions

        public static bool Installed;
    }
}
