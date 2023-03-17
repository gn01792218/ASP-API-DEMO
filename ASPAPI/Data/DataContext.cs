using ASPAPI.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace ASPAPI.Data
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options):base(options)
        { 
        }

        //定義表的關聯
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User_Role>()
                .HasOne(x => x.Role) //Role
                .WithMany(y => y.UserRoles) //User_Role的Join表格  
                .HasForeignKey(x => x.RoleId); //用RoleId來獲取
            modelBuilder.Entity<User_Role>()
                .HasOne(x => x.User) //User
                .WithMany(y => y.UserRoles) //User_Role的Join表格  
                .HasForeignKey(x => x.UserId); //用UserId來獲取
        }

        //建立table
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User_Role> User_Roles { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
        public DbSet<WalkDifficulty> WalkDifficulty { get; set; }
    }
}
