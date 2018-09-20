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
using PagedList;

namespace UrbaneMreze.Controllers
{
    public class HomeController : Controller
    {
        private SpotsDbContext dbSpots = new SpotsDbContext();
        private CommentsDbContext dbComments = new CommentsDbContext();
        private PhotosDbContext dbPhotos = new PhotosDbContext();
        private TypesDbContext dbTypes = new TypesDbContext();
        private SpotsTypesDbContext dbSpotTypes = new SpotsTypesDbContext();
        private PinsDbContext dbPins = new PinsDbContext();
        private ApplicationDbContext dbApp = new ApplicationDbContext();

        public ActionResult Index(string sortOrder, int? page)
        {
            string MarkersString = "[";

            var SpotArray = from x in dbSpots.Spots
                       select new { x.SpotGuid, x.SpotName, x.Latitude, x.Longitude, x.Description };

            var SpotTypes = dbSpotTypes.SpotsTypes;

            foreach (var i in SpotArray)
            {
                MarkersString += "{";
                MarkersString += String.Format("title: '{0}',", i.SpotName);
                MarkersString += String.Format("lat: '{0}',", i.Latitude);
                MarkersString += String.Format("lng: '{0}',", i.Longitude);
                MarkersString += String.Format("description: '{0}',", i.Description);
                foreach (var item in SpotTypes)
                {
                    if (i.SpotGuid == item.SpotGuid)
                    {
                        var Types = dbTypes.Types.First(x => x.TypeGuid == item.TypeGuid);
                        if (Types.TypeName== "Zanimivost")
                        {
                            MarkersString+=String.Format("url: '{0}'", "/Images/pinBlue.png");
                        }
                        else if (Types.TypeName == "Zapostavljena mesta")
                        {
                            MarkersString += String.Format("url: '{0}'", "/Images/pinRed.png");
                        }
                        else if (Types.TypeName == "Urejena zapostavljena mesta")
                        {
                            MarkersString += String.Format("url: '{0}'", "/Images/pinGreen.png");
                        }
                    }
                }
                MarkersString += "},";
            }

            MarkersString+= "]";
            ViewBag.Markers = MarkersString;

            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            var spots = from s in dbSpots.Spots
                        select s;

            List<SpotLight> spotLight = new List<SpotLight>();

            foreach (var item in spots)
            {
                SpotLight itemLight = new SpotLight();
                itemLight.SpotGuid = item.SpotGuid;

                foreach (var item2 in SpotTypes)
                {
                    if (item.SpotGuid == item2.SpotGuid)
                    {
                        var Types = dbTypes.Types.First(x => x.TypeGuid == item2.TypeGuid);
                        if (Types.TypeName == "Zanimivost")
                        {
                            itemLight.Style = "Type-Blue";
                        }
                        else if (Types.TypeName == "Zapostavljena mesta")
                        {
                            itemLight.Style = "Type-Red";
                        }
                        else if (Types.TypeName == "Urejena zapostavljena mesta")
                        {
                            itemLight.Style = "Type-Green";
                        }
                    }
                }
                spotLight.Add(itemLight);
            }

            ViewBag.SpotLight = spotLight;

            switch (sortOrder)
            {
                case "name_desc":
                    spots = spots.OrderByDescending(s => s.SpotName);
                    break;
                case "Date":
                    spots = spots.OrderBy(s => s.DateCreated);
                    break;
                case "date_desc":
                    spots = spots.OrderByDescending(s => s.DateCreated);
                    break;
                default:
                    spots = spots.OrderBy(s => s.SpotName);
                    break;
            }

            int pageSize = 6;
            int pageNumber = (page ?? 1);

            ViewBag.SpotList = spots.ToPagedList(pageNumber, pageSize);



            ViewBag.TypeGuid = new SelectList(dbTypes.Types, "TypeGuid", "TypeName");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string sortOrder, int? page, [Bind(Include = "SpotName,Description,Longitude,Latitude,TypeGuid")] Spot spot)
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

                dbSpots.Spots.Add(spot);
                dbSpots.SaveChanges();

                dbSpotTypes.SpotsTypes.Add(spotType);
                dbSpotTypes.SaveChanges();
            }

            string MarkersString = "[";

            var SpotArray = from x in dbSpots.Spots
                            select new { x.SpotGuid, x.SpotName, x.Latitude, x.Longitude, x.Description };

            var SpotTypes = dbSpotTypes.SpotsTypes;

            foreach (var i in SpotArray)
            {
                MarkersString += "{";
                MarkersString += String.Format("title: '{0}',", i.SpotName);
                MarkersString += String.Format("lat: '{0}',", i.Latitude);
                MarkersString += String.Format("lng: '{0}',", i.Longitude);
                MarkersString += String.Format("description: '{0}',", i.Description);
                foreach (var item in SpotTypes)
                {
                    if (i.SpotGuid == item.SpotGuid)
                    {
                        var Types = dbTypes.Types.First(x => x.TypeGuid == item.TypeGuid);
                        if (Types.TypeName == "Zanimivost")
                        {
                            MarkersString += String.Format("url: '{0}'", "/Images/pinBlue.png");
                        }
                        else if (Types.TypeName == "Zapostavljena mesta")
                        {
                            MarkersString += String.Format("url: '{0}'", "/Images/pinRed.png");
                        }
                        else if (Types.TypeName == "Urejena zapostavljena mesta")
                        {
                            MarkersString += String.Format("url: '{0}'", "/Images/pinGreen.png");
                        }
                    }
                }
                MarkersString += "},";
            }

            MarkersString += "]";
            ViewBag.Markers = MarkersString;

            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            var spots = from s in dbSpots.Spots
                        select s;

            List<SpotLight> spotLight = new List<SpotLight>();

            foreach (var item in spots)
            {
                SpotLight itemLight = new SpotLight();
                itemLight.SpotGuid = item.SpotGuid;

                foreach (var item2 in SpotTypes)
                {
                    if (item.SpotGuid == item2.SpotGuid)
                    {
                        var Types = dbTypes.Types.First(x => x.TypeGuid == item2.TypeGuid);
                        if (Types.TypeName == "Zanimivost")
                        {
                            itemLight.Style = "Type-Blue";
                        }
                        else if (Types.TypeName == "Zapostavljena mesta")
                        {
                            itemLight.Style = "Type-Red";
                        }
                        else if (Types.TypeName == "Urejena zapostavljena mesta")
                        {
                            itemLight.Style = "Type-Green";
                        }
                    }
                }
                spotLight.Add(itemLight);
            }

            ViewBag.SpotLight = spotLight;

            switch (sortOrder)
            {
                case "name_desc":
                    spots = spots.OrderByDescending(s => s.SpotName);
                    break;
                case "Date":
                    spots = spots.OrderBy(s => s.DateCreated);
                    break;
                case "date_desc":
                    spots = spots.OrderByDescending(s => s.DateCreated);
                    break;
                default:
                    spots = spots.OrderBy(s => s.SpotName);
                    break;
            }

            int pageSize = 6;
            int pageNumber = (page ?? 1);

            ViewBag.SpotList = spots.ToPagedList(pageNumber, pageSize);

            if (ModelState.IsValid)
            {
                ModelState.Clear();
            }
            else
            {
                TryUpdateModel(spot);
            }

            ViewBag.TypeGuid = new SelectList(dbTypes.Types, "TypeGuid", "TypeName", spot.TypeGuid);

            return View();
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

            var SpotTypes = dbSpotTypes.SpotsTypes;

            string MarkerString = "{";
            MarkerString += String.Format("title: '{0}', ", spot.SpotName);
            MarkerString += String.Format("lat: '{0}', ", spot.Latitude);
            MarkerString += String.Format("lng: '{0}', ", spot.Longitude);
            MarkerString += String.Format("description: '{0}',", spot.Description);
            foreach (var item in SpotTypes)
            {
                if (spot.SpotGuid == item.SpotGuid)
                {
                    var Types = dbTypes.Types.First(x => x.TypeGuid == item.TypeGuid);
                    if (Types.TypeName == "Zanimivost")
                    {
                        MarkerString += String.Format("url: '{0}'", "/Images/pinBlue.png");
                    }
                    else if (Types.TypeName == "Zapostavljena mesta")
                    {
                        MarkerString += String.Format("url: '{0}'", "/Images/pinRed.png");
                    }
                    else if (Types.TypeName == "Urejena zapostavljena mesta")
                    {
                        MarkerString += String.Format("url: '{0}'", "/Images/pinGreen.png");
                    }
                }
            }
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

            var SpotTypes = dbSpotTypes.SpotsTypes;

            string MarkerString = "{";
            MarkerString += String.Format("title: '{0}', ", spot.SpotName);
            MarkerString += String.Format("lat: '{0}', ", spot.Latitude);
            MarkerString += String.Format("lng: '{0}', ", spot.Longitude);
            MarkerString += String.Format("description: '{0}',", spot.Description);
            foreach (var item in SpotTypes)
            {
                if (spot.SpotGuid == item.SpotGuid)
                {
                    var Types = dbTypes.Types.First(x => x.TypeGuid == item.TypeGuid);
                    if (Types.TypeName == "Zanimivost")
                    {
                        MarkerString += String.Format("url: '{0}'", "/Images/pinBlue.png");
                    }
                    else if (Types.TypeName == "Zapostavljena mesta")
                    {
                        MarkerString += String.Format("url: '{0}'", "/Images/pinRed.png");
                    }
                    else if (Types.TypeName == "Urejena zapostavljena mesta")
                    {
                        MarkerString += String.Format("url: '{0}'", "/Images/pinGreen.png");
                    }
                }
            }
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

            if (ModelState.IsValid)
            {
                ModelState.Clear();
            }
            else
            {
                TryUpdateModel(comment);
            }

            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Activities()
        {
            var SpotArray = from x in dbSpots.Spots
                            select new { x.SpotGuid };

            var SpotTypes = dbSpotTypes.SpotsTypes;

            var countZapostavljena = 0;
            var countZanimivost = 0;
            var countUrejena = 0;
            foreach (var i in SpotArray)
            {
                foreach (var item in SpotTypes)
                {
                    if (i.SpotGuid == item.SpotGuid)
                    {
                        var Types = dbTypes.Types.First(x => x.TypeGuid == item.TypeGuid);
                        if (Types.TypeName == "Zanimivost")
                        {
                            countZanimivost += 1;
                        }
                        else if (Types.TypeName == "Zapostavljena mesta")
                        {
                            countZapostavljena += 1;
                        }
                        else if (Types.TypeName == "Urejena zapostavljena mesta")
                        {
                            countUrejena += 1;
                        }
                    }
                }
            }

            ViewBag.countZanimivost = countZanimivost;
            ViewBag.countZapostavljena = countZapostavljena;
            ViewBag.countUrejena = countUrejena;

            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}