using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demo2.Entities
{
    [Table("StoreProvider")]
    public class StoreProvider
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int StoreId { get; set; }
        public int ProviderId { get; set; }
        [Range(0, 100)]
        public double IntimacyLevel { get; set; }
    }
}
