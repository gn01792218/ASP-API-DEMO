using ASPAPI.Models.Domain;
using ASPAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASPAPI.Controllers
{
    [Route("api/[controller]")] //API URL [controller] 最自動抓這個Controll名稱"Regions"
    [ApiController]  //宣告這個Controller是API的
    public class Regions : ControllerBase
    {
        //依賴注入_regionRepository
        private readonly IRegionRepositiory _regionRepository;
        public Regions(IRegionRepositiory regionRepository)
        {
            _regionRepository = regionRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRegions()
        {
            var regions = await _regionRepository.GetAllAsync();

            //轉化成DTO資料
            var regionDTOs = new List<Models.DTOs.Region>();
            regions.ToList().ForEach(r =>
            {
                var regionDTO = new Models.DTOs.Region()
                {
                    Id = r.Id,
                    Name = r.Name,
                    Code = r.Code,
                    Area = r.Area,
                    Lat = r.Lat,
                    Long = r.Long,
                    Population = r.Population,
                };
                regionDTOs.Add(regionDTO);
            });
            return Ok(regionDTOs);  //回傳一個 200 ok 的回應
        }

        //依據ID獲取Region
        [HttpGet]
        [Route("{id:guid}")]  //需要透過Route帶ID (冒號聲明要是guid型態)
        public async Task<IActionResult> GetRegionById(Guid id)
        {
            var region = await _regionRepository.GetRegionByIdAsync(id);
            if (region == null) return NotFound();
            return Ok(region);
        }
    }
}
