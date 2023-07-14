using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
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
    public class CustomOfferController : Controller
    {
        // GET: CustomOffer

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
        public CustomOfferController()
        {
        }



        public CustomOfferController(AMUserManager userManager, AMSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }
        public ActionResult Index(string SearchTerm = "")
        {
            CustomOfferListingViewModel model = new CustomOfferListingViewModel();
            model.SearchTerm = SearchTerm;
            var NewCustomOffers = new List<CustomOfferModel>();
            var customOffers = CustomOfferServices.Instance.GetCustomOffers();
            foreach (var item in customOffers)
            {
                var Owner = UserManager.FindById(item.Owner);
                var Renter = UserManager.FindById(item.Rentee);
                var Ad = AdServices.Instance.GetAd(item.Item);
                NewCustomOffers.Add(new CustomOfferModel { Ad = Ad, CustomOffer = item, Owner = Owner, Renter = Renter });
            }
            model.CustomOffers = NewCustomOffers.Where(x=>x.Owner.Name.Contains(SearchTerm) || x.Renter.Name.Contains(SearchTerm)).ToList();
            return View("Index", "_AdminLayout", model);
        }

        public IEnumerable<User> SearchUsersByRole(string Role)
        {
            var users = UserManager.Users.AsQueryable().Where(X=>X.Role == Role);
            return users;
        }


        [HttpGet]
        public ActionResult Action(int ID = 0)
        {
            CustomOfferActionViewModel model = new CustomOfferActionViewModel();
            model.Owners = SearchUsersByRole("Owner").ToList();
            model.Rentees = SearchUsersByRole("User").ToList();
            model.Ads = AdServices.Instance.GetAd();
            List<string> statuses = new List<string>();
            statuses.Add("ACCEPTED");
            statuses.Add("DECLINED");
            statuses.Add("WITHDRAWN");
            statuses.Add("PENDING");
            model.StatusList = statuses;
            if (ID != 0)
            {
                var CustomOffer = CustomOfferServices.Instance.GetCustomOffer(ID);
                model.ID = CustomOffer.ID;
                model.Owner = CustomOffer.Owner;
                model.OfferDate = CustomOffer.OfferDate;
                model.Rentee = CustomOffer.Rentee;
                model.Item = CustomOffer.Item;
                model.Status = CustomOffer.Status;
                model.OfferedPrice = CustomOffer.OfferedPrice;
                model.RentingDate = CustomOffer.RentingDate;
                model.ReturnDate = CustomOffer.ReturnDate;
                model.RentingPeriod = CustomOffer.RentingPeriod;

            }
            return View("Action", "_AdminLayout", model);
        }

        [HttpPost]
        public ActionResult Action(CustomOfferActionViewModel model)
        {
            if (model.ID != 0)
            {
                var customOffer = CustomOfferServices.Instance.GetCustomOffer(model.ID);
                customOffer.ID = model.ID;
                customOffer.Owner = model.Owner;
                customOffer.OfferDate = model.OfferDate;
                customOffer.Rentee = model.Rentee;
                customOffer.Item = model.Item;
                customOffer.Status = model.Status;
                customOffer.OfferedPrice = model.OfferedPrice;
                customOffer.RentingDate = model.RentingDate;
                customOffer.ReturnDate = model.ReturnDate;
                customOffer.RentingPeriod = model.RentingPeriod;
                CustomOfferServices.Instance.UpdateCustomOffer(customOffer);





                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var customOffer = new CustomOffer();
                customOffer.Owner = model.Owner;
                customOffer.OfferDate = model.OfferDate;
                customOffer.Rentee = model.Rentee;
                customOffer.Item = model.Item;
                customOffer.Status = model.Status;
                customOffer.OfferedPrice = model.OfferedPrice;
                customOffer.RentingDate = model.RentingDate;
                customOffer.ReturnDate = model.ReturnDate;
                customOffer.RentingPeriod = model.RentingPeriod;


                CustomOfferServices.Instance.SaveCustomOffer(customOffer);
       
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpGet]
        public ActionResult Delete(int ID)
        {
            CustomOfferActionViewModel model = new CustomOfferActionViewModel();
            var customoffer = CustomOfferServices.Instance.GetCustomOffer(ID);
            model.ID = customoffer.ID;
            return PartialView("_Delete", model);
        }

        [HttpPost]
        public ActionResult Delete(CustomOfferActionViewModel model)
        {
            if (model.ID != 0)
            {
                var customOffer = CustomOfferServices.Instance.GetCustomOffer(model.ID);
                CustomOfferServices.Instance.DeleteCustomOffer(customOffer.ID);
            }

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

    }
}