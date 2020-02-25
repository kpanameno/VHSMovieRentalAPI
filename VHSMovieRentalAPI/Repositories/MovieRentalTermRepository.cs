using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VHSMovieRentalAPI.Interfaces;
using VHSMovieRentalAPI.Models;

namespace VHSMovieRentalAPI.Repositories
{
    public class MovieRentalTermRepository : DBContextRepository<MovieRentalTerm>, IMovieRentalTermRepository
    {

        public MovieRentalTermRepository(VHSMovieRentalDBContext context) : base(context)
        {
        }
    }
}
