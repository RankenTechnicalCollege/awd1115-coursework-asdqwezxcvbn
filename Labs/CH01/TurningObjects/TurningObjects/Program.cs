using System;

namespace TurningObjects
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Leaf leaf = new Leaf();
            Pancake pancake = new Pancake();
            Page page = new Page();
            Corner corner = new Corner();

            List<ITurnable> turnables = [ leaf, pancake, page, corner ];

            static void PrintTurn(List<ITurnable> t)
            {
                foreach (ITurnable turn in t)
                {
                    Console.WriteLine(turn.Turn());
                }
            }

            PrintTurn(turnables);   

            Console.WriteLine("Press Any Key Twice To Exit...");
            Console.ReadKey();  
        }
    }
}