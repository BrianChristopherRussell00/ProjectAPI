using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectAPI.Data;
using ProjectAPI.Models.Domain;
using ProjectAPI.Models.DTOs;
using ProjectAPI.Repository;

namespace ProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WalksController : ControllerBase
    {
        private readonly BrianRussellDbContext dbContext;
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;

        public WalksController(IMapper mapper, IWalkRepository walkRepository)
        {
            this.mapper = mapper;
            this.walkRepository = walkRepository;
        }
        //CREATE Walk
        // POST: /api/walks
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody]AddWalkRequestDto addWalkRequestDto)
        {
            if (ModelState.IsValid)
            {


                // Map DTO to Domain Model
                var walkDomainModel = mapper.Map<Walk>(addWalkRequestDto);
                await walkRepository.CreateAsync(walkDomainModel);

                //Map Domain Model to DTO   

                return Ok(mapper.Map<WalkDto>(walkDomainModel));
            }
            return BadRequest(ModelState);
        
        }

        //GET Walks 
        //GET: /api/walks
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
          var walksDomainModel =  await walkRepository.GetAllAsync();
            //Map Domain Model to DTO 
            return Ok(mapper.Map<List<WalkDto>>(walksDomainModel));
        
        }
        //Get Walk By Id    
        //GET: /api/Walks/{id}  
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
        {
            var walkDomainModel = await walkRepository.GetByIdAsync(id);
        if (walkDomainModel == null)
            {
                return NotFound();
            }
            //Map Domain Model to DTO   
            return Ok(mapper.Map<WalkDto>(walkDomainModel));
        }


        // UPDATE Walk BY Id
        // PUT: /api/Walks/{id}  
        [HttpPost]
        [Route("{id:guid}")]

        public async Task<IActionResult> Update([FromRoute]Guid id, UpdateWalkRequestDto updateWalkRequestDto)
        {
            if (ModelState.IsValid)
            {


                //Map DTO to Domain Model   
                var walkDomainModel = mapper.Map<Walk>(updateWalkRequestDto);
                await walkRepository.UpdateAsync(id, walkDomainModel);

                if (walkDomainModel == null)
                {
                    return NotFound();
                }
                //Map Domain Model to DTO   
                return Ok(mapper.Map<WalkDto>(walkDomainModel));
            }   
            return BadRequest(ModelState);
            }
        //Delete a Walk By Id   
        //DELETE: /api/Walks{id}        
        [HttpDelete]
        [Route("{id:guid}")]

        public async Task<IActionResult> Delete([FromRoute]Guid id)
        {
         var deletedWalkDomainModel =   await walkRepository.DeleteAsync(id);
            if (deletedWalkDomainModel == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<WalkDto>(deletedWalkDomainModel));


        }



    }
}
