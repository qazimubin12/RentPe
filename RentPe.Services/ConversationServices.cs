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

                return context.Conversations.Where(x=>x.SentBy == SentBy && x.RecievedBy == RecievedBy).ToList();

            }
        }


        public List<string> GetConversationChat(string SentBy)
        {
            using (var context = new DSContext())
            {

                return context.Conversations.Where(x => x.SentBy == SentBy).Select(x=>x.RecievedBy).Distinct().ToList();

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
