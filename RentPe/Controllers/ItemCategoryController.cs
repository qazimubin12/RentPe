using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
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
    public class ItemCategoryController : Controller
    {
        // GET: ItemCategory
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
        public ItemCategoryController()
        {
        }



        public ItemCategoryController(AMUserManager userManager, AMSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }


        public ActionResult Index(string SearchTerm = "")
        {
            ItemCategoryListingViewModel model = new ItemCategoryListingViewModel();
            model.SearchTerm = SearchTerm;
            model.ItemCategories = RentItemServices.Instance.GetRentItemCategories(SearchTerm);
            return View("Index", "_AdminLayout", model);
        }


        [HttpGet]
        public ActionResult Action(int ID = 0)
        {
            ItemCategoryActionViewModel model = new ItemCategoryActionViewModel();
            if (ID != 0)
            {
                var ItemCategory = RentItemServices.Instance.GetItemCategory(ID);
                model.ID = ItemCategory.ID;
                model.CategoryName = ItemCategory.CategoryName;
               
            }
            return View("Action", "_AdminLayout", model);
        }


        [HttpPost]
        public ActionResult Action(ItemCategoryActionViewModel model)
        {
            if (model.ID != 0)
            {
                var ItemCategory = RentItemServices.Instance.GetItemCategory(model.ID);
                ItemCategory.ID = model.ID;
                ItemCategory.CategoryName = model.CategoryName;
              
                RentItemServices.Instance.UpdateItemCategory(ItemCategory);
            }
            else
            {
                var ItemCategory = new ItemCategory();
                ItemCategory.CategoryName = model.CategoryName;
                RentItemServices.Instance.SaveItemCategory(ItemCategory);
            }


            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }



        [HttpGet]
        public ActionResult Delete(int ID)
        {
            ItemCategoryActionViewModel model = new ItemCategoryActionViewModel();
            var RentIem = RentItemServices.Instance.GetItemCategory(ID);
            model.ID = RentIem.ID;
            return PartialView("_Delete", model);
        }

        [HttpPost]
        public ActionResult Delete(ItemCategoryActionViewModel model)
        {
            if (model.ID != 0)
            {
                var RentIem = RentItemServices.Instance.GetItemCategory(model.ID);
                RentItemServices.Instance.GetItemCategory(RentIem.ID);
            }

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }
    }
}