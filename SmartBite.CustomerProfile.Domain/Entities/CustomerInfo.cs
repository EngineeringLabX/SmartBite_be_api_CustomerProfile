using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartBite.CustomerProfile.Domain.Entities
{
    [Table("CustomerInfo", Schema = "CustomerProfile")]
    public class CustomerInfo : BaseEntity
    {
        [Key]
        public int CustomerId { get; set; }

        public int UserId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerPhone { get; set; }
        public int AddressId { get; set; }
    }
}
