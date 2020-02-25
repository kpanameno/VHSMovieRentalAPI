using VHSMovieRentalAPI.Interfaces;
using VHSMovieRentalAPI.Models;

namespace VHSMovieRentalAPI.Repositories
{
    public class RoleRepository : DBContextRepository<Role>, IRoleRepository
    {

        public RoleRepository(VHSMovieRentalDBContext context): base(context)
        {

        }

    }
}
