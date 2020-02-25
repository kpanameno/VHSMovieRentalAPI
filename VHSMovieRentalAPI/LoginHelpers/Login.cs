using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VHSMovieRentalAPI.LoginHelpers
{
    public class Login
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public string RoleName { get; set; }
    }
}
