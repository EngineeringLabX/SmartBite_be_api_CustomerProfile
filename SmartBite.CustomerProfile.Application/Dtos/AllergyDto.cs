namespace SmartBite.CustomerProfile.Application.Dtos
{
    public record AllergyDto
    {
        public int AllergyId { get; set; }
        public required string AllergyName { get; set; }
    }
}
