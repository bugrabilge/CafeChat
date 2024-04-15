using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IAdminService
    {
        void AddCafe(Cafe cafeToAdd);
        void DeleteCafe(Cafe cafeToDelete);
        void DeleteCafeById(int cafeId);
        void ChangeCafeStatusById(int cafeId);
        List<Cafe> GetAllCafes();
    }
}
