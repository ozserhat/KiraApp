using Framework.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Entities.ComplexTypes
{
    public class UserDetail
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public User User { get; set; }
        public Role Role { get; set; }
        public List<User_Role> UserRoles { get; set; }
    }

    public class UserDetailList
    {
        public User User { get; set; }
        public Role Role { get; set; }
        public List<User_Role> UserRoles { get; set; }
    }
}
