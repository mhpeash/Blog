using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.Ajax.Utilities;
using PagedList;
using ProblemsBlog.Context;
using ProblemsBlog.Models;

namespace ProblemsBlog.Controllers
{
    public class SettingsAdminController : Controller
    {
        DatabaseContext db=new DatabaseContext();

        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( AdminControl admin,HttpPostedFileBase file)
        {

            if (file == null)
            {
                admin.Picture = "Images/" + "icon.png";
            }
            else
            {
                string filename = System.IO.Path.GetFileName(file.FileName);

                /*Saving the file in server folder*/
                file.SaveAs(Server.MapPath("~/AdminFolder/" + filename));

               admin.Picture = "AdminFolder/" + filename;
               

            }
            if (ModelState.IsValid)
            {
                db.TblAdminControls.Add(admin);
                db.SaveChanges();
                return RedirectToAction("Login");
            }


            return View(admin);
        }




        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]

        public ActionResult Login(AdminControl admin)
        {
            var checkuser =db.TblAdminControls.FirstOrDefault(a => a.AdminName == admin.AdminName && a.Password == admin.Password);
            
            if (checkuser==null)
            {
                ViewBag.Message = "Invalid UserName or Password";
            }



            else
            {
                Session["AdminName"] = checkuser.AdminName;
                Session["Adminid"] = checkuser.AdminControlId;
                return RedirectToAction("AdminWorld");
            }
            return View();
        }




        public ActionResult AdminWorld()
        {
            
            //admin basic info
            if (Session["Adminid"] == null)
            {
                return RedirectToAction("Login");
            }
            
           
                AdminControl admin = db.TblAdminControls.Find(Session["Adminid"]);
                if (admin == null)
                {
                    return RedirectToAction("Login");
                }

            ViewBag.AdminControl = admin;   

            //end admin basic info

            //last 5 New User
            ViewData["LatestNewUser"]=db.Users.OrderByDescending(i => i.UserId).Take(5);
            //last 5New Post
            ViewData["Latestpost"]=db.Post.OrderByDescending(p => p.Time).Take(5);
            //totaluser
            int a = db.Users.Count();
            ViewBag.Value = a;

            //total Post
             int b= db.Post.Count();
            ViewBag.TotalPost = b;

                 return View();
              
        }


        [HttpPost]
        public ActionResult AdminWorld( MessageToUser message)
        {
            //ADMIN INFO
            
            if (Session["Adminid"] == null)
            {
                return RedirectToAction("Login");
            }


            AdminControl admin = db.TblAdminControls.Find(Session["Adminid"]);
            if (admin == null)
            {
                return RedirectToAction("Login");
            }

            ViewBag.AdminControl = admin;
            //ADMIN INFO ends here

            //last 5 New User
            ViewData["LatestNewUser"] = db.Users.OrderByDescending(i => i.UserId).Take(5);
            //last 5 Post
            ViewData["Latestpost"] = db.Post.OrderByDescending(p => p.Time).Take(5);

            //totaluser
            int a = db.Users.Count();
            ViewBag.Value = a;

            //total Post
            int b = db.Post.Count();
            ViewBag.TotalPost = b;


            //set date for user message
            message.Date = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.TblToUser.Add(message);
                db.SaveChanges();
                return RedirectToAction("AdminWorld");
            }

            return View(message);
        }

        public ActionResult PostDetails(string searchBy, string searchitem, int? page)
        {
            if (Session["Adminid"] == null)
            {
                return RedirectToAction("Login");
            }

            if (searchBy == "Author")
            {

                return View(db.Post.Where(e => e.Author.StartsWith(searchitem) || searchitem == null).ToList().ToPagedList(page ?? 1, 5));
            }
            else
            {
                return View(db.Post.Where(e => e.PostTitle.StartsWith(searchitem) || searchitem == null).ToList().ToPagedList(page ?? 1, 5));


            }
        }

        public ActionResult SingleUserPost( int? id)
        {
            if (Session["Adminid"] == null)
            {
                return RedirectToAction("Login");
            }

            if (id  == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

           
            UserPost userpost = db.Post.Find(id);

            if (userpost == null)
            {
                return HttpNotFound();
            }

            //send user basic info to profile

            ViewBag.UserPost = userpost;

          

            // commenter name and image

            var joinimage = (from usr in db.Users
                             join cmnt in db.Comments on usr.UserId equals cmnt.UserId
                             where cmnt.PostId == id
                             select new
                             {
                                 usr.Name,
                                 usr.Image,
                                 cmnt.UserComment,
                                 cmnt.Time

                             }).ToList();
            List<UserJoinComment> aJoinCommentList = new List<UserJoinComment>();


            {
                foreach (var v in joinimage)
                {
                    UserJoinComment aJoinComment = new UserJoinComment();

                    aJoinComment.Comment = v.UserComment;
                    aJoinComment.CommenterName = v.Name;
                    aJoinComment.Image = v.Image;
                    aJoinComment.Time = v.Time;
                    aJoinCommentList.Add(aJoinComment);
                    ViewData["CommentsData"] = aJoinCommentList;

                }
            }



            return View();

           
        }



        public ActionResult DeletePost(int id)
        {

            
            if (Session["Adminid"]!=null)
            {
                UserPost userpost = db.Post.Find(id);
                db.Post.Remove(userpost);
                db.SaveChanges();
                return RedirectToAction("PostDetails", "SettingsAdmin");
 
 

            }
            return RedirectToAction("Login");

        }






        public ActionResult AdminLogout()
        {
            Session.Clear();
            Session.RemoveAll();
            Session.Abandon();
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
            Response.Cache.SetNoStore();

            FormsAuthentication.SignOut();

            
            return RedirectToAction("Login");
        }



        public ActionResult EditInfo(int? id)
        {
            if (Session["Adminid"] == null)
            {
                return RedirectToAction("Login");
            }

            if (id == null)
            {
                return RedirectToAction("Login");
            }
               AdminControl admin= db.TblAdminControls.Find(id);

               return View(admin);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditInfo(HttpPostedFileBase file,AdminControl admin)
        {
            if (file != null)
            {
                string filename = System.IO.Path.GetFileName(file.FileName);

                /*Saving the file in server folder*/
                file.SaveAs(Server.MapPath("~/AdminFolder/" + filename));

                admin.Picture = "AdminFolder/" + filename;
            }
            if (ModelState.IsValid)
            {
                db.Entry(admin).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("AdminWorld");
            }
            return View(admin);

        }



        public ActionResult CheckUserMessages()
        {
            if (Session["Adminid"] == null)
            {
                return RedirectToAction("Login");
            }


            return View(db.TblFromUser.OrderByDescending(e => e.Time).ToList());
        }


    }
}