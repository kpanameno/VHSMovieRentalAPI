using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VHSMovieRentalAPI.Models
{
    public class MovieLike
    {
        public int MovieLikeID { get; set; }
        public int MovieID { get; set; }
        public int UserID { get; set; }
        public DateTime Created { get; set; }

        public Movie Movie { get; set; }
        public User User { get; set; }
    }
}
