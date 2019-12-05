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

        public RedirectToRouteResult InsertDataBase(ConsumptionModel consumption)
        {
            if (db.InsertConsumption(consumption, (int)Session["User"]))
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

        public RedirectToRouteResult UpdateDataBase(ConsumptionModel consumption)
        {
            if (db.UpdateConsumption(consumption))
            {
                ViewBag.Error = null;
                return RedirectToAction("ShowConsumptions");
            }
            else
            {
                ViewBag.Error = "Adatbázis hiba történt!";
                return RedirectToAction("Update", new { id = consumption.Id});
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
            if (Session["User"] != null)
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
            else
            {
                return HttpNotFound();
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
            if (vm.FoodId > 0 && vm.ConsumptionId > 0)
            {
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
            else
            {
                return HttpNotFound();
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