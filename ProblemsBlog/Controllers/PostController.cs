using System;
using System.Collections;
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

  

        public ActionResult Home()
        {
             return View(db.Post.OrderByDescending(x => x.Time).ToList());

         
        }

        public ActionResult UserHome()
        {
            return View(db.Post.OrderByDescending(x => x.Time).ToList());
        }

        //Single PostDetails Details
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
          
            //send user basic info to profile

            ViewBag.UserPost = userpost;

            int postid = Convert.ToInt32(Session["PostId"]);

            //select all comments by postid

            ViewData["AllComments"] = db.Comments.Where(p => p.PostId == postid);

                 return View();
        }

       
        //Single PostDetails Details

        [HttpPost]
        public ActionResult Details(Comment aComment)
        {

            if (Session["Author"] == null)
            {
                return RedirectToAction("Login","Registration");
            }
            aComment.UserId = Convert.ToInt32(Session["UserId"]);
            aComment.UserName = Session["Author"].ToString();
            aComment.PostId = Convert.ToInt32(Session["PostId"]);

           // int a  = Convert.ToInt32(Session["UserId"]); 

           //commenter picture
            //db.Users.Where(b => b.Image ==a.ToString());

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
                return RedirectToAction("UserProfile","Registration");
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
            return RedirectToAction("Home");
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
