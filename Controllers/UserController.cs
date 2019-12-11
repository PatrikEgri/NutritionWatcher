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

        /// <summary>
        /// This controller returns the View, where we can see the data of the User.
        /// </summary>
        /// <returns></returns>
        public ViewResult ViewUserData()
        {
            return View(db.GetUserById((int)Session["User"]));
        }

        /// <summary>
        /// This controller waits for a UserModer as parameter. If it doesn't take the parameter,
        /// then it returns a View, where we can see the actual data of the User. If it takes the
        /// parameter, then it updates the user in the database and redirects to the View, where
        /// we can see the data of the User.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public ActionResult UpdateUsername()
        {
                ViewBag.Error = null;
                return View(db.GetUserById((int)Session["User"]));
        }

        public RedirectToRouteResult UpdateUsernameDB(UserModel user)
        {
            if (db.UpdateUsername(user.Id, user.Username))
            {
                ViewBag.Error = null;
                return RedirectToAction("ViewUserData");
            }
            else
            {
                ViewBag.Error = "Adatbázis hiba történt!";
                return RedirectToAction("ViewUserData");
            }
        }

        public ActionResult UpdateName()
        {
            ViewBag.Error = null;
            return View(db.GetUserById((int)Session["User"]));
        }

        public RedirectToRouteResult UpdateNameDB(UserModel user)
        {
            if (db.UpdateName(user.Id, user.Firstname, user.Lastname))
            {
                ViewBag.Error = null;
                return RedirectToAction("ViewUserData");
            }
            else
            {
                ViewBag.Error = "Adatbázis hiba történt!";
                return RedirectToAction("ViewUserData");
            }
        }

        public ActionResult UpdateEmail()
        {
            ViewBag.Error = null;
            return View(db.GetUserById((int)Session["User"]));
        }

        public RedirectToRouteResult UpdateEmailDB(UserModel user)
        {
            if (db.UpdateEmail(user.Id, user.Email))
            {
                ViewBag.Error = null;
                return RedirectToAction("ViewUserData");
            }
            else
            {
                ViewBag.Error = "Adatbázis hiba történt!";
                return RedirectToAction("ViewUserData");
            }
        }

        public ActionResult UpdateStyle()
        {
            ViewBag.Error = null;
            return View(new UpdateStyleViewModel 
            {
                UserId = (int)Session["User"],
                Styles = db.GetStyles()
            });
        }

        public RedirectToRouteResult UpdateStyleDB(UpdateStyleViewModel vm)
        {
            if (db.UpdateStyle(vm.UserId, vm.StyleId))
            {
                ViewBag.Error = null;
                return RedirectToAction("ViewUserData");
            }
            else
            {
                ViewBag.Error = "Adatbázis hiba történt!";
                return RedirectToAction("ViewUserData");
            }
        }

        public ActionResult UpdatePassword()
        {
            ViewBag.Error = null;
            return View(new UpdatePasswordViewModel
            {
                UserId = (int)Session["User"]
            });
        }

        public RedirectToRouteResult UpdatePasswordDB(UpdatePasswordViewModel vm)
        {
            if (db.Hash(vm.Password) == db.GetUserById((int)Session["User"]).Password) {
                if (db.UpdatePassword(vm.UserId, vm.NewPassword))
                {
                    ViewBag.Error = null;
                    return RedirectToAction("ViewUserData");
                }
                else
                {
                    ViewBag.Error = "Adatbázis hiba történt!";
                    return RedirectToAction("ViewUserData");
                }
            }
            else
            {
                return RedirectToAction("UpdatePassword");
            }
        }


        /// <summary>
        /// This controller waits for a UserModel as parameter. If it doesn't take the parameter,
        /// then it returns a View that goes for sure we want to delete the user. If it takes the
        /// parameter, then it deletes the user from the database and redirects to the Login View.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
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

        /// <summary>
        /// This controller waits for a UserModel as parameter. If it doesn't take the parameter,
        /// then it returns a View, where we can enter our username and password. If it takes the
        /// parameter, then it creates a Session with the UserModel and redirects to the View, 
        /// where we can see the data of the User.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
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
            return View("Login");
        }

        /// <summary>
        /// This controller waits for a UserModel as parameter. If it doesn't take the parameter,
        /// then it returns a View, where we can registrate. If it takes the parameter, then it
        /// inserts the user into the database and redirects to the Login View.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
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