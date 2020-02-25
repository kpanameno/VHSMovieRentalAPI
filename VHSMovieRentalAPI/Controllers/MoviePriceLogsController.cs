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
    public class MoviePriceLogsController : ControllerBase
    {
        private readonly IMoviePriceLogRepository oLogRepository;

        public MoviePriceLogsController(IMoviePriceLogRepository oLogRepository)
        {
            this.oLogRepository = oLogRepository;
        }

        // GET: api/MoviePriceLogs
        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public IEnumerable<MoviePriceLog> GetMoviePriceLogs()
        {
            return oLogRepository.GetAll();
        }

        // GET: api/MoviePriceLogs/5
        [Authorize(Roles = "Administrator")]
        [HttpGet("{id}")]
        public ActionResult<MoviePriceLog> GetMoviePriceLog(int iLogID)
        {
            var oLog = oLogRepository.GetById(iLogID);

            if (oLog == null)
            {
                return NotFound();
            }

            return oLog;
        }

        // PUT: api/MoviePriceLogs/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [Authorize(Roles = "Administrator")]
        [HttpPut("{id}")]
        public IActionResult PutMoviePriceLog(int iLogID, MoviePriceLog oLog)
        {
            if (iLogID != oLog.MoviePriceLogID)
            {
                return BadRequest();
            }

            try
            {
                oLogRepository.Update(oLog);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MoviePriceLogExists(iLogID))
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

        // POST: api/MoviePriceLogs
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public ActionResult<MoviePriceLog> PostMoviePriceLog(MoviePriceLog oLog)
        {
            oLogRepository.Create(oLog);
            return CreatedAtAction("GetMoviePriceLog", new { iLogID = oLog.MoviePriceLogID }, oLog);
        }

        // DELETE: api/MoviePriceLogs/5
        [HttpDelete("{id}")]
        public ActionResult<MoviePriceLog> DeleteMoviePriceLog(int iLogID)
        {
            var oLog = oLogRepository.GetById(iLogID);
            if (oLog == null)
            {
                return NotFound();
            }

            oLogRepository.Delete(oLog);
            return oLog;
        }

        private bool MoviePriceLogExists(int id)
        {
            bool bResult = false;

            if (oLogRepository.Find(x => x.MoviePriceLogID == id).ToList().Count > 0)
            {
                bResult = true;
            }

            return bResult;
        }
    }
}
