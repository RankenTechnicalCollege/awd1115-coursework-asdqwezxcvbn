using System;

namespace MyApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int rows, cols;
            bool isValid;

            do
            {
                Console.WriteLine("How Many Rows Should The Table Have?");
                isValid = int.TryParse(Console.ReadLine(), out rows);
            }
            while (!isValid);

            do
            {
                Console.WriteLine("How Many Columns Should The Table Have?");
                isValid = int.TryParse(Console.ReadLine(), out cols);
            }
            while (!isValid);

            for (int row = 0; row <= rows; row++)
            {
                if (row == 0)
                {
                    for (int col = 0; col <= cols; col++)
                    {
                        Console.Write($"{col,6} |");
                    }
                    Console.WriteLine();
                    Console.Write(new string('-', 8 * (cols + 1)));
                    Console.WriteLine();
                }

                else
                {
                    Console.Write($"{row,6} |");
                    for (int col = 1; col <= cols; col++)
                    {
                        Console.Write($"{row * col,6} |");
                    }
                    Console.WriteLine();
                }
            }
        }
    }
}