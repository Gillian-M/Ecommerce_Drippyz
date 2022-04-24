using Drippyz.Data;
using Drippyz.Data.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Drippyz.Models
{
    public class Product: IEntityBase
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }

        public string ImageURL { get; set; }

        public ProductCategory ProductCategory { get; set; }
        //Relationship with Store
        //Store Id Foreign key 

        public int StoreId { get; set; }
        [ForeignKey("StoreId")]
        public Store Store { get; set; }

    }
}
