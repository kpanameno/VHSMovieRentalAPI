using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class MovieTransactionsController : ControllerBase
    {
        private readonly IMovieTransactionRepository oRepository;

        public MovieTransactionsController(IMovieTransactionRepository oRepository)
        {
            this.oRepository = oRepository;
        }

        // GET: api/MovieTransactions
        [HttpGet]
        public IEnumerable<MovieTransaction> GetMovieTransaction()
        {
            return oRepository.GetAll();
        }

        // GET: api/MovieTransactions/5
        [HttpGet("{id}")]
        public ActionResult<MovieTransaction> GetMovieTransaction(int iMovieTransactionId)
        {
            var oTransaction = oRepository.GetWithDetailsById(iMovieTransactionId);

            if (oTransaction == null)
            {
                return NotFound();
            }

            return oTransaction;
        }

        // PUT: api/MovieTransactions/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public IActionResult PutMovieTransaction(int iMovieTransactionId, MovieTransaction oMovieTransaction)
        {
            if (iMovieTransactionId != oMovieTransaction.MovieTransactionID)
            {
                return BadRequest();
            }

            try
            {
                oRepository.Update(oMovieTransaction);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return NoContent();
        }

        // POST: api/MovieTransactions
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public ActionResult<MovieTransaction> PostMovieTransaction(MovieTransaction oMovieTransaction)
        {

            try
            {
                int iResult = oRepository.CreateTransaction(oMovieTransaction);
                if (iResult == 0)
                {
                    return BadRequest(new { Message = "Unable to create Movie Transaction" });
                }
                else
                {
                    return CreatedAtAction("GetMovieTransaction", new { iMovieTransactionId = oMovieTransaction.MovieTransactionID }, oMovieTransaction);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }

        }

        // DELETE: api/MovieTransactions/5
        [HttpDelete("{id}")]
        public ActionResult<MovieTransaction> DeleteMovieTransaction(int iMovieTransactionId)
        {
            var oMovieTransaction = oRepository.GetById(iMovieTransactionId);
            if (oMovieTransaction == null)
            {
                return NotFound();
            }

            oRepository.Delete(oMovieTransaction);

            return oMovieTransaction;
        }

        private bool MovieTransactionExists(int iMovieTransactionId)
        {
            bool bResult = false;

            if (oRepository.Find(e => e.MovieTransactionID == iMovieTransactionId).ToList().Count > 0)
            {
                bResult = true;
            }

            return bResult;
        }
    }
}
