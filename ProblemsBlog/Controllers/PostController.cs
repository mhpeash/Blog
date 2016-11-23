using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProblemsBlog.Core.BLL;
using ProblemsBlog.Models;
using ProblemsBlog.Context;

namespace ProblemsBlog.Controllers
{
    public class PostController : Controller
    {
        private DatabaseContext db = new DatabaseContext();
        private UserManager aManager=new UserManager();

    
        //public ActionResult Index()
        //{
        //    return View(db.Post.ToList());
        //}

        public ActionResult Home()
        {
           

            return View(db.Post.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Session["PostId"] = Convert.ToInt32(id);
            UserPost userpost = db.Post.Find(id);
            if (userpost == null)
            {
                return HttpNotFound();
            }
          
            ViewBag.UserPost = userpost;

            //return View(userpost);
            return View();
        }

        [HttpPost]
        public ActionResult Details(Comment aComment)
        {
            aComment.UserId = Convert.ToInt32(Session["UserId"]);
            aComment.UserName = Session["Username"].ToString();
            aComment.PostId = Convert.ToInt32(Session["PostId"]);
                 if (ModelState.IsValid)
                {
                    db.Comments.Add(aComment);
                    db.SaveChanges();
                    return RedirectToAction("Details");
                }

                 return View(aComment);


        }



        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserPost userpost = db.Post.Find(id);
            if (userpost == null)
            {
                return HttpNotFound();
            }
            return View(userpost);
        }

     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="UserPostId,UserId,PostContent,PostTitle,Author,Image,Time,Tempvalue")] UserPost userpost)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userpost).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userpost);
        }

    
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserPost userpost = db.Post.Find(id);
            if (userpost == null)
            {
                return HttpNotFound();
            }
            return View(userpost);
        }

     
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserPost userpost = db.Post.Find(id);
            db.Post.Remove(userpost);
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
