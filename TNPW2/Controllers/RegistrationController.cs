using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DataAccess.Dao;
using DataAccess.Model;
using TNPW2_video_2_ASP_MVC.Models;
using TNPW2_video_2_ASP_MVC.Class;

namespace TNPW2_video_2_ASP_MVC.Controllers
{
    public class RegistrationController : Controller
    {
        // GET: Registration
        public ActionResult Index()
        {
            HokejistiUserDao hokejistiUserDao = new HokejistiUserDao();
            IList<HokejistiUser> users = hokejistiUserDao.GetAll();

            ViewBag.users = users;

            return View();

        }

        public ActionResult Add(UserRegister userRegister)
        {
            if (ModelState.IsValid)
            {
                HokejistiUserDao hokejistiUserDao = new HokejistiUserDao();


                if (hokejistiUserDao.GetByLogin(userRegister.Login) == null)
                {
                    HokejistiRole vyukaRole = new HokejistiRoleDao().GetById(2);
                    HokejistiUser user = new HokejistiUser()
                    {
                        Login = userRegister.Login,
                        Name = userRegister.Name,
                        Surname = userRegister.Surname,
                        Password = userRegister.Password.HashPassword(),
                        Role = vyukaRole,
                        

                    };

                    hokejistiUserDao.Create(user);

                    FormsAuthentication.SetAuthCookie(user.Login, false);
                    TempData["message-success"] = "Login " + user.Login + " byl úspěšně přidán.";
                    return RedirectToAction("Index", "Home");

                }

                ModelState.AddModelError("Login", "Uživatel již existuje! Zvolte jiný login.");


                return View("Index", userRegister);

            }

            return View("Index", userRegister);

        }

    }
}