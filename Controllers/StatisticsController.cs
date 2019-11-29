using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NutritionWatcher.Models;

namespace NutritionWatcher.Controllers
{
    public class StatisticsController : Controller
    {
        DataBaseHandler db = new DataBaseHandler();
        // GET: Statistics
        public ActionResult CalorieConsumption()
        {
            var viewModel = db.GetCalorieViewModels(db.GetUserById((int)Session["User"]));
            if (viewModel != null)
            {
                ViewBag.Error = null;
                return View(viewModel);
            }
            else
            {
                ViewBag.Error = "Adatbázis hiba történt!";
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult ProteinConsumption()
        {
            var viewModel = db.GetCalorieViewModels(db.GetUserById((int)Session["User"]));
            if (viewModel != null)
            {
                ViewBag.Error = null;
                return View(viewModel);
            }
            else
            {
                ViewBag.Error = "Adatbázis hiba történt!";
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult FatConsumption()
        {
            var viewModel = db.GetCalorieViewModels(db.GetUserById((int)Session["User"]));
            if (viewModel != null)
            {
                ViewBag.Error = null;
                return View(viewModel);
            }
            else
            {
                ViewBag.Error = "Adatbázis hiba történt!";
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult HydrocarbonateConsumption()
        {
            var viewModel = db.GetCalorieViewModels(db.GetUserById((int)Session["User"]));
            if (viewModel != null)
            {
                ViewBag.Error = null;
                return View(viewModel);
            }
            else
            {
                ViewBag.Error = "Adatbázis hiba történt!";
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult MealCount()
        {
            var viewModel = db.GetConsumptions((int)Session["User"]);
            if (viewModel != null)
            {
                ViewBag.Error = null;
                return View(viewModel);
            }
            else
            {
                ViewBag.Error = "Adatbázis hiba történt!";
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Summary()
        {
            SummaryViewModel viewModel = new SummaryViewModel
            {
                CalorieViewModels = db.GetCalorieViewModels(db.GetUserById((int)Session["User"])),
                ConsumptionModels = db.GetConsumptions((int)Session["User"])
            }; 
        
            if (viewModel != null)
            {
                ViewBag.Error = null;
                return View(viewModel);
            }
            else
            {
                ViewBag.Error = "Adatbázis hiba történt!";
                return RedirectToAction("Index", "Home");
            }
        }
    }
}