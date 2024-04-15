using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using NToastNotify;

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

        public IActionResult AdminPage()
        {
            return View();
        }

        // CafeAdd Page
        [HttpPost]
        public IActionResult GoToCafeAddPage()
        {
            return RedirectToAction("CafeAdd", "Admin");
        }

        public IActionResult CafeAdd()
        {
            var managers = _userService.GetAllManagers();
            ViewBag.Managers = managers;
            return View();
        }

        [HttpPost]
        public IActionResult CafeAdd(Cafe cafeToAdd)
        {
            _adminService.AddCafe(cafeToAdd);
            _toastNotification.AddSuccessToastMessage("Cafe Başarıyla Eklendi.");
            return RedirectToAction("AdminPage", "Admin");
        }

        // AddUser Page
        [HttpPost]
        public IActionResult GoToAddUserPage()
        {
            return RedirectToAction("AddUser", "Admin");
        }

        public IActionResult AddUser()
        {
            var userTypes = _userService.GetAllUserTypes();
            ViewBag.UserTypes = userTypes;
            return View();
        }

        [HttpPost]
        public IActionResult AddUser(Users userToAdd)
        {
            _userService.UserAdd(userToAdd);
            _toastNotification.AddSuccessToastMessage("Kullanıcı Başarıyla Eklendi.");
            return RedirectToAction("AdminPage", "Admin");
        }

        [HttpPost]
        public IActionResult GoToDeleteCafePage()
        {
            return RedirectToAction("DeleteCafe", "Admin");
        }

        public IActionResult DeleteCafe()
        {
            var allCafes = _adminService.GetAllCafes();
            ViewBag.Cafes = allCafes;
            return View();
        }

        [HttpPost]
        public IActionResult DeleteCafe(Cafe cafe)
        {
            _adminService.DeleteCafeById(cafe.Id);
            _toastNotification.AddSuccessToastMessage("Cafe Başarıyla Silindi.");
            return RedirectToAction("DeleteCafe", "Admin");
        }

        [HttpPost]
        public IActionResult ChangeCafeStatus(Cafe cafe)
        {
            _adminService.ChangeCafeStatusById(cafe.Id);
            _toastNotification.AddSuccessToastMessage("Statü Başarıyla Değiştirildi.");
            return RedirectToAction("DeleteCafe", "Admin");
        }

        [HttpPost]
        public IActionResult GoToUpdateUserPage()
        {
            return RedirectToAction("UpdateUser", "Admin");
        }
        public IActionResult UpdateUser()
        {
            var allUsers = _userService.GetAllList();
            var userTypes = _userService.GetAllUserTypes();
            ViewBag.Users = allUsers;
            ViewBag.UserTypes = userTypes;

            return View();
        }

        [HttpPost]
        public IActionResult UpdateUser(Users user)
        {
            _userService.UserUpdate(user);
            _toastNotification.AddSuccessToastMessage("Kullanıcı Başarıyla Güncellendi.");
            return RedirectToAction("UpdateUser", "Admin");
        }
    }
}
