using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VHSMovieRentalAPI.Interfaces
{
   public interface IPagination<T> where T : class
    {
        IQueryable<T> GetPaginated(string sNameFilter, int iStartPage, int iRows, bool bSortByLikes, out int iTotalRows);
    }
}
