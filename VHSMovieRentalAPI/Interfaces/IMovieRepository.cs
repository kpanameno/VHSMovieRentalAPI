using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VHSMovieRentalAPI.Models;

namespace VHSMovieRentalAPI.Interfaces
{
    public interface IMovieRepository: IPagination<Movie>, IRepository<Movie>
    {
        IQueryable<Movie> GetAvailableMovies(string sTitleFilter, bool bAvailable, string sRoleName);
        IQueryable<Movie> GetMoviesByAvailability(bool bAvailable);
        int UpdateMovie(int iMovieId, Movie oMovieEntity, out string sErrorMessage);
    }
}
