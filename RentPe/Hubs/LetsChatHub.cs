using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.AspNetCore.Identity;
using RentPe.Entities;
using RentPe.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace RentPe.Hubs
{
    [HubName("letsChatHub")]
    public class LetsChatHub:Hub
    {

        public class ChatCustomOffer
        {
            public string Message { get; set; }
            public string Name { get; set; }
            public string SentBy { get; set; }
            public string RecievedBy { get; set; }
            public string Attachments { get; set; }
            public string FriendUniqueId { get; set; }

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
            // Save the conversation
            ConversationServices.Instance.SaveConversation(conversation);
            

            // Send the message to the intended receiver
            Clients.Client(message.FriendUniqueId).addNewPrivateMessageToPage(message.Name,message.Attachments, message.Message, Context.ConnectionId);
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
            // Save the conversation
            CustomOfferServices.Instance.SaveCustomOffer(customOffer);

            Clients.Client(offer.FriendUniqueId).addNewPrivateOfferToPage(offer.Name,offer.RentingPeriod,offer.RentingDate.ToShortDateString() ,offer.ReturnDate.ToShortDateString(),offer.OfferedPrice,Ad.ItemName,customOffer.Status, Ad.MainImage, offer.Message, Context.ConnectionId);

        }

    }
}