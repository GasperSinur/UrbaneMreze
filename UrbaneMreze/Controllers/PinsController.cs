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
            Pin pin = db.Pins.Find(id);
            if (pin == null)
            {
                return HttpNotFound();
            }

            PinAllViewModel pinAllViewModel = new PinAllViewModel();
            pinAllViewModel.PinGuid = pin.PinGuid;
            pinAllViewModel.Name = pin.Name;
            pinAllViewModel.Color = pin.Color;
            pinAllViewModel.Description = pin.Description;
            pinAllViewModel.DateCreated = pin.DateCreated;
            pinAllViewModel.DateModified = pin.DateModified;
            pinAllViewModel.UserCreatedID = pin.UserCreatedID;
            pinAllViewModel.UserModifiedID = pin.UserModifiedID;

            if (pin.Icon != null && pin.Icon.Length > 0)
            {
                pinAllViewModel.Icon = new MemoryPostedFile(pin.Icon);

                var base64 = Convert.ToBase64String(pin.Icon);
                var imgSrc = String.Format("data:image/gif;base64,{0}", base64);
                ViewBag.ImgSrc = imgSrc;
            }

            return View(pinAllViewModel);
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
                pin.UserCreatedID = Auxiliaries.GetUserId(User);
                pin.UserModifiedID = Auxiliaries.GetUserId(User);

                // Handle the icon
                if (pinViewModel.Icon != null && pinViewModel.Icon.ContentLength > 0)
                {
                    if (!Auxiliaries.ValidImageTypes.Contains(pinViewModel.Icon.ContentType))
                    {
                        ModelState.AddModelError("Icon", "Izberite sliko, ki je v enem od naštetih formatov: GIF, JPG, ali PNG.");
                        return View(pinViewModel);
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
            Pin pin = db.Pins.Find(id);
            if (pin == null)
            {
                return HttpNotFound();
            }

            PinEditViewModel pinEditViewModel = new PinEditViewModel();
            pinEditViewModel.PinGuid = pin.PinGuid;
            pinEditViewModel.Name = pin.Name;
            pinEditViewModel.Color = pin.Color;
            pinEditViewModel.Description = pin.Description;
            
            if (pin.Icon != null && pin.Icon.Length > 0)
            {
                pinEditViewModel.Icon = new MemoryPostedFile(pin.Icon);

                var base64 = Convert.ToBase64String(pin.Icon);
                var imgSrc = String.Format("data:image/gif;base64,{0}", base64);
                ViewBag.ImgSrc = imgSrc;
            }

            return View(pinEditViewModel);
        }

        // POST: Pins/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PinGuid,Name,Icon,Color,Description")] PinEditViewModel pinEditViewModel)
        {
            if (ModelState.IsValid)
            {
                Pin pin = db.Pins.Find(pinEditViewModel.PinGuid);
                pin.Name = pinEditViewModel.Name;
                pin.Color = pinEditViewModel.Color;
                pin.Description = pinEditViewModel.Description;

                pin.DateModified = DateTime.Now;
                pin.UserModifiedID = Auxiliaries.GetUserId(User);

                // Handle the image
                if (pinEditViewModel.Icon != null && pinEditViewModel.Icon.ContentLength > 0)
                {
                    if (!Auxiliaries.ValidImageTypes.Contains(pinEditViewModel.Icon.ContentType))
                    {
                        ModelState.AddModelError("Icon", "Izberite sliko, ki je v enem od naštetih formatov: GIF, JPG, ali PNG.");
                        if (pin.Icon != null && pin.Icon.Length > 0)
                        {
                            pinEditViewModel.Icon = new MemoryPostedFile(pin.Icon);

                            var base64 = Convert.ToBase64String(pin.Icon);
                            var imgSrc = String.Format("data:image/gif;base64,{0}", base64);
                            ViewBag.ImgSrc = imgSrc;
                        }
                        return View(pinEditViewModel);
                    }
                    else
                    {
                        using (var reader = new BinaryReader(pinEditViewModel.Icon.InputStream))
                        {
                            pin.Icon = reader.ReadBytes(pinEditViewModel.Icon.ContentLength);
                        }
                    }
                }

                db.Entry(pin).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pinEditViewModel);
        }

        // GET: Pins/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pin pin = db.Pins.Find(id);
            if (pin == null)
            {
                return HttpNotFound();
            }
            
            PinAllViewModel pinAllViewModel = new PinAllViewModel();
            pinAllViewModel.PinGuid = pin.PinGuid;
            pinAllViewModel.Name = pin.Name;
            pinAllViewModel.Color = pin.Color;
            pinAllViewModel.Description = pin.Description;
            pinAllViewModel.DateCreated = pin.DateCreated;
            pinAllViewModel.DateModified = pin.DateModified;
            pinAllViewModel.UserCreatedID = pin.UserCreatedID;
            pinAllViewModel.UserModifiedID = pin.UserModifiedID;

            if (pin.Icon != null && pin.Icon.Length > 0)
            {
                pinAllViewModel.Icon = new MemoryPostedFile(pin.Icon);

                var base64 = Convert.ToBase64String(pin.Icon);
                var imgSrc = String.Format("data:image/gif;base64,{0}", base64);
                ViewBag.ImgSrc = imgSrc;
            }

            return View(pinAllViewModel);
        }

        // POST: Pins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Pin pin = db.Pins.Find(id);
            db.Pins.Remove(pin);
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
