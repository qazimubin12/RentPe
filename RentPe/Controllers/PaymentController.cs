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
    public class PaymentController : Controller
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
        public PaymentController()
        {
        }
        public PaymentController(AMUserManager userManager, AMSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }
        // GET: Payment
        public ActionResult Index(string SearchTerm = "")
        {
            Session["ACTIVERADMIN"] = "Payment";

            PaymentListingViewModel model = new PaymentListingViewModel();
            model.SearchTerm = SearchTerm;
            var payments = PaymentServices.Instance.GetPayments(SearchTerm);
            var list = new List<PaymentModel>();
            foreach (var item in payments)
            {
                var order = OrderServices.Instance.GetOrder(item.OrderID);
                var owner = UserManager.FindById(order.Owner);
                var renter = UserManager.FindById(order.Renter);
                list.Add(new PaymentModel { OrderFull = order, OwnerFull = owner, PaymentFull = item, RenterFul = renter });

            }
            model.Payments = list;
            return View("Index", "_AdminLayout", model);
        }


        [HttpGet]
        public ActionResult Delete(int ID)
        {
            PaymentActionViewModel model = new PaymentActionViewModel();
            var Payment = PaymentServices.Instance.GetPayment(ID);
            model.ID = Payment.ID;
            return PartialView("_Delete", model);
        }

        [HttpPost]
        public ActionResult Delete(PaymentActionViewModel model)
        {
            if (model.ID != 0)
            {
                var Payment = PaymentServices.Instance.GetPayment(model.ID);
                PaymentServices.Instance.DeletePayment(Payment.ID);
            }

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

    }
}