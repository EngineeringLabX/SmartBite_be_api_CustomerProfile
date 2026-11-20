namespace SmartBite.CustomerProfile.Application.Dtos
{
    public record DietPatternDto
    {
        public int DietPatternId { get; set; }
        public required string DietPatternName { get; set; }
        public required string DietPatternDescription { get; set; }
    }
}
