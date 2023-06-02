using RentPe.Entities;
using RentPe.Services;
using RentPe.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RentPe.Controllers
{
    public class AdController : Controller
    {
        // GET: Ad
        public ActionResult Index(string SearchTerm = "")
        {
            AdListingViewModel model = new AdListingViewModel();
            model.SearchTerm = SearchTerm;
            model.Ads = AdServices.Instance.GetAd(SearchTerm);
            return View("Index", "_AdminLayout", model);
        }

        [HttpGet]
        public ActionResult AdAction(int ID)
        {
            AdActionViewModel model = new AdActionViewModel();
            model.ItemCategories = RentItemServices.Instance.GetRentItemCategories();
            if(ID != 0)
            {
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
                model.RentingPeriod  = ad.RentingPeriod;
                model.Featured = ad.Featured;
            }
            return View("AdAction", "_AdminLayout", model);
        }

        [HttpPost]
        public ActionResult AdAction(AdActionViewModel model)
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
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
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


        [HttpGet]
        public ActionResult Delete(int ID)
        {
            AdActionViewModel model = new AdActionViewModel();
            var ad = AdServices.Instance.GetAd(ID);
            model.ID = ad.ID;
            return PartialView("_Delete", model);
        }

        [HttpPost]
        public ActionResult Delete(AdActionViewModel model)
        {
            if (model.ID != 0)
            {
                var RentIem = AdServices.Instance.GetAd(model.ID);
                AdServices.Instance.DeleteAd(RentIem.ID);
            }

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }
    }
}