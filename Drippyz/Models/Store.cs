using System.ComponentModel.DataAnnotations;

namespace Drippyz.Models
{
    //inherit from Ientitybase
    public class Store
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Company Logo")]
        [Required(ErrorMessage = "Store Logo is required")]
        public string Glyph { get; set; }


        [Display(Name = "Company Name")]
        [Required(ErrorMessage = "Store Name is required")]
        public string Name { get; set; }

        [Display(Name = "About")]
        [Required(ErrorMessage = "Store Description is required")]
        public string Description { get; set; }

        //Relationship (Store can have a list of products)
        public List<Product> Products { get; set; }

    }
}