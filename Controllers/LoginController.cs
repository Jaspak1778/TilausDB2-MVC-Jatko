using TilausDB2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace TilausDB2.Controllers
{
    public class CheckSessionAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session["UserName"] == null)
            {
                filterContext.Result = new RedirectResult("~/home/login");
            }
            base.OnActionExecuting(filterContext);
        }
    }
    public class CheckAdminAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var userName = filterContext.HttpContext.Session["UserName"] as string;

            if (string.IsNullOrEmpty(userName))
            {
                filterContext.Result = new RedirectResult("~/home/login");
                return;
            }

            using (TilauksetEntities db = new TilauksetEntities())
            {
                var user = db.Logins.SingleOrDefault(x => x.UserName == userName);

                if (user.admin != 1)
                {
                    filterContext.Result = new RedirectResult("~/Logins/Kielletty");
                    return;
                    //filterContext.Result = new HttpStatusCodeResult(System.Net.HttpStatusCode.Forbidden);
                }
            }

            base.OnActionExecuting(filterContext);
        }
    }


    public class LoginController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Authorize(Logins LoginModel)
        {
            TilauksetEntities db = new TilauksetEntities();
            var LoggedUser = db.Logins.SingleOrDefault(x => x.UserName == LoginModel.UserName && x.PassWord == LoginModel.PassWord);
            if (LoggedUser != null)
            {
                ViewBag.LoginMessage = "Successfull login";
                ViewBag.LoggedStatus = "In";
                Session["UserName"] = LoggedUser.UserName;
                return RedirectToAction("Index", "Home"); //Tässä määritellään mihin onnistunut kirjautuminen johtaa --> Home/Index
            }
            else
            {
                ViewBag.LoginMessage = "Login unsuccessfull";
                ViewBag.LoggedStatus = "Out";
                LoginModel.LoginErrorMessage = "Tuntematon käyttäjätunnus tai salasana.";
                return View("Login", LoginModel);
            }
        }
        public ActionResult LogOut()
        {
            Session.Clear();
            return RedirectToAction("Endsession", "Home");
        }
    }
}