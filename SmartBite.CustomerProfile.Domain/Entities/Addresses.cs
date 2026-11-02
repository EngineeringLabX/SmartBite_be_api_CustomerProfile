using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartBite.CustomerProfile.Domain.Entities
{
    [Table("Addresses", Schema = "Common")]
    public class Addresses : BaseEntity
    {
        [Key]
        public int AddressId { get; set; }
        public int AddressTypeId { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
    }
}
