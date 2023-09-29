using Microsoft.AspNetCore.Mvc;
using Film_API.Data.Entities;
using AutoMapper;
using Film_API.Services.Franchises;
using Film_API.Data.DTOs.Movies;
using Film_API.Data.Exceptions;
using Film_API.Data.DTOs.Franchises;
using Humanizer;

namespace Film_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FranchisesController : ControllerBase
    {
        private readonly IFranchiseService _franchiseService;
        private readonly IMapper _mapper;

        public FranchisesController(IFranchiseService franchiseService, IMapper mapper)
        {
            _franchiseService = franchiseService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all franchises.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FranchiseBriefDTO>>> GetFranchises()
        {
            IEnumerable<Franchise> franchiseList = await _franchiseService.GetAllAsync();
            var dtoList = _mapper.Map<IEnumerable<FranchiseBriefDTO>>(franchiseList);
            return Ok(dtoList);
        }

        /// <summary>
        /// Get a franchise by id.
        /// </summary>
        /// <param name="id">The id of the franchise.</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<FranchiseDetailDTO>> GetFranchise(int id)
        {
            try
            {
                Franchise franchise = await _franchiseService.GetByIdAsync(id);

                FranchiseDetailDTO dto = _mapper.Map<FranchiseDetailDTO>(franchise);

                return Ok(dto);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Updates the franchise having the given id with data from the given DTO.
        /// Does nothing if no franchise has the given id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFranchise(int id, FranchisePutDTO dto)
        {
            if (id != dto.Id)
                return BadRequest("Id in path does not match id in body.");

            try
            {
                Franchise franchise = await _franchiseService.GetByIdAsync(id);

                _mapper.Map(dto, franchise);

                await _franchiseService.UpdateAsync(franchise);
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
        /// Create a new franchise.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<FranchisePostDTO>> PostFranchise(FranchisePostDTO dto)
        {
            Franchise franchise = _mapper.Map<Franchise>(dto);
            Franchise created = await _franchiseService.AddAsync(franchise);
            FranchisePostDTO createdDTO = _mapper.Map<FranchisePostDTO>(franchise);
            return CreatedAtAction(nameof(GetFranchise), new { id = created.Id }, createdDTO);
        }

        /// <summary>
        /// Deletes the franchise with the given id.
        /// </summary>
        /// <param name="id">Id if the franchise to be deleted.</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFranchise(int id)
        {
            try
            {
                await _franchiseService.DeleteByIdAsync(id);
            }
            catch (EntityNotFoundException Ex)
            {
                return NotFound(Ex.Message);
            }

            return NoContent();
        }
    }
}
