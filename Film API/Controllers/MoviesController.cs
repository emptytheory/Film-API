using Microsoft.AspNetCore.Mvc;
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
        /// <param name="franchiseId">Pass a franchiseId to include only movies in that franchise.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieBriefDTO>>> GetMovies(int? franchiseId = null)
        {
            IEnumerable<Movie> movieList;

            if (franchiseId is null)
            {
                movieList = await _movieService.GetAllAsync();
            }
            else
            {
                movieList = await _movieService.GetMoviesByFranchiseIdAsync(franchiseId.Value);
            }
            
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
            catch (NoRowsAffectedException Ex)
            {
                return StatusCode(204, Ex.Message);
            }

            return NoContent();
        }

        /// <summary>
        /// Replaces the characters of the movie having the given id with the characters whose ids are given. 
        /// </summary>
        /// <param name="characterIds">CharacterIds not in the database and repeat ids are ignored.</param>
        /// <param name="id">Id of the movie.</param>
        /// <returns></returns>
        [HttpPut("{id}/characters")]
        public async Task<IActionResult> UpdateMovieCharacters(int[] characterIds, int id)
        {
            try
            {
                await _movieService.UpdateCharactersAsync(characterIds, id);
            }
            catch (EntityNotFoundException Ex)
            {
                return NotFound(Ex.Message);
            }
            catch (NoRowsAffectedException Ex)
            {
                return StatusCode(204, Ex.Message);
            }

            return NoContent();
        }


        /// <summary>
        /// Create a new movie.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<MoviePostDTO>> PostMovie(MoviePostDTO dto)
        {

            // map
            Movie movie = _mapper.Map<Movie>(dto);

            // add via service
            Movie created = await _movieService.AddAsync(movie);

            // map back
            MoviePostDTO createdDTO = _mapper.Map<MoviePostDTO>(movie);

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
