using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DataAccess.Dao;
using TNPW2_video_2_ASP_MVC.Class;

namespace TNPW2_video_2_ASP_MVC.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignIn(string login, string password)
        {
            if (Membership.ValidateUser(login, password.HashPassword()))
            {
                var userDao = new HokejistiUserDao();
                var user = userDao.GetByLogin(login);
                userDao.Update(user);
                FormsAuthentication.SetAuthCookie(login, false);

               
                    return RedirectToAction("Index", "Home");
                }
              

            TempData["error"] = "Login nebo heslo není správné.";
            return RedirectToAction("Index", "Login");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear();

            return RedirectToAction("Index", "Login");
        }
    }
}