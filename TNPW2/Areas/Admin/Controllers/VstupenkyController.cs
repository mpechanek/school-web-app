using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccess.Dao;
using DataAccess.Model;

namespace TNPW2_video_2_ASP_MVC.Areas.Admin.Controllers
{
    public class VstupenkyController : Controller
    {
        // GET: Vstupenky
        public ActionResult Index( int? page)
        {
            int itemsOnPage = 5;
            int pg = page.HasValue ? page.Value : 1;
            int totalVstupenky;



            VstupenkaDao VstupenkaDao = new VstupenkaDao();
            IList<Vstupenka> vstupenky = VstupenkaDao.GetVstupenkyPaged(itemsOnPage, pg, out totalVstupenky);

            ViewBag.Pages = (int)Math.Ceiling((double)totalVstupenky / (double)itemsOnPage);
            ViewBag.CurrentPage = pg;

           



            return View(vstupenky);
            
        }

        public ActionResult Create()
        {

            return View();
        }

        public ActionResult Add(Vstupenka vstupenka)
        {
            if (ModelState.IsValid)
            {

                

                VstupenkaDao vstupenkaDao = new VstupenkaDao();
                vstupenkaDao.Create(vstupenka);

                TempData["message-success"] = "Vytvoření rezervace pro klienta " + vstupenka.Jmeno + " proběhlo úspěšně.";
            }
            else
            {
                return View("Create", vstupenka);
            }

            return RedirectToAction("Index");
        }
        public JsonResult SearchVstupenky(string query)
        {
            VstupenkaDao vstupenkaDao = new VstupenkaDao();
            IList<Vstupenka> vstupenky = vstupenkaDao.SearchVstupenka(query);

            List<string> names = (from Vstupenka v in vstupenky select v.Jmeno).ToList();

            return Json(names, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Search(string phrase)
        {
            VstupenkaDao vstupenkaDao = new VstupenkaDao();
            IList<Vstupenka> vstupenky = vstupenkaDao.SearchVstupenka(phrase);

            if (Request.IsAjaxRequest())
            {
                return PartialView("Index", vstupenky);
            }


            return View("Index", vstupenky);
        }
        public ActionResult Delete(int id)
        {
            try
            {
                VstupenkaDao vstupenkaDao = new VstupenkaDao();
                Vstupenka vstupenka = vstupenkaDao.GetById(id);
                

                vstupenkaDao.Delete(vstupenka);
                TempData["message-success"] = "Rezervace klienta " + vstupenka.Jmeno + " byla smazána.";
            }
            catch (Exception exception)
            {
                throw;
            }
            return RedirectToAction("Index", "Vstupenky");

        }
    }

}
