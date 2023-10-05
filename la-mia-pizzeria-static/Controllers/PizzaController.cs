using la_mia_pizzeria_static.CustomLoggers;
using la_mia_pizzeria_static.Database;
using la_mia_pizzeria_static.Models;
using la_mia_pizzeria_static.Models.Database_Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace la_mia_pizzeria_static.Controllers
{
    public class PizzaController : Controller
    {

        private ICustomLogger _myLogger;
        private PizzaContext _myDatabase;

        public PizzaController(PizzaContext db, ICustomLogger logger)
        {
            _myLogger = logger;
            _myDatabase = db;
        }


        public IActionResult Index()
        {
            _myLogger.WriteLog("L'utente è arrivato sulla pagina Pizza > Index");


            List<Pizza> pizzas = _myDatabase.Pizzas.Include(pizza => pizza.Category).ToList<Pizza>();

            return View("Index", pizzas);

        }

        public IActionResult Details(int id)
        {
            Pizza? foundedElement = _myDatabase.Pizzas.Where(element => element.Id == id).Include(pizza => pizza.Category).FirstOrDefault();

            if (foundedElement == null)
            {
                return NotFound($"La pizza con {id} non è stata trovata!");
            }
            else
            {
                return View("Details", foundedElement);
            }

        }

 
        [HttpGet]
        public IActionResult Create()
        {
            List<Category> categories = _myDatabase.Categories.ToList();

            PizzaFormModel model =
                new PizzaFormModel { Pizza = new Pizza(), Categories = categories };

            return View("Create", model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PizzaFormModel newPizza)
        {
            if (newPizza.Pizza.Image == null)
            {
                newPizza.Pizza.Image = "/img/default.jpg";
            }
            if (!ModelState.IsValid)
            {
                List<Category> categories = _myDatabase.Categories.ToList();
                newPizza.Categories = categories;

                return View("Create", newPizza);
            }


            _myDatabase.Pizzas.Add(newPizza.Pizza);
            _myDatabase.SaveChanges();

            return RedirectToAction("Index");

        }


        [HttpGet]
        public IActionResult Update(int id)
        {
            
                Pizza? pizzaToEdit = _myDatabase.Pizzas.Where(pizza => pizza.Id == id).FirstOrDefault();

                if (pizzaToEdit == null)
                {
                    return NotFound("La pizza che vuoi modificare non è stata trovata");
                }
                else
                {
                    List<Category> categories = _myDatabase.Categories.ToList();

                        PizzaFormModel model
                        = new PizzaFormModel { Pizza = pizzaToEdit, Categories = categories };

                    return View("Update", model);   
                }
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, PizzaFormModel data)
        {
            if (!ModelState.IsValid)
            {
                List<Category> categories = _myDatabase.Categories.ToList();
                data.Categories = categories;
                return View("Update", data);
            }

                Pizza? pizzaToUpdate = _myDatabase.Pizzas.Where(pizza => pizza.Id == id).FirstOrDefault();

                if (pizzaToUpdate != null)
                {
                    pizzaToUpdate.Name = data.Pizza.Name;
                    pizzaToUpdate.Description = data.Pizza.Description;
                    pizzaToUpdate.Price = data.Pizza.Price;
                    pizzaToUpdate.Image = data.Pizza.Image;
                    pizzaToUpdate.CategoryId = data.Pizza.CategoryId;

                   _myDatabase.SaveChanges();

                    return RedirectToAction("Details", "Pizza", new { id = pizzaToUpdate.Id });
                }
                else
                {
                    return NotFound("Mi dispiace non è stata trovata la pizza da aggiornare");
                }
            

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            
                Pizza? pizzaToDelete = _myDatabase.Pizzas.Where(pizza => pizza.Id == id).FirstOrDefault();

                if (pizzaToDelete != null)
                {
                    _myDatabase.Pizzas.Remove(pizzaToDelete);
                    _myDatabase.SaveChanges();

                    return RedirectToAction("Index");
                }
                else
                {
                    return NotFound("La pizza da eliminare non è stata trovata!");
                }
            
        }

    }

}
