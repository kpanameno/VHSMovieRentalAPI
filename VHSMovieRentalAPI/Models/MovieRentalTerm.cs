using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VHSMovieRentalAPI.Models
{
    public class MovieRentalTerm
    {
        public int MovieRentalTermID { get; set; }
        public int RentalDays { get; set; }
        public decimal LateReturnCharge { get; set; }

    }
}
