using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;


namespace PasswordValidator
{
    class Program
    {
        static void Main(string[] args)
        {
            string password = GetPasswordFromUser();
            PasswordStrength strength = ValidatePassword(password);

                Console.ResetColor();
                Console.Clear();
            if (strength == PasswordStrength.Strong)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Password is OK");
                Console.ReadLine();
            }
            else if (strength == PasswordStrength.Weak)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Password is OK but weak");
                Console.ReadLine();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Password is not strong enough");
                Console.ReadLine();
            }
        }

        static string GetPasswordFromUser()
        {
            Console.Clear();
            Console.ResetColor();
            Console.Write("Enter your password: ");
            return Console.ReadLine();
        }

        static PasswordStrength ValidatePassword(string password)
        {
            if (password.Length < 12 || password.Length > 64)
                return PasswordStrength.Invalid;

            bool hasUpperCase = false;
            bool hasLowerCase = false;
            bool hasDigit = false;
            bool hasSpecialChar = false;

            for (int i = 0; i < password.Length; i++)
            {
                char c = password[i];

                if (char.IsUpper(c))
                    hasUpperCase = true;
                else if (char.IsLower(c))
                    hasLowerCase = true;
                else if (char.IsDigit(c))
                    hasDigit = true;
                else if (IsSpecialCharacter(c))
                    hasSpecialChar = true;

                if (i >= 3)
                {
                    if (CheckConsecutiveDigits(password, i, 4) || CheckConsecutiveDigits(password, i, 6))
                        return PasswordStrength.Invalid;

                    if (i >= 4 && password[i] == password[i - 1] && password[i] == password[i - 2] && password[i] == password[i - 3])
                        return PasswordStrength.Invalid;
                }
            }

            if (hasUpperCase && hasLowerCase && hasDigit && hasSpecialChar)
            {
                if (password.Length >= 16)
                    return PasswordStrength.Strong;
                else
                    return PasswordStrength.Weak;
            }

            return PasswordStrength.Invalid;
        }

        static bool IsSpecialCharacter(char c)
        {
            return !char.IsLetterOrDigit(c);
        }

        static bool CheckConsecutiveDigits(string input, int startIndex, int count)
        {
            for (int i = startIndex; i < startIndex + count - 1; i++)
            {
                if (!char.IsDigit(input[i]) || input[i] != input[i + 1] - 1)
                    return false;
            }

            return true;
        }
    }

    enum PasswordStrength
    {
        Invalid,
        Weak,
        Strong
    }
}

