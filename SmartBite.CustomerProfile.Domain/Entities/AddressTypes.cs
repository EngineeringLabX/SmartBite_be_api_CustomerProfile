using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartBite.CustomerProfile.Domain.Entities
{
    [Table("AddressTypes", Schema = "Common")]
    public class AddressTypes : BaseEntity
    {
        [Key]
        public int AddressTypeId { get; set; }

        public required string AddressType { get; set; }
    }
}
