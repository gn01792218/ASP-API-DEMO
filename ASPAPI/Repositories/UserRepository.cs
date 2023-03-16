using ASPAPI.Models.Domain;

namespace ASPAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        //暫時的假資料
        private List<User> Users = new List<User>()
        {
            new User(){
                Id =Guid.NewGuid(),FirstName = "AAA",LastName = "GG",EmailAddress="AAA@gmail.com",
                UserName = "AG", Password="123",Roles =new List<string> {"reader"}
            },
            new User(){
                Id =Guid.NewGuid(),FirstName = "BBB",LastName = "GG",EmailAddress="BBB@gmail.com",
                UserName = "BG", Password="123",Roles =new List<string> {"reader","writer"}
            },
        };
        public async Task<User> GetUserAsync(string username, string password)
        {
            //驗證使用者
            //使用 StringComparison.InvariantCultureIgnoreCase 忽略大小寫
            var user =Users.Find(user => user.UserName.Equals(username,StringComparison.InvariantCultureIgnoreCase) && user.Password == password);
            return user;
        }
    }
}
