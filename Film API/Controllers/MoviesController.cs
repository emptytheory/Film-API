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
using AutoMapper;

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
        private readonly IMapper _mapper;

        public MoviesController(IMovieService movieService, IMapper mapper)
        {
            _movieService = movieService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all movies.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieBriefDTO>>> GetMovies()
        {
            var movieList = await _movieService.GetAllAsync();

            var dtoList = _mapper.Map<IEnumerable<MovieBriefDTO>>(movieList);

            return Ok(dtoList);
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

                MovieDetailDTO dto = _mapper.Map<MovieDetailDTO>(movie);

                return Ok(dto);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
 
        }

        /// <summary>
        /// Updates the movie with the given id with data from the given DTO.
        /// Does nothing if no movie has the given id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovie(int id, MoviePutDTO dto)
        {
            if (id != dto.Id)
                return BadRequest("Id in path does not match id in body.");

            try
            {
                Movie movie = await _movieService.GetByIdAsync(id);

                _mapper.Map(dto, movie);

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

        [HttpPost]
        public async Task<ActionResult<MoviePostDTO>> PostMovie(MoviePostDTO dto)
        {
            // Do properly with AutoMapper
            HashSet<Character> characters = new HashSet<Character>(await _movieService.GetCharacterArrayFromIdArray(dto.Characters));
            Movie movie = new()
            {
                Id = 0,
                Title = dto.Title,
                Genre = dto.Genre,
                ReleaseYear = dto.ReleaseYear,
                Director = dto.Director,
                Picture = dto.Picture,
                Trailer = dto.Trailer,
                FranchiseId = dto.FranchiseId,
                Characters = characters // The mapper can ignore this and set it to null. Then I can use a separate method for setting characters afterwards.
            };

            // add via service
            Movie created = await _movieService.AddAsync(movie);

            // Do properly with AutoMapper
            MoviePostDTO createdDTO = new(created.Title, created.Genre, created.ReleaseYear, created.Director, created.Picture, created.Trailer, created.FranchiseId, created.Characters.Select(c => c.Id).ToArray());

            return CreatedAtAction(nameof(GetMovie), new {id = created.Id }, createdDTO);
        }

        /// <summary>
        /// Deletes the movie with the given id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            try
            {
                await _movieService.DeleteByIdAsync(id);
            }
            catch (EntityNotFoundException Ex)
            {
                return NotFound(Ex.Message);
            }
           
            return NoContent();
        }

    }
}
