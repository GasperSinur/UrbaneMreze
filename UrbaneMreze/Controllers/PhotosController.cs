﻿using System;
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

            PhotoAllViewModel photoAllViewModel = new PhotoAllViewModel();
            photoAllViewModel.PhotoGuid = photo.PhotoGuid;
            photoAllViewModel.SpotGuid = photo.SpotGuid;
            photoAllViewModel.Description = photo.Description;
            photoAllViewModel.Longitude = photo.Longitude;
            photoAllViewModel.Latitude = photo.Latitude;
            photoAllViewModel.DateCreated = photo.DateCreated;
            photoAllViewModel.DateModified = photo.DateModified;
            photoAllViewModel.UserCreatedID = photo.UserCreatedID;
            photoAllViewModel.UserModifiedID = photo.UserModifiedID;

            if (photo.File != null && photo.File.Length > 0)
            {
                photoAllViewModel.File = new MemoryPostedFile(photo.File);

                var base64 = Convert.ToBase64String(photo.File);
                var imgSrc = String.Format("data:image/gif;base64,{0}", base64);
                ViewBag.ImgSrc = imgSrc;
            }

            return View(photoAllViewModel);
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
        public ActionResult Create([Bind(Include = "PhotoGuid,SpotGuid,Description,Longitude,Latitude,File")] PhotoViewModel photoViewModel)
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

                // Handle the icon
                if (photoViewModel.File != null && photoViewModel.File.ContentLength > 0)
                {
                    if (!Auxiliaries.ValidImageTypes.Contains(photoViewModel.File.ContentType))
                    {
                        ModelState.AddModelError("File", "Izberite sliko, ki je v enem od naštetih formatov: GIF, JPG, ali PNG.");
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

            PhotoEditViewModel photoEditViewModel = new PhotoEditViewModel();
            photoEditViewModel.PhotoGuid = photo.PhotoGuid;
            photoEditViewModel.SpotGuid = photo.SpotGuid;
            photoEditViewModel.Description = photo.Description;
            photoEditViewModel.Longitude = photo.Longitude;
            photoEditViewModel.Latitude = photo.Latitude;

            if (photo.File != null && photo.File.Length > 0)
            {
                photoEditViewModel.File = new MemoryPostedFile(photo.File);

                var base64 = Convert.ToBase64String(photo.File);
                var imgSrc = String.Format("data:image/gif;base64,{0}", base64);
                ViewBag.ImgSrc = imgSrc;
            }

            return View(photoEditViewModel);
        }

        // POST: Photos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PhotoGuid,SpotGuid,Description,Longitude,Latitude,File")] PhotoEditViewModel photoEditViewModel)
        {
            if (ModelState.IsValid)
            {
                Photo photo = db.Photos.Find(photoEditViewModel.PhotoGuid);
                photo.PhotoGuid = photoEditViewModel.PhotoGuid;
                photo.SpotGuid = photoEditViewModel.SpotGuid;
                photo.Description = photoEditViewModel.Description;
                photo.Longitude = photoEditViewModel.Longitude;
                photo.Latitude = photoEditViewModel.Latitude;

                photo.DateModified = DateTime.Now;
                photo.UserModifiedID = Auxiliaries.GetUserId(User);

                // Handle the photo
                if (photoEditViewModel.File != null && photoEditViewModel.File.ContentLength > 0)
                {
                    if (!Auxiliaries.ValidImageTypes.Contains(photoEditViewModel.File.ContentType))
                    {
                        ModelState.AddModelError("File", "Izberite sliko, ki je v enem od naštetih formatov: GIF, JPG, ali PNG.");
                    }
                    else
                    {
                        using (var reader = new BinaryReader(photoEditViewModel.File.InputStream))
                        {
                            photo.File = reader.ReadBytes(photoEditViewModel.File.ContentLength);
                            photo.Thumbnail = photo.File;
                        }
                    }
                }

                db.Entry(photo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SpotGuid = new SelectList(db.Spots, "SpotGuid", "SpotName", photoEditViewModel.SpotGuid);
            return View(photoEditViewModel);
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
