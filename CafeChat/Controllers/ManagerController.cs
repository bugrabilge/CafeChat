using BusinessLayer.Abstract;
using CafeChat.Constants;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using System.Data;

namespace CafeChat.Controllers
{
    public class ManagerController : Controller
    {
        private readonly IAdminService _adminService;
        private readonly IUsersService _userService;
        private readonly IToastNotification _toastNotification;
        private readonly Users loggedManager;

        public ManagerController(IAdminService adminService, IUsersService userService, IToastNotification toastNotification)
        {
            this._adminService = adminService;
            this._userService = userService;
            this._toastNotification = toastNotification;
            this.loggedManager = HomeController.LoggedUser();
        }

        [Authorize(Roles = "Admin, CafeManager")]
        public IActionResult ManagerPage()
        {
            return View();
        }

        // AddUser Page
        [Authorize(Roles = "Admin, CafeManager")]
        [HttpPost]
        public IActionResult GoToAddPersonelPage()
        {
            return RedirectToAction("AddPersonel", "Admin");
        }

        [Authorize(Roles = "Admin, CafeManager")]
        public IActionResult AddPersonel()
        {
            var userTypes = _userService.GetAllUserTypes();
            ViewBag.UserTypes = userTypes;
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin, CafeManager")]
        public IActionResult AddPersonel(Users userToAdd)
        {
            userToAdd.UserStatus = true;
            userToAdd.UserTypeId = 3;
            userToAdd.CafeId = loggedManager.CafeId;

            _userService.UserAdd(userToAdd);
            _toastNotification.AddSuccessToastMessage("Personel Başarıyla Eklendi.");
            return RedirectToAction("ManagerPage", "Manager");
        }

        [Authorize(Roles = "Admin, CafeManager")]
        [HttpPost]
        public IActionResult GoToUpdatePersonelPage()
        {
            return RedirectToAction("UpdatePersonel", "Manager");
        }

        [Authorize(Roles = "Admin, CafeManager")]
        public IActionResult UpdatePersonel()
        {
            var allPersonels = _userService.GetAllList().Where(x => x.UserTypeId == 3 && x.CafeId == loggedManager.CafeId);
            var userTypes = _userService.GetAllUserTypes();
            ViewBag.Users = allPersonels;
            ViewBag.UserTypes = userTypes;
            ViewBag.UserStatuses = UsersConstants.StatusList;

            return View();
        }

        [Authorize(Roles = "Admin, CafeManager")]
        [HttpPost]
        public IActionResult UpdatePersonel(Users user)
        {
            user.UserStatus = true;
            user.UserTypeId = 3;
            user.CafeId = loggedManager.CafeId;

            _userService.UserUpdate(user);
            _toastNotification.AddSuccessToastMessage("Personel Başarıyla Güncellendi.");
            return RedirectToAction("UpdatePersonel", "Manager");
        }

        [Authorize(Roles = "Admin, CafeManager")]
        [HttpPost]
        public IActionResult DeletePersonel(Users user)
        {
            _userService.UserDelete(user);
            _toastNotification.AddSuccessToastMessage("Personel Başarıyla Silindi.");
            return RedirectToAction("UpdatePersonel", "Manager");
        }
    }
}
