using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UrbaneMreze.Models;

namespace UrbaneMreze.Controllers
{
    public class SpotsController : Controller
    {
        private SpotsDbContext db = new SpotsDbContext();

        // GET: Spots
        public ActionResult Index()
        {
            return View(db.Spots.ToList());
        }

        // GET: Spots/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Spots spots = db.Spots.Find(id);
            if (spots == null)
            {
                return HttpNotFound();
            }
            return View(spots);
        }

        // GET: Spots/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Spots/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SpotGuid,SpotName,AuthorGuid,Description,Longitude,Latitude,DateCreated,DateModified")] Spots spots)
        {
            if (ModelState.IsValid)
            {
                spots.SpotGuid = Guid.NewGuid();
                db.Spots.Add(spots);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(spots);
        }

        // GET: Spots/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Spots spots = db.Spots.Find(id);
            if (spots == null)
            {
                return HttpNotFound();
            }
            return View(spots);
        }

        // POST: Spots/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SpotGuid,SpotName,AuthorGuid,Description,Longitude,Latitude,DateCreated,DateModified")] Spots spots)
        {
            if (ModelState.IsValid)
            {
                db.Entry(spots).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(spots);
        }

        // GET: Spots/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Spots spots = db.Spots.Find(id);
            if (spots == null)
            {
                return HttpNotFound();
            }
            return View(spots);
        }

        // POST: Spots/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Spots spots = db.Spots.Find(id);
            db.Spots.Remove(spots);
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
