using VHSMovieRentalAPI.Models;

namespace VHSMovieRentalAPI.Interfaces
{
    public interface IMovieLikeRepository : IRepository<MovieLike>
    {
        int AddMovieLike(int iMovieID, int iUserID);
    }
}
