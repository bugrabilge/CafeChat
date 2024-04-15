using BusinessLayer.Abstract;
using BusinessLayer.Results.Abstract;
using CafeChat.Constants;
using CafeChat.Models;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CafeChat.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUsersService _usersService;
        private readonly ILoginService _loginService;

        public HomeController(ILogger<HomeController> logger, IUsersService userService, ILoginService loginService)
        {
            _logger = logger;
            _usersService = userService;
            _loginService = loginService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Users user)
        {

            IDataResult<Users> result = _loginService.CheckLoginCredentials(user);
            ViewBag.Message = result.Message;
            ViewBag.Status = result.Status;

            if (result.Status) 
            {
                var sessionName = "userId";
                HttpContext.Session.SetInt32(sessionName, result.Data.Id);
                return RedirectDueToUserType(result.Data.UserTypeId);

            }
            else
            {
                return View();
            }
            // Check username and password
            //using (var context = new Context())
            //{
            //    var loginUser = context.Users.FirstOrDefault(x => x.Username == user.Username && x.Password == user.Password);

            //    if (loginUser != null)
            //    {
            //        var sessionName = "userId";
            //        HttpContext.Session.SetInt32(sessionName, loginUser.Id);
            //        ViewBag.Log = "Login Succeeded";

            //        // Cafe Personel
            //        if (loginUser.UserTypeId == (int)UsersConstants.UserTypes.CafePersonel)
            //        {
            //            return RedirectToAction("Index", "Service");
            //        }

            //        // Cafe Manager
            //        if (loginUser.UserTypeId == (int)UsersConstants.UserTypes.CafeManager)
            //        {
            //            return RedirectToAction("Index", "Service");
            //        }

            //        // Admin
            //        if(loginUser.UserTypeId == (int)UsersConstants.UserTypes.Admin)
            //        {
            //            return RedirectToAction("AdminPage", "Admin");
            //        }

            //    }
            //    else
            //    {
            //        ViewBag.Log = "Access Denied";
            //        return View();
            //    }
            //}
        }
        public IActionResult RedirectDueToUserType(int userTypeId)
        {
            switch (userTypeId)
            {
                case (int)UsersConstants.UserTypes.Admin:
                    return RedirectToAction("AdminPage", "Admin");

                case (int)UsersConstants.UserTypes.CafeManager:
                    return RedirectToAction("Index", "Service");

                case (int)UsersConstants.UserTypes.CafePersonel:
                    return RedirectToAction("Index", "Service");
                default:
                    return View();
            }
        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
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