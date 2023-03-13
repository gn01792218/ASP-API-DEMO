using ASPAPI.Models.Domain;
using ASPAPI.Models.DTOs;
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
        [ActionName("GetRegionById")] //為此方法取一個暴露出去的名稱
        public async Task<IActionResult> GetRegionById(Guid id)
        {
            var region = await _regionRepository.GetRegionByIdAsync(id);
            if (region == null) return NotFound();
            return Ok(region);
        }

        //新增Region
        //這裡接收的參數不使用Region
        //因為由於我們希望由後端建立id
        //所以這裡要使用AddRegionRequest作為參數傳遞
        [HttpPost]
        public async Task<IActionResult> AddRegion(AddRegionRequest addRegionRequest) 
        {
            //1.RequestDTO to Domain model
            var region = new Models.Domain.Region()
            {
                Name = addRegionRequest.Name,
                Code = addRegionRequest.Code,
                Area = addRegionRequest.Area,  
                Lat = addRegionRequest.Lat, 
                Long = addRegionRequest.Long,   
                Population = addRegionRequest.Population,
            };
            //2.pass to Reposetory
            //AddRegionAsync() will return a Region
            region =  await _regionRepository.AddRegionAsync(region);

            //3.Conver back to DTO
            var regionDTO = new Models.DTOs.Region()
            {
                Id = region.Id,
                Code = region.Code,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Population = region.Population,
            };
            //因為是新增，不會回傳Ok
            //使用CreatedAtAction較為方便，因為可以call 另一個 action function來使用
            //參數一 : 要使用的action function名稱 : 這裡使用nameof，讓程式自動判斷，比寫死"GetRegionById"要好
            //參數二 : 要帶過去的參數
            //參數三 : respon 的東西
            return CreatedAtAction(nameof(GetRegionById), new { id = regionDTO.Id }, regionDTO);
        }
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteRegion(Guid id)
        {
            //Get Region from db
            var region = await _regionRepository.DeleteRegionAsync(id);
            if(region == null) return NotFound();

            //Convert response back to DTO
            var regionDTO = new Models.DTOs.Region()
            {
                Id = region.Id,
                Name = region.Name,
                Code = region.Code,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Population = region.Population,
            };
            return Ok(regionDTO);
        }
    }
}
