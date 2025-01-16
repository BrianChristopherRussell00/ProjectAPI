using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectAPI.Data;
using ProjectAPI.Models.DTOs;
using ProjectAPI.Repository;

namespace ProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly BrianRussellDbContext dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(BrianRussellDbContext dbContext, IRegionRepository regionRepository,IMapper mapper)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var regionsDomain = await regionRepository.GetAllAsync();

            //var regionsDto = new List<RegionDto>();
            //foreach (var regionDomain in regionsDomain)
            //{
            //    regionsDto.Add(new RegionDto()
            //    {
            //        Id = regionDomain.Id,
            //        Code = regionDomain.Code,
            //        Name = regionDomain.Name,
            //        RegionImageUrl = regionDomain.RegionImageUrl

            //    });

            //}

            //Return DTOs   
         var regionsDto = mapper.Map<List<RegionDto>>(regionsDomain);

            return Ok(regionsDto);
        }
        //GET SINGLE REGION (Get Region By ID)
        //GET: https:// localhost:portnumber/api/regions
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {// var region = dbContext.Regions.FirstOrDefault(x=> x.Id == id);
            // Get region domain model from database
            var regionDomain = await regionRepository.GetByIdAsync(id);
            if (regionDomain == null)
            {
                return NotFound();
            }//Map/Convert Region Domain Model to Region DTO
            //var regionDto = new RegionDto()
            //{

            //    Id = regionDomain.Id,
            //    Code = regionDomain.Code,
            //    Name = regionDomain.Name,
            //    RegionImageUrl = regionDomain.RegionImageUrl
            //};
            //Return DTO back to client 
         var regionDto =   mapper.Map<RegionDto>(regionDomain);
            
            return Ok(regionDto);
        }

        //POST To Create New Region
        //POST: https:// localhost:portnumber/api/regions
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            if (ModelState.IsValid)
            {


                var regionDomainModel = mapper.Map<Region>(addRegionRequestDto);
                // Map or Convert DTO to Domain Model
                //var regionDomainModel = new Region
                //{
                //    Code = addRegionRequestDto.Code,
                //    Name = addRegionRequestDto.Name,
                //    RegionImageUrl = addRegionRequestDto.RegionImageUrl
                //};
                // Use Domain Mode to create Region
                regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);

                // Map Domain Model back to Dto
                //var regionDto = new RegionDto
                //{
                //    Id = regionDomainModel.Id,
                //    Code = regionDomainModel.Code,
                //    Name = regionDomainModel.Name,
                //    RegionImageUrl = regionDomainModel.RegionImageUrl
                //};
                var regionDto = mapper.Map<RegionDto>(regionDomainModel);
                return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        //Update Region
        //PUT: https:// localhost:portnumber/api/regions{id}
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            if (ModelState.IsValid)
            {
                //Map DTO to Domain Model
                var regionDomainModel = new Region
                {
                    Code = updateRegionRequestDto.Code,
                    Name = updateRegionRequestDto.Name,
                    RegionImageUrl = updateRegionRequestDto.RegionImageUrl
                };

                //Check if region exists


                var regionDto = new RegionDto
                {
                    Id = regionDomainModel.Id,
                    Code = regionDomainModel.Code,
                    Name = regionDomainModel.Name,
                    RegionImageUrl = regionDomainModel.RegionImageUrl
                };
                return Ok(regionDto);

            }
            else
            {
                return BadRequest(ModelState);
            }



        }

        //Delete Region
        //DELETE: https:// localhost:portnumber/api/regions{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
         var regionDomainModel= await regionRepository.DeleteAsync(id);
         if (regionDomainModel == null)
            {
                return NotFound();
            }
            //Delete Region
        

            //Return deleted Region back
            //Map Domain Model to DTO
            //var regionDto = new RegionDto
            //{
            //    Id = regionDomainModel.Id,
            //    Code = regionDomainModel.Code,
            //    Name = regionDomainModel.Name,
            //    RegionImageUrl = regionDomainModel.RegionImageUrl
            //};


            return Ok(mapper.Map<RegionDto>(regionDomainModel));


        }
    }
}
