using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VHSMovieRentalAPI.Interfaces;
using VHSMovieRentalAPI.LoginHelpers;

namespace VHSMovieRentalAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        
        private readonly IUserRepository oUserRepository;

        public AuthController(IUserRepository oUserRepository)
        {
            this.oUserRepository = oUserRepository;
        }

        // GET api/values
        [HttpPost, Route("login")]
        public IActionResult Login([FromBody]Login user)
        {
            if (string.IsNullOrEmpty(user.UserName))
            {
                return BadRequest(new { ErrorMessage = "Please specify an username." });
            }

            string sErrorMessage;
            var oExistingUser = oUserRepository.Login(user.UserName, user.Password, out sErrorMessage);

            if (oExistingUser == null)
            {
                return BadRequest(new { ErrorMessage = sErrorMessage });
            }

            // Fill user
            user.UserID = oExistingUser.UserID;
            user.Password = "";
            user.Token = oExistingUser.Token;
            user.RoleName = oExistingUser.Role.Name;

            return Ok(user);
        }

        [HttpPost, Route("forgotpassword/{Email}")]
        public IActionResult ForgotPassword(string Email)
        {
            string sErrorMessage;
            oUserRepository.ForgotPassword(Email, out sErrorMessage);

            if (!string.IsNullOrEmpty(sErrorMessage))
                return BadRequest(new { Message = sErrorMessage });
            else
                return Ok("An Email was sent to your account.");

        }


        [HttpPost, Route("passwordreset")]
        public IActionResult ResetPassword([FromBody]Login oLogin)
        {
            string sErrorMessage;
            oUserRepository.ResetPassword(oLogin.UserID, oLogin.Token, oLogin.Password, out sErrorMessage);

            if (!string.IsNullOrEmpty(sErrorMessage))
                return BadRequest(new { Message = sErrorMessage });
            else
                return Ok("Your password has been updated!");

        }

        [HttpPost, Route("activateaccount/{id}")]
        public IActionResult ActivateUser(int id)
        {
            string sErrorMessage;
            oUserRepository.ActivateAccount(id, out sErrorMessage);

            if (!string.IsNullOrEmpty(sErrorMessage))
                return BadRequest(new { Message = sErrorMessage });
            else
                return Ok("Your account has been activated!");

        }


    }
}