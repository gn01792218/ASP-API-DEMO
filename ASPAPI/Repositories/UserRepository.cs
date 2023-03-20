using ASPAPI.Data;
using ASPAPI.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace ASPAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _db;
        public UserRepository(DataContext db)
        {
            _db = db;
        }
        public async Task<User> GetUserAsync(string username, string password)
        {
            //獲取使用者
            var user =await _db.Users.FirstOrDefaultAsync(user => user.UserName.ToLower() == username.ToLower() && user.Password == password);
            if (user == null) return null;

            //獲取Roles
            var userRoles = await _db.User_Roles.Where(x => x.UserId == user.Id).ToListAsync();
            
            //如果前端需要只有字串的陣列
            //把User_Roles表格，轉換成單純字串陣列給Roles
            if (userRoles.Any())  //檢查userRoles是否為空
            {
                user.Roles = new List<string>();
                foreach (var userRole in userRoles)
                {
                    var role = await _db.Roles.FirstOrDefaultAsync(r => r.Id == userRole.RoleId);
                    //假如有role,加進User中
                    if (role != null)
                    {
                        user.Roles.Add(role.Name);
                    };
                }
            }

            user.Password = null;  //記得把密碼設為null，不要傳出去
            return user;
        }
    }
}
