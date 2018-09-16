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
        private TypesDbContext dbTypes = new TypesDbContext();
        private SpotsTypesDbContext dbSpotTypes = new SpotsTypesDbContext();

        // GET: Spots
        public ActionResult Index()
        {
            var spotTypes = dbSpotTypes.SpotsTypes;
            var types = dbTypes.Types;

            ViewBag.types = types;
            ViewBag.spotTypes = spotTypes;

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

            var spotType = dbSpotTypes.SpotsTypes.First(x => x.SpotGuid == id.Value);

            var Type = dbTypes.Types.First(x => x.TypeGuid == spotType.TypeGuid);

            ViewBag.typeDetails = Type;

            return View(spot);
        }

        // GET: Spots/Create
        public ActionResult Create()
        {
            ViewBag.TypeGuid = new SelectList(dbTypes.Types, "TypeGuid", "TypeName");
            return View();
        }

        // POST: Spots/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SpotName,Description,Longitude,Latitude,TypeGuid")] Spot spot)
        {
            if (ModelState.IsValid)
            {
                spot.SpotGuid = Guid.NewGuid();
                spot.DateCreated = DateTime.Now;
                spot.DateModified = spot.DateCreated;
                spot.UserCreatedID = Auxiliaries.GetUserId(User);
                spot.UserModifiedID = Auxiliaries.GetUserId(User);

                SpotType spotType = new SpotType();
                spotType.SpotTypeGuid = Guid.NewGuid();
                spotType.SpotGuid = spot.SpotGuid;
                spotType.TypeGuid = spot.TypeGuid;
                spotType.DateCreated = DateTime.Now;
                spotType.DateModified = spotType.DateCreated;

                db.Spots.Add(spot);
                db.SaveChanges();

                dbSpotTypes.SpotsTypes.Add(spotType);
                dbSpotTypes.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.TypeGuid = new SelectList(dbTypes.Types, "TypeGuid", "TypeName", spot.TypeGuid);
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
            var spotType = dbSpotTypes.SpotsTypes.First(x => x.SpotGuid == id.Value);

            var Type = dbTypes.Types.First(x => x.TypeGuid == spotType.TypeGuid);
            spot.TypeGuid = Type.TypeGuid;
            ViewBag.TypeGuid = new SelectList(dbTypes.Types, "TypeGuid", "TypeName", spot.TypeGuid);
            return View(spot);
        }

        // POST: Spots/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SpotGuid,SpotName,Description,Longitude,Latitude,TypeGuid")] SpotEditViewModel spotEditViewModel)
        {
            var spotTypeFind = dbSpotTypes.SpotsTypes.First(x => x.SpotGuid == spotEditViewModel.SpotGuid);

            var spotTypeGuid = spotTypeFind.SpotTypeGuid;

            if (ModelState.IsValid)
            {
                Spot spot = db.Spots.Find(spotEditViewModel.SpotGuid);
                spot.SpotName = spotEditViewModel.SpotName;
                spot.Description = spotEditViewModel.Description;
                spot.Longitude = spotEditViewModel.Longitude;
                spot.Latitude = spotEditViewModel.Latitude;

                spot.DateModified = DateTime.Now;
                spot.UserModifiedID = Auxiliaries.GetUserId(User);

                SpotType spotType = dbSpotTypes.SpotsTypes.Find(spotTypeGuid);
                spotType.TypeGuid = spotEditViewModel.TypeGuid;
                spotType.DateModified = DateTime.Now;

                db.Entry(spot).State = EntityState.Modified;
                db.SaveChanges();

                dbSpotTypes.Entry(spotType).State = EntityState.Modified;
                dbSpotTypes.SaveChanges();

                return RedirectToAction("Index");
            }
            ViewBag.TypeGuid = new SelectList(dbTypes.Types, "TypeGuid", "TypeName", spotEditViewModel.TypeGuid);
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
            var spotType = dbSpotTypes.SpotsTypes.First(x => x.SpotGuid == id.Value);

            var Type = dbTypes.Types.First(x => x.TypeGuid == spotType.TypeGuid);

            ViewBag.typeDelete = Type;
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
