using System;

namespace MyApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string ccNum;

            do
            {
                Console.WriteLine("Enter your credit card number:");
                ccNum = Console.ReadLine();
            } while (String.IsNullOrEmpty(ccNum) && ccNum.Length < 16 || ccNum.Length > 19);

            string maskedCcNum = String.Empty;

            for (int i = 0; i < ccNum.Length; i++)
            {
                if (ccNum[i] == '-' || Char.IsWhiteSpace(ccNum[i]) || i >= ccNum.Length - 4 )
                {
                    maskedCcNum += ccNum[i];
                }
                else
                {
                    maskedCcNum += 'X';
                }
            }

            Console.WriteLine($"Your credit card number is {maskedCcNum}");

            Console.WriteLine("Press Any Key Twice To Exit...");
            Console.ReadKey();
        }
    }
}