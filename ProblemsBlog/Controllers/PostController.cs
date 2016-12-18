using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
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

            //latest post
            ViewData["LatestPost"] = db.Post.OrderByDescending(p => p.Time).Take(3);
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

            //ViewData["AllComments"] = db.Comments.Where(p => p.PostId == postid);
            
          



            //join value of comenter pic and comment

            var joinimage= (from usr in db.Users
                join cmnt in db.Comments on usr.UserId equals cmnt.UserId
                where cmnt.PostId == postid
                select new
                {
                    usr.Name,
                    usr.Image,
                    cmnt.UserComment

                }).ToList();
            List<UserJoinComment> aJoinCommentList = new List<UserJoinComment>();

           
            {
                foreach (var v in joinimage)
                {
                    UserJoinComment aJoinComment = new UserJoinComment();

                    aJoinComment.Comment = v.UserComment;
                    aJoinComment.CommenterName = v.Name;
                    aJoinComment.Image = v.Image;
                    aJoinCommentList.Add(aJoinComment);
                    ViewData["CommentsData"] = aJoinCommentList;

                }
            }



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

            //int a = Convert.ToInt32(Session["UserId"]);

            ////commenter picture
            //ViewData["CommentImage"] = db.Users.Where(b => b.Image == a.ToString());

           
                if (ModelState.IsValid)
                {
                    db.Comments.Add(aComment);
                    db.SaveChanges();
                    return RedirectToAction("Details");
                }

                 return View(aComment);


        }

        public ActionResult UserSinglePostDetails( int? id)
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



            // commenter name and image

            var joinimage = (from usr in db.Users
                             join cmnt in db.Comments on usr.UserId equals cmnt.UserId
                             where cmnt.PostId == postid
                             select new
                             {
                                 usr.Name,
                                 usr.Image,
                                 cmnt.UserComment

                             }).ToList();
            List<UserJoinComment> aJoinCommentList = new List<UserJoinComment>();


            {
                foreach (var v in joinimage)
                {
                    UserJoinComment aJoinComment = new UserJoinComment();

                    aJoinComment.Comment = v.UserComment;
                    aJoinComment.CommenterName = v.Name;
                    aJoinComment.Image = v.Image;
                    aJoinCommentList.Add(aJoinComment);
                    ViewData["CommentsData"] = aJoinCommentList;

                }
            }



            return View();

        }
        
        [HttpPost]
        public ActionResult UserSinglePostDetails(Comment aComment)
        {


            if (Session["Author"] == null)
            {
                return RedirectToAction("Login", "Registration");
            }


            int postid = Convert.ToInt32(Session["PostId"]);



            // commenter name and image

            var joinimage = (from usr in db.Users
                             join cmnt in db.Comments on usr.UserId equals cmnt.UserId
                             where cmnt.PostId == postid
                             select new
                             {
                                 usr.Name,
                                 usr.Image,
                                 cmnt.UserComment

                             }).ToList();
            List<UserJoinComment> aJoinCommentList = new List<UserJoinComment>();


            {
                foreach (var v in joinimage)
                {
                    UserJoinComment aJoinComment = new UserJoinComment();

                    aJoinComment.Comment = v.UserComment;
                    aJoinComment.CommenterName = v.Name;
                    aJoinComment.Image = v.Image;
                    aJoinCommentList.Add(aJoinComment);
                    ViewData["CommentsData"] = aJoinCommentList;

                }
            }














            aComment.UserId = Convert.ToInt32(Session["UserId"]);
            aComment.UserName = Session["Author"].ToString();
            aComment.PostId = Convert.ToInt32(Session["PostId"]);

            if (ModelState.IsValid)
            {
                db.Comments.Add(aComment);
                db.SaveChanges();
                return RedirectToAction("UserSinglePostDetails");
            }

            return View(aComment);
        }

        public ActionResult DeletePost(int id)
        {
            UserPost userpost = db.Post.Find(id);
            db.Post.Remove(userpost);
            db.SaveChanges();
            return RedirectToAction("UserProfile", "Registration");
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
            return RedirectToAction("UserProfile","Registration");
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
