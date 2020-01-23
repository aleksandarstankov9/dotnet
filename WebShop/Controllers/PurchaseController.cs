using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebShop.Data;
using WebShop.Models;
using WebShop.Services;

namespace WebShop.Controllers
{
    public class PurchaseController : Controller
    {
        private readonly WebShopDbContext db;
        private readonly UserManager<ApplicationUser> um;
        private CartService cService;

        public PurchaseController(WebShopDbContext _db, UserManager<ApplicationUser> _um, CartService _cService)
        {
            db = _db;
            um = _um;
            cService = _cService;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            Cart cart = cService.ReadCart();
            if (cart.Items.Count() == 0)
            {
                return RedirectToAction("Index", "Home");
            }

            if (cart.Items.Count() == 1)
            {
                ViewBag.Message = "Product will be delivered to your home address";
            }
            else
            {
                ViewBag.Message = "Products will be delivered to your home address";
            }

            ApplicationUser user = await um.GetUserAsync(User);
            string id = user.Id;
            PurchaseOrder p1 = new PurchaseOrder
            {
                PersonId = id,
                DateOfOrder = DateTime.Now
            };

            try
            {
                db.PurchaseOrders.Add(p1);
                db.SaveChanges();

                int pId = p1.PurchaseOrderId;

                foreach (CartItem ci in cart.Items)
                {
                    Item it1 = new Item
                    {
                        PurchaseOrderId = pId,
                        ProductId = ci.Product.ProductId,
                    };
                    db.Items.Add(it1);
                    db.SaveChanges();
                }

                cService.DeleteCart();

                return View();
            }
            catch (Exception)
            {

                return RedirectToAction("Index", "Home");
            }
        }
    }
}