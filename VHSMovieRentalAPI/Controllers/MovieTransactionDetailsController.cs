using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VHSMovieRentalAPI.Interfaces;
using VHSMovieRentalAPI.Models;

namespace VHSMovieRentalAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MovieTransactionDetailsController : ControllerBase
    {
        private readonly IMovieTransactionDetailRepository oRepository;

        public MovieTransactionDetailsController(IMovieTransactionDetailRepository oRepository)
        {
            this.oRepository = oRepository;
        }

        // GET: api/MovieTransactionDetails
        [HttpGet]
        public IEnumerable<MovieTransactionDetail> GetMovieTransactionDetail()
        {
            return oRepository.GetAll();
        }

        // GET: api/MovieTransactionDetails/5
        [HttpGet("{id}")]
        public ActionResult<MovieTransactionDetail> GetMovieTransactionDetail(int iDetailId)
        {
            var movieTransactionDetail = oRepository.GetById(iDetailId);

            if (movieTransactionDetail == null)
            {
                return NotFound();
            }

            return movieTransactionDetail;
        }

        // PUT: api/MovieTransactionDetails/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public IActionResult PutMovieTransactionDetail(int iDetailId, MovieTransactionDetail oMovieTransactionDetail)
        {
            if (iDetailId != oMovieTransactionDetail.MovieTransactionID)
            {
                return BadRequest();
            }

            try
            {
                oRepository.Update(oMovieTransactionDetail);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return NoContent();
        }

        [Authorize(Roles = "Administrator")]
        [HttpPut("{id}/rentalreturn")]
        public IActionResult PutRentalReturn(int iDetailId)
        {
            try
            {
                oRepository.ReturnMovieRental(iDetailId);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return NoContent();
        }

        // POST: api/MovieTransactionDetails
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public ActionResult<MovieTransactionDetail> PostMovieTransactionDetail(MovieTransactionDetail oMovieTransactionDetail)
        {
            oRepository.Create(oMovieTransactionDetail);

            return CreatedAtAction("GetMovieTransactionDetail", new { iDetaild = oMovieTransactionDetail.MovieTransactionDetailID }, oMovieTransactionDetail);
        }

        // DELETE: api/MovieTransactionDetails/5
        [HttpDelete("{id}")]
        public ActionResult<MovieTransactionDetail> DeleteMovieTransactionDetail(int iDetailId)
        {
            var oTransactionDetail = oRepository.GetById(iDetailId);
            if (oTransactionDetail == null)
            {
                return NotFound();
            }

            oRepository.Delete(oTransactionDetail);

            return oTransactionDetail;
        }

        private bool MovieTransactionDetailExists(int iDetailId)
        {
            bool bResult = false;

            if (oRepository.Find(e => e.MovieTransactionDetailID == iDetailId).ToList().Count > 0)
            {
                bResult = true;
            }

            return bResult;
        }
    }
}
