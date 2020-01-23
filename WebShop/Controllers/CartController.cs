using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebShop.Models;
using WebShop.Services;

namespace WebShop.Controllers
{
    
    public class CartController : Controller
    {
        private readonly WebShopDbContext db;
        private Cart cart;
        private CartService cService;

        public CartController(WebShopDbContext _db, CartService _cService)
        {
            db = _db;
            cService = _cService;
            cart = cService.ReadCart();
        }

        public IActionResult Index()
        {
            return View(cart);
        }

        public IActionResult AddItem(int productId)
        {
            Product p1 = db.Products.SingleOrDefault(p => p.ProductId == productId);

            if (p1 != null)
            {
                cart.AddItem(p1, 1);
                cService.SaveCart(cart);
            }
            return RedirectToAction("Index");
        }

        public IActionResult DeleteItem(int productId)
        {
            Product p1 = db.Products.SingleOrDefault(p => p.ProductId == productId);

            if (p1 != null)
            {
                cart.DeleteItem(p1.ProductId);
                cService.SaveCart(cart);
            }
            return RedirectToAction("Index");
        }

        public IActionResult UpdateItem(int productId, int quantity)
        {
            Product p1 = db.Products.SingleOrDefault(p => p.ProductId == productId);

            if (p1 != null)
            {
                cart.UpdateItem(p1, quantity);
                cService.SaveCart(cart);
            }
            return RedirectToAction("Index");
        }
    }
}