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
    public class CommentsController : Controller
    {
        private CommentsDbContext db = new CommentsDbContext();

        // GET: Comments
        public ActionResult Index()
        {
            var comments = db.Comments.Include(c => c.Spot);
            return View(comments.ToList());
        }

        // GET: Comments/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // GET: Comments/Create
        public ActionResult Create()
        {
            ViewBag.SpotGuid = new SelectList(db.Spots, "SpotGuid", "SpotName");
            return View();
        }

        // POST: Comments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SpotGuid,Title,Text")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                comment.CommentGuid = Guid.NewGuid();

                comment.DateCreated = DateTime.Now;
                comment.DateModified = comment.DateCreated;
                comment.UserCreatedID = Auxiliaries.GetUserId(User);
                comment.UserModifiedID = Auxiliaries.GetUserId(User);

                db.Comments.Add(comment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SpotGuid = new SelectList(db.Spots, "SpotGuid", "SpotName", comment.SpotGuid);
            return View(comment);
        }

        // GET: Comments/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            ViewBag.SpotGuid = new SelectList(db.Spots, "SpotGuid", "SpotName", comment.SpotGuid);
            return View(comment);
        }

        // POST: Comments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CommentGuid,SpotGuid,Title,Text")] CommentEditViewModel commentEditViewModel)
        {
            if (ModelState.IsValid)
            {
                Comment comment = db.Comments.Find(commentEditViewModel.CommentGuid);
                comment.SpotGuid = commentEditViewModel.SpotGuid;
                comment.Title = commentEditViewModel.Title;
                comment.Text = commentEditViewModel.Text;
                
                comment.DateModified = DateTime.Now;
                comment.UserModifiedID = Auxiliaries.GetUserId(User);

                db.Entry(comment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SpotGuid = new SelectList(db.Spots, "SpotGuid", "SpotName", commentEditViewModel.SpotGuid);
            return View(commentEditViewModel);
        }

        // GET: Comments/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Comment comment = db.Comments.Find(id);
            db.Comments.Remove(comment);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //[HttpPost, ActionName("DeleteUser")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmedUser(Guid id)
        {
            Comment comment = db.Comments.Find(id);
            db.Comments.Remove(comment);
            db.SaveChanges();
            return RedirectToAction("Details", "Home", routeValues: new { id = comment.SpotGuid } );
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
