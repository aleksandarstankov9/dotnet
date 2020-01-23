using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebShop.Data;
using WebShop.Models;

namespace WebShop.Controllers
{
    public class HomeController : Controller
    {
        public WebShopDbContext db;        

        public HomeController(WebShopDbContext _db)
        {
            db = _db;            
        }

        public IActionResult Index(int id = 0)
        {
            ViewBag.Id = id;
            ViewBag.Categories = db.Categories.ToList();
            IEnumerable<Product> products = db.Products;
            if (id != 0)
            {
                products = products.Where(p => p.CategoryId == id);
            }
            return View(products.ToList());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }        
    }
}
