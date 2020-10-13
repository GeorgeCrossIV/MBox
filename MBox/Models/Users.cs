using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MBox.Models
{

    public class UserRootObject
    {
        public UserData data { get; set; }
    }

    public class UserData
    {
        public Users users { get; set; }
    }

    public class Users
    {
        public User[] values { get; set; }
    }

    public class User
    {
        [Key]
        public int id { get; set; }
        public string email { get; set; }
        public string ip { get; set; }
    }
}
