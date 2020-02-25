using System;
using System.Collections.Generic;
using System.Linq;
using VHSMovieRentalAPI.Interfaces;
using VHSMovieRentalAPI.Models;

namespace VHSMovieRentalAPI.Repositories
{
    public class MovieLikeRepository : DBContextRepository<MovieLike>, IMovieLikeRepository
    {

        public MovieLikeRepository(VHSMovieRentalDBContext context) : base(context)
        {

        }

        public int AddMovieLike(int iMovieID, int iUserID)
        {

            int iResult = -1;
            var oMovieLike = oContext.MovieLikes.Where(x => x.MovieID == iMovieID && x.UserID == iUserID).FirstOrDefault();
            // Allow only one like per movie-user
            if (oMovieLike == null)
            {
                MovieLike oLike = new MovieLike();
                oLike.MovieID = iMovieID;
                oLike.UserID = iUserID;
                oLike.Created = DateTime.Now;
                Create(oLike);
                iResult = oLike.MovieLikeID;
            }
            else
            {
                Delete(oMovieLike);
                iResult = 0;
            }

            return iResult;
        }

    }
}
