using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TilausDB2.Models;
using TilausDB2.ViewModels;

namespace TilausDB2.Controllers
{
    [CheckSession]
    public class TilauksetController : Controller
    {
        private readonly TilauksetEntity db = new TilauksetEntity();

        // GET: Tilaukset
        public ActionResult Index()
        {
            ViewBag.Title = "Tilaukset";
            var tilaukset = db.Tilaukset.Include(t => t.Asiakkaat).Include(t => t.Postitoimipaikat);
            return View(tilaukset.ToList());
        }

        #region TilausOtsikot
        public ActionResult TilausOtsikot(string sortOrder)
        {
            #region Viewbaglauseet
            ViewBag.CurrentSort = sortOrder;
            ViewBag.Päiväys = String.IsNullOrEmpty(sortOrder) ? "sortByDate" : "";
            

            #endregion

            var orders = db.Tilaukset.Include(o => o.Asiakkaat);

            switch (sortOrder) 
            {
                case "sortByDate":
                    orders = orders.OrderByDescending(p => p.Tilauspvm);
                    ViewBag.Text = "ByDate";
                    break;
                default:
                    orders = orders.OrderBy(p => p.Tilauspvm);
                    ViewBag.Text = "Default";
                    break;
            }
            return View(orders.ToList());
        }
        #endregion


        // GET: Tilaukset/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tilaukset tilaukset = db.Tilaukset.Find(id);
            if (tilaukset == null)
            {
                return HttpNotFound();
            }
            return View(tilaukset);
        }

        // GET: Tilaukset/Create
        public ActionResult Create(string returnUrl)
        {

            ViewBag.ReturnUrl = returnUrl;
            /*ViewBag.AsiakasID = new SelectList(db.Asiakkaat, "Osoite", "Nimi");*/
            ViewBag.AsiakasID = new SelectList(db.Asiakkaat, "AsiakasID", "Nimi");
            ViewBag.Toimitusosoite = new SelectList(db.Asiakkaat, "Osoite", "Osoite");
            ViewBag.Postinumero = new SelectList(db.Postitoimipaikat, "Postinumero", "Postitoimipaikka");
            
            return View();
        }

        // POST: Tilaukset/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TilausID,AsiakasID,Toimitusosoite,Postinumero,Tilauspvm,Toimituspvm")] Tilaukset tilaukset, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                db.Tilaukset.Add(tilaukset);
                db.SaveChanges();
                return RedirectToAction(returnUrl);
            }

            ViewBag.AsiakasID = new SelectList(db.Asiakkaat, "AsiakasID", "Nimi", tilaukset.AsiakasID);
            ViewBag.Postinumero = new SelectList(db.Postitoimipaikat, "Postinumero", "Postitoimipaikka", tilaukset.Postinumero);
            return View(tilaukset);
        }

        // GET: Tilaukset/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tilaukset tilaukset = db.Tilaukset.Find(id);
            if (tilaukset == null)
            {
                return HttpNotFound();
            }
            ViewBag.AsiakasID = new SelectList(db.Asiakkaat, "AsiakasID", "Nimi", tilaukset.AsiakasID);
            ViewBag.Postinumero = new SelectList(db.Postitoimipaikat, "Postinumero", "Postitoimipaikka", tilaukset.Postinumero);
            return View(tilaukset);
        }

        // POST: Tilaukset/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TilausID,AsiakasID,Toimitusosoite,Postinumero,Tilauspvm,Toimituspvm")] Tilaukset tilaukset)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tilaukset).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AsiakasID = new SelectList(db.Asiakkaat, "AsiakasID", "Nimi", tilaukset.AsiakasID);
            ViewBag.Postinumero = new SelectList(db.Postitoimipaikat, "Postinumero", "Postitoimipaikka", tilaukset.Postinumero);
            return View(tilaukset);
        }

        // GET: Tilaukset/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tilaukset tilaukset = db.Tilaukset.Find(id);
            if (tilaukset == null)
            {
                return HttpNotFound();
            }
            return View(tilaukset);
        }

        // POST: Tilaukset/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tilaukset tilaukset = db.Tilaukset.Find(id);
            db.Tilaukset.Remove(tilaukset);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        #region OrderSummary
        public ActionResult Ordersummary()
        {
            var orderSummary = from o in db.Tilaukset
                               join od in db.Tilausrivit on o.TilausID equals od.TilausID
                               join p in db.Tuotteet on od.TuoteID equals p.TuoteID
                               select new OrderSummaryData
                               {
                                   TilausID = o.TilausID,
                                   AsiakasID = (int)o.AsiakasID,
                                   Tilauspvm = (DateTime)o.Tilauspvm,
                                   Toimituspvm = (DateTime)o.Toimituspvm,
                                   Toimitusosoite = o.Toimitusosoite,
                                   Postinumero = o.Postinumero,
                                   TuoteID = p.TuoteID,
                                   Ahinta = (float)p.Ahinta,
                                   Maara = (int)od.Maara,
                                   Nimi = p.Nimi,
                                   Lisätieto_1 = (string)p.Lisätieto_1,
                                   Lisätieto_2 = (string)p.Lisätieto_1,
                                   Kuva = (string)p.Kuva
                               };


            return View(orderSummary);
        }
        #endregion

        #region Tilausrivit

        public ActionResult PTilausRivit(int? orderid)
        {
            if ((orderid == null) || (orderid == 0))
            {
                return HttpNotFound();
            }
            else
            {
                var orderRowsList = from od in db.Tilausrivit
                                    join p in db.Tuotteet on od.TuoteID equals p.TuoteID

                                    where od.TilausID == orderid
                                    //orderby-lause
                                    select new OrderRows
                                    {
                                        TilausID = (int)od.TilausID,
                                        TuoteID = p.TuoteID,
                                        Ahinta = (float)od.Ahinta,
                                        Maara = (int)od.Maara,
                                        Nimi = p.Nimi,
                                        

                                    };
                return PartialView(orderRowsList);
            }

        }
        #endregion

        
    }
}
