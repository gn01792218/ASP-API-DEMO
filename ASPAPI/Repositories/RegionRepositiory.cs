using ASPAPI.Data;
using ASPAPI.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace ASPAPI.Repositories
{
    public class RegionRepositiory:IRegionRepositiory
    {
        //依賴注入db
        private readonly DataContext _db;
        public RegionRepositiory(DataContext db)
        {
            _db = db;
        }

        //取得所有清單的方法
        public IEnumerable<Region> GetAll()
        {
            return _db.Regions.ToList();
        }
        //取得所有清單的異步方法
        public async Task<IEnumerable<Region>> GetAllAsync()
        {
            return await _db.Regions.ToListAsync();
        }

        //依據Id獲取Region
        public async Task<Region> GetRegionByIdAsync(Guid id) 
        {
            return await _db.Regions.FirstOrDefaultAsync(r => r.Id == id);
        }

        //新增Region
        public async Task<Region> AddRegionAsync(Region region)
        {
            //id由後端給
            region.Id = Guid.NewGuid();
            await _db.Regions.AddAsync(region);
            await _db.SaveChangesAsync();
            return region;
        }

        //刪除Region
        public async Task<Region> DeleteRegionAsync(Guid id)
        {
            var region =await _db.Regions.FirstOrDefaultAsync(r => r.Id == id);
            if(region != null)
            {
                _db.Regions.Remove(region);
                await _db.SaveChangesAsync();
            }
  
            return region;
        }
    }
}
