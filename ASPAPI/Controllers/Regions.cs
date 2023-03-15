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
            //validate
            if (!VaildateAddRegionAsync(addRegionRequest)) return BadRequest(ModelState);

            //1.RequestDTO to Domain model
            var region = new Models.Domain.Region()
            {
                Code = addRegionRequest.Code,
                Name = addRegionRequest.Name,
                Area = addRegionRequest.Area,
                Lat = addRegionRequest.Lat,
                Long = addRegionRequest.Long,
                Population = addRegionRequest.Population,
            };
            //2.pass to Reposetory
            //AddRegionAsync() will return a Region
            region = await _regionRepository.AddRegionAsync(region);

            //3.Conver back to DTO
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
            //因為是新增，不會回傳Ok
            //使用CreatedAtAction較為方便，因為可以call 另一個 action function來使用
            //參數一 : 要使用的action function名稱 : 這裡使用nameof，讓程式自動判斷，比寫死"GetRegionById"要好
            //參數二 : 要帶過去的參數
            //參數三 : respon 的東西
            return CreatedAtAction(nameof(GetRegionById), new { id = regionDTO.Id }, regionDTO);
        }
        //修改
        [HttpPut]
        [Route("{id:guid}")] //id從URL拿
        public async Task<IActionResult> UpdateRegionAsync([FromRoute] Guid id, [FromBody] UpdateRegionRequest updateRegion) //region從FromBody拿
        {
            //validate
            if (!VaildateUpdateRegionAsync(updateRegion)) return BadRequest(ModelState);
            //把updateRegion 還原成 Region
            var region = new Models.Domain.Region()
            {
                Code = updateRegion.Code,
                Name = updateRegion.Name,
                Area = updateRegion.Area,
                Lat = updateRegion.Lat,
                Long = updateRegion.Long,
                Population = updateRegion.Population,
            };
            //向Repository要DB資料
            region = await _regionRepository.UpdateRegionAsync(id, region);
            if (region == null) return NotFound();
            //轉換回DTO
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

        //刪除
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteRegion(Guid id)
        {
            //Get Region from db
            var region = await _regionRepository.DeleteRegionAsync(id);
            if (region == null) return NotFound();

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

        #region Private methods
        private bool VaildateAddRegionAsync(AddRegionRequest addRegionRequest)
        {
            //檢查整個物件是否為空
            if(addRegionRequest == null)
            {
                ModelState.AddModelError(nameof(addRegionRequest),
                    $"Add Region Data is Require");
                return false;
            };
            //檢查null和空白字元
            if (string.IsNullOrWhiteSpace(addRegionRequest.Code))
            {
                //添加系統訊息
                ModelState.AddModelError(nameof(addRegionRequest.Code), "Cannot be null or white space");
            };
            if (string.IsNullOrWhiteSpace(addRegionRequest.Name))
            {
                //添加系統訊息
                ModelState.AddModelError(nameof(addRegionRequest.Name), "Cannot be null or white space");
            };

            //檢查數字
            if(addRegionRequest.Area <= 0)
            {
                ModelState.AddModelError(nameof(addRegionRequest.Area),
                    $"{nameof(addRegionRequest.Area)} cannot be less than or equal to zero");
            };
            if (addRegionRequest.Population <= 0)
            {
                ModelState.AddModelError(nameof(addRegionRequest.Population),
                    $"{nameof(addRegionRequest.Population)} cannot be less than or equal to zero");
            };

            //最終返回
            if (ModelState.ErrorCount > 0) return false;
            return true;
        }
        private bool VaildateUpdateRegionAsync(UpdateRegionRequest updateRegionRequest)
        {
            //檢查整個物件是否為空
            if (updateRegionRequest == null)
            {
                ModelState.AddModelError(nameof(updateRegionRequest),
                    $"Add Region Data is Require");
                return false;
            };
            //檢查null和空白字元
            if (string.IsNullOrWhiteSpace(updateRegionRequest.Code))
            {
                //添加系統訊息
                ModelState.AddModelError(nameof(updateRegionRequest.Code), "Cannot be null or white space");
            };
            if (string.IsNullOrWhiteSpace(updateRegionRequest.Name))
            {
                //添加系統訊息
                ModelState.AddModelError(nameof(updateRegionRequest.Name), "Cannot be null or white space");
            };

            //檢查數字
            if (updateRegionRequest.Area <= 0)
            {
                ModelState.AddModelError(nameof(updateRegionRequest.Area),
                    $"{nameof(updateRegionRequest.Area)} cannot be less than or equal to zero");
            };
            if (updateRegionRequest.Population <= 0)
            {
                ModelState.AddModelError(nameof(updateRegionRequest.Population),
                    $"{nameof(updateRegionRequest.Population)} cannot be less than or equal to zero");
            };

            //最終返回
            if (ModelState.ErrorCount > 0) return false;
            return true;
        }
        #endregion
    }
}
