using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NutritionWatcher.Models;

namespace NutritionWatcher.Controllers
{
    public class FoodController : Controller
    {
        DataBaseHandler db = new DataBaseHandler();

        /// <summary>
        /// This controller waits for a FoodModel as parameter. If it doesn't take the parameter, 
        /// then it returns with a View, where we can create the new food. If it takes the parameter,
        /// then it inserts the food to the database and redirects to the view, where we can see all the foods.
        /// </summary>
        /// <param name="food"></param>
        /// <returns></returns>
        public ActionResult Insert()
        {           
            ViewBag.Error = null;
            return View(new FoodModel());            
        }

        public ActionResult InsertDataBase(FoodModel food)
        {
            if (!ModelState.IsValid) return View("Insert", food);

            if (db.InsertFood(food))
            {
                return RedirectToAction("ViewFoods");
            }
            else
            {
                ViewBag.Error = "Adatbázis hiba történt!";
                return RedirectToAction("Insert");
            }
        }

        /// <summary>
        /// This controller waits for an Id and a FoodModel as parameters. If it takes the Id, but it
        /// doesn't take the FoodModel, then it redirects to the View, where we can see and modify the
        /// data of the food that has this Id. If it takes both of the parameters, then it updates the
        /// food in the database and redirects to the View, where we can see all the foods.
        /// </summary>
        /// <param name="foodId"></param>
        /// <param name="food"></param>
        /// <returns></returns>
        public ViewResult Update(int id)
        {            
            FoodModel food = db.GetFoodById(id);

            if (food == null)
            {
                ViewBag.Error = "Adatbázis hiba történt!";
                return View("ViewFoods");
            }
            else
            {
                ViewBag.Error = null;
                return View(food);
            }
        }

        public ActionResult UpdateDataBase(FoodModel food)
        {
            if (!ModelState.IsValid) return View("Update", food);

            if (db.UpdateFood(food))
            {
                ViewBag.Error = null;
                return RedirectToAction("ViewFoods");
            }
            else
            {
                ViewBag.Error = "Adatbázis hiba történt!";
                return View("ViewFoods");
            }
        }

        /// <summary>
        /// This controller waits for an Id and a FoodModel as parameters. If it takes the Id, but it
        /// doesn't take the FoodModel, then it redirects to the View that goes for sure we want to delete.
        /// If it takes both of the parameters, then it deletes the food from the database.
        /// </summary>
        /// <param name="foodId"></param>
        /// <param name="food"></param>
        /// <returns></returns>
        public ViewResult Delete(int id)
        {
            FoodModel food = db.GetFoodById(id);

            if (food == null)
            {
                ViewBag.Error = "Adatbázis hiba történt!";
                return View("ViewFoods");
            }
            else
            {
                ViewBag.Error = null;
                return View(food);
            }
        }

        public ActionResult DeleteDataBase(int id)
        {
            if (!db.GetUserById((int)Session["User"]).Permission.Equals("admin"))
            {
                ViewBag.Error = "A törléshez nincs megfelelő jogosultságod!";
                return View("ViewFoods");
            }

            if (db.DeleteFood(id))
            {
                ViewBag.Error = null;
                return RedirectToAction("ViewFoods");
            }
            else
            {
                ViewBag.Error = "Adatbázis hiba történt!";
                return View("ViewFoods");
            }
        }

        /// <summary>
        /// This controller returns with the View, where we can see all the foods.
        /// </summary>
        /// <returns></returns>
        public ViewResult ViewFoods()
        {
            return View(db.GetFoods());
        }

        public ActionResult ViewFood(int id)
        {
            FoodModel food = db.GetFoodById(id);
            if (food != null)
            {
                return View(food);
            }
            else
            {
                return HttpNotFound();
            }
        }
    }
}