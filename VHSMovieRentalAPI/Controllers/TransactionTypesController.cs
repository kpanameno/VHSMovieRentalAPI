using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VHSMovieRentalAPI.Models;
using VHSMovieRentalAPI.Interfaces;

namespace VHSMovieRentalAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionTypesController : ControllerBase
    {
        private readonly ITransactionTypeRepository oRepository;

        public TransactionTypesController(ITransactionTypeRepository oRepository)
        {
            this.oRepository = oRepository;
        }

        // GET: api/TransactionTypes
        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public IEnumerable<TransactionType> GetTransactionType()
        {
            return oRepository.GetAll();
        }

        // GET: api/TransactionTypes/5
        [Authorize(Roles = "Administrator")]
        [HttpGet("{id}")]
        public ActionResult<TransactionType> GetTransactionType(int iTransactionTypeID)
        {
            var oTransactionType = oRepository.GetById(iTransactionTypeID);

            if (oTransactionType == null)
            {
                return NotFound();
            }

            return oTransactionType;
        }

        // PUT: api/TransactionTypes/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [Authorize(Roles = "Administrator")]
        [HttpPut("{id}")]
        public IActionResult PutTransactionType(int iTransactionTypeID, TransactionType oTransactionType)
        {
            if (iTransactionTypeID != oTransactionType.TransactionTypeID)
            {
                return BadRequest();
            }

            try
            {
                oRepository.Update(oTransactionType);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransactionTypeExists(iTransactionTypeID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TransactionTypes
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public ActionResult<TransactionType> PostTransactionType(TransactionType oTransactionType)
        {

            oRepository.Create(oTransactionType);
            return CreatedAtAction("GetTransactionType", new { iTransactionTypeID = oTransactionType.TransactionTypeID }, oTransactionType);
        }

        // DELETE: api/TransactionTypes/5
        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id}")]
        public ActionResult<TransactionType> DeleteTransactionType(int iTransactionTypeID)
        {
            var oExistingTransactionType = oRepository.GetById(iTransactionTypeID);
            if (oExistingTransactionType != null)
            {
                oRepository.Delete(oExistingTransactionType);
            }
            else
            {
                return NotFound();
            }

            return oExistingTransactionType;
        }

        private bool TransactionTypeExists(int id)
        {
            bool bResult = false;

            if (oRepository.Find(x => x.TransactionTypeID == id).ToList().Count > 0)
            {
                bResult = true;
            }

            return bResult;
        }
    }
}
