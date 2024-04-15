using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DataAccessLayer.Concrete
{
    public class Context : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=DESKTOP-HCML6IK;database=CafeChat; integrated security=true;TrustServerCertificate=True;");
        }

        public DbSet<Users> Users { get; set; }
        public DbSet<Cafe> Cafe { get; set; }
        public DbSet<UserType> UserType { get; set; }
    }
}
