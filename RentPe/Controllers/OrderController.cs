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
    public class OrderController : Controller
    {
        // GET: Order
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
        public OrderController()
        {
        }



        public OrderController(AMUserManager userManager, AMSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }
        public ActionResult Index(string SearchTerm = "")
        {
            Session["ACTIVERADMIN"] = "Order";

            OrderListingViewModel model = new OrderListingViewModel();
            model.SearchTerm = SearchTerm;
            var NewOrders = new List<OrderViewModelNew>();
            var Orders = OrderServices.Instance.GetOrders();
            foreach (var item in Orders)
            {
                var Owner = UserManager.FindById(item.Owner);
                var Renter = UserManager.FindById(item.Renter);
                var Ad = AdServices.Instance.GetAd(int.Parse(item.Item));
                NewOrders.Add(new OrderViewModelNew { Ad = Ad, Order = item, Owner = Owner, Renter = Renter });
            }
            model.Orders = NewOrders.Where(x => x.Owner.Name.Contains(SearchTerm) || x.Renter.Name.Contains(SearchTerm)).ToList();
            return View("Index", "_AdminLayout", model);
        }

        public IEnumerable<User> SearchUsersByRole(string Role)
        {
            var users = UserManager.Users.AsQueryable().Where(X => X.Role == Role);
            return users;
        }


        [HttpGet]
        public ActionResult Action(int ID = 0)
        {
            Session["ACTIVERADMIN"] = "Order";

            OrderActionViewModel model = new OrderActionViewModel();
            model.Owners = SearchUsersByRole("Owner").ToList();
            model.Renters = SearchUsersByRole("User").ToList();
            model.Ads = AdServices.Instance.GetAd();
            List<string> statuses = new List<string>();
            statuses.Add("VERIFYING PAYMENT");
            statuses.Add("PAYMENT PENDING");
            statuses.Add("PAYMENT CONFIRMED");
            model.StatusList = statuses;
            if (ID != 0)
            {
                var Order = OrderServices.Instance.GetOrder(ID);
                model.ID = Order.ID;
                model.OrderNo = Order.OrderNo;
                model.Owner = Order.Owner;
                model.AmountPaid = Order.AmountPaid;
                model.AmountRemain = Order.AmountRemain;
                model.TotalAmount = Order.TotalAmount;
                model.Renter = Order.Renter;
                model.Item = Order.Item;
                model.Date = Order.Date;
                model.Status = Order.Status;
                model.VideoOfUnboxing = Order.VideoOfUnboxing;
                model.VideoOfPacking = Order.VideoOfPacking;

            }
            return View("Action", "_AdminLayout", model);
        }

        [HttpPost]
        public ActionResult Action(OrderActionViewModel model)
        {
            if (model.ID != 0)
            {
                var Order = OrderServices.Instance.GetOrder(model.ID);
                Order.ID = model.ID;
                Order.OrderNo = model.OrderNo;
                Order.Owner = model.Owner;
                Order.AmountPaid = model.AmountPaid;
                Order.AmountRemain = model.AmountRemain;
                Order.TotalAmount = model.TotalAmount;
                Order.Renter = model.Renter;
                Order.Item = model.Item;
                Order.Date = model.Date;
                Order.Status = model.Status;
                Order.VideoOfUnboxing = model.VideoOfUnboxing;
                Order.VideoOfPacking = model.VideoOfPacking;
                OrderServices.Instance.UpdateOrder(Order);





                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var Order = new Order();
                Order.OrderNo = model.OrderNo;
                Order.Owner = model.Owner;
                Order.AmountPaid = model.AmountPaid;
                Order.AmountRemain = model.AmountRemain;
                Order.TotalAmount = model.TotalAmount;
                Order.Renter = model.Renter;
                Order.Item = model.Item;
                Order.Date = model.Date;
                Order.Status = model.Status;
                Order.VideoOfUnboxing = model.VideoOfUnboxing;
                Order.VideoOfPacking = model.VideoOfPacking;


                OrderServices.Instance.SaveOrder(Order);

                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpGet]
        public ActionResult Delete(int ID)
        {
            OrderActionViewModel model = new OrderActionViewModel();
            var Order = OrderServices.Instance.GetOrder(ID);
            model.ID = Order.ID;
            return PartialView("_Delete", model);
        }

        [HttpPost]
        public ActionResult Delete(OrderActionViewModel model)
        {
            if (model.ID != 0)
            {
                var Order = OrderServices.Instance.GetOrder(model.ID);
                OrderServices.Instance.DeleteOrder(Order.ID);
            }

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

    }
}