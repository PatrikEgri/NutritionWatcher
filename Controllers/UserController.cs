using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NutritionWatcher.Models;

namespace NutritionWatcher.Controllers
{
    public class UserController : Controller
    {
        DataBaseHandler db = new DataBaseHandler();

        public ViewResult ViewUserData()
        {
            return View(db.GetUserById((int)Session["User"]));
        }

        public ViewResult UpdateUsername()
        {            
            ViewBag.Error = null;
            return View(new UpdateUsernameViewModel() { Username = db.GetUserById((int)Session["User"]).Username });
        }

        public ActionResult UpdateUsernameDB(UpdateUsernameViewModel user)
        {
            if (!ModelState.IsValid) return View("UpdateUsername", user);

            if (db.UpdateUsername((int)Session["User"], user.Username))
            {
                ViewBag.Error = null;
                return RedirectToAction("ViewUserData");
            }
            else
            {
                ViewBag.Error = "Adatbázis hiba történt!";
                return View("ViewUserData");
            }
        }

        public ActionResult UpdateName()
        {
            ViewBag.Error = null;
            UserModel user = db.GetUserById((int)Session["User"]);
            return View(new UpdateNameViewModel() { Firstname = user.Firstname, Lastname = user.Lastname});
        }

        public ActionResult UpdateNameDB(UpdateNameViewModel user)
        {
            if (!ModelState.IsValid) return View("UpdateName", user);

            if (db.UpdateName((int)Session["User"], user.Firstname, user.Lastname))
            {
                ViewBag.Error = null;
                return RedirectToAction("ViewUserData");
            }
            else
            {
                ViewBag.Error = "Adatbázis hiba történt!";
                return View("ViewUserData");
            }
        }

        public ViewResult UpdateEmail()
        {
            ViewBag.Error = null;
            return View(new UpdateEmailViewModel() { Email = db.GetUserById((int)Session["User"]).Email });
        }

        public ActionResult UpdateEmailDB(UpdateEmailViewModel user)
        {
            if (!ModelState.IsValid) return View("UpdateEmail", user);

            if (db.UpdateEmail((int)Session["User"], user.Email))
            {
                ViewBag.Error = null;
                return RedirectToAction("ViewUserData");
            }
            else
            {
                ViewBag.Error = "Adatbázis hiba történt!";
                return View("ViewUserData");
            }
        }

        public ActionResult UpdateStyle()
        {
            ViewBag.Error = null;
            return View(new UpdateStyleViewModel 
            {
                Styles = db.GetStyles()
            });
        }

        public ActionResult UpdateStyleDB(UpdateStyleViewModel vm)
        {
            if (!ModelState.IsValid) 
            {
                vm.Styles = db.GetStyles();
                return View("UpdateStyle", vm); 
            }

            if (db.UpdateStyle((int)Session["User"], vm.StyleId))
            {
                ViewBag.Error = null;
                return RedirectToAction("ViewUserData");
            }
            else
            {
                ViewBag.Error = "Adatbázis hiba történt!";
                return View("ViewUserData");
            }
        }

        public ActionResult UpdatePassword()
        {
            ViewBag.Error = null;
            return View(new UpdatePasswordViewModel());
        }

        public ActionResult UpdatePasswordDB(UpdatePasswordViewModel vm)
        {
            if (!ModelState.IsValid) return View("UpdatePassword", vm);

            if (db.Hash(vm.Password) == db.GetUserById((int)Session["User"]).Password) 
            {
                if (db.UpdatePassword((int)Session["User"], vm.NewPassword))
                {
                    ViewBag.Error = null;
                    return RedirectToAction("ViewUserData");
                }
                else
                {
                    ViewBag.Error = "Adatbázis hiba történt!";
                    return View("ViewUserData");
                }
            }
            else
            {
                ViewBag.Error = "Nem jól adta meg jelenlegi jelszavát!";
                return View("UpdatePassword");
            }
        }


        public ActionResult DeleteUser()
        {
            ViewBag.Error = null;
            return View(db.GetUserById((int)Session["User"]));
        }

        public RedirectToRouteResult DeleteUserDB(int userId)
        {
            if (db.DeleteUser(userId))
            {
                return RedirectToAction("Logout");
            }
            else
            {
                ViewBag.Error = "Adatbázis hiba történt!";
                return RedirectToAction("DeleteUser");
            }
        }

        public ActionResult Login()
        {
            ViewBag.Error = null;
            return View(new LoginUserModel());         
        }

        [HttpPost]
        public ActionResult LoginDB(LoginUserModel user)
        {
            if (!ModelState.IsValid /*&& user.Id == 0*/)
            {
                return View("Login", user);
            }

            UserModel loggedIn = db.GetUser(user);
            if (loggedIn == null)
            {
                ViewBag.Error = "Sikertelen bejelentkezés!";
                return View("Login");
            }
            else
            {
                ViewBag.Error = null;
                Session["User"] = loggedIn.Id;
                Session["Style"] = loggedIn.Style;
                return RedirectToAction("ViewUserData");
            }
        }

        /// <summary>
        /// This controller sets the value of the Session to null and redirects to the Login View.
        /// </summary>
        /// <returns></returns>
        public ViewResult Logout()
        {
            ViewBag.Error = null;
            Session["User"] = null;
            Session["Style"] = null;
            return View("Login");
        }

        public ViewResult Registration()
        {
            ViewBag.Error = null;
            return View(new RegistrationUserModel());
        }

        [HttpPost]
        public ActionResult RegistrationDB(RegistrationUserModel user)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Error = null;
                return View("Registration", user);
            }

            if (db.InsertUser(user))
            {
                ViewBag.Error = null;
                return RedirectToAction("Login");
            }
            else
            {
                ViewBag.Error = "Adatbázis hiba történt!";
                return View("Registration");
            }
        }
    }
}