using RentPe.Database;
using RentPe.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentPe.Services
{
    public class PaymentServices
    {
        #region Singleton
        public static PaymentServices Instance
        {
            get
            {
                if (instance == null) instance = new PaymentServices();
                return instance;
            }
        }
        private static PaymentServices instance { get; set; }
        private PaymentServices()
        {
        }
        #endregion





        public List<Payment> GetPayments(string SearchTerm = "")
        {
            using (var context = new DSContext())
            {
                if (SearchTerm != "")
                {
                    return context.Payments.Where(p => p.Name != null && p.Name.ToLower()
                                            .Contains(SearchTerm.ToLower()))
                                            .OrderBy(x => x.Name)
                                            .ToList();
                }
                else
                {
                    return context.Payments.OrderBy(x => x.Name).ToList();
                }
            }
        }





        public Payment GetPayment(int ID)
        {
            using (var context = new DSContext())
            {

                return context.Payments.Find(ID);

            }
        }

        public void SavePayment(Payment Payment)
        {
            using (var context = new DSContext())
            {
                context.Payments.Add(Payment);
                context.SaveChanges();
            }
        }

        public void UpdatePayment(Payment Payment)
        {
            using (var context = new DSContext())
            {
                context.Entry(Payment).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void DeletePayment(int ID)
        {
            using (var context = new DSContext())
            {

                var Payment = context.Payments.Find(ID);
                context.Payments.Remove(Payment);
                context.SaveChanges();
            }
        }
    }
}
