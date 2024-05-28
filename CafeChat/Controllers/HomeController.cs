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
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.AspNetCore.Identity;

namespace CafeChat.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUsersService _usersService;
        private readonly ILoginService _loginService;
        public static Users loggedUser;

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
                ClaimsIdentity identity = _loginService.SetRolesAndAuthenticate(result.Data);
                var principal = new ClaimsPrincipal(identity);
                var login = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                loggedUser = result.Data;

                return RedirectDueToUserType(result.Data.UserTypeId);

            }
            else
            {
                return View();
            }
        }
        public IActionResult RedirectDueToUserType(int userTypeId)
        {
            switch (userTypeId)
            {
                case (int)UsersConstants.UserTypes.Admin:
                    return RedirectToAction("AdminPage", "Admin");

                case (int)UsersConstants.UserTypes.CafeManager:
                    return RedirectToAction("ManagerPage", "Manager");

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

        public IActionResult AccessDeniedPage()
        {
            return View();
        }

        public static Users LoggedUser()
        {
            return loggedUser;
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