using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectAPI.Data;
using ProjectAPI.Models.DTOs;

namespace ProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly BrianRussellDbContext dbContext;
        //CREATE Walk
       // POST: /api/walks
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody]AddWalkRequestDto addWalkRequestDto)
        {
            // Map DTO to Domain Model
            await dbContext.Walks.AddAsync(addWalkRequestDto);
            await dbContext.SaveChangesAsync();
            return addWalkRequestDto;
        }

    }
}
