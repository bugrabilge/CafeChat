using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Users
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int? CafeId { get; set; }
        public int UserTypeId { get; set; }
        public virtual UserType UserType { get; set; }
        public bool UserStatus { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
