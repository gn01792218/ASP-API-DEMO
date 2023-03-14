namespace ASPAPI.Models.DTOs
{
    public class AddWalkRequest
    {
        public string Name { get; set; }
        public double Length { get; set; }

        //以下兩個ID由前端經由後端給的ID資料來設定
        public Guid RegionId { get; set; }
        public Guid WalkDifficultyId { get; set; }
    }
}
