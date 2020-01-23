using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebShop.Data;

namespace WebShop.Controllers
{
    [Authorize(Roles ="admin")]
    public class RoleController : Controller
    {
        private readonly UserManager<ApplicationUser> um;
        private readonly ApplicationDbContext db;
        private readonly RoleManager<IdentityRole> rm;

        public RoleController(UserManager<ApplicationUser> _um, ApplicationDbContext _db, RoleManager<IdentityRole> _rm)
        {
            um = _um;
            db = _db;
            rm = _rm;
        }

        public IActionResult Index(string message)
        {
            ViewBag.Message = message;
            return View(rm.Roles.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IdentityRole ir)
        {
            var result = await rm.CreateAsync(ir);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", new { message = $"Role {ir.Name} created successfully" });
            }
            else
            {
                ViewBag.Message = "Error occurred while creating a new role";
                return View(ir);
            }
        }

        public IActionResult AddToRole()
        {
            ViewBag.Users = new SelectList(db.Users, "UserName", "UserName");
            ViewBag.Roles = new SelectList(db.Roles, "Name", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToRole(string user, string role)
        {
            ApplicationUser au = await um.FindByNameAsync(user);

            var rezultat = await um.AddToRoleAsync(au, role);

            if (rezultat.Succeeded)
            {
                return RedirectToAction("Index", new { message = $"User {user} added to the role {role}" });
            }
            else
            {
                ViewBag.Poruka = "Error occurred while adding user to role";
                return View();
            }
        }

        public async Task<IActionResult> Delete(string roleName)
        {
            IdentityRole ir = await rm.FindByNameAsync(roleName);
            return View(ir);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmation(string roleName)
        {
            IdentityRole ir = await rm.FindByNameAsync(roleName);

            try
            {
                db.Roles.Remove(ir);
                await db.SaveChangesAsync();

                return RedirectToAction("Index", new { message = "Role deleted" });
            }
            catch (Exception)
            {
                ViewBag.Message = "Error occurred while deleting role";
                return View(ir);
            }

        }
    }
}