namespace TingTingOS.Utils
{
    public static class Reference
    {
        public static string Version = "0.0.1";

        public static string RootPath = @"0:\";
        public static string CurrentDir = RootPath;

        public static string DefaultAccessPrefix = "$ ";
        public static string RootAccessPrefix = "# ";

        public static bool IsInstalled;

        public static Accounts.Account UserAccount = null;
    }
}
