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

        public IActionResult DetailsUser(int id)
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
                    return View("DetailsUser", foundedElement);
                }
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View("Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Pizza newPizza)
        {
            if (newPizza.Image == null)
            {
                newPizza.Image = "/img/default.jpg";
            }
            if (!ModelState.IsValid)
            {
                return View("Create", newPizza);
            }

            using (PizzaContext db = new PizzaContext())
            {
                db.Pizzas.Add(newPizza);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
        }


        [HttpGet]
        public IActionResult Update(int id)
        {
            using (PizzaContext db = new PizzaContext())
            {
                Pizza? pizzaToEdit = db.Pizzas.Where(pizza => pizza.Id == id).FirstOrDefault();

                if (pizzaToEdit == null)
                {
                    return NotFound("La pizza che vuoi modificare non è stata trovata");
                }
                else
                {
                    return View("Update", pizzaToEdit);
                }
            }
        }

    }
}
