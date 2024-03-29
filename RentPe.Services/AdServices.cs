﻿using RentPe.Database;
using RentPe.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentPe.Services
{
    public class AdServices
    {
        #region Singleton
        public static AdServices Instance
        {
            get
            {
                if (instance == null) instance = new AdServices();
                return instance;
            }
        }
        private static AdServices instance { get; set; }
        private AdServices()
        {
        }
        #endregion

        public List<Ad> GetAd(string SearchTerm = "")
        {
            using (var context = new DSContext())
            {
                if (SearchTerm != "")
                {
                    return context.Ads.Where(p => p.ItemName != null && p.ItemName.ToLower()
                                            .Contains(SearchTerm.ToLower()))
                                            .OrderBy(x => x.ItemName)
                                            .ToList();
                }
                else
                {
                    return context.Ads.OrderBy(x => x.ItemName).ToList();
                }
            }
        }


        public List<Ad> GetAdViaSort(string SortingMethod)
        {
            using (var context = new DSContext())
            {
                if (SortingMethod == "byId")
                {
                    return context.Ads.OrderBy(x => x.ID).ToList();
                }
                else if (SortingMethod == "latest")
                {
                    return context.Ads.OrderByDescending(x => x.EntryDate).ToList();

                }
                else if(SortingMethod == "lowtohigh")
                {
                    var sortedAds = context.Ads
                            .ToList() // Fetch the ads into memory
                            .OrderBy(x => float.Parse(x.Price))
                            .ToList();
                    return sortedAds;
                }
                else if(SortingMethod == "hightolow")
                {
                    var sortedAds = context.Ads
                          .ToList() // Fetch the ads into memory
                          .OrderByDescending(x => float.Parse(x.Price))
                          .ToList();
                    return sortedAds;

                }
                else
                {
                    return context.Ads.OrderBy(x => x.ItemName).ToList();

                }
            }
        }

        public List<Ad> GetAdWithTime(string SearchTerm = "")
        {
            using (var context = new DSContext())
            {
                List<Ad> ads;
                if (SearchTerm != "")
                {
                    ads = context.Ads.Where(p => p.ItemName != null && p.ItemName.ToLower()
                                        .Contains(SearchTerm.ToLower()))
                                        .OrderBy(x => x.ItemName)
                                        .ToList();
                }
                else
                {
                    ads = context.Ads.OrderBy(x => x.ItemName).ToList();
                }


                return ads;
            }
        }

        public List<Ad> GetAdWithTime(string Category,string SearchTerm = "")
        {
            using (var context = new DSContext())
            {
                List<Ad> ads;
                if (Category != "All")
                {
                    if (SearchTerm != "")
                    {
                        ads = context.Ads.Where(p => p.ItemCategory == Category && p.ItemName != null && p.ItemName.ToLower()
                                            .Contains(SearchTerm.ToLower()))
                                            .OrderBy(x => x.ItemName)
                                            .ToList();
                    }
                    else
                    {
                        ads = context.Ads.Where(x => x.ItemCategory == Category).OrderBy(x => x.ItemName).ToList();
                    }
                }
                else
                {
                    if (SearchTerm != "")
                    {
                        ads = context.Ads.Where(p => p.ItemName != null && p.ItemName.ToLower()
                                            .Contains(SearchTerm.ToLower()))
                                            .OrderBy(x => x.ItemName)
                                            .ToList();
                    }
                    else
                    {
                        ads = context.Ads.OrderBy(x => x.ItemName).ToList();
                    }
                }


                return ads;
            }
        }




        public Ad GetAd(int ID)
        {
            using (var context = new DSContext())
            {

                return context.Ads.Find(ID);

            }
        }

        public void SaveAd(Ad Ad)
        {
            using (var context = new DSContext())
            {
                context.Ads.Add(Ad);
                context.SaveChanges();
            }
        }

        public void UpdateAd(Ad Ad)
        {
            using (var context = new DSContext())
            {
                context.Entry(Ad).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void DeleteAd(int ID)
        {
            using (var context = new DSContext())
            {

                var Ad = context.Ads.Find(ID);
                context.Ads.Remove(Ad);
                context.SaveChanges();
            }
        }







        public List<AdImage> GetAdImages(int ID)
        {
            using (var context = new DSContext())
            {

                return context.AdImages.Where(x => x.AdID == ID).ToList();
            }
        }





        public AdImage GetAdImage(int ID)
        {
            using (var context = new DSContext())
            {

                return context.AdImages.Find(ID);

            }
        }

        public void SaveAdImage(AdImage AdImage)
        {
            using (var context = new DSContext())
            {
                context.AdImages.Add(AdImage);
                context.SaveChanges();
            }
        }

        public void UpdateAdImage(AdImage AdImage)
        {
            using (var context = new DSContext())
            {
                context.Entry(AdImage).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void DeleteAdImage(int ID)
        {
            using (var context = new DSContext())
            {

                var AdImage = context.AdImages.Find(ID);
                context.AdImages.Remove(AdImage);
                context.SaveChanges();
            }
        }





    }
}

