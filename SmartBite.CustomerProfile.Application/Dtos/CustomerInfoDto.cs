namespace SmartBite.CustomerProfile.Application.Dtos
{
    public record CustomerInfoDto
    {
        public int CustomerId { get; set; }
        public int UserId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerPhone { get; set; }
        public int AddressId { get; set; }
    }
}
