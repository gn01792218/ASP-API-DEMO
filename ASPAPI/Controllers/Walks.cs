using ASPAPI.Models.Domain;
using ASPAPI.Models.DTOs;
using ASPAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ASPAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Walks : ControllerBase
    {
        //依賴注入Repository
        private readonly IWalkRepository _walkRepository;
        public Walks(IWalkRepository walkRepository) 
        {
            _walkRepository = walkRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var walks =  await _walkRepository.GetAllAsync();

            //Convert to DTO
            var walksDTO = new List<Models.DTOs.Walk>();
            walks.ToList().ForEach(w =>
            {
                var walkDTO = new Models.DTOs.Walk()
                {
                    Id = w.Id,
                    Name = w.Name,
                    Length = w.Length,
                    RegionId = w.RegionId,
                    WalkDifficultyId = w.WalkDifficultyId,
                    Region = new Models.DTOs.Walk().toRegionDTO(w.Region),
                    WalkDifficulty = new Models.DTOs.Walk().toWalkDifficultyDTO(w.WalkDifficulty),
                };
                walksDTO.Add(walkDTO);
            });
            return Ok(walksDTO);
        }
        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetByIdAsync")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var w = await _walkRepository.GetByIdAsync(id);
            if (w == null) return NotFound();
            //cover to DTO
            var DTO = new Models.DTOs.Walk()
            {
                Id = w.Id,
                Name = w.Name,
                Length = w.Length,
                RegionId = w.RegionId,
                WalkDifficultyId = w.WalkDifficultyId,
                Region = new Models.DTOs.Walk().toRegionDTO(w.Region),
                WalkDifficulty = new Models.DTOs.Walk().toWalkDifficultyDTO(w.WalkDifficulty),
            };
            return Ok(DTO);
        }
        [HttpPost]
        public async Task<IActionResult> AddWalk([FromBody]AddWalkRequest walkRequest)
        {
            if (walkRequest == null) return NotFound();
            //request to model
            var walk = new Models.Domain.Walk()
            {
                Name = walkRequest.Name,
                Length = walkRequest.Length,
                RegionId = walkRequest.RegionId,
                WalkDifficultyId =walkRequest.WalkDifficultyId,
            };

            var w = await _walkRepository.AddAsync(walk);
            if(w == null) return NotFound();    
            //cover to DTO
            var DTO = new Models.DTOs.Walk()
            {
                Id = w.Id,
                Name = w.Name,
                Length = w.Length,
                RegionId = w.RegionId,
                WalkDifficultyId = w.WalkDifficultyId,
            };
            return CreatedAtAction(nameof(GetByIdAsync), new {id = DTO.Id},DTO);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateWalk([FromRoute]Guid id, [FromBody]UpdateWalkRequest updateWalkRequest)
        {
            var walk =await _walkRepository.UpdateAsync(id, updateWalkRequest);
            if (walk == null) return NotFound();
            //conver to DTO
            var DTO = new Models.DTOs.Walk() 
            {
                Id = walk.Id,
                Name = walk.Name,
                Length = walk.Length,
                RegionId = walk.RegionId,
                WalkDifficultyId = walk.WalkDifficultyId,
            };
            return Ok(DTO);
        }
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteWalk(Guid id)
        {
            var walk =await _walkRepository.DeleteAsync(id);
            if( walk == null) return NotFound();
            //conver to DTO
            var DTO = new Models.DTOs.Walk()
            {
                Id = walk.Id,
                Name = walk.Name,
                Length = walk.Length,
                RegionId = walk.RegionId,
                WalkDifficultyId = walk.WalkDifficultyId,
            };
            return Ok(DTO);
        }
    }
}
