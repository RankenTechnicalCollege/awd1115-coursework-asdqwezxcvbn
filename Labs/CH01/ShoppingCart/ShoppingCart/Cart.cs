using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart
{
    internal class Cart
    {
        private string? _cartId;
        private Dictionary<string, double> _items;

        public Cart(string cartId)
        {
            _cartId = cartId;
            _items = new Dictionary<string, double>();
        }

        public void AddItem(string item, double price)
        {
            _items.Add(item, price);
        }

        public void RemoveItem(string item)
        {
            _items.Remove(item);
        }

        public double GetTotal()
        {
            return _items.Values.Sum();
        }

        public override string ToString()
        {
            string result = "Cart ID: " + _cartId + "\nItems:\n";
            foreach (var item in _items)
            {
                result += $"{item.Key}: ${item.Value}\n";
            }
            result += "Total: $" + GetTotal();
            return result;
        }
    }
}
