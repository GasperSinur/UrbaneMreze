using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UrbaneMreze.Models;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Dynamic;

namespace UrbaneMreze.Controllers
{
    public class HomeController : Controller
    {
        private SpotsDbContext dbSpots = new SpotsDbContext();
        private CommentsDbContext dbComments = new CommentsDbContext();
        private PhotosDbContext dbPhotos = new PhotosDbContext();
        private PinsDbContext dbPins = new PinsDbContext();
        private ApplicationDbContext dbApp = new ApplicationDbContext();

        public ActionResult Index()
        {
            string MarkersString = "[";

            var test = from x in dbSpots.Spots
                       select new { x.SpotName, x.Latitude, x.Longitude, x.Description };

            foreach (var i in test)
            {
                MarkersString +="{";
                MarkersString+=String.Format("title: '{0}',", i.SpotName);
                MarkersString+=String.Format("lat: '{0}',", i.Latitude);
                MarkersString+=String.Format("lng: '{0}',", i.Longitude);
                MarkersString+=String.Format("description: '{0}'", i.Description);
                MarkersString += "},";
            }

            MarkersString+= "]";
            ViewBag.Markers = MarkersString;

            return View(dbSpots.Spots.ToList());
        }

        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Spot spot = dbSpots.Spots.Find(id);
            if (spot == null)
            {
                return HttpNotFound();
            }

            string MarkerString = "{";
            MarkerString += String.Format("title: '{0}', ", spot.SpotName);
            MarkerString += String.Format("lat: '{0}', ", spot.Latitude);
            MarkerString += String.Format("lng: '{0}', ", spot.Longitude);
            MarkerString += String.Format("description: '{0}'", spot.Description);
            MarkerString += "}";
            
            ViewBag.Marker = MarkerString;

            var spots = dbSpots.Spots.Where(x => x.SpotGuid == id.Value);
            var comments = dbComments.Comments.Where(x => x.SpotGuid == id.Value);
            var photos = dbPhotos.Photos.Where(x => x.SpotGuid == id.Value);

            foreach (var item in spots)
            {
                item.SpotAuthorUsername = dbApp.Users.Find(item.UserCreatedID.ToString()).UserName;
            }
            ViewBag.Spots = spots;

            foreach (var item in comments)
            {
                item.CommentAuthorUsername = dbApp.Users.Find(item.UserCreatedID.ToString()).UserName;
            }
            ViewBag.Comments = comments;

            List<PhotoLight> photosLight = new List<PhotoLight>();

            foreach (var item in photos)
            {
                PhotoLight itemLight = new PhotoLight();
                itemLight.PhotoGuid = item.PhotoGuid;
                itemLight.Description = item.Description;
                itemLight.DateCreated = item.DateCreated;
                if (item.Thumbnail != null && item.Thumbnail.Length > 0)
                {
                    var base64 = Convert.ToBase64String(item.Thumbnail);
                    var imgSrc = String.Format("data:image/gif;base64,{0}", base64);
                    itemLight.ThumbnailSrc = imgSrc;
                }
                photosLight.Add(itemLight);

            }

            ViewBag.PhotosLight = photosLight;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Details([Bind(Include = "SpotGuid,Title,Text")] Comment comment, Guid? id)
        {
            if (ModelState.IsValid)
            {
                comment.CommentGuid = Guid.NewGuid();
                comment.SpotGuid = id.Value;
                comment.DateCreated = DateTime.Now;
                comment.DateModified = comment.DateCreated;
                comment.UserCreatedID = Auxiliaries.GetUserId(User);
                comment.UserModifiedID = Auxiliaries.GetUserId(User);

                dbComments.Comments.Add(comment);
                dbComments.SaveChanges();
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Spot spot = dbSpots.Spots.Find(id);
            if (spot == null)
            {
                return HttpNotFound();
            }

            string MarkerString = "{";
            MarkerString += String.Format("title: '{0}', ", spot.SpotName);
            MarkerString += String.Format("lat: '{0}', ", spot.Latitude);
            MarkerString += String.Format("lng: '{0}', ", spot.Longitude);
            MarkerString += String.Format("description: '{0}'", spot.Description);
            MarkerString += "}";

            ViewBag.Marker = MarkerString;

            var spots = dbSpots.Spots.Where(x => x.SpotGuid == id.Value);
            var comments = dbComments.Comments.Where(x => x.SpotGuid == id.Value);
            var photos = dbPhotos.Photos.Where(x => x.SpotGuid == id.Value);

            ViewBag.Spots = spots;

            foreach(var item in comments)
            {
                item.CommentAuthorUsername = dbApp.Users.Find(item.UserCreatedID.ToString()).UserName;
            }
            ViewBag.Comments = comments;

            List<PhotoLight> photosLight = new List<PhotoLight>();

            foreach (var item in photos)
            {
                PhotoLight itemLight = new PhotoLight();
                itemLight.PhotoGuid = item.PhotoGuid;
                itemLight.Description = item.Description;
                itemLight.DateCreated = item.DateCreated;
                if (item.Thumbnail != null && item.Thumbnail.Length > 0)
                {
                    var base64 = Convert.ToBase64String(item.Thumbnail);
                    var imgSrc = String.Format("data:image/gif;base64,{0}", base64);
                    itemLight.ThumbnailSrc = imgSrc;
                }
                photosLight.Add(itemLight);
                
            }

            ViewBag.PhotosLight = photosLight;

            

            return View();
        }

            public ActionResult About()
        {
            return View();
        }

        public ActionResult Activities()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}