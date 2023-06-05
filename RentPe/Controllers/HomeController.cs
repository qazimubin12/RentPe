﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using RentPe.Entities;
using RentPe.Services;
using RentPe.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.Mvc;

namespace RentPe.Controllers
{
    public class HomeController : Controller
    {
        private AMSignInManager _signInManager;
        private AMUserManager _userManager;
        public AMSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<AMSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }
        public AMUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<AMUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        private AMRolesManager _rolesManager;
        public AMRolesManager RolesManager
        {
            get
            {
                return _rolesManager ?? HttpContext.GetOwinContext().GetUserManager<AMRolesManager>();
            }
            private set
            {
                _rolesManager = value;
            }
        }
        public ActionResult Index()
        {
            HomeViewModel model = new HomeViewModel();
            model.ExclusiveAds = AdServices.Instance.GetAd().Where(x => x.Tag == "Exclusive").ToList();
            model.FeaturedAds = AdServices.Instance.GetAd().Where(x => x.Featured == "Yes").ToList();
            model.LatestAds = AdServices.Instance.GetAd().OrderByDescending(item => item.ID).Take(8).ToList();
            return View(model);
        }


        [HttpGet]
        public ActionResult PostAd()
        {
            ProductViewModel model = new ProductViewModel();
            model.ItemCategories = CategoryServices.Instance.GetRentItemCategories();

            return View("PostAd", "_Layout", model);
        }

        [HttpPost]
        public ActionResult PostAd(ProductViewModel model)
        {
            if (model.ID != 0)
            {
                var ad = AdServices.Instance.GetAd(model.ID);
                ad.ID = model.ID;
                ad.UserName = model.UserName;
                ad.Contact = model.Contact;
                ad.Privacy = model.Privacy;
                ad.ItemName = model.ItemName;
                ad.UserID = model.UserID;
                ad.ItemDescription = model.ItemDescription;
                ad.AvailableFrom = model.AvailableFrom;
                ad.AvailableTo = model.AvailableTo;
                ad.Authenticity = model.Authenticity;
                ad.ItemCategory = model.ItemCategory;
                ad.Type = model.Type;
                ad.Negotiable = model.Negotiable;
                ad.Condition = model.Condition;
                ad.EntryDate = model.EntryDate;
                ad.Location = model.Location;
                ad.Price = model.Price;
                ad.Note = model.Note;
                ad.AdStatus = model.AdStatus;
                ad.RentingPeriod = model.RentingPeriod;
                ad.Featured = model.Featured;
                AdServices.Instance.UpdateAd(ad);
                return Json(new {success=true},JsonRequestBehavior.AllowGet);
            }
            else
            {
                var ad = new Ad();
                ad.ID = model.ID;
                ad.UserName = model.UserName;
                ad.Contact = model.Contact;
                ad.Privacy = model.Privacy;
                ad.ItemName = model.ItemName;
                ad.UserID = model.UserID;
                ad.ItemDescription = model.ItemDescription;
                ad.AvailableFrom = model.AvailableFrom;
                ad.AvailableTo = model.AvailableTo;
                ad.Authenticity = model.Authenticity;
                ad.ItemCategory = model.ItemCategory;
                ad.Type = model.Type;
                ad.Negotiable = model.Negotiable;
                ad.Condition = model.Condition;
                ad.EntryDate = model.EntryDate;
                ad.Location = model.Location;
                ad.Price = model.Price;
                ad.Note = model.Note;
                ad.AdStatus = model.AdStatus;
                ad.RentingPeriod = model.RentingPeriod;
                ad.Featured = model.Featured;
                AdServices.Instance.SaveAd(ad);
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
        }
            

        //public ActionResult Product(int ID)
        //{
        //    ProductViewModel model = new ProductViewModel();
            
        //}
        public ActionResult View(int ID)
        {
            AdActionViewModel model = new AdActionViewModel();
            var ad = AdServices.Instance.GetAd(ID);
            model.ID = ad.ID;
            model.UserName = ad.UserName;
            model.Contact = ad.Contact;
            model.Privacy = ad.Privacy;
            model.ItemName = ad.ItemName;
            model.UserID = ad.UserID;
            model.ItemDescription = ad.ItemDescription;
            model.AvailableFrom = ad.AvailableFrom;
            model.AvailableTo = ad.AvailableTo;
            model.Authenticity = ad.Authenticity;
            model.ItemCategory = ad.ItemCategory;
            model.Type = ad.Type;
            model.Negotiable = ad.Negotiable;
            model.Condition = ad.Condition;
            model.EntryDate = ad.EntryDate;
            model.Location = ad.Location;
            model.Price = ad.Price;
            model.Note = ad.Note;
            model.AdStatus = ad.AdStatus;
            model.RentingPeriod = ad.RentingPeriod;
            model.Featured = ad.Featured;
            model.Tag = ad.Tag;
            model.MainImage = ad.MainImage;

            model.otherImages = AdServices.Instance.GetAdImages(ad.ID).Select(x => x.ImageURL).ToList();
            return View(model);
        }

        public ActionResult Shop(string SearchTerm= "")
        {
            HomeShopViewModel model = new HomeShopViewModel();
            model.ItemsCategories = CategoryServices.Instance.GetRentItemCategories();
            model.Ads = AdServices.Instance.GetAd(SearchTerm);
            return View("Shop", "_Layout", model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }


        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Login(string Email, string Password)
        {
            if (!string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password))
            {
                return RedirectToAction("Dashboard", "Admin");
            }
            else
            {
                return View();
            }
        }



        //[HttpPost]
        //public ActionResult Login(string Email, string Password)
        //{
        //    var user = DBNull;/* AMUserManager(username, password);*/
        //    if (user != null)
        //    {
        //        Session["ID"] = user.ID.ToString();
        //        Session["UserName"] = user.UserName.ToString();
        //        Session["Email"] = user.Email.ToString();
        //        Session["Role"] = user.Role.ToString();
        //        if (user.Role == "Admin")
        //        {
        //            return RedirectToAction("AdminDashboard", "Home");
        //        }
        //        else if (user.Role == "Kitchen Staff")
        //        {
        //            return RedirectToAction("KitchenDashboard", "Home");
        //        }
        //        else if (user.Role == "Cashier")
        //        {
        //            return RedirectToAction("BillingDashboard", "Home");
        //        }

        //        else if (user.Role == "Waiter")
        //        {
        //            return RedirectToAction("WaiterApp", "Home");
        //        }
        //        else if (user.Role == "Kitchen Master")
        //        {
        //            return RedirectToAction("KitchenDashboard", "Home");
        //        }
        //    }
        //    else
        //    {

        //    }
        //    {
        //        string msg = "Invalid Password or UserName";
        //        TempData["ErrorMessage"] = msg;
        //    }

        //    Session["ID"] = null;
        //    Session["UserName"] = null;
        //    Session["Email"] = null;
        //    Session["Role"] = null;
        //    return RedirectToAction("Index", "Login");

        //}


    }
}