using la_mia_pizzeria_static.Models.Database_Models;

namespace la_mia_pizzeria_static.Models
{
    public class PizzaFormModel
    {
        public Pizza Pizza { get; set; }
        public List<Category>? Categories { get; set; }
    }
}
