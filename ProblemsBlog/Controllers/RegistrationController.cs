using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ProblemsBlog.Core.BLL;
using ProblemsBlog.Models;
using ProblemsBlog.Context;

namespace ProblemsBlog.Controllers
{
    public class RegistrationController : Controller
    {
        private DatabaseContext db = new DatabaseContext();
        private UserManager aManager=new UserManager();

        // GET: /Registration/
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        // GET: /Registration/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: /Registration/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Registration/Create
      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserId,Name,Email,UserName,Password,ConfirmPassword,Image,Location")] User user, HttpPostedFileBase file)
        {

            if (file == null)
            {
                user.Image = "Images/" + "icon.png";
            }
        else
            {
                string filename = System.IO.Path.GetFileName(file.FileName);

                /*Saving the file in server folder*/
                file.SaveAs(Server.MapPath("~/Images/" + filename));

                user.Image = "Images/" + filename;
                
            }

           
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Confirmation");
            }

            return View(user);
        }


        public ActionResult Confirmation()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]

        public ActionResult Login(User user)
        {
            var loggedInUser = db.Users.FirstOrDefault(c => c.UserName == user.UserName && c.Password == user.Password);



            if (loggedInUser == null)
            {

                ViewBag.Message = "Invalid UserName or Password";

            }
            else
            {

                Session["UserId"] = loggedInUser.UserId.ToString();
                Session["Username"] = loggedInUser.UserName;
                Session["Author"] = loggedInUser.Name;
                return RedirectToAction("UserProfile");
            }
            return View();

        }

        public ActionResult UserProfile()
        {

            if (Session["UserId"] != null)
            {
                //all user basic onfo
                Models.User aUser = aManager.GetAllUserInfo(Convert.ToInt32(Session["UserId"]));
                ViewBag.User = aUser;

                // all status from user db
                ViewData["AllStatus"] = aManager.GetAllPostbyUserID(Convert.ToInt32(Session["UserId"]));



                return View();
            }

            return RedirectToAction("Login");
        }

        [HttpPost]
        public ActionResult UserProfile(UserPost userPost, HttpPostedFileBase file)
        {
            if (Session["UserId"] != null)

            {

                //all user basic onfo
                Models.User aUser = aManager.GetAllUserInfo(Convert.ToInt32(Session["UserId"]));
                ViewBag.User = aUser;

                // all status from user db
                ViewData["AllStatus"] = aManager.GetAllPostbyUserID(Convert.ToInt32(Session["UserId"]));




                userPost.UserId = Convert.ToInt32(Session["UserId"]);
                userPost.Time = DateTime.Now;
                userPost.Author = Session["Author"].ToString();

                if (file == null)
                {
                    userPost.Image = "Images/" + "logo.jpg";
                }
                else
                {
                    string filename = System.IO.Path.GetFileName(file.FileName);

                    /*Saving the file in server folder*/
                    file.SaveAs(Server.MapPath("~/Images/" + filename));

                    userPost.Image = "Images/" + filename;
                }

                if (ModelState.IsValid)
                {
                    db.Post.Add(userPost);
                    db.SaveChanges();
                    return RedirectToAction("UserProfile");
                }

                return View(userPost);
            }

            return RedirectToAction("Login");
        }


        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            
            Session.Abandon();

            HttpCookie aCookie = new HttpCookie(FormsAuthentication.FormsCookieName, "");
            aCookie.Expires = DateTime.Now.AddMonths(-5);
            Response.Cookies.Add(aCookie);

            return RedirectToAction("Login");
        }

        // GET: /Registration/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: /Registration/Edit/5
     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User user, HttpPostedFileBase file)
        {
            
            string filename = System.IO.Path.GetFileName(file.FileName);

            /*Saving the file in server folder*/
            file.SaveAs(Server.MapPath("~/Images/" + filename));

            user.Image = "Images/" + filename;
            
          
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("UserProfile");
            }
            return View(user);
        }

        // GET: /Registration/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: /Registration/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
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
