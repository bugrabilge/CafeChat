using BusinessLayer.Abstract;
using CafeChat.Constants;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class UsersManager : IUsersService
    {
        IUsersDal _usersDal;
        IUserTypeDal _userTypeDal;

        public UsersManager(IUsersDal usersDal, IUserTypeDal userTypeDal)
        {
            _usersDal = usersDal;
            _userTypeDal = userTypeDal;
        }

        public List<Users> GetAllList()
        {
            return _usersDal.GetListAll();
        }

        public Users GetByID(int id)
        {
            return _usersDal.GetByID(id);
        }

        public void UserAdd(Users users)
        {
            _usersDal.Insert(users);
        }

        public void UserDelete(Users users)
        {
            _usersDal.Delete(users);
        }

        public void UserUpdate(Users users)
        {
            _usersDal.Update(users);
        }

        public List<Users> GetAllManagers()
        {
            return _usersDal.GetListAll().Where(u => u.UserTypeId == (int)UsersConstants.UserTypes.CafeManager).ToList();
        }

        public List<UserType> GetAllUserTypes()
        {
            return _userTypeDal.GetListAll();
        }
    }
}
