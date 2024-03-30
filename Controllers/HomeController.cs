using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Web.Mvc;

using TilausDB2.Models;


namespace TilausDB2.Controllers
{

    public class HomeController : Controller
    {
        private TilauksetEntity db = new TilauksetEntity();

        public ActionResult Index()
        {
            // Fetch data from dbo.Myynnit table
            var salesData = db.Myynnit.ToList();

            // Fetch data from dbo.Sivustolla_vierailijat table
            var visitorData = db.Sivustolla_vierailijat.ToList();

            // Join the two datasets based on the common key Vuosi
            var joinedData = from s in salesData
                             join v in visitorData on s.Vuosi equals v.Vuosi
                             select new { Vuosi = s.Vuosi, Kokonaismyynti = s.Kokonaismyynti, Vierailijat = v.Vierailijat };

            // Prepare data for the chart
            var labels = joinedData.Select(d => d.Vuosi.ToString()).ToArray();
            var sales = joinedData.Select(d => d.Kokonaismyynti).ToArray();
            var visitors = joinedData.Select(d => d.Vierailijat).ToArray();

            // Pass data to the view
            ViewBag.Labels = labels;
            ViewBag.SalesData = sales;
            ViewBag.VisitorData = visitors;
            
            ViewBag.LoginError = 0; //Ei pakoteta modaalia login-ruutua tässä kohden, vaan pelkästään, jos on yritetty kirjautua ja kirjautuminen on epäonnistunut
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Tietoja Northwind-yhtiöstä ja Careeriasta";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Yhteystiedot";

            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Authorize(Logins LoginModel)
        {
            TilauksetEntity db = new TilauksetEntity();
            //Haetaan käyttäjän/Loginin tiedot annetuilla tunnustiedoilla tietokannasta LINQ -kyselyllä
            var LoggedUser = db.Logins.SingleOrDefault(x => x.UserName == LoginModel.UserName && x.PassWord == LoginModel.PassWord);
            if (LoggedUser != null)
            {
                ViewBag.LoginMessage = "Successfull login";
                ViewBag.LoggedStatus = "In";
                Session["UserName"] = LoggedUser.UserName;
                Session["LoginID"] = LoggedUser.LoginId;
                //Session["AccessLevel"] = LoggedUser.AccessLevel;
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
            Session.Abandon();
            ViewBag.LoggedStatus = "Out";
            return RedirectToAction("Index", "Home"); //Uloskirjautumisen jälkeen pääsivulle
        }
    }

}