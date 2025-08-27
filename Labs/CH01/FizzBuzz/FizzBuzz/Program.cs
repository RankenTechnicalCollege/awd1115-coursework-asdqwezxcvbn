using System;

namespace FizzBuzz
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("enter Your Upper Limit Of Your Fizz Buzz");
            int upperLimit = int.Parse(Console.ReadLine());

            for (int i = 1; i <= upperLimit; i++)
            {
                if (i % 3 == 0 && i % 5 == 0)
                {
                    Console.WriteLine("FizzBuzz");
                }
                else if (i % 3 == 0)
                {
                    Console.WriteLine("Fizz");
                }
                else if (i % 5 == 0)
                {
                    Console.WriteLine("Buzz");
                }
                else
                {
                    Console.WriteLine(i);
                }
            }

            Console.WriteLine("Press Enter Twice To Exit");
            Console.ReadLine();
        }
    }
}