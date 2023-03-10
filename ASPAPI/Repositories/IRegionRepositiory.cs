using ASPAPI.Models.Domain;

namespace ASPAPI.Repositories
{
    public interface IRegionRepositiory
    {
        //取得所有清單的方法
        IEnumerable<Region> GetAll();
    }
}
