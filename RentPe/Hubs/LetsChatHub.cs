using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.AspNetCore.Identity;
using RentPe.Entities;
using RentPe.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using static RentPe.Hubs.LetsChatHub;

namespace RentPe.Hubs
{
    [HubName("letsChatHub")]
    public class LetsChatHub:Hub
    {
        public class OfferStatus
        {
            public string FriendUniqueId { get; set; }
            public int OfferID { get; set; }
            public string status { get; set; }
        }
        public class ChatCustomOffer
        {
            public int ID { get; set; }
            public string Message { get; set; }
            public string Name { get; set; }
            public string SentBy { get; set; }
            public string RecievedBy { get; set; }
            public string Attachments { get; set; }
            public string FriendUniqueId { get; set; }
            public string MyUniequeID { get; set; }
            public int Item { get; set; }
            public float OfferedPrice { get; set; }
            public DateTime RentingDate { get; set; }
            public int RentingPeriod { get; set; }
            public DateTime ReturnDate { get; set; }
        }
        public class ChatMessage
        {
            public string Message { get; set; }
            public string Name { get; set; }
            public int Item { get; set; }
            public string SentBy { get; set; }
            public string RecievedBy { get; set; }
            public string IsOffer { get; set; }
            public string Attachments { get; set; }
            public string FriendUniqueId { get; set; }
        }

        [HubMethodName("onConnected")]
        public override Task OnConnected()
        {
            // Logic to execute when a client connects
            string connectionId = Context.ConnectionId;

            // Send the onConnected event to the client
            Clients.Client(connectionId).onConnected(connectionId);

            return base.OnConnected();
        }


        public void sendPrivateMessage(ChatMessage message)
        {
            // Create a new conversation
            var conversation = new Conversation();
            conversation.SentBy = message.SentBy;
            conversation.RecievedBy = message.RecievedBy;
            conversation.Message = message.Message;
            conversation.Date = DateTime.Now;
            conversation.Attachments = message.Attachments;
            conversation.SentByName = message.Name;
            conversation.Item = message.Item;
            conversation.IsOffer = false;
            // Save the conversation
            ConversationServices.Instance.SaveConversation(conversation);
            

            // Send the message to the intended receiver
            Clients.Client(message.FriendUniqueId).addNewPrivateMessageToPage(message.Name,message.Attachments, message.Message, Context.ConnectionId);
        }

        public void sendAcceptRequest(OfferStatus offerStatus)
        {
            var offer = CustomOfferServices.Instance.GetCustomOffer(offerStatus.OfferID);
            offer.Status= offerStatus.status;
            CustomOfferServices.Instance.UpdateCustomOffer(offer);

            var Order = new Order();
            Order.Status = "PAYMENT PENDING";
            Order.Owner = offer.Owner;
            Order.Renter = offer.Rentee;
            Order.Date = DateTime.Now;
            Order.OrderNo = DateTime.Now.ToString("ddmmhhss");
            Order.Item = offer.Item.ToString();
            Order.TotalAmount = (float)Math.Floor(offer.OfferedPrice * 0.025f);
            Order.AmountPaid = 0;
            Order.AmountRemain = (float)Math.Floor(offer.OfferedPrice * 0.025f);

            OrderServices.Instance.SaveOrder(Order);
            Clients.Client(offerStatus.FriendUniqueId).updateStatusToPage(offer.ID, offer.Status,Context.ConnectionId);

        }

        public void sendwithdrawalrequest(OfferStatus offerStatus)
        {
            var offer = CustomOfferServices.Instance.GetCustomOffer(offerStatus.OfferID);
            offer.Status = offerStatus.status;
            CustomOfferServices.Instance.UpdateCustomOffer(offer);

            Clients.Client(offerStatus.FriendUniqueId).updateStatusToPage(offer.ID, offer.Status, Context.ConnectionId);

        }

        public void sendDeclineRequest(OfferStatus offerStatus)
        {
            var offer = CustomOfferServices.Instance.GetCustomOffer(offerStatus.OfferID);
            offer.Status = offerStatus.status;
            CustomOfferServices.Instance.UpdateCustomOffer(offer);


            Clients.Client(offerStatus.FriendUniqueId).updateStatusToPage(offer.ID, offer.Status, Context.ConnectionId);

        }


        public void sendPrivateOffer(ChatCustomOffer offer)
        {
            var customOffer = new CustomOffer();
            customOffer.OfferedPrice = offer.OfferedPrice;
            customOffer.RentingDate = offer.RentingDate;
            customOffer.ReturnDate = offer.ReturnDate;
            customOffer.OfferDate = DateTime.Now;
            customOffer.Owner = offer.RecievedBy;
            customOffer.Rentee = offer.SentBy;
            customOffer.Item = offer.Item;
            customOffer.RentingPeriod = offer.RentingPeriod;
            customOffer.Status = "PENDING";
            var Ad = AdServices.Instance.GetAd(offer.Item);
            CustomOfferServices.Instance.SaveCustomOffer(customOffer);

            var conversation = new Conversation();
            conversation.SentBy = offer.SentBy;
            conversation.RecievedBy = offer.RecievedBy;
            conversation.Message = offer.Message;
            conversation.Date = DateTime.Now;
            conversation.Attachments = offer.Attachments;
            conversation.SentByName = offer.Name;
            conversation.Item = offer.Item;
            conversation.IsOffer = true;
            ConversationServices.Instance.SaveConversation(conversation);
            // Save the conversation

            Clients.Client(offer.FriendUniqueId).addNewPrivateOfferToPage(customOffer.ID,offer.Name,offer.RentingPeriod,offer.RentingDate.ToShortDateString() ,offer.ReturnDate.ToShortDateString(),offer.OfferedPrice,Ad.ItemName,customOffer.Status, Ad.MainImage, offer.Message, Context.ConnectionId);
            Clients.Client(offer.MyUniequeID).changeButtonContainerID(customOffer.ID,Context.ConnectionId);
        }


    }
}