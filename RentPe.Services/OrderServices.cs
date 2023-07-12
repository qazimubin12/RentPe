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

    public class OrderServices
    {
        #region Singleton
        public static OrderServices Instance
        {
            get
            {
                if (instance == null) instance = new OrderServices();
                return instance;
            }
        }
        private static OrderServices instance { get; set; }
        private OrderServices()
        {   
        }
        #endregion

       
        public List<Order> GetOrder(string Owner,string Renter)
        {
            using (var context = new DSContext())
            {

                return context.Orders.Where(x=>x.Renter == Renter && x.Owner == Owner
                                                  || x.Renter == Owner && x.Owner == Renter).ToList();

            }
        }



    
        public List<Order> GetOrderByRentee(string Rentee)
        {
            using (var context = new DSContext())
            {
                var Orders = context.Orders
            .Where(c => c.Renter == Rentee || c.Renter == Rentee)
            .GroupBy(c => new { Rentee = c.Renter == Rentee ? c.Renter : c.Renter, c.Item })
            .Select(g => g.OrderByDescending(c => c.Date).FirstOrDefault())
            .ToList();

                return Orders;
            }
        }

        public Order GetOrder(int ID)
        {
            using (var context = new DSContext())
            {
                return context.Orders.Find(ID);

            }
        }

        public void SaveOrder(Order Order)
        {
            using (var context = new DSContext())
            {
                context.Orders.Add(Order);
                context.SaveChanges();
            }
        }

        public void UpdateOrder(Order Order)
        {
            using (var context = new DSContext())
            {
                context.Entry(Order).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void DeleteOrder(int ID)
        {
            using (var context = new DSContext())
            {

                var Order = context.Orders.Find(ID);
                context.Orders.Remove(Order);
                context.SaveChanges();
            }
        }


    }
}
