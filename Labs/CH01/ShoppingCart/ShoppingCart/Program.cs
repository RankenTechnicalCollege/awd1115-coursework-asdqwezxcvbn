using ShoppingCart;
using System;

namespace MyApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Cart cart1 = new Cart("CART1234");

            cart1.AddItem("Lollypop", 2.5);
            cart1.AddItem("Gum", 1.5);
            cart1.AddItem("Soda", 3.75);

            Console.WriteLine(cart1);

            Console.WriteLine("Press Any Key Twice To Exit");
            Console.ReadKey();  
        }
    }
}