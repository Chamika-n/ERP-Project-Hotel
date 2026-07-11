using Microsoft.AspNetCore.Mvc;

namespace GrandHotel.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()     
        {
            return View();
        }
    }
}
