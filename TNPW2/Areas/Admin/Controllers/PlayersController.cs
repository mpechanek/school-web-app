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

namespace TNPW2_video_2_ASP_MVC.Areas.Admin.Controllers
{
    [Authorize]
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

            HokejistiUser user = new HokejistiUserDao().GetByLogin(User.Identity.Name);

            if (user.Role.Identificator == "trener")
                return View("IndexCtenar", hokejisti);

            if (Request.IsAjaxRequest())
            {
                return PartialView(hokejisti);
            }

            return View(hokejisti);


        }

        public ActionResult League(int id)
        {
            IList<Hokejista> hokejisti = new HokejistaDao().GetPlayersInLeagueId(id);
            ViewBag.Leagues = new HokejistaLigaDao().GetAll();
            HokejistiUser user = new HokejistiUserDao().GetByLogin(User.Identity.Name);
            if (user.Role.Identificator == "trener")
                return View("IndexCtenar", hokejisti);

            return View("Index",hokejisti);
        }

        public ActionResult Post(int id)
        {
            IList<Hokejista> hokejisti = new HokejistaDao().GetPlayersWithPost(id);
            ViewBag.Posty = new HokejistaPostDao().GetAll();
            return View("IndexCtenar", hokejisti);
        }

        public JsonResult SearchPlayers(string query)
        {
            HokejistaDao hokejistaDao = new HokejistaDao();
            IList<Hokejista> hokejisti = hokejistaDao.SearchPlayers(query);

            List<string> names = (from Hokejista h in hokejisti select h.Jmeno).ToList();

            return Json(names, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Search(string phrase)
        {
            HokejistaDao hokejistaDao = new HokejistaDao();
            IList<Hokejista> hokejisti = hokejistaDao.SearchPlayers(phrase);

            if (Request.IsAjaxRequest())
            {
                return PartialView("IndexCtenar", hokejisti);
            }


            return View("IndexCtenar", hokejisti);
        }


        public ActionResult Detail(int id, bool? zobrazPopis)
        {
            HokejistaDao hokejistaDao = new HokejistaDao();
            Hokejista h = hokejistaDao.GetById(id);

            if (Request.IsAjaxRequest())
            {
                return PartialView(h);
            }

            //  ViewBag.ZobrazPopis = zobrazPopis;
            return View(h);

        }

        [Authorize(Roles = "redaktor")]

        public ActionResult Create()
        {
            HokejistaLigaDao hokejistaLigaDao = new HokejistaLigaDao();
            HokejistaPostDao hokejistaPostDao = new HokejistaPostDao();
            IList<HokejistaLiga> ligy = hokejistaLigaDao.GetAll();
            IList<HokejistaPost> posty = hokejistaPostDao.GetAll();
            ViewBag.Ligy = ligy;
            ViewBag.Posty = posty;

            return View();
        }
        [HttpPost]
        [Authorize(Roles = "redaktor")]


        public ActionResult Add(Hokejista hokejista, HttpPostedFileBase picture, int ligaId, int postId)
        {
            if (ModelState.IsValid)
            {

                if (picture != null)
                {
                    //picture.SaveAs(Server.MapPath("~/Uploads/Hokejista/") + picture.FileName);
                    if (picture.ContentType == "image/jpeg" || picture.ContentType == "image/png")
                    {
                        Image image = Image.FromStream(picture.InputStream);

                        if (image.Height > 200 || image.Width > 200)
                        {
                            Image smallImage = ImageHelper.ScaleImage(image, 200, 200);
                            Bitmap h = new Bitmap(smallImage);

                            Guid guid = Guid.NewGuid();
                            string imageName = guid.ToString() + ".jpg";

                            h.Save(Server.MapPath("~/uploads/hokejista/" + imageName), ImageFormat.Jpeg);

                            smallImage.Dispose();
                            h.Dispose();

                            hokejista.ImageName = imageName;
                        }
                        else
                        {
                            picture.SaveAs(Server.MapPath("~/uploads/hokejista/" + picture.FileName));
                        }

                    }
                }

                HokejistaLigaDao hokejistaLigaDao = new HokejistaLigaDao();
                HokejistaLiga hokejistaLiga = hokejistaLigaDao.GetById(ligaId);

                hokejista.Liga = hokejistaLiga;

                HokejistaPostDao hokejistaPostDao = new HokejistaPostDao();
                HokejistaPost hokejistaPost = hokejistaPostDao.GetById(postId);

                hokejista.Post = hokejistaPost;

                HokejistaDao hokejistaDao = new HokejistaDao();
                hokejistaDao.Create(hokejista);

                TempData["message-success"] = "Hokejista " + hokejista.Jmeno + " byl úspěšně přidán.";
            }
            else
            {
                return View("Create", hokejista);
            }

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "redaktor")]

        public ActionResult Edit(int id)
        {
            HokejistaDao hokejistaDao = new HokejistaDao();
            HokejistaLigaDao hokejistaLigaDao = new HokejistaLigaDao();
            HokejistaPostDao hokejistaPostDao = new HokejistaPostDao();

            Hokejista h = hokejistaDao.GetById(id);
            ViewBag.Ligy = hokejistaLigaDao.GetAll();
            ViewBag.Posty = hokejistaPostDao.GetAll();

            return View(h);
        }

        [Authorize(Roles = "redaktor")]
        [HttpPost]
        public ActionResult Update(Hokejista hokejista, HttpPostedFileBase picture, int ligaId, int postId)
        {
            try
            {
                HokejistaDao hokejistaDao = new HokejistaDao();
                HokejistaLigaDao hokejistaLigaDao = new HokejistaLigaDao();
                HokejistaLiga hokejistaLiga = hokejistaLigaDao.GetById(ligaId);
                hokejista.Liga = hokejistaLiga;
                HokejistaPostDao hokejistaPostDao = new HokejistaPostDao();
                HokejistaPost hokejistaPost = hokejistaPostDao.GetById(postId);
                hokejista.Post = hokejistaPost;

                if (picture != null)
                {
                    if (picture.ContentType == "image/jpeg" || picture.ContentType == "image/png")
                    {
                        Image image = Image.FromStream(picture.InputStream);
                        Guid guid = Guid.NewGuid();
                        string imageName = guid.ToString() + ".jpg";

                       

                        if (image.Height > 200 || image.Width > 200)
                        {
                            Image smallImage = ImageHelper.ScaleImage(image, 200, 200);
                            Bitmap h = new Bitmap(smallImage);
                            h.Save(Server.MapPath("~/uploads/hokejista/" + imageName), ImageFormat.Jpeg);
                            smallImage.Dispose();
                            h.Dispose();

                        }
                        else
                        {
                            picture.SaveAs(Server.MapPath("~/uploads/hokejista/" + picture.FileName));
                        }

                        if (!String.IsNullOrEmpty(hokejista.ImageName))
                        {
                            System.IO.File.Delete(Server.MapPath("~/uploads/hokejista/" + hokejista.ImageName));
                        }


                        hokejista.ImageName = imageName;

                    }
                }

                hokejistaDao.Update(hokejista);

                TempData["message-success"] = "Hráč " + hokejista.Jmeno + " byl upraven.";
            }
            catch (Exception exception)
            {
                throw;

            }

            return RedirectToAction("Index", "Players");
        }

        [Authorize(Roles = "redaktor")]

        public ActionResult Delete(int id)
        {
            try
            {
                HokejistaDao hokejistaDao = new HokejistaDao();
                Hokejista hokejista = hokejistaDao.GetById(id);
                if (!string.IsNullOrEmpty(hokejista.ImageName))
                {
                    System.IO.File.Delete(Server.MapPath("~/uploads/hokejista/" + hokejista.ImageName));
                }


                hokejistaDao.Delete(hokejista);
                TempData["message-success"] = "Hráč " + hokejista.Jmeno + " byl smazán.";
            }
            catch (Exception exception)
            {
                throw;
            }
            return RedirectToAction("Index", "Players");

        }
        [Authorize(Roles = "trener  ")]

        public ActionResult EditTrener(int id)
        {
            HokejistaDao hokejistaDao = new HokejistaDao();
            HokejistaLigaDao hokejistaLigaDao = new HokejistaLigaDao();
            HokejistaPostDao hokejistaPostDao = new HokejistaPostDao();

            Hokejista h = hokejistaDao.GetById(id);
            ViewBag.Ligy = hokejistaLigaDao.GetAll();
            ViewBag.Posty = hokejistaPostDao.GetAll();

            return View(h);
        }

        [Authorize(Roles = "trener")]
        [HttpPost]
        public ActionResult UpdateTrener(Hokejista hokejista, int ligaId, int postId)
        {
            try
            {
                HokejistaDao hokejistaDao = new HokejistaDao();
                HokejistaLigaDao hokejistaLigaDao = new HokejistaLigaDao();
                HokejistaLiga hokejistaLiga = hokejistaLigaDao.GetById(ligaId);
                hokejista.Liga = hokejistaLiga;

                HokejistaPostDao hokejistaPostDao = new HokejistaPostDao();
                HokejistaPost hokejistaPost = hokejistaPostDao.GetById(postId);
                hokejista.Post = hokejistaPost;

                hokejistaDao.Update(hokejista);
                
                TempData["message-success"] = "Hráč " + hokejista.Jmeno + " byl upraven.";
            }
            catch (Exception exception)
            {
                throw;

            }

            return RedirectToAction("Index", "Players");
        }
    }

}