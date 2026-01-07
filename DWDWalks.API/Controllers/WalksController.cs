using AutoMapper;
using DWDWalks.API.Models.Domain;
using DWDWalks.API.Models.DTO;
using DWDWalks.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DWDWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;

        public WalksController(IMapper mapper, IWalkRepository walkRepository)
        {
            this.mapper = mapper;
            this.walkRepository = walkRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
        {
            if (ModelState.IsValid)
            {
                return BadRequest();
            }
            // Map DTO to Domain Model
            var domainModel = mapper.Map<Walk>(addWalkRequestDto);

            if (domainModel == null)
            {
                return BadRequest();
            }

            var createdWalk = await walkRepository.CreateAsync(domainModel);

            return Ok(mapper.Map<WalkDto>(createdWalk));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] string? filterOn, [FromQuery] string? filterQuery, [FromQuery] string? sortBy, [FromQuery] bool? isAscending)
        {
            var getWalks = await walkRepository.GetAllAsync(filterOn, filterQuery, sortBy, isAscending ?? true);

            return Ok(mapper.Map<List<WalkDto>>(getWalks));
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var walk = await walkRepository.GetByIdAsync(id);
            if (walk == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<WalkDto>(walk));
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateByIdAsync([FromRoute] Guid id, [FromBody] UpdateWalkRequestDto updateWalkRequestDto)
        {
            if (ModelState.IsValid)
            {
                return BadRequest();
            }

            var domainModel = mapper.Map<Walk>(updateWalkRequestDto);
            domainModel = await walkRepository.UpdateAsync(id, domainModel);
            if (domainModel == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<WalkDto>(domainModel));
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteByIdAsync([FromRoute] Guid id)
        {
            var deletedWalk = await walkRepository.DeleteByIdAsync(id);
            if (deletedWalk == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<WalkDto>(deletedWalk));
        }
    }
}
