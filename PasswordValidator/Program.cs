using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace PasswordValidator
{
    internal class Program
    {
        #region Main
        static void Main(string[] args)
        {
            Controller();
        }

        private static void Controller()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region View
        public static void View(string password)
        {
            Console.Title = "Password Validator";

            Console.WriteLine("Password Rules" + "\n1. Needs to be between 12 and 64 characters" + "\n2. Needs to have a upper and lowercase" + "\n3. Needs to be a mix of characters and numbers" + "\n4. Needs to have a speciel character");

            Console.WriteLine("\nType you're password: ");
            Console.Write("> ");
            password = Console.ReadLine();



            Console.ReadLine();
        }
        #endregion

        #region Controller
        public static void Controller(string password)
        {
            View(password);
        }
        #endregion
    }
}
