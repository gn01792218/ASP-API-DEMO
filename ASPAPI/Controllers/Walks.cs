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
        private readonly IRegionRepositiory _regionRepositiory;
        private readonly IWalkDifficultyRepository _walkDifficultyRepository;
        public Walks(IWalkRepository walkRepository,IRegionRepositiory regionRepositiory,IWalkDifficultyRepository walkDifficultyRepository) 
        {
            _walkRepository = walkRepository;
            _regionRepositiory = regionRepositiory;
            _walkDifficultyRepository = walkDifficultyRepository;
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
            if (!await ValidateAddRequest(walkRequest)) return BadRequest(ModelState);
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
            if (!await ValidateUpdateRequest(updateWalkRequest)) return BadRequest(ModelState);
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
        #region Pravate methods
        private async Task<bool> ValidateAddRequest(AddWalkRequest addWalkRequest)
        {
            if (addWalkRequest == null)
            {
                ModelState.AddModelError(nameof(addWalkRequest), $"{nameof(addWalkRequest)} is required");
                return false;
            }
            if(string.IsNullOrWhiteSpace(addWalkRequest.Name))
            {
                ModelState.AddModelError(nameof(addWalkRequest.Name),
                    $"{nameof(addWalkRequest.Name)} cannot be null or space");
            }
            if (addWalkRequest.Length <= 0)
            {
                ModelState.AddModelError(nameof(addWalkRequest.Length),
                    $"{nameof(addWalkRequest.Length)} must greater than zero");
            }
            //驗證關聯資料表的ID(請先依賴注入)
            var region =await _regionRepositiory.GetRegionByIdAsync(addWalkRequest.RegionId);
            if(region == null) 
            {
                ModelState.AddModelError(nameof(addWalkRequest.RegionId),
                    $"{nameof(addWalkRequest.RegionId)} is not valid Region Id");
            }

            var walkDefficulty =await _walkDifficultyRepository.GetByIdAsync(addWalkRequest.WalkDifficultyId);
            if(walkDefficulty == null)
            {
                ModelState.AddModelError(nameof(addWalkRequest.WalkDifficultyId),
                    $"{nameof(addWalkRequest.WalkDifficultyId)} is not valid WalkDifficulty Id");
            }
            if (ModelState.ErrorCount > 0) return false;
            return true;
        }
        private async Task<bool> ValidateUpdateRequest(UpdateWalkRequest updateWalkRequest)
        {
            if (updateWalkRequest == null)
            {
                ModelState.AddModelError(nameof(updateWalkRequest), $"{nameof(updateWalkRequest)} is required");
                return false;
            }
            if (string.IsNullOrWhiteSpace(updateWalkRequest.Name))
            {
                ModelState.AddModelError(nameof(updateWalkRequest.Name),
                    $"{nameof(updateWalkRequest.Name)} cannot be null or space");
            }
            if (updateWalkRequest.Length <= 0)
            {
                ModelState.AddModelError(nameof(updateWalkRequest.Length),
                    $"{nameof(updateWalkRequest.Length)} must greater than zero");
            }
            //驗證關聯資料表的ID(請先依賴注入)
            var region = await _regionRepositiory.GetRegionByIdAsync(updateWalkRequest.RegionId);
            if (region == null)
            {
                ModelState.AddModelError(nameof(updateWalkRequest.RegionId),
                    $"{nameof(updateWalkRequest.RegionId)} is not valid Region Id");
            }

            var walkDefficulty = await _walkDifficultyRepository.GetByIdAsync(updateWalkRequest.WalkDifficultyId);
            if (walkDefficulty == null)
            {
                ModelState.AddModelError(nameof(updateWalkRequest.WalkDifficultyId),
                    $"{nameof(updateWalkRequest.WalkDifficultyId)} is not valid WalkDifficulty Id");
            }
            if (ModelState.ErrorCount > 0) return false;
            return true;
        }
        #endregion
    }
}
