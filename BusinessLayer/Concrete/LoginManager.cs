using BusinessLayer.Abstract;
using BusinessLayer.Results.Abstract;
using BusinessLayer.Results.Concrete;
using CafeChat.Constants;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using EntityLayer.Constants;
using Microsoft.AspNetCore.Authentication.Cookies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class LoginManager : ILoginService
    {
        IUsersDal _usersDal;

        public LoginManager(IUsersDal usersDal)
        {
            _usersDal = usersDal;
        }
        public IDataResult<Users> CheckLoginCredentials(Users user)
        {
            var loginUser = _usersDal.GetListAll().FirstOrDefault(x => x.Username == user.Username && x.Password == user.Password);

            if (loginUser != null && !loginUser.UserStatus)
            {
                return new ErrorDataResult<Users>(AppMessages.ACCOUNT_NOT_ACTIVATED);
            }

            if (loginUser != null && loginUser.UserStatus)
            {
                return new SuccessDataResult<Users>(loginUser, AppMessages.LOGIN_SUCCEEDED);
            }
            else
            {
                return new ErrorDataResult<Users>(AppMessages.ACCESS_DENIED);
            }
        }

        public ClaimsIdentity SetRolesAndAuthenticate(Users user)
        {
            string userRole = "";

            switch (user.UserTypeId)
            {
                case 1:
                    userRole = "Admin";
                    break;
                case 2:
                    userRole = "CafeManager";
                    break;
                case 3:
                    userRole = "CafePersonel";
                    break;
            }

            ClaimsIdentity identity = new ClaimsIdentity(new[] {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, userRole)
            }, CookieAuthenticationDefaults.AuthenticationScheme);

            return identity;
        }
    }
}
