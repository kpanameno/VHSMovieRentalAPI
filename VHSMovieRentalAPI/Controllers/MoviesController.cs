using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VHSMovieRentalAPI.Interfaces;
using VHSMovieRentalAPI.Models;

namespace VHSMovieRentalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieRepository oMovieRepository;

        public MoviesController(IMovieRepository oMovieRepository)
        {
            this.oMovieRepository = oMovieRepository;
        }

        // GET: api/Movies/{availability}
        [AllowAnonymous]
        [HttpGet]
        public IEnumerable<Movie> GetMovies([FromQuery] string Title = "", [FromQuery] bool Available = true)
        {
            string sRoleName ="";

            if (User.FindFirst(ClaimTypes.Role) != null)
                sRoleName = User.FindFirst(ClaimTypes.Role).Value;

            return oMovieRepository.GetAvailableMovies(Title, Available, sRoleName);
        }

        // GET: api/Movies/5
        [AllowAnonymous]
        [HttpGet("{iMovieID}")]
        public ActionResult<Movie> GetMovie(int iMovieID)
        {
            var oMovie = oMovieRepository.GetById(iMovieID);

            if (oMovie == null)
            {
                return NotFound();
            }

            return oMovie;
        }

        // PUT: api/Movies/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [Authorize(Roles = "Administrator")]
        [HttpPut("{iMovieId}")]
        public IActionResult PutMovie(int iMovieId, Movie oMovie)
        {
            if (iMovieId != oMovie.MovieID)
            {
                return BadRequest();
            }

            try
            {

                oMovie.UpdatedUserID = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value); // Logged user is the one updating the record
                string sErrorMessage = "";
                int iResult = oMovieRepository.UpdateMovie(iMovieId, oMovie, out sErrorMessage);

                if (!string.IsNullOrEmpty(sErrorMessage))
                {
                    return BadRequest(new { Message = sErrorMessage });
                }

                if (iResult == -1)
                {
                    return NotFound();
                }

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(iMovieId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }

        // POST: api/Movies
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public ActionResult<Movie> PostMovie(Movie movie)
        {
            movie.UpdatedUserID = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value); // Logged user is the one updating the record
            movie.Created = DateTime.Now;
            movie.Updated = DateTime.Now;
            oMovieRepository.Create(movie);

            return CreatedAtAction("GetMovie", new { iMovieID = movie.MovieID }, movie);
        }

        // DELETE: api/Movies/5
        [Authorize(Roles = "Administrator")]
        [HttpDelete("{iMovieId}")]
        public ActionResult<Movie> DeleteMovie(int iMovieID)
        {
            var oExistingMovie = oMovieRepository.GetById(iMovieID);
            if (oExistingMovie != null)
            {
                oMovieRepository.Delete(oExistingMovie);
            }
            else
            {
                return NotFound();
            }

            return Ok();
        }

        private bool MovieExists(int id)
        {
            bool bResult = false;

            if (oMovieRepository.Find(x => x.MovieID == id).ToList().Count > 0)
            {
                bResult = true;
            }

            return bResult;
        }
    }
}
