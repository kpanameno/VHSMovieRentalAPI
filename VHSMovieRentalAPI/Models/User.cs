using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VHSMovieRentalAPI.Models
{
    public class User
    {
        public int UserID { get; set; }
        public int RoleID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public string FullName { get; set; }
        public bool Active { get; set; }
        public string Email { get; set; }
        public DateTime Created { get; set; }

        public Role Role { get; set; }
    }
}
