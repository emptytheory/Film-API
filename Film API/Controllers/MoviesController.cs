using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Film_API.Data;
using Film_API.Data.Entities;
using Film_API.Services.Movies;
using Film_API.Data.Exceptions;
using Film_API.Data.DTOs.Movie;
using System.Net.Mime;

namespace Film_API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        /// <summary>
        /// Get all movies.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieBriefDTO>>> GetMovies()
        {
            var movieList = await _movieService.GetAllAsync();
            // Replace with AutoMapper
            return Ok(movieList.Select(movie => new MovieBriefDTO(movie.Id, movie.Title)));
        }

        /// <summary>
        /// Get a movie by its id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<MovieDetailDTO>> GetMovie(int id)
        {
            try
            {
                Movie movie = await _movieService.GetByIdAsync(id);

                // Replace with AutoMapper
                MovieDetailDTO dto = new(movie.Id, 
                                   movie.Title, 
                                   movie.Genre, 
                                   movie.ReleaseYear, 
                                   movie.Director, 
                                   movie.Picture, 
                                   movie.Trailer, 
                                   movie.FranchiseId,
                                   movie.Characters.Select(c => c.Id).ToArray());

                return Ok(dto);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
 
        }

        // PUT: api/Movies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovie(int id, MoviePutDTO dto)
        {
            if (id != dto.Id)
                return BadRequest("Id in path does not match id in body.");

            try
            {
                Movie movie = await _movieService.GetByIdAsync(id);

                // Replace with AutoMapper
                movie.Title = dto.Title;
                movie.Genre = dto.Genre;
                movie.ReleaseYear = dto.ReleaseYear;
                movie.Director = dto.Director;
                movie.Picture = dto.Picture;
                movie.Trailer = dto.Trailer;
                movie.FranchiseId = dto.FranchiseId;

                await _movieService.UpdateAsync(movie);
            }
            catch (EntityNotFoundException Ex)
            {
                return NotFound(Ex.Message);
            }
            catch (NoEffectUpdateException Ex)
            {
                return StatusCode(204, Ex.Message);
            }

            return NoContent();
        }

        // POST: api/Movies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Movie>> PostMovie(Movie movie)
        {
          //if (_context.Movies == null)
          //{
          //    return Problem("Entity set 'FilmDbContext.Movies'  is null.");
          //}
          //  _context.Movies.Add(movie);
          //  await _context.SaveChangesAsync();

            return CreatedAtAction("GetMovie", new { id = movie.Id }, movie);
        }

        // DELETE: api/Movies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            //if (_context.Movies == null)
            //{
            //    return NotFound();
            //}
            //var movie = await _context.Movies.FindAsync(id);
            //if (movie == null)
            //{
            //    return NotFound();
            //}

            //_context.Movies.Remove(movie);
            //await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MovieExists(int id)
        {
            return false; //(_context.Movies?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
