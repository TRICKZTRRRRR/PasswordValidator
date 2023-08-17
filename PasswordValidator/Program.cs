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
            Console.Write("Enter a password: ");
            string password = Console.ReadLine();

            // Call the method to check password strength
            PasswordStrengthResult result = CheckPasswordStrength(password);

            // Display the appropriate message based on the result
            if (result == PasswordStrengthResult.Strong)
                Console.ForegroundColor = ConsoleColor.Green;
            else if (result == PasswordStrengthResult.Weak)
                Console.ForegroundColor = ConsoleColor.Yellow;
            else
                Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine(GetResultMessage(result));
            Console.ResetColor();
            Console.ReadLine();
        }

        // Enum to define the possible password strength results
        enum PasswordStrengthResult
        {
            Strong,
            Weak,
            Invalid
        }

        // Method to check password strength
        static PasswordStrengthResult CheckPasswordStrength(string password)
        {
            if (password.Length < 12 || password.Length > 64)
                return PasswordStrengthResult.Invalid;

            bool hasUpperCase = false;
            bool hasLowerCase = false;
            bool hasDigit = false;
            bool hasSpecialChar = false;
            char prevChar = '\0';
            int consecutiveCount = 1;

            foreach (char c in password)
            {
                if (char.IsUpper(c)) hasUpperCase = true;
                if (char.IsLower(c)) hasLowerCase = true;
                if (char.IsDigit(c)) hasDigit = true;
                if (char.IsSymbol(c) || char.IsPunctuation(c)) hasSpecialChar = true;

                // Check for consecutive characters
                if (c == prevChar)
                {
                    consecutiveCount++;
                    if (consecutiveCount >= 4)
                        return PasswordStrengthResult.Weak;
                }
                else
                {
                    consecutiveCount = 1;
                }

                prevChar = c;
            }

            // Check for the specific number sequences
            if (password.Contains("1234") || password.Contains("3456"))
                return PasswordStrengthResult.Weak;

            // Determine the final result based on the conditions
            if (hasUpperCase && hasLowerCase && hasDigit && hasSpecialChar)
                return PasswordStrengthResult.Strong;
            else
                return PasswordStrengthResult.Weak;
        }

        // Method to get the appropriate result message
        static string GetResultMessage(PasswordStrengthResult result)
        {
            if (result == PasswordStrengthResult.Strong)
                return "Password is OK";
            else if (result == PasswordStrengthResult.Weak)
                return "Password is OK but considered weak";
            else
                return "Password is not strong enough";
        }
    }
}

