using System;
using System.IO;
using System.Text;

using TingTingOS.Utils;

namespace TingTingOS.Accounts
{
    public class Account
    {
        private string name;
        private string password;

        public Account(string name, string password)
        {
            this.name = name;
            this.password = Convert.ToBase64String(Encoding.ASCII.GetBytes(password));
        }

        public string GetUsername()
        {
            return name;
        }

        public string GetPassword(bool decrypt = false)
        {
            if (decrypt)
            {
                return Encoding.ASCII.GetString(Convert.FromBase64String(password));
            }
            else
            {
                return password;
            }

        }

        public void CreateAccount()
        {
            string accountPath = Reference.RootPath + @"Accounts\" + name + ".txt";

            if (!Directory.Exists(Reference.RootPath + "Accounts"))
            {
                Directory.CreateDirectory(Reference.RootPath + "Accounts");
            }

            if (!File.Exists(accountPath))
            {
                File.WriteAllText(accountPath, name + ":" + password);

                ColorConsole.WriteLine(ConsoleColor.Green, "Account created for " + name);
            }
            else
            {
                ColorConsole.WriteLine(ConsoleColor.Red, "This account already exists.");
            }
        }
    }
}
