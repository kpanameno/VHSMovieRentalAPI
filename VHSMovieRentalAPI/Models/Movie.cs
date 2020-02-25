using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VHSMovieRentalAPI.Models
{
    public class Movie
    {
        public int MovieID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
        public decimal RentalPrice { get; set; }
        public decimal SalePrice { get; set; }
        public bool Available { get; set; }
        public DateTime Created { get; set; }
        public int UpdatedUserID { get; set; }
        public DateTime Updated { get; set; }

        public User UpdatedUser { get; set; }
        public ICollection<MovieLike> MovieLikes { get; set; }
    }
}
