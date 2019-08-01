using DataAccess.Dao;
using DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TNPW2_video_2_ASP_MVC.Areas.Admin.Controllers
{
    [Authorize]
    public class MenuController : Controller
    {
        [ChildActionOnly]
        // GET: Admin/Menu
        public ActionResult Index()
        {
            HokejistiUserDao hokejistiUserDao = new HokejistiUserDao();
            HokejistiUser hokejistiUser = hokejistiUserDao.GetByLogin(User.Identity.Name);
            return View(hokejistiUser);
        }
    }
}