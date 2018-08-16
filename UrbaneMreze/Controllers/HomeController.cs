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

namespace UrbaneMreze.Controllers
{
    public class HomeController : Controller
    {
        private SpotsDbContext dbSpots = new SpotsDbContext();

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

            return View(spot);
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