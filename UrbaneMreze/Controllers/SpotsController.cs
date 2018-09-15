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
            Spot spot = db.Spots.Find(id);
            if (spot == null)
            {
                return HttpNotFound();
            }
            return View(spot);
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
        public ActionResult Create([Bind(Include = "SpotName,Description,Longitude,Latitude")] Spot spot)
        {
            if (ModelState.IsValid)
            {
                spot.SpotGuid = Guid.NewGuid();
                
                spot.DateCreated = DateTime.Now;
                spot.DateModified = spot.DateCreated;
                spot.UserCreatedID = Auxiliaries.GetUserId(User);
                spot.UserModifiedID = Auxiliaries.GetUserId(User);

                db.Spots.Add(spot);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(spot);
        }

        // GET: Spots/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Spot spot = db.Spots.Find(id);
            if (spot == null)
            {
                return HttpNotFound();
            }
            return View(spot);
        }

        // POST: Spots/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SpotGuid,SpotName,Description,Longitude,Latitude")] SpotEditViewModel spotEditViewModel)
        {
            if (ModelState.IsValid)
            {
                Spot spot = db.Spots.Find(spotEditViewModel.SpotGuid);
                spot.SpotName = spotEditViewModel.SpotName;
                spot.Description = spotEditViewModel.Description;
                spot.Longitude = spotEditViewModel.Longitude;
                spot.Latitude = spotEditViewModel.Latitude;

                spot.DateModified = DateTime.Now;
                spot.UserModifiedID = Auxiliaries.GetUserId(User);

                db.Entry(spot).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(spotEditViewModel);
        }

        // GET: Spots/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Spot spot = db.Spots.Find(id);
            if (spot == null)
            {
                return HttpNotFound();
            }
            return View(spot);
        }

        // POST: Spots/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Spot spot = db.Spots.Find(id);
            db.Spots.Remove(spot);
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
