using ASPAPI.Models.Domain;

namespace ASPAPI.Repositories
{
    public interface IRegionRepositiory
    {
        //取得所有清單的方法
        IEnumerable<Region> GetAll();

        //獲取所有清單的異步方法
        Task<IEnumerable<Region>> GetAllAsync();

        //依據Id獲取Region
        Task<Region> GetRegionByIdAsync(Guid id);
    }
}
