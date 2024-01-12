using System.ComponentModel.DataAnnotations;

namespace WebStoreApp.Models.Brand
{
    public class BrandCreateVM
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        [Display(Name ="Brand name")]
        public string BrandName { get; set; } = null!;
    }
}
