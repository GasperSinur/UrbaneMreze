using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UrbaneMreze.Models;

namespace UrbaneMreze.Controllers
{
    public class PinsController : Controller
    {
        private PinsDbContext db = new PinsDbContext();

        // GET: Pins
        public ActionResult Index()
        {
            return View(db.Pins.ToList());
        }

        // GET: Pins/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pin pins = db.Pins.Find(id);
            if (pins == null)
            {
                return HttpNotFound();
            }
            return View(pins);
        }

        // GET: Pins/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pins/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Icon,Color,Description")] PinViewModel pinViewModel)
        {
            if (ModelState.IsValid)
            {
                Pin pin = new Pin();
                pin.PinGuid = Guid.NewGuid();
                pin.Name = pinViewModel.Name;
                pin.Color = pinViewModel.Color;
                pin.Description = pinViewModel.Description;

                pin.DateCreated = DateTime.Now;
                pin.DateModified = DateTime.Now;
                pin.UserCreatedID = User.Identity.GetUserId();
                pin.UserModifiedID = User.Identity.GetUserId();

                // Handle the icon
                if (pinViewModel.Icon != null && pinViewModel.Icon.ContentLength > 0)
                {
                    if (!Auxiliaries.ValidImageTypes.Contains(pinViewModel.Icon.ContentType))
                    {
                        ModelState.AddModelError("Icon", "Choose an image in one of the following formats: GIF, JPG, or PNG.");
                    }
                    else
                    {
                        using (var reader = new BinaryReader(pinViewModel.Icon.InputStream))
                        {
                            pin.Icon = reader.ReadBytes(pinViewModel.Icon.ContentLength);
                        }
                    }
                }

                db.Pins.Add(pin);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pinViewModel);
        }

        // GET: Pins/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pin pins = db.Pins.Find(id);
            if (pins == null)
            {
                return HttpNotFound();
            }
            return View(pins);
        }

        // POST: Pins/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PinGuid,Name,Icon,Color,Description")] Pin pins)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pins).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pins);
        }

        // GET: Pins/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pin pins = db.Pins.Find(id);
            if (pins == null)
            {
                return HttpNotFound();
            }
            return View(pins);
        }

        // POST: Pins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Pin pins = db.Pins.Find(id);
            db.Pins.Remove(pins);
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
