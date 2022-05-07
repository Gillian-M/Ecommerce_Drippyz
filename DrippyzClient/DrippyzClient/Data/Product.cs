﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Drippyz.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ProductCategory
    {
        Pint = 1,
        Minipint,
        Bar
    }
    public class Product
    {
        //[Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }

        public string ImageURL { get; set; }

        public ProductCategory ProductCategory { get; set; }
        

        public override string ToString()
        {
            return String.Format("Id: {0} | Name: {1} | Description: {2} | Price: {3} ImageUR: {4} | Product Type: {5}", Id, Name, Description, Price, ImageURL, ProductCategory);
            
        }

    }
}
