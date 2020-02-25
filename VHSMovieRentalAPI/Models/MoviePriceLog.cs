using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VHSMovieRentalAPI.Models
{
    public class MoviePriceLog
    {
        public int MoviePriceLogID { get; set; }
        public int MovieID { get; set; }
        public string Title { get; set; }
        public decimal RentalPrice { get; set; }
        public decimal SalePrice { get; set; }
        public DateTime Created { get; set; }
        public int UpdatedUserID { get; set; }
        public DateTime Updated { get; set; }

        public User UpdatedUser { get; set; }
    }
}
