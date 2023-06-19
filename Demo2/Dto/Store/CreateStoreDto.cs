using System.ComponentModel.DataAnnotations;

namespace Demo2.Dto.Store
{
    public class CreateStoreDto
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Tên không được bỏ trống")] 
        public string Name { get; set; }
        public DateTime OpenTime { get; set; }
        public DateTime CloseTime { get; set; }
    }
}
