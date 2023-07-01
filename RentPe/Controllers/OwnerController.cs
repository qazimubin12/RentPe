using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using RentPe.Services;
using RentPe.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RentPe.Controllers
{
    public class OwnerController : Controller
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

        public OwnerController()
        {
        }
        public OwnerController(AMUserManager userManager, AMSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }
        // GET: Owner
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Dashboard()
        {
            UserDashboardViewModel model = new UserDashboardViewModel();
            model.SignedInUser = UserManager.FindById(User.Identity.GetUserId());
            return View("Dashboard", "_Layout", model);
        }
    }
}