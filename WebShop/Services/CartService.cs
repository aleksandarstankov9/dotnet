using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop.Models;
using WebShop.Extensions;

namespace WebShop.Services
{
    public class CartService
    {
        private readonly IHttpContextAccessor accessor;

        public CartService(IHttpContextAccessor _accessor)
        {
            accessor = _accessor;
        }

        public Cart ReadCart()
        {
            Cart c = null;

            ISession session = accessor.HttpContext.Session;

            if (session.DeserializeCart("cartKey") != null)
            {
                c = session.DeserializeCart("cartKey");
            }
            else
            {
                c = new Cart();
            }

            return c;
        }

        public void SaveCart(Cart c)
        {
            accessor.HttpContext.Session.SerializeCart("cartKey", c);
        }

        public void DeleteCart()
        {
            accessor.HttpContext.Session.Clear();
        }
    }
}
