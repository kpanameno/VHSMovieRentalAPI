using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VHSMovieRentalAPI.Models;

namespace VHSMovieRentalAPI.Interfaces
{
    public interface IMovieRepository: IPagination<Movie>, IRepository<Movie>
    {
        IQueryable<Movie> GetAvailableMovies( int iPageSize, int iPageStart, int iPages, int iRows,
            string sTitleFilter, bool bAvailable, string sSortCol, string sSortDirection, string sRoleName);
        IQueryable<Movie> GetMoviesByAvailability(bool bAvailable);
        int UpdateMovie(int iMovieId, Movie oMovieEntity, out string sErrorMessage);
    }
}
