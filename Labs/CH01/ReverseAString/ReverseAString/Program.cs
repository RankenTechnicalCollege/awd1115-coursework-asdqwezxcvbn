using System;

namespace MyApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string name = "Ashtin";

            for (int i = name.Length - 1; i >= 0; i--)
            {
                Console.WriteLine(name[i]);
            }
        }
    }
}