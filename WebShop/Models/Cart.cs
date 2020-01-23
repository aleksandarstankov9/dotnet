using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebShop.Models
{
    public class Cart
    {
        private List<CartItem> itemCollection = new List<CartItem>();

        public virtual void AddItem(Product product, int quantity)
        {
            CartItem ci = itemCollection.SingleOrDefault(c => c.Product.ProductId == product.ProductId);

            if (ci == null)
            {
                ci = new CartItem
                { 
                    Product = product,
                    Quantity = quantity
                };
                itemCollection.Add(ci);
            }
            else
            {
                ci.Quantity += quantity;
            }
        }

        public virtual void DeleteItem(int id)
        {
            CartItem ci = itemCollection.SingleOrDefault(c => c.Product.ProductId == id);

            if (ci != null)
            {
                itemCollection.Remove(ci);
            }
        }

        public virtual void UpdateItem(Product product, int quantity)
        {
            CartItem ci = itemCollection.SingleOrDefault(c => c.Product.ProductId == product.ProductId);

            if (ci != null)
            {
                ci.Quantity = quantity;
            }
        }

        public virtual decimal CartValue()
        {
            decimal value = itemCollection.Sum(c => c.Product.Price * c.Quantity);

            return value;
        }


        public virtual void DeleteCart()
        {
            itemCollection.Clear();
        }

        public List<CartItem> Items => itemCollection;
    }
}
