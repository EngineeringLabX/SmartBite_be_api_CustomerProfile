using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartBite.CustomerProfile.Domain.Entities
{
    [Table("DietPatterns", Schema = "Common")]
    public class DietPattern : BaseEntity
    {
        [Key]
        public int DietPatternId { get; set; }
        public required string DietPatternName { get; set; }
        public required string DietPatternDescription { get; set; }
    }
}
