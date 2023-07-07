using Microsoft.AspNet.SignalR;
using RentPe.Entities;
using RentPe.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentPe
{
    public class ChatHub:Hub
    {
        public void SendMessage(string message, string imageURL, string sentBy, string receivedBy)
        {
            var conversation = new Conversation();
            conversation.SentBy = sentBy;
            conversation.RecievedBy = receivedBy;
            conversation.Message = message;
            conversation.Date = DateTime.Now;
            conversation.Attachments = imageURL;

            // Save the conversation
            ConversationServices.Instance.SaveConversation(conversation);

            // Broadcast the message to the intended receiver
            Clients.Client(receivedBy).SendAsync("ReceiveMessage", message, Context.ConnectionId);
        }


    }
}