using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VHSMovieRentalAPI.Interfaces;
using VHSMovieRentalAPI.Models;

namespace VHSMovieRentalAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MovieLikesController : ControllerBase
    {
        private readonly IMovieLikeRepository oMovieLikeRepository;

        public MovieLikesController(IMovieLikeRepository oMovieLikeRepository)
        {
            this.oMovieLikeRepository = oMovieLikeRepository;
        }

        // GET: api/MovieLikes
        [AllowAnonymous]
        [HttpGet]
        public IEnumerable<MovieLike> GetMovieLikes()
        {
            return oMovieLikeRepository.GetAll();
        }

        // GET: api/MovieLikes/5
        [HttpGet("{id}")]
        public ActionResult<MovieLike> GetMovieLike(int iMovieLikeID)
        {
            var oMovieLike = oMovieLikeRepository.GetById(iMovieLikeID);
            if (oMovieLike == null)
            {
                return NotFound();
            }
            else
            {
                return oMovieLike;
            }
        }

        // PUT: api/MovieLikes/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public IActionResult PutMovieLike(int iMovieLikeID, MovieLike oMovieLike)
        {
            if (iMovieLikeID != oMovieLike.MovieLikeID)
            {
                return BadRequest();
            }

            try
            {
                var oExistingMovieLike = oMovieLikeRepository.GetById(iMovieLikeID);
                oExistingMovieLike.MovieID = oMovieLike.MovieID;
                oExistingMovieLike.UserID = oMovieLike.UserID;

                oMovieLikeRepository.Update(oExistingMovieLike);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return NoContent();
        }

        // POST: api/MovieLikes
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [Authorize(Roles = "Customer")]
        [HttpPost]
        public ActionResult<MovieLike> PostMovieLike(MovieLike oMovieLike)
        {
            // Adds or removes (if already exists) a Like for a specific Movie
            int iMovieLikeID = oMovieLikeRepository.AddMovieLike(oMovieLike.MovieID, 
                int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value));
            
            if (iMovieLikeID == 0)
            {
                return NoContent();
            }
            else
            {
                return CreatedAtAction("GetMovieLike", new { id = iMovieLikeID }, oMovieLike);
            }
        }

        // DELETE: api/MovieLikes/5
        [HttpDelete("{id}")]
        public ActionResult<MovieLike> DeleteMovieLike(int iMovieLikeID)
        {
            var oExistingLike = oMovieLikeRepository.GetById(iMovieLikeID);
            if (oExistingLike != null)
            {
                oMovieLikeRepository.Delete(oExistingLike);
            }
            else
            {
                return NotFound();
            }

            return oExistingLike;
        }

        private bool MovieLikeExists(int id)
        {
            bool bResult = false;

            if (oMovieLikeRepository.Find(e => e.MovieLikeID == id).ToList().Count > 0)
            {
                bResult = true;
            }

            return bResult;
        }
    }
}
