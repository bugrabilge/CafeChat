using BusinessLayer.Abstract;
using BusinessLayer.Results.Abstract;
using BusinessLayer.Results.Concrete;
using CafeChat.Constants;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using EntityLayer.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

            if (loginUser != null)
            {
                return new SuccessDataResult<Users>(loginUser, AppMessages.LOGIN_SUCCEEDED);
            }
            else
            {
                return new ErrorDataResult<Users>(AppMessages.ACCESS_DENIED);
            }
        }
    }
}
