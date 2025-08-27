using System;

namespace MyApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Input Your Name!");
            string name = Console.ReadLine();

            Console.WriteLine("Input Your Age!");
            double age = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine($"Hello {name}, you are {age} years old.");

            Console.WriteLine("Press Any Key Twice To Exit...");
            Console.ReadKey();
        }
    }
}