using NutritionWatcher.Models;
using System.Web.Mvc;
using System.Collections.Generic;

namespace NutritionWatcher.Controllers
{
    public class ConsumptionController : Controller
    {
        DataBaseHandler db = new DataBaseHandler();

        // GET: Consumption
        public ActionResult Insert()
        {
            
            ViewBag.Error = null;
            return View(new ConsumptionModel());
            
        }

        public ActionResult InsertDataBase(ConsumptionModel consumption)
        {
            if (!ModelState.IsValid) return View("Insert", consumption);

            if (db.InsertConsumption(consumption, (int)Session["User"]))
            {
                ViewBag.Error = null;
                return RedirectToAction("ShowConsumptions");
            }
            else
            {
                ViewBag.Error = "Adatbázis hiba történt!";
                return View("ShowConsumptions");
            }
        }

        public ActionResult Update(int id)
        {
            ConsumptionModel consumption = db.GetConsumptionById(id);
            if (consumption != null)
            {
                ViewBag.Error = null;
                return View(consumption);
            }
            else
            {
                ViewBag.Error = "Adatbázis hiba történt!";
                return RedirectToAction("ShowConsumption", new { id = id});
            }
        }

        public ActionResult UpdateDataBase(ConsumptionModel consumption)
        {
            if (!ModelState.IsValid) return View("Update", consumption);

            if (db.UpdateConsumption(consumption))
            {
                ViewBag.Error = null;
                return RedirectToAction("ShowConsumptions");
            }
            else
            {
                ViewBag.Error = "Adatbázis hiba történt!";
                return View("Update", new { id = consumption.Id});
            }
        }

        public ActionResult Delete(int id)
        {
            ConsumptionModel consumption = db.GetConsumptionById(id);

            if (consumption != null)
            {
                ViewBag.Error = null;
                return View(consumption);
            }
            else
            {
                ViewBag.Error = "Adatbázis hiba történt!";
                return RedirectToAction("ShowConsumption", new { id = id});
            }
        }

        public RedirectToRouteResult DeleteDataBase(ConsumptionModel consumption)
        {
            if (db.DeleteConsumption(consumption))
            {
                ViewBag.Error = null;
                return RedirectToAction("ShowConsumptions");
            }
            else
            {
                ViewBag.Error = "Adatbázis hiba történt!";
                return RedirectToAction("ShowConsumption", new { id = consumption.Id});
            }
        }

        public ActionResult ShowConsumptions()
        {
            List<ConsumptionModel> consumptions = db.GetConsumptions((int)Session["User"]);
            if (consumptions != null)
            {
                return View(consumptions);
            }
            else
            {
                return View();
            }
        }

        public ActionResult ShowConsumption(int id)
        {
            ConsumptionModel consumption = db.GetConsumptionById(id);

            if (consumption != null)
            {
                ViewBag.Error = null;
                return View(consumption);
            }
            else
            {
                ViewBag.Error = "Adatbázis hiba történt!";
                return RedirectToAction("ShowConsumptions");
            }
        }

        public ActionResult Assignment()
        {
            ConsumptionAssignmentViewModel viewModel = new ConsumptionAssignmentViewModel
            {
                Foods = db.GetFoods(),
                Consumptions = db.GetConsumptions((int)Session["User"]),
                Gramm = 0
            };

            if (viewModel.Foods == null || viewModel.Consumptions == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(viewModel);
            }
        }

        public ActionResult AssignmentDataBase(ConsumptionAssignmentViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Consumptions = db.GetConsumptions((int)Session["User"]);
                vm.Foods = db.GetFoods();
                return View("Assignment", vm);
            }
            
            if (db.AssignConsumption(vm))
            {
                ViewBag.Error = null;
                return RedirectToAction("ShowConsumptions");
            }
            else
            {
                ViewBag.Error = "Adatbázis hiba történt!";
                return RedirectToAction("ShowConsumptions");
            }
            
        }

        public ActionResult ShowAssignments(int id)
        {
            List<CalorieViewModel> vm = db.GetCalorieViewModelsByConsumptionId((int)Session["User"], id);

            if (vm != null)
            {
                ViewBag.Error = null;
                return View(vm);
            }
            else
            {
                ViewBag.Error = "Adatbázis hiba történt!";
                return RedirectToAction("ShowConsumption", new { id = id});
            }
        }

        public RedirectToRouteResult DeleteAssignment(int assignId, int consumptionId)
        {
            if (db.DeleteAssignment(assignId))
            {
                ViewBag.Error = null;
                return RedirectToAction("ShowAssignments", new { id = consumptionId });
            }
            else
            {
                ViewBag.Error = "Adatbázis hiba történt!";
                return RedirectToAction("ShowAssignments", new { id = consumptionId });
            }
        }
    }
}