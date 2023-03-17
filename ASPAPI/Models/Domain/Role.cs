namespace ASPAPI.Models.Domain
{
    public class Role
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        //關聯表
        public List<User_Role> UserRoles { get; set; }
    }
}
