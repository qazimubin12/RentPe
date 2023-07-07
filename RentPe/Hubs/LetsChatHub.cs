using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
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
        public class ChatMessage
        {
            public string Message { get; set; }
            public string SentBy { get; set; }
            public string RecievedBy { get; set; }
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

            // Save the conversation
            ConversationServices.Instance.SaveConversation(conversation);


            // Send the message to the intended receiver
            Clients.Client(message.FriendUniqueId).addNewPrivateMessageToPage(message.SentBy, message.Message, Context.ConnectionId);
        }

    }
}