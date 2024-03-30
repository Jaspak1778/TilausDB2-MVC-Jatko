using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Web.Mvc;

using TilausDB2.Models;
using System.Drawing;


namespace TilausDB2.Controllers
{

    public class HomeController : Controller
    {
        private TilauksetEntity db = new TilauksetEntity();

        public ActionResult Index()
        {
            #region statistiikka1
            var salesData = db.Myynnit.ToList();

            // Haetaan Sivustolla_vierailijat 
            var visitorData = db.Sivustolla_vierailijat.ToList();

            // Yhdistetään avaimella Vuosi
            var joinedData = from s in salesData
                             join v in visitorData on s.Vuosi equals v.Vuosi
                             select new { Vuosi = s.Vuosi, Kokonaismyynti = s.Kokonaismyynti, Vierailijat = v.Vierailijat };

            // Kootaan data
            //joinedData.Select <-- tällä voidaan viitata haluttuun propertyyn datasta joka on tässä tapauksessa kooste (>JOIN)
            var labels = joinedData.Select(d => d.Vuosi.ToString()).ToArray();
            var sales = joinedData.Select(d => d.Kokonaismyynti).ToArray();
            var visitors = joinedData.Select(d => d.Vierailijat).ToArray();
            
            // Viedään dataa ViewBagien kautta.
            ViewBag.Labels = labels;
            ViewBag.SalesData = sales;
            ViewBag.VisitorData = visitors;

            #endregion

            #region statistiikka2
            //ForeignerData.Select <-- tällä voidaan viitata haluttuun propertyyn tietokanta taulussa.
            var ForeignerData = db.Vierailija_kohde.ToList();
            var Maa = ForeignerData.Select(d => d.Maa.ToString()).ToArray();
            var kaupunki = ForeignerData.Select(d => d.Kaupunki.ToString()).ToArray();
            var henkilomaara = ForeignerData.Select(d => d.Henkilomaara).ToArray();
            #endregion

            #region statistiikka 3
            var groupedData = ForeignerData
            .GroupBy(d => d.Maa)
            .Select(group => new { Maa = group.Key, TotalHenkilomaara = group.Sum(d => d.Henkilomaara) })
            .ToList();

            var maa = groupedData.Select(d => d.Maa).ToArray();
            var totalHenkilomaara = groupedData.Select(d => d.TotalHenkilomaara).ToArray();

            #endregion
            ViewBag.maa = maa;
            ViewBag.Maa = Maa;
            ViewBag.Kaupunki = kaupunki;
            ViewBag.HenkilöLKM = henkilomaara;
            ViewBag.TotalHenkilomaara = totalHenkilomaara;

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