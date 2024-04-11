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
    public class Sivustolla_vierailijatController : Controller
    {
        private TilauksetEntity db = new TilauksetEntity();

        // GET: Sivustolla_vierailijat
        public ActionResult Index()
        {
            var sivustolla_vierailijat = db.Sivustolla_vierailijat.Include(s => s.Myynnit);
            return View(sivustolla_vierailijat.ToList());
        }

        // GET: Sivustolla_vierailijat/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sivustolla_vierailijat sivustolla_vierailijat = db.Sivustolla_vierailijat.Find(id);
            if (sivustolla_vierailijat == null)
            {
                return HttpNotFound();
            }
            return View(sivustolla_vierailijat);
        }

        // GET: Sivustolla_vierailijat/Create
        public ActionResult Create()
        {
            ViewBag.Vuosi = new SelectList(db.Myynnit, "Vuosi", "Vuosi");
            return View();
        }

        // POST: Sivustolla_vierailijat/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Vierailijat,Vuosi")] Sivustolla_vierailijat sivustolla_vierailijat)
        {
            if (ModelState.IsValid)
            {
                db.Sivustolla_vierailijat.Add(sivustolla_vierailijat);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Vuosi = new SelectList(db.Myynnit, "Vuosi", "Vuosi", sivustolla_vierailijat.Vuosi);
            return View(sivustolla_vierailijat);
        }

        // GET: Sivustolla_vierailijat/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sivustolla_vierailijat sivustolla_vierailijat = db.Sivustolla_vierailijat.Find(id);
            if (sivustolla_vierailijat == null)
            {
                return HttpNotFound();
            }
            ViewBag.Vuosi = new SelectList(db.Myynnit, "Vuosi", "Vuosi", sivustolla_vierailijat.Vuosi);
            return View(sivustolla_vierailijat);
        }

        // POST: Sivustolla_vierailijat/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Vierailijat,Vuosi")] Sivustolla_vierailijat sivustolla_vierailijat)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sivustolla_vierailijat).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Vuosi = new SelectList(db.Myynnit, "Vuosi", "Vuosi", sivustolla_vierailijat.Vuosi);
            return View(sivustolla_vierailijat);
        }

        // GET: Sivustolla_vierailijat/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sivustolla_vierailijat sivustolla_vierailijat = db.Sivustolla_vierailijat.Find(id);
            if (sivustolla_vierailijat == null)
            {
                return HttpNotFound();
            }
            return View(sivustolla_vierailijat);
        }

        // POST: Sivustolla_vierailijat/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sivustolla_vierailijat sivustolla_vierailijat = db.Sivustolla_vierailijat.Find(id);
            db.Sivustolla_vierailijat.Remove(sivustolla_vierailijat);
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
