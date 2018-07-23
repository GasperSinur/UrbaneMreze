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
    public class PhotosController : Controller
    {
        private PhotosDbContext db = new PhotosDbContext();

        // GET: Photos
        public ActionResult Index()
        {
            var photos = db.Photos.Include(p => p.Spot);
            return View(photos.ToList());
        }

        // GET: Photos/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Photo photo = db.Photos.Find(id);
            if (photo == null)
            {
                return HttpNotFound();
            }
            return View(photo);
        }

        // GET: Photos/Create
        public ActionResult Create()
        {
            ViewBag.SpotGuid = new SelectList(db.Spots, "SpotGuid", "SpotName");
            return View();
        }

        // POST: Photos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SpotGuid,Description,Longitude,Latitude,File")] PhotoViewModel photoViewModel)
        {
            if (ModelState.IsValid)
            {
                Photo photo = new Photo();
                photo.PhotoGuid = Guid.NewGuid();
                photo.SpotGuid = photoViewModel.SpotGuid;
                photo.Description = photoViewModel.Description;
                photo.Longitude = photoViewModel.Longitude;
                photo.Latitude = photoViewModel.Latitude;

                photo.DateCreated = DateTime.Now;
                photo.DateModified = DateTime.Now;
                photo.UserCreatedID = Auxiliaries.GetUserId(User);
                photo.UserModifiedID = Auxiliaries.GetUserId(User);

                // Handle the photo
                if (photoViewModel.File != null && photoViewModel.File.ContentLength > 0)
                {
                    if (!Auxiliaries.ValidImageTypes.Contains(photoViewModel.File.ContentType))
                    {
                        ModelState.AddModelError("Icon", "Choose an image in one of the following formats: GIF, JPG, or PNG.");
                    }
                    else
                    {
                        using (var reader = new BinaryReader(photoViewModel.File.InputStream))
                        {
                            photo.File = reader.ReadBytes(photoViewModel.File.ContentLength);
                            photo.Thumbnail = photo.File;
                        }
                    }
                }

                db.Photos.Add(photo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SpotGuid = new SelectList(db.Spots, "SpotGuid", "SpotName", photoViewModel.SpotGuid);
            return View(photoViewModel);
        }

        // GET: Photos/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Photo photo = db.Photos.Find(id);
            if (photo == null)
            {
                return HttpNotFound();
            }
            ViewBag.SpotGuid = new SelectList(db.Spots, "SpotGuid", "SpotName", photo.SpotGuid);
            return View(photo);
        }

        // POST: Photos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PhotoGuid,SpotGuid,Description,Longitude,Latitude,File,Thumbnail,ContentType,DateCreated,DateModified,UserCreatedID,UserModifiedID")] Photo photo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(photo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SpotGuid = new SelectList(db.Spots, "SpotGuid", "SpotName", photo.SpotGuid);
            return View(photo);
        }

        // GET: Photos/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Photo photo = db.Photos.Find(id);
            if (photo == null)
            {
                return HttpNotFound();
            }
            return View(photo);
        }

        // POST: Photos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Photo photo = db.Photos.Find(id);
            db.Photos.Remove(photo);
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
