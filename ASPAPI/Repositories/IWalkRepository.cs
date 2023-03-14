using ASPAPI.Models.Domain;

namespace ASPAPI.Repositories
{
    public interface IWalkRepository
    {
        Task<IEnumerable<Walk>> GetAllAsync();
        Task<Walk> GetByIdAsync(Guid id);
        Task<Walk> AddAsync(Walk walk);
        Task<Walk> UpdateAsync(Guid id, Models.DTOs.UpdateWalkRequest updateWalkRequest);
        Task<Walk> DeleteAsync(Guid id);
    }
}
