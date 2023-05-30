using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using RentPe.Entities;
using RentPe.Services;
using RentPe.ViewModels;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RentPe.Controllers
{
    public class ItemController : Controller
    {
        // GET: Item
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
        public ItemController()
        {
        }



        public ItemController(AMUserManager userManager, AMSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }


        public ActionResult Index(string SearchTerm ="")
        {
            ItemListingViewModel model = new ItemListingViewModel();
            model.SearchTerm = SearchTerm;
            model.RentItems = RentItemServices.Instance.GetRentItem(SearchTerm);
            return View("Index","_AdminLayout",model);
        }


        [HttpGet]
        public ActionResult Action(int ID = 0)
        {
            ItemActionViewModel model = new ItemActionViewModel();
            if (ID != 0)
            {
                var Item = RentItemServices.Instance.GetRentItem(ID);
                model.ID= Item.ID;
                model.ItemName = Item.ItemName;
                model.UserID = Item.UserID;
                model.Favourites = Item.Favourites;
                model.ItemDescription = Item.ItemDescription;
                model.ItemCategory = Item.ItemCategory;
                model.RentingDate = Item.RentingDate;
                model.ReturnDate = Item.ReturnDate;
                model.ItemSize = Item.ItemSize;
                model.ItemColor = Item.ItemColor;
                model.Negotiable = Item.Negotiable;
                model.AvailableFrom = Item.AvailableFrom;
                model.AvailableTo = Item.AvailableTo;
                model.Brand = Item.Brand;
                model.Condition = Item.Condition;
                model.EntryDate = Item.EntryDate;
                model.Location = Item.Location;
                model.RentingPeriod = Item.RentingPeriod;
                model.Note = Item.Note;
                model.Label = Item.Label;
            }
            return View("Action", "_AdminLayout", model);
        }


        [HttpPost]
        public ActionResult Action(ItemActionViewModel model)
        {
            if (model.ID != 0)
            {
                var Item = RentItemServices.Instance.GetRentItem(model.ID);
                Item.ID = model.ID;
                Item.ItemName = model.ItemName;
                Item.UserID = model.UserID;
                Item.Favourites = model.Favourites;
                Item.ItemDescription = Item.ItemDescription;
                Item.ItemCategory = model.ItemCategory;
                Item.RentingDate = model.RentingDate;
                Item.ReturnDate = model.ReturnDate;
                Item.ItemSize = model.ItemSize;
                Item.ItemColor = model.ItemColor;
                Item.Negotiable = model.Negotiable;
                Item.AvailableFrom = model.AvailableFrom;
                Item.AvailableTo = model.AvailableTo;
                Item.Brand = model.Brand;
                Item.Condition = model.Condition;
                Item.EntryDate = model.EntryDate;
                Item.Location = model.Location;
                Item.RentingPeriod = model.RentingPeriod;
                Item.UserID = User.Identity.GetUserId();
                Item.Note = model.Note;
                Item.Label = model.Label;
                RentItemServices.Instance.UpdateRentItem(Item);
            }
            else
            {
                var Item = new RentItem();
                Item.ItemName = model.ItemName;
                Item.UserID = model.UserID;
                Item.Favourites = model.Favourites;
                Item.ItemDescription = Item.ItemDescription;
                Item.ItemCategory = model.ItemCategory;
                Item.RentingDate = model.RentingDate;
                Item.ReturnDate = model.ReturnDate;
                Item.ItemSize = model.ItemSize;
                Item.ItemColor = model.ItemColor;
                Item.Negotiable = model.Negotiable;
                Item.AvailableFrom = model.AvailableFrom;
                Item.AvailableTo = model.AvailableTo;
                Item.Brand = model.Brand;
                Item.Condition = model.Condition; 
                Item.UserID = User.Identity.GetUserId();

                Item.EntryDate = model.EntryDate;
                Item.Location = model.Location;
                Item.RentingPeriod = model.RentingPeriod;
                Item.Note = model.Note;
                Item.Label = model.Label;
                RentItemServices.Instance.SaveRentItem(Item);
            }


            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }



        [HttpGet]
        public ActionResult Delete(int ID)
        {
            ItemActionViewModel model = new ItemActionViewModel();
            var RentIem = RentItemServices.Instance.GetRentItem(ID);
            model.ID = RentIem.ID;
            return PartialView("_Delete", model);
        }

        [HttpPost]
        public ActionResult Delete(ItemActionViewModel model)
        {
            if (model.ID != 0)
            {
                var RentIem = RentItemServices.Instance.GetRentItem(model.ID);
                RentItemServices.Instance.DeleteRentItem(RentIem.ID);
            }

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }
    }
}