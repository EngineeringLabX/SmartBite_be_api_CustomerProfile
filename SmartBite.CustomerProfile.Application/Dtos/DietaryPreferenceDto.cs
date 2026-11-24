namespace SmartBite.CustomerProfile.Application.Dtos
{
    public record DietaryPreferenceDto
    {
        public int DietaryPreferenceId { get; set; }
        public int CustomerId { get; set; }
        public int DietPatternId { get; set; }
    }
}
