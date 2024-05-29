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
    [CheckSession]
    public class MyynnitController : Controller
    {
        private TilauksetEntities db = new TilauksetEntities();

        // GET: Myynnit
        public ActionResult Index()
        {
            var myynnit = db.Myynnit.Include(m => m.Sivustolla_vierailijat);
            return View(myynnit.ToList());
        }

        // GET: Myynnit/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Myynnit myynnit = db.Myynnit.Find(id);
            if (myynnit == null)
            {
                return HttpNotFound();
            }
            return View(myynnit);
        }

        // GET: Myynnit/Create
        public ActionResult Create()
        {
            ViewBag.Vuosi = new SelectList(db.Sivustolla_vierailijat, "Vuosi", "Vuosi");
            return View();
        }

        // POST: Myynnit/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Vuosi,Kokonaismyynti")] Myynnit myynnit)
        {
            if (ModelState.IsValid)
            {
                db.Myynnit.Add(myynnit);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Vuosi = new SelectList(db.Sivustolla_vierailijat, "Vuosi", "Vuosi", myynnit.Vuosi);
            return View(myynnit);
        }

        // GET: Myynnit/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Myynnit myynnit = db.Myynnit.Find(id);
            if (myynnit == null)
            {
                return HttpNotFound();
            }
            ViewBag.Vuosi = new SelectList(db.Sivustolla_vierailijat, "Vuosi", "Vuosi", myynnit.Vuosi);
            return View(myynnit);
        }

        // POST: Myynnit/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Vuosi,Kokonaismyynti")] Myynnit myynnit)
        {
            if (ModelState.IsValid)
            {
                db.Entry(myynnit).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Vuosi = new SelectList(db.Sivustolla_vierailijat, "Vuosi", "Vuosi", myynnit.Vuosi);
            return View(myynnit);
        }

        // GET: Myynnit/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Myynnit myynnit = db.Myynnit.Find(id);
            if (myynnit == null)
            {
                return HttpNotFound();
            }
            return View(myynnit);
        }

        // POST: Myynnit/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Myynnit myynnit = db.Myynnit.Find(id);
            db.Myynnit.Remove(myynnit);
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
