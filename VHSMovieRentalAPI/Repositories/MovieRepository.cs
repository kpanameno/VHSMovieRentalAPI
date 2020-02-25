using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VHSMovieRentalAPI.Interfaces;
using VHSMovieRentalAPI.Models;

namespace VHSMovieRentalAPI.Repositories
{
    public class MovieRepository : DBContextRepository<Movie>, IPagination<Movie>, IMovieRepository
    {
        private IMoviePriceLogRepository oLogRepository;

        public MovieRepository(VHSMovieRentalDBContext context) : base(context)
        {
            oLogRepository = new MoviePriceLogRepository(oContext);
        }

        public IQueryable<Movie> GetAvailableMovies(string sTitleFilter, bool bAvailable, string sRoleName)
        {
            var oRole = oContext.Roles.AsNoTracking().Where(x => x.Name.ToLower().Trim() == sRoleName.ToLower().Trim()).FirstOrDefault();
            if (oRole != null)
            {
                if (oRole.Name.Trim().ToLower() != "administrator")
                {
                    bAvailable = true;
                }
            }
            else
            {
                bAvailable = true;
            }
            // Filter available
            var oMovieList = oContext.Movies.Where(x => x.Available == bAvailable).AsQueryable();

            if (!string.IsNullOrEmpty(sTitleFilter))
            {
                sTitleFilter = sTitleFilter.Trim().ToLower();
                return oMovieList.Where(x => x.Title.Trim().ToUpper().Contains(sTitleFilter));
            }
            else
            {
                return oMovieList;
            }

        }

        public IQueryable<Movie> GetMoviesByAvailability(bool bAvailable)
        {
            return oContext.Movies.Where(x => x.Available == bAvailable).AsQueryable();
        }

        public IQueryable<Movie> GetPaginated(string sTitleFilter, int iStartPage, int iRows, bool bSortByLikes, out int iTotalRows)
        {
            var oResultList = oContext.Movies.Include(x => x.MovieLikes).AsQueryable();

            // Filter by Movie Name
            if (!string.IsNullOrEmpty(sTitleFilter.Trim()))
            {
                oResultList = oResultList.Where(x => x.Title.Trim().ToUpper().Contains(sTitleFilter.Trim().ToUpper()));
            }

            // Set resulting total rows
            iTotalRows = oResultList.Count();

            // By default sort by Title
            if (bSortByLikes)
            {
                oResultList = oResultList.OrderBy(x => x.MovieLikes.Count())
                    .Skip((iStartPage * iRows))
                    .Take(iRows);
            }
            else
            {
                oResultList = oResultList.OrderBy(x => x.Title)
                    .Skip((iStartPage * iRows))
                    .Take(iRows);
            }

            return oResultList;
        }

        /// <summary>
        /// iResult = -1 -> Non existing record
        /// iResult = 0 -> Not Updated
        /// iResult = 1 -> Updated
        /// </summary>
        /// <param name="oMovieEntity"></param>
        /// <returns></returns>
        public int UpdateMovie(int iExistingID, Movie oMovieEntity, out string sErrorMessage)
        {
            int iResult = 0;
            sErrorMessage = "";

            if (iExistingID == 0)
            {
                iResult = -1;
                return iResult;
            }

            var oExistingMovie = oContext.Movies.AsNoTracking().Where(x => x.MovieID == iExistingID).FirstOrDefault();
            if (oExistingMovie == null)
            {
                iResult = -1;
                return iResult;
            }

            // Only Admins can update Availabilty
            var oAdminRole = oContext.Roles.Where(x => x.Name.Trim().ToLower() == "administrator").FirstOrDefault();
            var oUserModifies = oContext.Users.AsNoTracking().Where(x => x.UserID == oMovieEntity.UpdatedUserID).FirstOrDefault();

            if (oExistingMovie.Available != oMovieEntity.Available)
            {
                if (oUserModifies.RoleID != oAdminRole.RoleID)
                {
                    sErrorMessage = "Only Admin users can update Availability";
                    return iResult;
                }
            }

            // Update current Movie
            oMovieEntity.Created = oExistingMovie.Created;
            oMovieEntity.Updated = DateTime.Now;
            Update(oMovieEntity);

            // Verifiy if a Log must be created
            bool bCreateLog = false;
            if (oMovieEntity.RentalPrice != oExistingMovie.RentalPrice ||
                oMovieEntity.SalePrice != oExistingMovie.SalePrice ||
                oMovieEntity.Title.Trim() != oExistingMovie.Title.Trim())
            {
                bCreateLog = true;
            }

            if (bCreateLog)
            {
                MoviePriceLog oLog = new MoviePriceLog();
                oLog.MovieID = oExistingMovie.MovieID;
                oLog.Title = oExistingMovie.Title.Trim();
                oLog.RentalPrice = oExistingMovie.RentalPrice;
                oLog.SalePrice = oExistingMovie.SalePrice;
                oLog.Created = DateTime.Now;
                oLog.UpdatedUserID = oExistingMovie.UpdatedUserID;
                oLog.Updated = DateTime.Now;
                oLogRepository.Create(oLog);
            }

            return iResult;
        }
    }
}
