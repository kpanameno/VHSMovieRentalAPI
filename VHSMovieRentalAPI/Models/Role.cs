using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VHSMovieRentalAPI.Models
{
    public class Role
    {
        public int RoleID { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}
