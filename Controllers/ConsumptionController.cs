using NutritionWatcher.Models;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace NutritionWatcher.Controllers
{
    public class ConsumptionController : Controller
    {
        ApplicationDbContext _context = new ApplicationDbContext();

        // GET: Consumption
        public ActionResult Insert()
        {
            
            ViewBag.Error = null;
            return View(new ConsumptionModel());
            
        }

        public ActionResult InsertDataBase(ConsumptionModel consumption)
        {
            if (!ModelState.IsValid) return View("Insert", consumption);

            int id = (int)Session["User"];
            UserModel user = _context.NWUsers.SingleOrDefault(x => x.Id.Equals(id));
            consumption.User = user;

            _context.Consumptions.Add(consumption);
            _context.SaveChanges();

            return RedirectToAction("ShowConsumptions");

            //if (db.InsertConsumption(consumption, (int)Session["User"]))
            //{
            //    ViewBag.Error = null;
            //    return RedirectToAction("ShowConsumptions");
            //}
            //else
            //{
            //    ViewBag.Error = "Adatbázis hiba történt!";
            //    return View("ShowConsumptions");
            //}
        }

        public ActionResult Update(int id)
        {
            //ConsumptionModel consumption = db.GetConsumptionById(id);
            ConsumptionModel consumption = _context.Consumptions.Include(x => x.User).SingleOrDefault(x => x.Id.Equals(id));
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

            ConsumptionModel conInDb = _context.Consumptions.SingleOrDefault(x => x.Id.Equals(consumption.Id));
            conInDb.Date = consumption.Date;
            conInDb.Time = consumption.Time;
            _context.SaveChanges();

            return RedirectToAction("ShowConsumptions");

            //if (db.UpdateConsumption(consumption))
            //{
            //    ViewBag.Error = null;
            //    return RedirectToAction("ShowConsumptions");
            //}
            //else
            //{
            //    ViewBag.Error = "Adatbázis hiba történt!";
            //    return View("Update", new { id = consumption.Id});
            //}
        }

        public ActionResult Delete(int id)
        {
            ConsumptionModel consumption = _context.Consumptions.SingleOrDefault(x => x.Id.Equals(id)); //db.GetConsumptionById(id);

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

        public RedirectToRouteResult DeleteDataBase(int id)
        {
            _context.Consumptions.Remove(_context.Consumptions.SingleOrDefault(x => x.Id.Equals(id)));
            _context.SaveChanges();

            return RedirectToAction("ShowConsumptions");

            //if (db.DeleteConsumption(consumption))
            //{
            //    ViewBag.Error = null;
            //    return RedirectToAction("ShowConsumptions");
            //}
            //else
            //{
            //    ViewBag.Error = "Adatbázis hiba történt!";
            //    return RedirectToAction("ShowConsumption", new { id = consumption.Id});
            //}
        }

        public ActionResult ShowConsumptions()
        {
            int id = (int)Session["User"];
            List<ConsumptionModel> consumptions = _context.Consumptions.Include(x => x.User).ToList().FindAll(x => x.User.Id.Equals(id)); //db.GetConsumptions((int)Session["User"]);
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
            ConsumptionModel consumption = _context.Consumptions.SingleOrDefault(x => x.Id.Equals(id)); //db.GetConsumptionById(id);

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
            int id = (int)Session["User"];
            ConsumptionAssignmentViewModel viewModel = new ConsumptionAssignmentViewModel
            {
                Foods = _context.Foods.ToList(), //db.GetFoods(),
                Consumptions = _context.Consumptions.Include(x => x.User).ToList().FindAll(x => x.User.Id.Equals(id)), //db.GetConsumptions((int)Session["User"]),
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
            int id = (int)Session["User"];
            if (!ModelState.IsValid)
            {
                vm.Consumptions = _context.Consumptions.Include(x => x.User).ToList().FindAll(x => x.User.Id.Equals(id)); //db.GetConsumptions((int)Session["User"]);
                vm.Foods = _context.Foods.ToList(); //db.GetFoods();
                return View("Assignment", vm);
            }

            _context.CalorieViewModels.Add(new CalorieViewModel
            {
                ConsumedGramms = vm.Gramm,
                Consumption = _context.Consumptions.SingleOrDefault(x => x.Id.Equals(vm.ConsumptionId)),
                Food = _context.Foods.SingleOrDefault(x => x.Id.Equals(vm.FoodId))
            });
            _context.SaveChanges();

            return RedirectToAction("ShowConsumptions");

            //if (db.AssignConsumption(vm))
            //{
            //    ViewBag.Error = null;
            //    return RedirectToAction("ShowConsumptions");
            //}
            //else
            //{
            //    ViewBag.Error = "Adatbázis hiba történt!";
            //    return RedirectToAction("ShowConsumptions");
            //}
        }

        public ActionResult ShowAssignments(int id)
        {
            int userid = (int)Session["User"];
            List<CalorieViewModel> vm = _context.CalorieViewModels.Include(x => x.Consumption).Include(x => x.Consumption.User).Include(x => x.Food).ToList().FindAll(x => x.Consumption.User.Id.Equals(userid)); //db.GetCalorieViewModelsByConsumptionId((int)Session["User"], id);

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
            _context.CalorieViewModels.Remove(_context.CalorieViewModels.SingleOrDefault(x => x.Id.Equals(assignId)));
            _context.SaveChanges();

            return RedirectToAction("ShowAssignments", new { id = consumptionId });

            //if (db.DeleteAssignment(assignId))
            //{
            //    ViewBag.Error = null;
            //    return RedirectToAction("ShowAssignments", new { id = consumptionId });
            //}
            //else
            //{
            //    ViewBag.Error = "Adatbázis hiba történt!";
            //    return RedirectToAction("ShowAssignments", new { id = consumptionId });
            //}
        }
    }
}