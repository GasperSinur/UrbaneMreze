using Microsoft.AspNet.Identity;
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
    public class EntitiesController : Controller
    {
        private EntitiesDbContext db = new EntitiesDbContext();

        // GET: Entities
        public ActionResult Index()
        {
            var entities = db.Entities.Include(e => e.Pin);
            return View(entities.ToList());
        }

        // GET: Entities/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Entity entity = db.Entities.Find(id);
            if (entity == null)
            {
                return HttpNotFound();
            }
            return View(entity);
        }

        // GET: Entities/Create
        public ActionResult Create()
        {
            ViewBag.PinGuid = new SelectList(db.Pins, "PinGuid", "Name");
            return View();
        }

        // POST: Entities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PinGuid,EntityName,Description")] Entity entity)
        {
            if (ModelState.IsValid)
            {
                entity.EntityGuid = Guid.NewGuid();
                
                entity.DateCreated = DateTime.Now;
                entity.DateModified = entity.DateCreated;
                entity.UserCreatedID = Auxiliaries.GetUserId(User);
                entity.UserModifiedID = Auxiliaries.GetUserId(User);

                db.Entities.Add(entity);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PinGuid = new SelectList(db.Pins, "PinGuid", "Name", entity.PinGuid);
            return View(entity);
        }

        // GET: Entities/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Entity entity = db.Entities.Find(id);
            if (entity == null)
            {
                return HttpNotFound();
            }
            ViewBag.PinGuid = new SelectList(db.Pins, "PinGuid", "Name", entity.PinGuid);
            return View(entity);
        }

        // POST: Entities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EntityGuid,PinGuid,EntityName,Description")] EntityEditViewModel entityEditViewModel)
        {
            if (ModelState.IsValid)
            {
                Entity entity = db.Entities.Find(entityEditViewModel.EntityGuid);
                entity.PinGuid = entityEditViewModel.PinGuid;
                entity.EntityName = entityEditViewModel.EntityName;
                entity.Description = entityEditViewModel.Description;

                entity.DateModified = DateTime.Now;
                entity.UserModifiedID = Auxiliaries.GetUserId(User);

                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PinGuid = new SelectList(db.Pins, "PinGuid", "Name", entityEditViewModel.PinGuid);
            return View(entityEditViewModel);
        }

        // GET: Entities/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Entity entity = db.Entities.Find(id);
            if (entity == null)
            {
                return HttpNotFound();
            }
            return View(entity);
        }

        // POST: Entities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Entity entity = db.Entities.Find(id);
            db.Entities.Remove(entity);
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
