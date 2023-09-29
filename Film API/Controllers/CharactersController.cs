using Microsoft.AspNetCore.Mvc;
using Film_API.Data.Entities;
using AutoMapper;
using Film_API.Services.Characters;
using Film_API.Data.DTOs.Character;
using Film_API.Data.Exceptions;

namespace Film_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharactersController : ControllerBase
    {
        private readonly ICharacterService _characterService;
        private readonly IMapper _mapper;

        public CharactersController(ICharacterService characterService, IMapper mapper)
        {
            _characterService = characterService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all characters.
        /// </summary>
        /// <param name="franchiseId">Pass a franchiseId to return only the characters in the given franchise.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CharacterBriefDTO>>> GetCharacters(int? franchiseId = null)
        {
            try
            {
                IEnumerable<Character> characterList;

                if (franchiseId is null)
                {
                    characterList = await _characterService.GetAllAsync();
                }
                else
                {
                    characterList = await _characterService.GetCharactersByFranchiseIdAsync(franchiseId.Value);
                }

                var dtoList = _mapper.Map<IEnumerable<CharacterBriefDTO>>(characterList);
                return Ok(dtoList);
            }
            catch (EntityNotFoundException Ex)
            {
                return NotFound(Ex.Message);
            }
        }

        /// <summary>
        /// Get a character by its id.
        /// </summary>
        /// <param name="id">The id of the character.</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<CharacterDetailDTO>> GetCharacter(int id)
        {
            try
            {
                Character character = await _characterService.GetByIdAsync(id);

                CharacterDetailDTO dto = _mapper.Map<CharacterDetailDTO>(character);

                return Ok(dto);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Updates the character having the given id with data from the given DTO.
        /// Does nothing if no character has the given id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCharacter(int id, CharacterPutDTO dto)
        {
            if (id != dto.Id)
                return BadRequest("Id in path does not match id in body.");

            try
            {
                Character character = await _characterService.GetByIdAsync(id);

                _mapper.Map(dto, character);

                await _characterService.UpdateAsync(character);
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
        /// Create a new character.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<CharacterPostDTO>> PostCharacter(CharacterPostDTO dto)
        {
            Character character = _mapper.Map<Character>(dto);
            Character created = await _characterService.AddAsync(character);
            CharacterPostDTO createdDTO = _mapper.Map<CharacterPostDTO>(character);
            return CreatedAtAction(nameof(GetCharacter), new { id = created.Id }, createdDTO);
        }

        /// <summary>
        /// Deletes the character with the given id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCharacter(int id)
        {
            try
            {
                await _characterService.DeleteByIdAsync(id);
            }
            catch (EntityNotFoundException Ex)
            {
                return NotFound(Ex.Message);
            }

            return NoContent();
        }

        /// <summary>
        /// Replaces the movies of the character having the given id with the movies whose ids are given. 
        /// </summary>
        /// <param name="movieIds">MovieIds not in the database and repeat ids are ignored.</param>
        /// <param name="id">Id of the movie.</param>
        /// <returns></returns>
        [HttpPut("{id}/movies")]
        public async Task<IActionResult> UpdateCharacterMovies(int[] movieIds, int id)
        {
            try
            {
                await _characterService.UpdateMoviesAsync(movieIds, id);
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

    }
}
