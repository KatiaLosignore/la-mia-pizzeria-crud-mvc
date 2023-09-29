using la_mia_pizzeria_static.Database;
using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Mvc;

namespace la_mia_pizzeria_static.Controllers
{
    public class PizzaController : Controller
    {
        public IActionResult Index()
        {
            using (PizzaContext db = new PizzaContext())
            {
                List<Pizza> pizzas = db.Pizzas.ToList<Pizza>();

                return View("Index", pizzas);
            }


        }

        public IActionResult Details(int id)
        {
            using (PizzaContext db = new PizzaContext())
            {
                Pizza? foundedElement = db.Pizzas.Where(element => element.Id == id).FirstOrDefault();

                if (foundedElement == null)
                {
                    return NotFound($"La pizza con {id} non è stata trovata!");
                }
                else
                {
                    return View("Details", foundedElement);
                }
            }
        }

        public IActionResult UserIndex()
        {
            using (PizzaContext db = new PizzaContext())
            {
                List<Pizza> pizzas = db.Pizzas.ToList<Pizza>();

                return View("UserIndex", pizzas);
            }
        }
    }
}
