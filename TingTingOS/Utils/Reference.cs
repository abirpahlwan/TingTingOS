namespace TingTingOS.Utils
{
    public static class Reference
    {
        public static string Version = "0.0.5";

        public static string RootPath = @"0:\";
        public static string CurrentDirectory = RootPath;

        public static string DefaultAccessPrefix = "$ ";
        public static string RootAccessPrefix = "# ";

        public static Cosmos.System.FileSystem.CosmosVFS FAT = new Cosmos.System.FileSystem.CosmosVFS();

        public static Accounts.Account UserAccount = null;
    }
}
