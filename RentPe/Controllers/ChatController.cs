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
    public class ChatController : Controller
    {
        #region  ServicesRegion
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

        public ChatController()
        {
        }
        public ChatController(AMUserManager userManager, AMSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        #endregion
        // GET: Chat
        public ActionResult Index(string SentBy = "",string RecievedBy = "")
        {
            ChatListingViewModel model = new ChatListingViewModel();
            model.Users = UserManager.Users.Where(x=>x.Role != "Admin").ToList();
            var MainChatList = new List<MainChatModel>();
            var LastSavedConvo = ConversationServices.Instance.GetLastConversation();
            if (SentBy == "" && RecievedBy == "")
            {
                var conversation = ConversationServices.Instance.GetConversation(LastSavedConvo.SentBy, LastSavedConvo.RecievedBy);
                model.USentBy = UserManager.FindById(LastSavedConvo.SentBy);
                model.URecievedBy = UserManager.FindById(LastSavedConvo.RecievedBy);
                CustomOffer GetClosestOffer(DateTime chatTime)
                {
                    var closestOffer = CustomOfferServices.Instance.GetCustomOfferByRenteeCheckViceVersa(LastSavedConvo.SentBy, LastSavedConvo.RecievedBy)

                        .Where(co => co.OfferDate < chatTime)
                        .OrderByDescending(co => co.OfferDate)
                        .FirstOrDefault();

                    return closestOffer;
                }

                foreach (var item in conversation)
                {
                    if (item.IsOffer == true)
                    {
                        MainChatList.Add(new MainChatModel { Chats = item, CustomOffer = GetClosestOffer(item.Date) });
                    }
                    else
                    {
                        MainChatList.Add(new MainChatModel { Chats = item });

                    }
                }

            }
            else
            {
                var conversation = ConversationServices.Instance.GetConversation(SentBy, RecievedBy); 
                model.USentBy = UserManager.FindById(SentBy);
                model.URecievedBy = UserManager.FindById(RecievedBy);
                CustomOffer GetClosestOffer(DateTime chatTime)
                {
                    var closestOffer = CustomOfferServices.Instance.GetCustomOfferByRenteeCheckViceVersa(model.SentBy, model.RecievedBy)

                        .Where(co => co.OfferDate < chatTime)
                        .OrderByDescending(co => co.OfferDate)
                        .FirstOrDefault();

                    return closestOffer;
                }

                foreach (var item in conversation)
                {
                    if (item.IsOffer == true)
                    {
                        MainChatList.Add(new MainChatModel { Chats = item, CustomOffer = GetClosestOffer(item.Date) });
                    }
                    else
                    {
                        MainChatList.Add(new MainChatModel { Chats = item });

                    }
                }


            }


            model.Chats = MainChatList;








            return View("Index", "_AdminLayout", model);

        }


        [HttpPost]
        public ActionResult DeleteBulkMessages(List<int> selectedIds)
        {

            foreach (var id in selectedIds)
            {
                ConversationServices.Instance.DeleteConversation(id);
            }

            // Redirect or return a response indicating the deletion was successful
            return RedirectToAction("Index","Chat");
        }
    }
}