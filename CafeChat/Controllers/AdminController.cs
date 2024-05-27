using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using NToastNotify;
using CafeChat.Constants;
using static CafeChat.Constants.UsersConstants;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace CafeChat.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;
        private readonly IUsersService _userService;
        private readonly IToastNotification _toastNotification;

        public AdminController(IAdminService adminService, IUsersService userService, IToastNotification toastNotification)
        {
            this._adminService = adminService;
            this._userService = userService;
            this._toastNotification = toastNotification;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AdminPage()
        {
            return View();
        }

        // CafeAdd Page
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult GoToCafeAddPage()
        {
            return RedirectToAction("CafeAdd", "Admin");
        }
        
        [Authorize(Roles = "Admin")]
        public IActionResult CafeAdd()
        {
            var managers = _userService.GetAllManagers();
            ViewBag.Managers = managers;
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult CafeAdd(Cafe cafeToAdd)
        {
            _adminService.AddCafe(cafeToAdd);
            _toastNotification.AddSuccessToastMessage("Cafe Başarıyla Eklendi.");
            return RedirectToAction("AdminPage", "Admin");
        }

        // AddUser Page
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult GoToAddUserPage()
        {
            return RedirectToAction("AddUser", "Admin");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AddUser()
        {
            var userTypes = _userService.GetAllUserTypes();
            ViewBag.UserTypes = userTypes;
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult AddUser(Users userToAdd)
        {
            _userService.UserAdd(userToAdd);
            _toastNotification.AddSuccessToastMessage("Kullanıcı Başarıyla Eklendi.");
            return RedirectToAction("AdminPage", "Admin");
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult GoToDeleteCafePage()
        {
            return RedirectToAction("DeleteCafe", "Admin");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult DeleteCafe()
        {
            var allCafes = _adminService.GetAllCafes();
            ViewBag.Cafes = allCafes;
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult DeleteCafe(Cafe cafe)
        {
            _adminService.DeleteCafeById(cafe.Id);
            _toastNotification.AddSuccessToastMessage("Cafe Başarıyla Silindi.");
            return RedirectToAction("DeleteCafe", "Admin");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult ChangeCafeStatus(Cafe cafe)
        {
            _adminService.ChangeCafeStatusById(cafe.Id);
            _toastNotification.AddSuccessToastMessage("Statü Başarıyla Değiştirildi.");
            return RedirectToAction("DeleteCafe", "Admin");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult GoToUpdateUserPage()
        {
            return RedirectToAction("UpdateUser", "Admin");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult UpdateUser()
        {
            var allUsers = _userService.GetAllList();
            var userTypes = _userService.GetAllUserTypes();
            ViewBag.Users = allUsers;
            ViewBag.UserTypes = userTypes;
            ViewBag.UserStatuses = UsersConstants.StatusList ;

            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult UpdateUser(Users user)
        {
            _userService.UserUpdate(user);
            _toastNotification.AddSuccessToastMessage("Kullanıcı Başarıyla Güncellendi.");
            return RedirectToAction("UpdateUser", "Admin");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult DeleteUser(Users user)
        {
            _userService.UserDelete(user);
            _toastNotification.AddSuccessToastMessage("Kullanıcı Başarıyla Silindi.");
            return RedirectToAction("UpdateUser", "Admin");
        }
    }
}
