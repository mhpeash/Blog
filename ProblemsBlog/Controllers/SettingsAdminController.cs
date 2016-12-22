using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
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
            //last 5 Post
            ViewData["Latestpost"]=db.Post.OrderByDescending(p => p.Time).Take(5);



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





    }
}