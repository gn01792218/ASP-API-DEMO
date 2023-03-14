using ASPAPI.Models.Domain;

namespace ASPAPI.Repositories
{
    public interface IWalkDifficultyRepository
    {
        Task<IEnumerable<WalkDifficulty>> GetAllAsync();
        Task<WalkDifficulty> GetByIdAsync(Guid id);
        Task<WalkDifficulty> AddAsync(Models.DTOs.AddWalkDifficultyRequest addWalkDifficulty);
        Task<WalkDifficulty> UpdateAsync(Guid id, Models.DTOs.UpdateWalkDifficultyRequest updateWalkDifficulty);
        Task<WalkDifficulty> DeleteAsync(Guid id);
    }
}
