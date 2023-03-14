using ASPAPI.Data;
using ASPAPI.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.Xml;

namespace ASPAPI.Repositories
{
    public class WalkDifficultyRepository : IWalkDifficultyRepository
    {
        //依賴注入db
        private readonly DataContext _db;
        public WalkDifficultyRepository(DataContext db) 
        {
            _db = db;
        }
        //API
        public async Task<IEnumerable<WalkDifficulty>> GetAllAsync()
        {
            return await _db.WalkDifficulty.ToListAsync();
        }
        public async Task<WalkDifficulty> GetByIdAsync(Guid id)
        {
            var walkDifficulty = await _db.WalkDifficulty.FirstOrDefaultAsync(x => x.Id == id);
            return walkDifficulty;
        }

        public async Task<WalkDifficulty> AddAsync(Models.DTOs.AddWalkDifficultyRequest addWalkDifficulty)
        {
            if (addWalkDifficulty == null) return null;
            var walkDifficulty = new WalkDifficulty()
            {
                Id = Guid.NewGuid(),
                Code = addWalkDifficulty.Code,
            };
            await _db.WalkDifficulty.AddAsync(walkDifficulty);
            await _db.SaveChangesAsync();
            return walkDifficulty;
        }

        public async Task<WalkDifficulty> UpdateAsync(Guid id, Models.DTOs.UpdateWalkDifficultyRequest updateWalkDifficulty)
        {
            var walkDifficulty = await _db.WalkDifficulty.FirstOrDefaultAsync(w => w.Id == id);
            if (walkDifficulty != null)
            {
                walkDifficulty.Code = updateWalkDifficulty.Code;
                await _db.SaveChangesAsync();
            }
            return walkDifficulty;
        }
        public async Task<WalkDifficulty> DeleteAsync(Guid id)
        {
            var walkDifficulty = await _db.WalkDifficulty.FirstOrDefaultAsync(w => w.Id == id);
            if (walkDifficulty != null)
            {
                _db.Remove(walkDifficulty);
                await _db.SaveChangesAsync();
            }
            return walkDifficulty;
        }
    }
}
