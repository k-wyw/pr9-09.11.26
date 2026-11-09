using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pr9
{
    public static class CartManager
    {
        public static List<Product> CartItems = new List<Product>();

        public static void Add(Product product)
        {
            CartItems.Add(product);
        }

        public static void Clear()
        {
            CartItems.Clear();
        }
    }
}