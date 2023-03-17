using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASPAPI.Models.Domain
{
    public class User
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [NotMapped] //使用這個才會忽略沒有primary key
        public List<string> Roles { get; set; } //單純裝UserRole的陣列，如果前端有需要的話
        //關聯表
        public List<User_Role> UserRoles { get; set; }

    }
}
