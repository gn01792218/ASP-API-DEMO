using ASPAPI.Models.Domain;

namespace ASPAPI.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserAsync(string username, string password);
    }
}
