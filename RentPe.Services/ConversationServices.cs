using Microsoft.AspNet.Identity;
using RentPe.Database;
using RentPe.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace RentPe.Services
{

    public class ConversationServices
    {
        #region Singleton
        public static ConversationServices Instance
        {
            get
            {
                if (instance == null) instance = new ConversationServices();
                return instance;
            }
        }
        private static ConversationServices instance { get; set; }
        private ConversationServices()
        {   
        }
        #endregion

       
        public List<Conversation> GetConversation(string SentBy,string RecievedBy)
        {
            using (var context = new DSContext())
            {

                return context.Conversations.Where(x=>x.SentBy == SentBy && x.RecievedBy == RecievedBy
                                                  || x.SentBy == RecievedBy && x.RecievedBy == SentBy).ToList();

            }
        }




        public Conversation GetLastConversation()
        {
            using (var context = new DSContext())
            {

                return context.Conversations.OrderByDescending(x=>x.ID).FirstOrDefault();

            }
        }
        public List<Conversation> GetConversationChat(string Rentee)
        {
            using (var context = new DSContext())
            {
                var conversations = context.Conversations
            .Where(c => c.SentBy == Rentee || c.RecievedBy == Rentee)
            .GroupBy(c => new { Rentee = c.SentBy == Rentee ? c.SentBy : c.RecievedBy, c.Item })
            .Select(g => g.OrderByDescending(c => c.Date).FirstOrDefault())
            .ToList();

                return conversations;
            }
        }

        public List<Conversation> GetConversationChat()
        {
            using (var context = new DSContext())
            {
                var conversations = context.Conversations
                    .GroupBy(c => new { c.SentBy,c.RecievedBy, c.Item })
                    .Select(g => g.OrderByDescending(c => c.Date).FirstOrDefault())
                    .ToList();

                return conversations;
            }
        }


        public Conversation GetConversation(int ID)
        {
            using (var context = new DSContext())
            {
                return context.Conversations.Find(ID);

            }
        }

        public void SaveConversation(Conversation Conversation)
        {
            using (var context = new DSContext())
            {
                context.Conversations.Add(Conversation);
                context.SaveChanges();
            }
        }

        public void UpdateConversation(Conversation Conversation)
        {
            using (var context = new DSContext())
            {
                context.Entry(Conversation).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void DeleteConversation(int ID)
        {
            using (var context = new DSContext())
            {

                var Conversation = context.Conversations.Find(ID);
                context.Conversations.Remove(Conversation);
                context.SaveChanges();
            }
        }


    }
}
