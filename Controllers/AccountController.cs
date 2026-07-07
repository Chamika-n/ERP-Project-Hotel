using GrandHotel.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
namespace GrandHotel.Controllers
{
    public class AccountController : Controller
    {
        // 1. GET: Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // 2. POST: Login
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {

                if (model.Email == "admin@hotelerp.com" && model.Password == "Admin@123")
                {
                    HttpContext.Session.SetString("UserRole", "Admin");
                    return RedirectToAction("Index", "Dashboard");
                }


                else if (model.Email == "reception@hotelerp.com" && model.Password == "Recep@123")
                {
                    HttpContext.Session.SetString("UserRole", "Receptionist");
                    return RedirectToAction("Index", "Dashboard");
                }


                else if (model.Email == "cashier@hotelerp.com" && model.Password == "Cash@123")
                {
                    HttpContext.Session.SetString("UserRole", "Cashier");
                    return RedirectToAction("Index", "Dashboard");
                }


                else if (model.Email == "housekeeping@hotelerp.com" && model.Password == "House@123")
                {
                    HttpContext.Session.SetString("UserRole", "Housekeeping");
                    return RedirectToAction("Index", "Dashboard");
                }


                else if (model.Email == "inventory@hotelerp.com" && model.Password == "Inven@123")
                {
                    HttpContext.Session.SetString("UserRole", "InventoryManager");
                    return RedirectToAction("Index", "Dashboard");
                }

                ModelState.AddModelError(string.Empty, "Invalid email or password.");
            }

            return View(model);
        }
    }
}