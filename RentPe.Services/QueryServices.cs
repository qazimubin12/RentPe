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
    public class QueryServices
    {
        #region Singleton
        public static QueryServices Instance
        {
            get
            {
                if (instance == null) instance = new QueryServices();
                return instance;
            }
        }
        private static QueryServices instance { get; set; }
        private QueryServices()
        {
        }
        #endregion





        public List<Query> GetQuerries(string SearchTerm = "")
        {
            using (var context = new DSContext())
            {
                if (SearchTerm != "")
                {
                    return context.Queries.Where(p => p.Name != null && p.Name.ToLower()
                                            .Contains(SearchTerm.ToLower()))
                                            .OrderBy(x => x.Name)
                                            .ToList();
                }
                else
                {
                    return context.Queries.OrderBy(x => x.Name).ToList();
                }
            }
        }





        public Query GetQuery(int ID)
        {
            using (var context = new DSContext())
            {

                return context.Queries.Find(ID);

            }
        }

        public void SaveQuery(Query Query)
        {
            using (var context = new DSContext())
            {
                context.Queries.Add(Query);
                context.SaveChanges();
            }
        }

        public void UpdateQuery(Query Query)
        {
            using (var context = new DSContext())
            {
                context.Entry(Query).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void DeleteQuery(int ID)
        {
            using (var context = new DSContext())
            {

                var Query = context.Queries.Find(ID);
                context.Queries.Remove(Query);
                context.SaveChanges();
            }
        }
    }
}
