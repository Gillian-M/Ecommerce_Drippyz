using Drippyz.Data;
using Drippyz.Data.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Drippyz.Models
{
    //View Model 
    public class NewProductVM
    {
        public int Id { get; set; }

        [Display(Name = "Product Name")]
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Display(Name = "Product Description")]
        [Required(ErrorMessage = "Product Description is required")]
        public string Description { get; set; }

        [Display(Name = "Product Price")]
        [Required(ErrorMessage = "Price is required")]
        public double Price { get; set; }
        [Display(Name = "Image URL")]
        [Required(ErrorMessage = "Image URL is required")]

        public string ImageURL { get; set; }
        [Display(Name = "Product Category Name")]
        [Required(ErrorMessage = "Product Category is required")]

        public ProductCategory ProductCategory { get; set; }
        //Relationship with Store
        [Display(Name = "Select a Store")]
        [Required(ErrorMessage = "Store is required")]
        public int StoreId { get; set; }
     

    }
}


