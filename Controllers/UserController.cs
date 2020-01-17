using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NutritionWatcher.Models;
using System.Data.Entity;

namespace NutritionWatcher.Controllers
{
    public class UserController : Controller
    {
        ApplicationDbContext _context = new ApplicationDbContext();

        public ViewResult ViewUserData()
        {
            int userid = (int)Session["User"];
            var user = _context.NWUsers.Include(x => x.Style).SingleOrDefault(x => x.Id.Equals(userid));
            return View(user);
        }

        public ViewResult UpdateUsername()
        {            
            ViewBag.Error = null;
            int id = (int)Session["User"];
            var user = _context.NWUsers.SingleOrDefault(x => x.Id == id);
            return View(new UpdateUsernameViewModel() { Username = user.Username });
        }

        public ActionResult UpdateUsernameDB(UpdateUsernameViewModel user)
        {
            if (!ModelState.IsValid) return View("UpdateUsername", user);

            int id = (int)Session["User"];
            _context.NWUsers.SingleOrDefault(x => x.Id.Equals(id)).Username = user.Username;
            _context.SaveChanges();

            return RedirectToAction("ViewUserData");

            //if (db.UpdateUsername((int)Session["User"], user.Username))
            //{
            //    ViewBag.Error = null;
            //    return RedirectToAction("ViewUserData");
            //}
            //else
            //{
            //    ViewBag.Error = "Adatbázis hiba történt!";
            //    return View("ViewUserData");
            //}
        }

        public ViewResult UpdateName()
        {
            ViewBag.Error = null;
            int id = (int)Session["User"];
            UserModel user = _context.NWUsers.SingleOrDefault(x => x.Id.Equals(id));
            return View(new UpdateNameViewModel() { Firstname = user.Firstname, Lastname = user.Lastname});
        }

        public ActionResult UpdateNameDB(UpdateNameViewModel user)
        {
            if (!ModelState.IsValid) return View("UpdateName", user);

            int id = (int)Session["User"];
            UserModel userInDb = _context.NWUsers.SingleOrDefault(x => x.Id.Equals(id));
            userInDb.Firstname = user.Firstname;
            userInDb.Lastname = user.Lastname;
            _context.SaveChanges();

            return RedirectToAction("ViewUserData");

            //if (db.UpdateName((int)Session["User"], user.Firstname, user.Lastname))
            //{
            //    ViewBag.Error = null;
            //    return RedirectToAction("ViewUserData");
            //}
            //else
            //{
            //    ViewBag.Error = "Adatbázis hiba történt!";
            //    return View("ViewUserData");
            //}
        }

        public ViewResult UpdateEmail()
        {
            ViewBag.Error = null;
            int id = (int)Session["User"];
            return View(new UpdateEmailViewModel() { Email = _context.NWUsers.SingleOrDefault(x => x.Id.Equals(id)).Email });
        }

        public ActionResult UpdateEmailDB(UpdateEmailViewModel user)
        {
            if (!ModelState.IsValid) return View("UpdateEmail", user);

            int id = (int)Session["User"];
            _context.NWUsers.SingleOrDefault(x => x.Id.Equals(id)).Email = user.Email;
            _context.SaveChanges();

            return RedirectToAction("ViewUserData");

            //if (db.UpdateEmail((int)Session["User"], user.Email))
            //{
            //    ViewBag.Error = null;
            //    return RedirectToAction("ViewUserData");
            //}
            //else
            //{
            //    ViewBag.Error = "Adatbázis hiba történt!";
            //    return View("ViewUserData");
            //}
        }

        public ActionResult UpdateStyle()
        {
            ViewBag.Error = null;
            return View(new UpdateStyleViewModel
            {
                Styles = _context.Styles.ToList()
            });
        }

        public ActionResult UpdateStyleDB(UpdateStyleViewModel vm)
        {
            if (!ModelState.IsValid) 
            {
                vm.Styles = _context.Styles.ToList();//db.GetStyles();
                return View("UpdateStyle", vm); 
            }

            int id = (int)Session["User"];
            _context.NWUsers.Include(x => x.Style).SingleOrDefault(x => x.Id.Equals(id)).Style = _context.Styles.SingleOrDefault(x => x.Id.Equals(vm.StyleId));
            _context.SaveChanges();

            return RedirectToAction("ViewUserData");

            //if (db.UpdateStyle((int)Session["User"], vm.StyleId))
            //{
            //    ViewBag.Error = null;
            //    return RedirectToAction("ViewUserData");
            //}
            //else
            //{
            //    ViewBag.Error = "Adatbázis hiba történt!";
            //    return View("ViewUserData");
            //}
        }

        public ActionResult UpdatePassword()
        {
            ViewBag.Error = null;
            return View(new UpdatePasswordViewModel());
        }

        public ActionResult UpdatePasswordDB(UpdatePasswordViewModel vm)
        {
            if (!ModelState.IsValid) return View("UpdatePassword", vm);

            int id = (int)Session["User"];
            if (Hash(vm.Password) == _context.NWUsers.SingleOrDefault(x => x.Id.Equals(id)).Password)//db.GetUserById((int)Session["User"]).Password) 
            {
                _context.NWUsers.SingleOrDefault(x => x.Id.Equals(id)).Password = Hash(vm.NewPassword);
                _context.SaveChanges();

                return RedirectToAction("ViewUserData");

                //if (db.UpdatePassword((int)Session["User"], vm.NewPassword))
                //{
                //    ViewBag.Error = null;
                //    return RedirectToAction("ViewUserData");
                //}
                //else
                //{
                //    ViewBag.Error = "Adatbázis hiba történt!";
                //    return View("ViewUserData");
                //}
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
            int id = (int)Session["User"];
            return View(_context.NWUsers.SingleOrDefault(x => x.Id.Equals(id)));
        }

        public RedirectToRouteResult DeleteUserDB(int userId)
        {
            int id = (int)Session["User"];
            _context.Consumptions.Include(x => x.User).ToList().RemoveAll(x => x.User.Equals(_context.NWUsers.FirstOrDefault(y => y.Id.Equals(id))));
            _context.NWUsers.Remove(_context.NWUsers.SingleOrDefault(x => x.Id.Equals(id)));
            _context.SaveChanges();

            return RedirectToAction("Logout");

            //if (db.DeleteUser(userId))
            //{
            //    return RedirectToAction("Logout");
            //}
            //else
            //{
            //    ViewBag.Error = "Adatbázis hiba történt!";
            //    return RedirectToAction("DeleteUser");
            //}
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

            string hash = Hash(user.Password);
            UserModel loggedIn = _context.NWUsers.SingleOrDefault(x => x.Username.Equals(user.Username) && x.Password.Equals(hash));
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

            _context.NWUsers.Add(new UserModel
            {
                Username = user.Username,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Password = Hash(user.Password),
                Email = user.Email,
                Permission = (from x in _context.Permissions where x.Name.Equals("member") select x).FirstOrDefault(),
                Style = (from x in _context.Styles where x.Name.Equals("light") select x).FirstOrDefault() 
            });
            _context.SaveChanges();

            return RedirectToAction("Login");

            //if (db.InsertUser(user))
            //{
            //    ViewBag.Error = null;
            //    return RedirectToAction("Login");
            //}
            //else
            //{
            //    ViewBag.Error = "Adatbázis hiba történt!";
            //    return View("Registration");
            //}
        }

        string Hash(string a)
        {
            return a.GetHashCode() > 0 ? (a.GetHashCode() << 5).GetHashCode().ToString() : (a.GetHashCode() >> 5).GetHashCode().ToString();
        }
    }
}