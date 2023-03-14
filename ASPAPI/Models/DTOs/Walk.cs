using ASPAPI.Models.Domain;

namespace ASPAPI.Models.DTOs
{
    public class Walk
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Length { get; set; }
        public Guid RegionId { get; set; }
        public Guid WalkDifficultyId { get; set; }

        //Navigation Properties
        public Region Region { get; set; }
        public WalkDifficulty WalkDifficulty { get; set; }

        //轉換方法
        public Region toRegionDTO(Domain.Region region)
        {
            var DTO = new Region() 
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Population = region.Population,
            };
            return DTO;
        }
        public WalkDifficulty toWalkDifficultyDTO(Domain.WalkDifficulty w)
        {
            var DTO = new WalkDifficulty()
            {
                Id = w.Id,
                Code = w.Code,
            };
            return DTO;
        }
    }
}
