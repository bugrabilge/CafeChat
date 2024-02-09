using Microsoft.AspNetCore.Mvc;

namespace CafeChat.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult AdminPage()
        {
            return View();
        }
    }
}
