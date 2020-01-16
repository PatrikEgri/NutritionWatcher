using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NutritionWatcher.Models;
using System.Data.Entity;

namespace NutritionWatcher.Controllers
{
    public class StatisticsController : Controller
    {
        ApplicationDbContext _context = new ApplicationDbContext();

        // GET: Statistics
        public ActionResult CalorieConsumption()
        {
            int id = (int)Session["User"];
            var viewModel = _context.CalorieViewModels.Include(x => x.Food).Include(x => x.Consumption).Include(x => x.Consumption.User).ToList().FindAll(x => x.Consumption.User.Id.Equals(id));
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
            int id = (int)Session["User"];
            var viewModel = _context.CalorieViewModels.Include(x => x.Food).Include(x => x.Consumption).Include(x => x.Consumption.User).ToList().FindAll(x => x.Consumption.User.Id.Equals(id));
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
            int id = (int)Session["User"];
            var viewModel = _context.CalorieViewModels.Include(x => x.Food).Include(x => x.Consumption).Include(x => x.Consumption.User).ToList().FindAll(x => x.Consumption.User.Id.Equals(id));
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
            int id = (int)Session["User"];
            var viewModel = _context.CalorieViewModels.Include(x => x.Food).Include(x => x.Consumption).Include(x => x.Consumption.User).ToList().FindAll(x => x.Consumption.User.Id.Equals(id));
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
            int id = (int)Session["User"];
            var viewModel = _context.Consumptions.Include(x => x.User).ToList().FindAll(x => x.User.Id.Equals(id));
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
            int id = (int)Session["User"];
            SummaryViewModel viewModel = new SummaryViewModel
            {
                CalorieViewModels = _context.CalorieViewModels.Include(x => x.Food).Include(x => x.Consumption).Include(x => x.Consumption.User).ToList().FindAll(x => x.Consumption.User.Id.Equals(id)),
                ConsumptionModels = _context.Consumptions.Include(x => x.User).ToList().FindAll(x => x.User.Id.Equals(id))
            }; 
        
            if (viewModel.CalorieViewModels != null && viewModel.ConsumptionModels != null)
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

        public ActionResult SummaryByDate(string date)
        {
            int id = (int)Session["User"];
            SummaryViewModel vm = new SummaryViewModel
            {
                CalorieViewModels = _context.CalorieViewModels.Include(x => x.Food).Include(x => x.Consumption).Include(x => x.Consumption.User).ToList().FindAll(x => x.Consumption.User.Id.Equals(id)),
                ConsumptionModels = _context.Consumptions.Include(x => x.User).ToList().FindAll(x => x.User.Id.Equals(id))
            };

            if (vm.CalorieViewModels != null && vm.ConsumptionModels != null)
            {
                ViewBag.Error = null;
                return View(vm);
            }
            else
            {
                ViewBag.Error = "Adatbázis hiba történt!";
                return RedirectToAction("Index", "Home");
            }
        }
    }
}