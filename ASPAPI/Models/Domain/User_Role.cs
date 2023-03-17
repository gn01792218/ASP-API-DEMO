namespace ASPAPI.Models.Domain
{
    public class User_Role
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }

        //關聯表
        public User User { get; set; }
        public Role Role { get; set; }
    }
}
