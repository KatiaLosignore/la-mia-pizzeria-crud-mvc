using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace la_mia_pizzeria_static.Models
{
    public class Pizza
    {
        public int Id { get; set; }

        [MaxLength(150)]
        public string Name { get; set; }

        [Column(TypeName = "text")]
        public string Description { get; set; }
        public float Price { get; set; }

        [MaxLength(500)]
        public string Image { get; set; }

        public Pizza(string name, string description, float price, string image)
        {
            this.Name = name;
            this.Description = description;
            this.Price = price;
            this.Image = image;
        }


    }
}
