using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TilausDB2.Models;

namespace TilausDB2.Controllers
{
    public class Vierailija_kohdeController : Controller
    {
        private TilauksetEntity db = new TilauksetEntity();

        // GET: Vierailija_kohde
        public ActionResult Index()
        {
            return View(db.Vierailija_kohde.ToList());
        }

        // GET: Vierailija_kohde/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vierailija_kohde vierailija_kohde = db.Vierailija_kohde.Find(id);
            if (vierailija_kohde == null)
            {
                return HttpNotFound();
            }
            return View(vierailija_kohde);
        }

        // GET: Vierailija_kohde/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Vierailija_kohde/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Maa,Kaupunki,Henkilomaara,RiviID")] Vierailija_kohde vierailija_kohde)
        {
            if (ModelState.IsValid)
            {
                db.Vierailija_kohde.Add(vierailija_kohde);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vierailija_kohde);
        }

        // GET: Vierailija_kohde/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vierailija_kohde vierailija_kohde = db.Vierailija_kohde.Find(id);
            if (vierailija_kohde == null)
            {
                return HttpNotFound();
            }
            return View(vierailija_kohde);
        }

        // POST: Vierailija_kohde/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Maa,Kaupunki,Henkilomaara,RiviID")] Vierailija_kohde vierailija_kohde)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vierailija_kohde).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vierailija_kohde);
        }

        // GET: Vierailija_kohde/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vierailija_kohde vierailija_kohde = db.Vierailija_kohde.Find(id);
            if (vierailija_kohde == null)
            {
                return HttpNotFound();
            }
            return View(vierailija_kohde);
        }

        // POST: Vierailija_kohde/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Vierailija_kohde vierailija_kohde = db.Vierailija_kohde.Find(id);
            db.Vierailija_kohde.Remove(vierailija_kohde);
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
