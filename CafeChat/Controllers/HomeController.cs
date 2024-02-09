using BusinessLayer.Abstract;
using CafeChat.Models;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CafeChat.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUsersService _usersService;

        public HomeController(ILogger<HomeController> logger, IUsersService userService)
        {
            _logger = logger;
            _usersService = userService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Users user)
        {
            // Check username and password
            using (var context = new Context())
            {
                var loginUser = context.Users.FirstOrDefault(x => x.Username == user.Username && x.Password == user.Password);

                if (loginUser != null)
                {
                    // Cafe Personel
                    if (loginUser.UserType == 0)
                    {
                        var sessionName = "userId";
                        HttpContext.Session.SetInt32(sessionName, loginUser.UserID);
                        ViewBag.Log = "Login Succeeded";
                        return RedirectToAction("Index", "Service");
                    }

                    // Cafe Manager
                    if (loginUser.UserType == 1)
                    {
                        var sessionName = "userId";
                        HttpContext.Session.SetInt32(sessionName, loginUser.UserID);
                        ViewBag.Log = "Login Succeeded";
                        return RedirectToAction("Index", "Service");
                    }

                    // Admin
                    else
                    {
                        var sessionName = "userId";
                        HttpContext.Session.SetInt32(sessionName, loginUser.UserID);
                        ViewBag.Log = "Login Succeeded";
                        return RedirectToAction("AdminPage", "Admin");
                    }
                }
                else
                {
                    ViewBag.Log = "Access Denied";
                    return View();
                }
            }
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