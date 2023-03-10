using ASPAPI.Data;
using ASPAPI.Models.Domain;

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
    }
}
