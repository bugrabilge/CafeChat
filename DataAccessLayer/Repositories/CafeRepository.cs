using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class CafeRepository
    {
        Context c = new Context();
        public void Delete(Cafe t)
        {
            c.Remove(t);
            c.SaveChanges();
        }

        public Cafe GetByID(int id)
        {
            return c.Set<Cafe>().Find(id);
        }

        public List<Cafe> GetListAll()
        {
            return c.Set<Cafe>().ToList();
        }

        public void Insert(Cafe t)
        {
            c.Add(t);
            c.SaveChanges();
        }

        public void Update(Cafe t)
        {
            c.Update(t);
            c.SaveChanges();
        }
    }
}
