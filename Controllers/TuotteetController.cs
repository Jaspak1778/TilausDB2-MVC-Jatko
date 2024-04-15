using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TilausDB2.Models;
using PagedList;

namespace TilausDB2.Controllers
{
    public class TuotteetController : Controller
    {
        private TilauksetEntity db = new TilauksetEntity();

        // GET: Tuotteet
        //public ActionResult Index()
        //{
        //    return View(db.Tuotteet.ToList());
        //}

        // GET: Products
        public ActionResult Index(string sortOrder, string currentFilter1, string searchString1, int? page, int? pagesize)
        {
            #region Viewbaglauseet
            ViewBag.CurrentSort = sortOrder;
            ViewBag.ProductNameSortParm = String.IsNullOrEmpty(sortOrder) ? "productname_desc" : "";
            ViewBag.UnitPriceSortParm = sortOrder == "UnitPrice" ? "unitprice_desc" : "UnitPrice";

            #endregion

            #region if Sivutushaku
            if (searchString1 != null)
            {
                page = 1;
            }
            else
            {
                searchString1 = currentFilter1;
            }

            ViewBag.currentFilter1 = searchString1;
            #endregion

            #region LINQ haku kannasta
            var tuotteet = from p in db.Tuotteet
                           select p;
            #endregion

            #region ehtolauseet,lajittelu ja switch

            if (!String.IsNullOrEmpty(searchString1))
            {

                switch (sortOrder)
                {
                    case "productname_desc":
                        tuotteet = tuotteet.Where(p => p.Nimi.Contains(searchString1)).OrderByDescending(p => p.Nimi);
                        break;
                    case "UnitPrice":
                        tuotteet = tuotteet.Where(p => p.Nimi.Contains(searchString1)).OrderBy(p => p.Ahinta);
                        break;
                    case "unitprice_desc":
                        tuotteet = tuotteet.Where(p => p.Nimi.Contains(searchString1)).OrderByDescending(p => p.Ahinta);
                        break;
                    default:
                        tuotteet = tuotteet.Where(p => p.Nimi.Contains(searchString1)).OrderBy(p => p.Ahinta);
                        break;
                }
            }
            else
            {
                switch (sortOrder)
                {

                    case "productname_desc":
                        tuotteet = tuotteet.OrderByDescending(p => p.Nimi);
                        break;
                    case "UnitPrice":
                        tuotteet = tuotteet.OrderBy(p => p.Ahinta);
                        break;
                    case "unitprice_desc":
                        tuotteet = tuotteet.OrderByDescending(p => p.Ahinta);
                        break;
                    default:
                        tuotteet = tuotteet.OrderBy(p => p.Ahinta);
                        break;

                }
            }
            #endregion

            #region muuttujia
            int pageSize = (pagesize ?? 8);
            int pageNumber = (page ?? 1);
            #endregion

            return View(tuotteet.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult CardIndex()
        {
            return View(db.Tuotteet.ToList());
        }


        // GET: Tuotteet/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tuotteet tuotteet = db.Tuotteet.Find(id);
            if (tuotteet == null)
            {
                return HttpNotFound();
            }
            return View(tuotteet);
        }

        // GET: Tuotteet/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tuotteet/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TuoteID,Nimi,Ahinta,Kuva,Lisätieto_1,Lisätieto_2")] Tuotteet tuotteet)
        {
            if (ModelState.IsValid)
            {
                db.Tuotteet.Add(tuotteet);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tuotteet);
        }

        // GET: Tuotteet/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tuotteet tuotteet = db.Tuotteet.Find(id);
            if (tuotteet == null)
            {
                return HttpNotFound();
            }
            return View(tuotteet);
        }

        // POST: Tuotteet/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TuoteID,Nimi,Ahinta,Kuva,Lisätieto_1,Lisätieto_2")] Tuotteet tuotteet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tuotteet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tuotteet);
        }

        // GET: Tuotteet/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tuotteet tuotteet = db.Tuotteet.Find(id);
            if (tuotteet == null)
            {
                return HttpNotFound();
            }
            return View(tuotteet);
        }

        // POST: Tuotteet/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tuotteet tuotteet = db.Tuotteet.Find(id);
            db.Tuotteet.Remove(tuotteet);
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
    }
}
