namespace ASPAPI.Models.DTOs
{
    public class UpdateRegionRequest
    {
        //專門用來定義post方法時要接收的資料格式
        //我們希望格式為除了id以外
        public string Code { get; set; }
        public string Name { get; set; }
        public double Area { get; set; }
        public double Lat { get; set; }
        public double Long { get; set; }
        public long Population { get; set; }
    }
}
