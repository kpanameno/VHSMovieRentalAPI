using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VHSMovieRentalAPI.Interfaces;
using VHSMovieRentalAPI.Models;

namespace VHSMovieRentalAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository oUserRepository;

        public UsersController(IUserRepository oUserRepository)
        {
            this.oUserRepository = oUserRepository;
        }

        // GET: api/Users
        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public IEnumerable<User> GetUsers()
        {
            return oUserRepository.GetUserListWithRole();
        }

        // GET: api/Users/5
        [Authorize(Roles = "Administrator")]
        [HttpGet("{iUserID}")]
        public ActionResult<User> GetUser(int iUserID)
        {

            var oUser = oUserRepository.GetUserWithRole(iUserID);

            if (oUser == null)
            {
                return NotFound();
            }

            return oUser;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{iUserID}")]
        public IActionResult PutUser(int iUserID, User oUser)
        {
            if (iUserID != oUser.UserID)
            {
                return BadRequest();
            }

            try
            {
                string sErrorMessage;
                string sRoleName = "";
                int iUpdatedUserID = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

                if (User.FindFirst(ClaimTypes.Role) != null)
                    sRoleName = User.FindFirst(ClaimTypes.Role).Value;

                oUserRepository.UpdateUser(iUpdatedUserID, sRoleName, oUser, out sErrorMessage);

                if (!string.IsNullOrEmpty(sErrorMessage))
                    return BadRequest(new { Message = sErrorMessage });
                else
                    return CreatedAtAction("GetUser", new { iUserID = oUser.UserID }, oUser);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(iUserID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        // POST: api/Users
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public ActionResult<User> PostUser(User user)
        {
            string sErrorMessage;
            int iUserID = oUserRepository.CreateUser(user, out sErrorMessage);

            if (!string.IsNullOrEmpty(sErrorMessage))
                return BadRequest(new { Message = sErrorMessage });
            else
                return CreatedAtAction("GetUser", new { id = iUserID }, user);

        }

        // DELETE: api/Users/5
        [HttpDelete("{iUserID}")]
        public ActionResult<User> DeleteUser(int iUserID)
        {
            User oExistingUser = oUserRepository.GetById(iUserID);
            if (oExistingUser != null)
            {
                oUserRepository.Delete(oExistingUser);
            }
            else
            {
                return NotFound();
            }

            return oExistingUser;
        }

        private bool UserExists(int id)
        {
            bool bResult = false;

            if (oUserRepository.Find(x => x.UserID == id).ToList().Count > 0)
            {
                bResult = true;
            }

            return bResult;
        }

    }
}
