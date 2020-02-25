using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VHSMovieRentalAPI.Models;

namespace VHSMovieRentalAPI.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        IEnumerable<User> GetUserListWithRole();
        User GetUserWithRole(int iUserID);
        User UpdateUser(int iUpdatedUserID, string sRoleName, User oUser, out string sErrorMessage);
        User Login(string sUserName, string sPassword, out string sErrorMessage);
        int CreateUser(User oUser, out string sErrorMessage);
        void ForgotPassword(string sEmail, out string sErrorMessage);
        void ResetPassword(int iUserID, string sToken, string sNewPassword, out string sErrorMessage);
        bool ActivateAccount(int iUserID, out string sErrorMessage);
        bool UserExistsByUserName(string sUserName);
    }
}
