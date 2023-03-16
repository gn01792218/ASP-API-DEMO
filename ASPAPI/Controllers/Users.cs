using ASPAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASPAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Users : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenHandlerRepository _tokenHandlerRepository;
        public Users(IUserRepository userRepository, ITokenHandlerRepository tokenHandlerRepository)
        {
            _userRepository = userRepository;
            _tokenHandlerRepository = tokenHandlerRepository;
        }
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginAsync(Models.DTOs.LoginRequest loginRequest)
        {
            //1.簡單驗證Request資料(有裝套件的話這裡不用寫任何東西)
            //2.查詢資料庫中有無此使用者
            var user = await _userRepository.GetUserAsync(loginRequest.UserName,loginRequest.Password);
            if (user == null) return BadRequest("Username or password is incorrect.");
            //產生JWT給使用者
            var token = await _tokenHandlerRepository.CreateTokenAsync(user);
            return Ok(token);
        }
    }
}
