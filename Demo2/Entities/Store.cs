using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demo2.Entities
{
    [Table("Store")]
    public class Store
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        [StringLength(50)]
        public string Address { get; set; }
        [Required]
        public DateTime OpenTime { get; set; }
        [Required]
        public DateTime CloseTime { get; set; }
    }
}
