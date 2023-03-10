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
        public IActionResult GetAllRegions() 
        {
            var regions = _regionRepository.GetAll();
            return Ok(regions);  //回傳一個 200 ok 的回應
        }
    }
}
