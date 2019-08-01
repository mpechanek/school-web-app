using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccess.Model;
using System.Web.Mvc;
using System.Drawing;
using TNPW2_video_2_ASP_MVC.Class;
using System.Drawing.Imaging;
using DataAccess.Dao;

namespace TNPW2_video_2_ASP_MVC.Controllers
{
    //[Authorize]
    public class PlayersController : Controller
    {
        // GET: Players
        public ActionResult Index(int? page)
        {
            int itemsOnPage = 5;
            int pg = page.HasValue ? page.Value : 1;
            int totalPlayers;



            HokejistaDao hokejstaDao = new HokejistaDao();
            IList<Hokejista> hokejisti = hokejstaDao.GetHokejistiPaged(itemsOnPage, pg, out totalPlayers);

            ViewBag.Pages = (int)Math.Ceiling((double)totalPlayers / (double)itemsOnPage);
            ViewBag.CurrentPage = pg;

            ViewBag.Leagues = new HokejistaLigaDao().GetAll();
            ViewBag.Posty = new HokejistaPostDao().GetAll();

         

            return View(hokejisti);

        }

        public ActionResult Detail(int id, bool? zobrazPopis)
        {
           
            HokejistaDao hokejistaDao = new HokejistaDao();
            Hokejista h = hokejistaDao.GetById(id);

            return View(h);

        }

        public ActionResult League(int id)
        {
            IList<Hokejista> hokejisti = new HokejistaDao().GetPlayersInLeagueId(id);
            ViewBag.Leagues = new HokejistaLigaDao().GetAll();
            return View("Index", hokejisti);
        }

        public ActionResult Post(int id)
        {
            IList<Hokejista> hokejisti = new HokejistaDao().GetPlayersWithPost(id);
            ViewBag.Posty = new HokejistaPostDao().GetAll();
            return View("Index", hokejisti);
        }

        public ActionResult Sort(string orderColumn, bool asc)
        {
            IList<Hokejista> hokejisti = new HokejistaDao().GetSortedEntities(orderColumn, asc);
           
            return View("Index", hokejisti);
        }


    }
}