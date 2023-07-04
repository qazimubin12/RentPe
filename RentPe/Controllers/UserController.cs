﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using RentPe.Models;
using RentPe.Services;
using RentPe.ViewModels;
using RentPe.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace RentPe.Controllers
{
    public class UserController : Controller
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

        public UserController()
        {
        }
        public UserController(AMUserManager userManager, AMSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        // GET: User
        public ActionResult Index(string searchterm)
        {
            UsersListingViewModel model = new UsersListingViewModel();
            model.Users = SearchUsers(searchterm);
            model.Roles = RolesManager.Roles.ToList();
            return View("Index", "_AdminLayout", model);
        }

        public IEnumerable<User> SearchUsers(string searchTerm)
        {
            var users = UserManager.Users.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                users = users.Where(a => a.Email.ToLower().Contains(searchTerm.ToLower()));
            }
            return users;
        }

        [HttpPost]
        public ActionResult UserInfo(string Photo, string Address, string City, string NIC)
        {
            
            var Userinfo = UserInfoServices.Instance.GetUserInfo(User.Identity.GetUserId());
            Userinfo.Address = Address;
            Userinfo.Photo = Photo;
            Userinfo.City = City;
            Userinfo.NIC = NIC;
            UserInfoServices.Instance.UpdateUserInfo(Userinfo);
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Register(RegisterViewModel model)
        {
            model.Roles = RolesManager.Roles.ToList();
            return PartialView(model);
        }

        [HttpGet]
        public async Task<ActionResult> Action(string ID)
        {
            UserActionModel model = new UserActionModel();
            model.Roles = RolesManager.Roles.ToList();

            if (!string.IsNullOrEmpty(ID))
            {
                var user = await UserManager.FindByIdAsync(ID);
                model.ID = user.Id;
                model.Name = user.Name;
                model.Contact = user.PhoneNumber;
                model.Email = user.Email;

                model.RoleID = user.Role;
                model.Password = user.Password;
            }
            return PartialView("_Action", model);
        }


        [HttpGet]  
        public ActionResult Dashboard()
        {
            UserDashboardViewModel model = new UserDashboardViewModel();
            model.SignedInUser = UserManager.FindById(User.Identity.GetUserId());

            model.UserInfo = UserInfoServices.Instance.GetUserInfo(model.SignedInUser.Id);
            model.Contact = model.SignedInUser.PhoneNumber;
            model.Email = model.SignedInUser.Email;
            model.ID = model.SignedInUser.Id;
            model.UserName = model.SignedInUser.UserName;
            model.Name = model.SignedInUser.Name;


            var listOfOffers = new List<CustomerOfferModel>();
            var customoffers = CustomOfferServices.Instance.GetCustomOfferByRentee(model.ID);
            foreach (var item in customoffers)
            {
                var AdItem = AdServices.Instance.GetAd(item.Item);
                var Owner = UserManager.FindById(item.Owner);
                var Rentee = UserManager.FindById(item.Rentee);
                listOfOffers.Add(new CustomerOfferModel
                {
                    Item = AdItem,
                    OfferedPrice = item.OfferedPrice,
                    OfferDate = item.OfferDate,
                    Owner = Owner,
                    Rentee = Rentee,
                    Status = item.Status,
                    RentingDate = item.RentingDate,
                    ReturnDate = item.ReturnDate,
                    RentingPreiod = item.RentingPreiod
                });
            }
            model.CustomOffers = listOfOffers;
            var InboxList = new List<ChatInboxModel>();
            model.Chats = ConversationServices.Instance.GetConversationChat(model.SignedInUser.Id);
            foreach (var item in model.Chats)
            {
                var SentBy =UserManager.FindById(item.SentBy);
                var RecivedBy = UserManager.FindById(item.RecievedBy);
                InboxList.Add(new ChatInboxModel { RecievedBy = RecivedBy, SentBy = SentBy, Message = item.Message, Date = item.Date });
            }
            model.InboxList = InboxList;
            return View("Dashboard", "_Layout",model);
        }


        // GET: Conversation/ChatPartial
        public ActionResult ChatPartial(string SentBy,string RecievedBy)
        {
            UserDashboardViewModel model = new UserDashboardViewModel();    
            var conversation = ConversationServices.Instance.GetConversation(SentBy,RecievedBy);
            model.Chats = conversation;
            model.SignedInUser = UserManager.FindById(User.Identity.GetUserId());
            return PartialView("_Chat", model);
        }



        [HttpPost]
        public async Task<JsonResult> Action(UserActionModel model)
        {

            var role = await RolesManager.FindByIdAsync(model.RoleID);
            var user = new User { UserName = model.Email, Email = model.Email, PhoneNumber = model.Contact, Name = model.Name, Role = role.Name, Password = model.Password };

            var result = await UserManager.CreateAsync(user, model.Password);

            JsonResult json = new JsonResult();


            if (!string.IsNullOrEmpty(model.ID)) //update record
            {
                var user1 = await UserManager.FindByIdAsync(model.ID);

                user1.Id = model.ID;
                user1.Name = model.Name;
                user1.PhoneNumber = model.Contact;
                user1.Email = model.Email;
                user1.Role = model.RoleID;
                var token = await UserManager.GeneratePasswordResetTokenAsync(model.ID);
                var result2 = await UserManager.ResetPasswordAsync(model.ID, token, model.Password);
                result = await UserManager.UpdateAsync(user);
                json.Data = new { success = result.Succeeded, Message = string.Join(", ", result.Errors) };
                return json;


            }
            else
            {
                var User = new User();
                User.Name = model.Name;
                User.PhoneNumber = model.Contact;
                User.Email = model.Email;
                User.Password = model.Password;
                User.Role = model.RoleID;
                User.UserName = model.Email;
                if (result.Succeeded)
                {
                    await UserManager.AddToRoleAsync(user.Id, role.Name);
                }

                json.Data = new { success = result.Succeeded, Message = string.Join(", ", result.Errors) };

                return json;
            }
        }

        [HttpGet]
        public async Task<ActionResult> Delete(string ID)
        {
            UserActionModel model = new UserActionModel();

            var user = await UserManager.FindByIdAsync(ID);

            model.ID = user.Id;

            return PartialView("_Delete", model);
        }

        [HttpPost]
        public async Task<JsonResult> Delete(UserActionModel model)
        {
            JsonResult json = new JsonResult();

            IdentityResult result = null;

            if (!string.IsNullOrEmpty(model.ID)) //we are trying to delete a record
            {
                var user = await UserManager.FindByIdAsync(model.ID);

                result = await UserManager.DeleteAsync(user);

                json.Data = new { success = result.Succeeded, Message = string.Join(", ", result.Errors) };
            }
            else
            {
                json.Data = new { success = false, Message = "Invalid user." };
            }

            return json;
        }



        [HttpGet]
        public async Task<ActionResult> UserRoles(string ID)
        {
            UserRoleModel model = new UserRoleModel();
            model.UserID = ID;
            var user = await UserManager.FindByIdAsync(ID);
            var userRoleIDs = user.Roles.Select(x => x.RoleId).ToList();


            model.UserRoles = RolesManager.Roles.Where(x => userRoleIDs.Contains(x.Id)).ToList();
            model.Roles = RolesManager.Roles.Where(x => !userRoleIDs.Contains(x.Id)).ToList();
            return PartialView("_UserRoles", model);
        }



        [HttpPost]
        public async Task<JsonResult> UserRoles(UserActionModel model)
        {
            JsonResult json = new JsonResult();
            IdentityResult result = null;
            if (!string.IsNullOrEmpty(model.ID)) //update record
            {
                var user = await UserManager.FindByIdAsync(model.ID);

                user.Id = model.ID;
                user.Name = model.Name;
                user.PhoneNumber = model.Contact;
                user.Email = model.Email;
                user.Password = model.Password;
                user.Role = model.RoleID;
                result = await UserManager.UpdateAsync(user);

            }
            else
            {
                var User = new User();
                User.Name = model.Name;
                User.PhoneNumber = model.Contact;
                User.Email = model.Email;
                User.Password = model.Password;
                User.UserName = model.Email;
                User.Role = model.RoleID;
                result = await UserManager.CreateAsync(User);

            }

            json.Data = new { success = result.Succeeded, Message = string.Join(", ", result.Errors) };

            return json;
        }




        [HttpPost]
        public async Task<JsonResult> UserRoleOperation(string userID, string roleID, bool isDelete = false)
        {
            JsonResult json = new JsonResult();

            var user = await UserManager.FindByIdAsync(userID);
            var role = await RolesManager.FindByIdAsync(roleID);

            if (user != null && role != null)
            {
                IdentityResult result = null;
                if (!isDelete)
                {
                    result = await UserManager.AddToRoleAsync(userID, role.Name);
                }
                else
                {
                    result = await UserManager.RemoveFromRoleAsync(userID, role.Name);
                }
                json.Data = new { success = result.Succeeded, Message = string.Join(", ", result.Errors) };

            }
            else
            {
                json.Data = new { success = false, Message = "Invalid Operation" };

            }


            return json;
        }
    }
}