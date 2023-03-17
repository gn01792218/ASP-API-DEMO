using ASPAPI.Models.Domain;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ASPAPI.Repositories
{
    public class TokenHandlerRepository : ITokenHandlerRepository
    {
        //1.依賴注入appsetting的Config檔案
        private readonly IConfiguration _configuration;
        public TokenHandlerRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public Task<string> CreateTokenAsync(User user)
        {

            //1.用Claim來製作Token的內容
            var claims = new List<Claim>();
            //Claims建構子(指定要宣告的型態,要做成Token的value)
            claims.Add(new Claim(ClaimTypes.GivenName,user.FirstName));
            claims.Add(new Claim(ClaimTypes.Surname, user.LastName));
            claims.Add(new Claim(ClaimTypes.Email, user.EmailAddress));
            user.Roles.ForEach(role =>
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            });

            //2.取得appsetting中設置的私鑰key
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            //3.製作Token
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256); //用私鑰作一個證書
            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),  //Token可用時間
                signingCredentials:credentials);
            var tokenString = Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));
            return tokenString;
        }
    }
}
