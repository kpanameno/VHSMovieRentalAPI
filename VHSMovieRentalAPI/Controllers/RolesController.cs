using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VHSMovieRentalAPI.Interfaces;
using VHSMovieRentalAPI.Models;

namespace VHSMovieRentalAPI.Controllers
{
    [Authorize(Roles = "Administrator")]
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleRepository oRoleRepository;

        public RolesController(IRoleRepository oRoleRepository)
        {
            this.oRoleRepository = oRoleRepository;
        }

        // GET: api/Roles
        [HttpGet]
        public IEnumerable<Role> GetRoles()
        {
            return oRoleRepository.GetAll();
        }

        // GET: api/Roles/5
        [HttpGet("{id}")]
        public ActionResult<Role> GetRole(int iRoleId)
        {
            var oRole = oRoleRepository.GetById(iRoleId);
            if (oRole == null)
            {
                return NotFound();
            }
            else
            {
                return oRole;
            }

        }

        // PUT: api/Roles/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public IActionResult PutRole(int iRoleId, Role oRole)
        {
            if (iRoleId != oRole.RoleID)
            {
                return BadRequest();
            }

            try
            {
                var oExistingRole = oRoleRepository.GetById(iRoleId);
                oExistingRole.Name = oRole.Name.Trim();
                oExistingRole.Updated = DateTime.Now;

                oRoleRepository.Update(oExistingRole);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return NoContent();
        }

        // POST: api/Roles
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public ActionResult<Role> PostRole(Role oNewRole)
        {

            oRoleRepository.Create(oNewRole);

            return CreatedAtAction("GetRole", new { id = oNewRole.RoleID }, oNewRole);
        }

        // DELETE: api/Roles/5
        [HttpDelete("{id}")]
        public ActionResult<Role> DeleteRole(int iRoleId)
        {
            var oExistingRole = oRoleRepository.GetById(iRoleId);
            if (oExistingRole != null)
            {
                oRoleRepository.Delete(oExistingRole);
            }
            else
            {
                return NotFound();
            }

            return oExistingRole;
        }

        private bool RoleExists(int id)
        {
            bool bResult = false;
            
            if (oRoleRepository.Find(e => e.RoleID == id).ToList().Count > 0)
            {
                bResult = true;
            }

            return bResult;
        }
    }
}
