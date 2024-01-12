using System.ComponentModel.DataAnnotations;

namespace WebStoreApp.Models.Brand
{
    public class BrandIndexVM
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Brand Name")]
        public string BrandName { get; set; } = null!;
    }
}
