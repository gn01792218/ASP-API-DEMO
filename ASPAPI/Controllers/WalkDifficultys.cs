using ASPAPI.Models.DTOs;
using ASPAPI.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASPAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "reader")] //全部需要JWT驗證，且要有reader的權限
    public class WalkDifficultys : ControllerBase
    {
        private readonly IWalkDifficultyRepository _walkDifficultyRepository;
        public WalkDifficultys(IWalkDifficultyRepository walkDifficultyRepository)
        {
            _walkDifficultyRepository = walkDifficultyRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var walkDifficultys = await _walkDifficultyRepository.GetAllAsync();
            //convert to DTO
            var DTOS = new List<Models.DTOs.WalkDifficulty>();
            walkDifficultys.ToList().ForEach(w =>
            {
                var walkDifficulty = new Models.DTOs.WalkDifficulty()
                {
                    Id = w.Id,
                    Code = w.Code,
                };
                DTOS.Add(walkDifficulty);
            });
            return Ok(DTOS);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName(nameof(GetById))]
        public async Task<IActionResult> GetById(Guid id)
        {
            var walkDifficulty = await _walkDifficultyRepository.GetByIdAsync(id);
            if (walkDifficulty == null) return NotFound();
            //conver to DTO
            var DTO = new Models.DTOs.WalkDifficulty()
            {
                Id = walkDifficulty.Id,
                Code = walkDifficulty.Code
            };
            return Ok(DTO);
        }

        [HttpPost]
        [Authorize(Roles = "writer")] //需要JWT驗證，且要有writer的權限
        public async Task<IActionResult> AddAsync([FromBody] AddWalkDifficultyRequest addWalkDifficultyRequest)
        {
            var walkDifficulty = await _walkDifficultyRepository.AddAsync(addWalkDifficultyRequest);
            if (walkDifficulty == null) return NotFound();
            //convert to DTO
            var DTO = new Models.DTOs.WalkDifficulty()
            {
                Id = walkDifficulty.Id,
                Code = walkDifficulty.Code
            };
            return CreatedAtAction(nameof(GetById), new { id = walkDifficulty.Id }, DTO);
        }

        [HttpPut]
        [Route("{id:guid}")]
        [Authorize(Roles = "writer")] //需要JWT驗證，且要有writer的權限
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] UpdateWalkDifficultyRequest updateWalkDifficultyRequest)
        {
            var walkDifficulty = await _walkDifficultyRepository.UpdateAsync(id, updateWalkDifficultyRequest);
            if (walkDifficulty == null) return NotFound();
            //convert to DTO
            var DTO = new Models.DTOs.WalkDifficulty()
            {
                Id = walkDifficulty.Id,
                Code = walkDifficulty.Code
            };
            return Ok(DTO);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        [Authorize(Roles = "writer")] //需要JWT驗證，且要有writer的權限
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var walkDifficulty = await _walkDifficultyRepository.DeleteAsync(id);
            if (walkDifficulty == null) return NotFound();

            //convert to DTO
            var DTO = new Models.DTOs.WalkDifficulty()
            {
                Id = walkDifficulty.Id,
                Code = walkDifficulty.Code
            };
            return Ok(DTO);
        }
    }
}
