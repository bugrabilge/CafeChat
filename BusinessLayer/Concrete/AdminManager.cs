using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class AdminManager : IAdminService
    {
        ICafeDal _cafeDal;

        public AdminManager(ICafeDal cafeDal)
        {
            _cafeDal = cafeDal;
        }

        public void AddCafe(Cafe cafeToAdd)
        {
            _cafeDal.Insert(cafeToAdd);
        }

        public void DeleteCafe(Cafe cafeToDelete)
        {
            _cafeDal.Delete(cafeToDelete);
        }

        public void DeleteCafeById(int cafeId)
        {
            var cafeToDelete = _cafeDal.GetByID(cafeId);
            _cafeDal.Delete(cafeToDelete);
        }

        public void ChangeCafeStatusById(int cafeId)
        {
            var cafeToUpdate = _cafeDal.GetByID(cafeId);
            cafeToUpdate.Status = !cafeToUpdate.Status;
            _cafeDal.Update(cafeToUpdate);
        }

        public List<Cafe> GetAllCafes()
        {
            return _cafeDal.GetListAll();
        }
    }
}
