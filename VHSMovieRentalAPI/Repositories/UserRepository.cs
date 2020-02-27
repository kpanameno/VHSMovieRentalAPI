using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using VHSMovieRentalAPI.Interfaces;
using VHSMovieRentalAPI.Models;
using VHSMovieRentalAPI.SecurityHelpers;

namespace VHSMovieRentalAPI.Repositories
{
    public class UserRepository : DBContextRepository<User>, IUserRepository
    {

        private readonly VHSMovieRentalSettings oSettings;

        public UserRepository(VHSMovieRentalDBContext oContext, IOptions<VHSMovieRentalSettings> oSettings) : base(oContext)
        {
            this.oSettings = oSettings.Value;
        }

        public IEnumerable<User> GetUserListWithRole()
        {
            var oUserList = oContext.Users.Include(x => x.Role);
            return oUserList;
        }

        public User GetUserWithRole(int iUserID)
        {
            var oUserList = oContext.Users.Include(x => x.Role).Where(x => x.UserID == iUserID).FirstOrDefault();
            return oUserList;
        }

        public bool UserExistsByUserName(string sUserName)
        {
            bool bResult = false;

            if (oContext.Users.AsNoTracking().Where(x => x.UserName.Trim().ToLower().Equals(sUserName)).Count() > 0)
            {
                bResult = true;
            }

            return bResult;
        }

        public User UpdateUser(int iUpdatedUserID, string sRoleName, User oUser, out string sErrorMessage)
        {
            sErrorMessage = "";
            try
            {

                // Get values stored in the DB
                var oExistingUser = oContext.Users.AsNoTracking().Where(x => x.UserID == oUser.UserID).FirstOrDefault();
                var oExistingRole = oContext.Roles.AsNoTracking().Where(x => x.Name.ToLower().Trim() == sRoleName.ToLower().Trim()).FirstOrDefault();

                // Only admins can update other users

                if (iUpdatedUserID != oExistingUser.UserID && oExistingRole.Name.Trim().ToLower() != "administrator")
                {
                    sErrorMessage = "You have no privileges to update other Users";
                    return null;
                }


                // Only admins can update roles
                // This means that customers can update their user without affecting the Role

                if (oUser.RoleID != oExistingUser.RoleID && oExistingRole.Name.Trim().ToLower() != "administrator")
                {
                    sErrorMessage = "You have no privileges to update a User's Role";
                    return null;
                }

                // Encrypt new Password
                if (oExistingUser.Password != oUser.Password)
                    oUser.Password = Encrypt(oUser.Password);

                // Dont update these fields
                oUser.Created = oExistingUser.Created;
                oUser.Active = oExistingUser.Active;

                // Update record
                Update(oUser);

            }
            catch (Exception ex)
            {
                sErrorMessage = ex.Message;
            }


            return oUser;
        }

        public int CreateUser(User oUser, out string sErrorMessage)
        {
            int iResult = 0;
            sErrorMessage = "";

            try
            {
                // Validate Fields
                string sValidUser = "";
                if (oUser.RoleID == 0)
                    sValidUser += " Role ID";

                if (string.IsNullOrEmpty(oUser.UserName))
                    sValidUser += " Username";

                if (string.IsNullOrEmpty(oUser.Password))
                    sValidUser += " Password";

                if (string.IsNullOrEmpty(oUser.FullName))
                    sValidUser += " Name";

                if (string.IsNullOrEmpty(oUser.Password))
                    sValidUser += " Password";

                if (!string.IsNullOrEmpty(sValidUser))
                {
                    sErrorMessage = "The following fields are required " + sValidUser;
                    return -1;
                }

                // Username exists
                if (UserExistsByUserName(oUser.UserName))
                {
                    sErrorMessage = "Username: " + oUser.UserName + " already exists. Please try another one.";
                    return -1;
                }

                // Create User
                oUser.Password = Encrypt(oUser.Password);
                oUser.Active = false;  // User activition must be done via Email
                oUser.Created = DateTime.Now;
                Create(oUser);

                // Assing result value
                iResult = oUser.UserID;

                // Create Email
                string sTo = oUser.Email;
                string sSubject = "VHS Movie Rental | Please verify your account";
                string sBody = "<html>" +
                    "<head>" +
                    "<title>VHS Movie Rental | Password reset </title>" +
                    "</head>" +
                    "<body>" +
                    "<h1 style = \"color: #5e9ca0;\"> VHS Movie Rental</h1>" +
                    "<h2 style = \"color: #2e6c80;\"> Account activation: <strong> " + oUser.FullName + " </strong></h2>" +
                    "<p> Click <a href=\"https://localhost:44306/api/auth/accountactivation/" + iResult + "\">here </a>to activate your account</p>" +
                    "<p> Thank you for your business.</p>" +
                    "</body>" +
                    "</html>";
                SendMail(sTo, sSubject, sBody);
            }
            catch (Exception ex)
            {
                sErrorMessage = ex.Message;
            }

            return iResult;
        }

        public User Login(string sUserName, string sPassword, out string sErrorMessage)
        {
            sErrorMessage = "";
            sPassword = Encrypt(sPassword);
            var oUser = oContext.Users.Include(x => x.Role).SingleOrDefault(x => x.UserName == sUserName && x.Password == sPassword);

            if (oUser != null)
            {

                if (!oUser.Active)
                {
                    sErrorMessage = "The User is not active";
                    return null;
                }

                var oTokenHandler = new JwtSecurityTokenHandler();
                var oKey = Encoding.ASCII.GetBytes(oSettings.SecretKey);
                var oTokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.NameIdentifier, oUser.UserID.ToString()),
                    new Claim(ClaimTypes.Role, oUser.Role.Name)
                    })
                };
                oTokenDescriptor.Expires = DateTime.UtcNow.AddDays(7);
                oTokenDescriptor.SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(oKey), SecurityAlgorithms.HmacSha256Signature);

                var token = oTokenHandler.CreateToken(oTokenDescriptor);
                oUser.Token = oTokenHandler.WriteToken(token);
                oUser.Password = null;
            }
            else
            {
                sErrorMessage = "Wrong credentials.";
            }

            return oUser;
        }

        public void ForgotPassword(string sEmail, out string sErrorMessage)
        {
            sErrorMessage = "";
            sEmail = sEmail.ToLower().Trim();
            string sOneTimeToken;
            try
            {
                var oUser = oContext.Users.Include(x => x.Role).Where(x => x.Email.ToLower().Trim() == sEmail).FirstOrDefault();
                if (oUser != null)
                {

                    // Active User
                    if (!oUser.Active)
                    {
                        sErrorMessage = "User is not active.";
                        return;
                    }

                    // Create Token
                    var oTokenHandler = new JwtSecurityTokenHandler();
                    var oKey = Encoding.ASCII.GetBytes(oSettings.SecretKey + oUser.Password); // Using old password as combination
                    var oTokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                            new Claim(ClaimTypes.NameIdentifier, oUser.UserID.ToString()),
                            new Claim(ClaimTypes.Role, oUser.Role.Name),
                        })
                    };

                    oTokenDescriptor.Expires = DateTime.UtcNow.AddDays(1);
                    oTokenDescriptor.SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(oKey), SecurityAlgorithms.HmacSha256Signature);

                    var token = oTokenHandler.CreateToken(oTokenDescriptor);
                    sOneTimeToken = oTokenHandler.WriteToken(token);
                    Console.WriteLine(sOneTimeToken);

                    // Create Email
                    string sTo = sEmail;
                    string sSubject = "VHS Movie Rental | Password Recovery";
                    string sBody = "<html>" +
                        "<head>" +
                        "<title>VHS Movie Rental | Password reset </title>" +
                        "</head>" +
                        "<body>" +
                        "<h1 style = \"color: #5e9ca0;\"> VHS Movie Rental</h1>" +
                        "<h2 style = \"color: #2e6c80;\"> Password reset for: <strong> " + oUser.FullName + " </strong></h2>" +
                        "<p> Click <a href=\"https://localhost:44306/api/auth/passwordreset/" + oUser.UserID + "/" + sOneTimeToken + "\">here </a>to reset your password</p>" +
                        "<p> Thank you for your business.</p>" +
                        "</body>" +
                        "</html>"; ;
                    SendMail(sTo, sSubject, sBody);

                }
                else
                {
                    sErrorMessage = "The specified User does not exists.";
                }
            }
            catch (Exception ex)
            {
                sErrorMessage = ex.Message;
            }

        }

        public void ResetPassword(int iUserID, string sToken, string sNewPassword, out string sErrorMessage)
        {
            sErrorMessage = "";

            var oUser = oContext.Users.Where(x => x.UserID == iUserID && x.Active).FirstOrDefault();
            if (oUser == null)
            {
                sErrorMessage = "User not found.";
                return;
            }

            // Validate Token
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var validationParameters = GetValidationParameters(oSettings.SecretKey + oUser.Password);
                SecurityToken validatedToken;
                IPrincipal principal = handler.ValidateToken(sToken, validationParameters, out validatedToken);

                // Set new Password
                oUser.Password = sNewPassword;
                Update(oUser);
            }
            catch (Microsoft.IdentityModel.Tokens.SecurityTokenInvalidSignatureException)
            {
                sErrorMessage = "The current Token is invalid.";
            }

        }

        private static TokenValidationParameters GetValidationParameters(string sKey)
        {
            // Secret key is combined with the persistent Password
            return new TokenValidationParameters()
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(sKey)) // The same key as the one that generate the token
            };
        }

        public bool ActivateAccount(int iUserID, out string sErrorMessage)
        {
            bool bResult = false;
            sErrorMessage = "";
            var oUser = oContext.Users.Where(x => x.UserID == iUserID).FirstOrDefault();

            // Valid user ID
            if (oUser == null)
            {
                sErrorMessage = "User not found";
                return false;
            }

            if (oUser.Active)
            {
                sErrorMessage = "User already active";
                return false;
            }

            // Update User
            oUser.Active = true;
            Update(oUser);

            return bResult;
        }


        private void SendMail(string sTo, string sSubject, string sBody)
        {
            // Create Email
            SmtpClient oClient = new SmtpClient("smtp.gmail.com", 587);
            oClient.EnableSsl = true;
            oClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            oClient.UseDefaultCredentials = false;
            oClient.Credentials = new NetworkCredential("omarpanameno.2@gmail.com", "avnqlernjgjkyxij");

            MailMessage oMailMessage = new MailMessage();
            oMailMessage.From = new MailAddress("noreply@vhsmovierental.com");
            oMailMessage.To.Add(sTo);
            oMailMessage.IsBodyHtml = true;
            oMailMessage.Subject = sSubject;
            oMailMessage.Body = sBody;
            oClient.Send(oMailMessage);
        }

        private string Encrypt(string sPassword)
        {
            UTF8Encoding oEncoding = new UTF8Encoding();
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] data = md5.ComputeHash(oEncoding.GetBytes(sPassword));

            return Convert.ToBase64String(data);
        }
    }
}
