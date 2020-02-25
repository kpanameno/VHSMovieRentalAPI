using System.Collections.Generic;
using System.Linq;
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
    public class MovieRentalTermsController : ControllerBase
    {
        private readonly IMovieRentalTermRepository oRepository;

        public MovieRentalTermsController(IMovieRentalTermRepository oRepository)
        {
            this.oRepository = oRepository;
        }

        // GET: api/MovieRentalTerms
        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public IEnumerable<MovieRentalTerm> GetMovieRentalTerm()
        {
            return oRepository.GetAll();
        }

        // GET: api/MovieRentalTerms/5
        [Authorize(Roles = "Administrator")]
        [HttpGet("{id}")]
        public ActionResult<MovieRentalTerm> GetMovieRentalTerm(int iMovieRentalTermID)
        {
            var oMovieRentalTerm = oRepository.GetById(iMovieRentalTermID);

            if (oMovieRentalTerm == null)
            {
                return NotFound();
            }

            return oMovieRentalTerm;
        }

        // PUT: api/MovieRentalTerms/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [Authorize(Roles = "Administrator")]
        [HttpPut("{id}")]
        public IActionResult PutMovieRentalTerm(int iMovieRentalTermID, MovieRentalTerm oMovieRentalTerm)
        {
            if (iMovieRentalTermID != oMovieRentalTerm.MovieRentalTermID)
            {
                return BadRequest();
            }

            try
            {
                oRepository.Update(oMovieRentalTerm);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieRentalTermExists(iMovieRentalTermID))
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

        // POST: api/MovieRentalTerms
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public ActionResult<MovieRentalTerm> PostMovieRentalTerm(MovieRentalTerm oMovieRentalTerm)
        {
            oRepository.Create(oMovieRentalTerm);
            return CreatedAtAction("GetMovieRentalTerm", new { iMovieRentalTermID = oMovieRentalTerm.MovieRentalTermID }, oMovieRentalTerm);
        }

        // DELETE: api/MovieRentalTerms/5
        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id}")]
        public ActionResult<MovieRentalTerm> DeleteMovieRentalTerm(int iMovieRentalTermID)
        {
            var oRentalTerm = oRepository.GetById(iMovieRentalTermID);
            if (oRentalTerm != null)
            {
                oRepository.Delete(oRentalTerm);
            }
            else
            {
                return NotFound();
            }

            return oRentalTerm;
        }

        private bool MovieRentalTermExists(int id)
        {
            bool bResult = false;

            if (oRepository.Find(x => x.MovieRentalTermID == id).ToList().Count > 0)
            {
                bResult = true;
            }

            return bResult;
        }
    }
}
