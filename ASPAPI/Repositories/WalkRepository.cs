using ASPAPI.Data;
using ASPAPI.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace ASPAPI.Repositories
{
    public class WalkRepository : IWalkRepository
    {
        private readonly DataContext _db;
        public WalkRepository(DataContext db) 
        { 
            _db = db;
        }
        public async Task<IEnumerable<Walk>> GetAllAsync()
        {
            //使用Include獲取關聯資料表
            return await _db.Walks
                .Include(w => w.Region)
                .Include(w => w.WalkDifficulty)
                .ToListAsync();
        }
        public async Task<Walk> GetByIdAsync(Guid id)
        {
            return await _db.Walks
                .Include(w => w.Region)
                .Include(w => w.WalkDifficulty)
                .FirstOrDefaultAsync(w => w.Id == id);
        }

        public async Task<Walk> AddAsync(Walk walk)
        {
            if (walk == null) return null;
            walk.Id = Guid.NewGuid();
           await _db.Walks.AddAsync(walk);
            await _db.SaveChangesAsync();
           return walk;
        }

        public async Task<Walk> UpdateAsync(Guid id,Models.DTOs.UpdateWalkRequest updateWalkRequest)
        {
            if (id == null) return null;
            var walk = await _db.Walks.FirstOrDefaultAsync(w => w.Id == id);
            if(walk != null)
            {
                walk.Name = updateWalkRequest.Name;
                walk.Length = updateWalkRequest.Length;
                walk.WalkDifficultyId = updateWalkRequest.WalkDifficultyId;
                walk.RegionId = updateWalkRequest.RegionId;
                await _db.SaveChangesAsync();
            }
            return walk;
        }

        public async Task<Walk> DeleteAsync(Guid id)
        {
            if (id == null) return null;
            var walk = _db.Walks.FirstOrDefault(w => w.Id == id);
            if(walk != null)
            {
                _db.Remove(walk);
                await _db.SaveChangesAsync();
            }
            return walk;
        }
    }
}
