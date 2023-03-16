using ASPAPI.Models.Domain;

namespace ASPAPI.Repositories
{
    public interface ITokenHandlerRepository
    {
        Task<string> CreateTokenAsync(User user);
    }
}
