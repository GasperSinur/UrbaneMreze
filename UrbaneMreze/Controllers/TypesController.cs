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
    [Authorize(Roles = "SuperAdmin")]
    public class TypesController : Controller
    {
        private TypesDbContext db = new TypesDbContext();

        // GET: Types
        public ActionResult Index()
        {
            var types = db.Types.Include(t => t.Entity).Include(t => t.Pin);
            return View(types.ToList());
        }

        // GET: Types/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Models.Type type = db.Types.Find(id);
            if (type == null)
            {
                return HttpNotFound();
            }
            return View(type);
        }

        // GET: Types/Create
        public ActionResult Create()
        {
            ViewBag.EntityGuid = new SelectList(db.Entities, "EntityGuid", "EntityName");
            ViewBag.PinGuid = new SelectList(db.Pins, "PinGuid", "Name");
            return View();
        }

        // POST: Types/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EntityGuid,PinGuid,TypeName,Description")] Models.Type type)
        {
            if (ModelState.IsValid)
            {
                type.TypeGuid = Guid.NewGuid();

                type.DateCreated = DateTime.Now;
                type.DateModified = type.DateCreated;
                type.UserCreatedID = Auxiliaries.GetUserId(User);
                type.UserModifiedID = Auxiliaries.GetUserId(User);

                db.Types.Add(type);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EntityGuid = new SelectList(db.Entities, "EntityGuid", "EntityName", type.EntityGuid);
            ViewBag.PinGuid = new SelectList(db.Pins, "PinGuid", "Name", type.PinGuid);
            return View(type);
        }

        // GET: Types/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Models.Type type = db.Types.Find(id);
            if (type == null)
            {
                return HttpNotFound();
            }
            ViewBag.EntityGuid = new SelectList(db.Entities, "EntityGuid", "EntityName", type.EntityGuid);
            ViewBag.PinGuid = new SelectList(db.Pins, "PinGuid", "Name", type.PinGuid);
            return View(type);
        }

        // POST: Types/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TypeGuid,EntityGuid,PinGuid,TypeName,Description")] TypeEditViewModel typeEditViewModel)
        {
            if (ModelState.IsValid)
            {
                Models.Type type = db.Types.Find(typeEditViewModel.TypeGuid);
                type.TypeName = typeEditViewModel.TypeName;
                type.Description = typeEditViewModel.Description;

                type.DateModified = DateTime.Now;
                type.UserModifiedID = Auxiliaries.GetUserId(User);

                db.Entry(type).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EntityGuid = new SelectList(db.Entities, "EntityGuid", "EntityName", typeEditViewModel.EntityGuid);
            ViewBag.PinGuid = new SelectList(db.Pins, "PinGuid", "Name", typeEditViewModel.PinGuid);
            return View(typeEditViewModel);
        }

        // GET: Types/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Models.Type type = db.Types.Find(id);
            if (type == null)
            {
                return HttpNotFound();
            }
            return View(type);
        }

        // POST: Types/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Models.Type type = db.Types.Find(id);
            db.Types.Remove(type);
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
