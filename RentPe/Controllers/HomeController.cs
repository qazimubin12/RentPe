using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Office.Interop.Word;
using RentPe.Entities;
using RentPe.Services;
using RentPe.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using System.Xml.Linq;

namespace RentPe.Controllers
{
    public class HomeController : Controller
    {
        private AMSignInManager _signInManager;
        private AMUserManager _userManager;
        public AMSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<AMSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }
        public AMUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<AMUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        private AMRolesManager _rolesManager;
        public AMRolesManager RolesManager
        {
            get
            {
                return _rolesManager ?? HttpContext.GetOwinContext().GetUserManager<AMRolesManager>();
            }
            private set
            {
                _rolesManager = value;
            }
        }

        public ActionResult CustomOffer(int ID)
        {
            CustomOfferViewModel model = new CustomOfferViewModel();
            model.ItemFull = AdServices.Instance.GetAd(ID);
            model.Rentee = User.Identity.GetUserId();
            model.OwnerFull = UserManager.FindById(model.ItemFull.UserID);
            return PartialView("_CustomOffer", model);
        }

        [HttpPost]
        public ActionResult ContactUs(ContactUsViewModel model)
        {
            //Add saved
            var query = new Query();
            query.PhoneNumber = model.PhoneNumber;
            query.Subject = model.Subject;
            query.Email = model.Email;
            query.Message = model.Message;
            query.Name = model.Name;
            QueryServices.Instance.SaveQuery(query);

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SendCustomOffer(CustomOfferViewModel model)
        {
            var customOffer = new CustomOffer();
            customOffer.OfferDate = DateTime.Now;
            customOffer.OfferedPrice = model.OfferedPrice;
            customOffer.ReturnDate = model.ReturnDate;
            customOffer.Rentee = model.Rentee;
            customOffer.RentingDate = model.RentingDate;
            customOffer.Owner = model.Owner;
            customOffer.Item = model.Item;
            var itemFull = AdServices.Instance.GetAd(model.Item);
            customOffer.RentingPreiod = model.RentingPreiod;
            customOffer.Status = "PENDING";
            CustomOfferServices.Instance.SaveCustomOffer(customOffer);

            var conversation = new Conversation();
            conversation.Message = model.Message+" "+itemFull.ItemName;
            conversation.SentBy = User.Identity.GetUserId();
            conversation.RecievedBy = model.Owner;
            conversation.Date = DateTime.Now;
            ConversationServices.Instance.SaveConversation(conversation);   
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetProductDetails(int productId)
        {
            // Replace this with your logic to retrieve product details from your data source

            var product = AdServices.Instance.GetAd(productId);
          
            return Json(product, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Index()
        {
            Session["ACTIVER"] = "HOME";
            HomeViewModel model = new HomeViewModel();
            var ExclusiveAds = AdServices.Instance.GetAdWithTime().Where(x => x.Tag == "Exclusive").ToList();
            var LatestAds = AdServices.Instance.GetAdWithTime().OrderByDescending(item => item.ID).Take(8).ToList();
            var FeaturedAds = AdServices.Instance.GetAd().Where(x => x.Featured == "Yes").ToList();
            model.ItemsCategories = CategoryServices.Instance.GetRentItemCategories();

            var ExclusiveList = new List<AdWithTimeModel>();
            foreach (var ad in ExclusiveAds)
            {
                DateTime adEntryDate = ad.EntryDate; // Assuming the entry date property of the Ad entity is named "EntryDate"

                // Calculate the time difference between the ad entry date and the current date
                TimeSpan timeDifference = DateTime.Now - adEntryDate;

                // Initialize variables to store the result
                string result;
                int totalMinutes = (int)timeDifference.TotalMinutes;
                int totalHours = (int)timeDifference.TotalHours;
                int totalDays = (int)timeDifference.TotalDays;

                // Check the time difference and format the result accordingly
                if (totalMinutes < 60)
                {
                    result = $"{totalMinutes} minutes";
                }
                else if (totalHours < 24)
                {
                    result = $"{totalHours} hours";
                }
                else
                {
                    result = $"{totalDays} days";
                }

                // Create a new AdWithTime object and add it to the list
                AdWithTimeModel adWithTime = new AdWithTimeModel
                {
                    Ad = ad,
                    Time = result
                };
                ExclusiveList.Add(adWithTime);
            }


            var LatestList = new List<AdWithTimeModel>();
            foreach (var ad in LatestAds)
            {
                DateTime adEntryDate = ad.EntryDate; // Assuming the entry date property of the Ad entity is named "EntryDate"

                // Calculate the time difference between the ad entry date and the current date
                TimeSpan timeDifference = DateTime.Now - adEntryDate;

                // Initialize variables to store the result
                string result;
                int totalMinutes = (int)timeDifference.TotalMinutes;
                int totalHours = (int)timeDifference.TotalHours;
                int totalDays = (int)timeDifference.TotalDays;

                // Check the time difference and format the result accordingly
                if (totalMinutes < 60)
                {
                    result = $"{totalMinutes} minutes";
                }
                else if (totalHours < 24)
                {
                    result = $"{totalHours} hours";
                }
                else
                {
                    result = $"{totalDays} days";
                }

                // Create a new AdWithTime object and add it to the list
                AdWithTimeModel adWithTime = new AdWithTimeModel
                {
                    Ad = ad,
                    Time = result
                };
                LatestList.Add(adWithTime);
            }


            var FeaturedList = new List<AdWithTimeModel>();
            foreach (var ad in FeaturedAds)
            {
                DateTime adEntryDate = ad.EntryDate; // Assuming the entry date property of the Ad entity is named "EntryDate"

                // Calculate the time difference between the ad entry date and the current date
                TimeSpan timeDifference = DateTime.Now - adEntryDate;

                // Initialize variables to store the result
                string result;
                int totalMinutes = (int)timeDifference.TotalMinutes;
                int totalHours = (int)timeDifference.TotalHours;
                int totalDays = (int)timeDifference.TotalDays;

                // Check the time difference and format the result accordingly
                if (totalMinutes < 60)
                {
                    result = $"{totalMinutes} minutes";
                }
                else if (totalHours < 24)
                {
                    result = $"{totalHours} hours";
                }
                else
                {
                    result = $"{totalDays} days";
                }

                // Create a new AdWithTime object and add it to the list
                AdWithTimeModel adWithTime = new AdWithTimeModel
                {
                    Ad = ad,
                    Time = result
                };
                FeaturedList.Add(adWithTime);
            }

            model.ExclusiveAds = ExclusiveList;
            model.FeaturedAds = FeaturedList;
            model.LatestAds = LatestList;
            return View(model);
        }



        [HttpPost]
        public ActionResult SaveRating(int Stars,string Name,string Email,string Message,string UserID)
        {
            var Rating = new UserRating();  
            Rating.Name = Name;
            Rating.Email = Email;
            Rating.Message = Message;

            Rating.Stars = Stars * 20;
            Rating.UserID = UserID;
            Rating.Date = DateTime.Now;
            UserRatingServices.Instance.SaveUserRating(Rating);
            return Json(new {success=true},JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult PostAd()
        {
            if (User.Identity.IsAuthenticated)
            {
                ProductViewModel model = new ProductViewModel();
                model.ItemCategories = CategoryServices.Instance.GetRentItemCategories();

                return View("PostAd", "_Layout", model);
            }
            else
            {
                return RedirectToAction("Register", "Account");
            }
        }

        [HttpPost]
        public ActionResult PostAd(ProductViewModel model)
        {
            if (model.ID != 0)
            {
                var ad = AdServices.Instance.GetAd(model.ID);
                ad.ID = model.ID;
                ad.UserName = model.UserName;
                ad.Contact = model.Contact;
                ad.Privacy = model.Privacy;
                ad.ItemName = model.ItemName;
                ad.UserID = model.UserID;
                ad.ItemDescription = model.ItemDescription;
                ad.AvailableFrom = model.AvailableFrom;
                ad.AvailableTo = model.AvailableTo;
                ad.Authenticity = model.Authenticity;
                ad.ItemCategory = model.ItemCategory;
                ad.Type = model.Type;
                ad.Negotiable = model.Negotiable;
                ad.Condition = model.Condition;
                ad.EntryDate = DateTime.Now;
                ad.Location = model.Location;
                ad.Price = model.Price;
                ad.Note = model.Note;
                ad.AdStatus = model.AdStatus;
                ad.RentingPeriod = model.RentingPeriod;
                ad.Featured = model.Featured;
                AdServices.Instance.UpdateAd(ad);
                return Json(new {success=true},JsonRequestBehavior.AllowGet);
            }
            else
            {
                var ad = new Ad();
                ad.ID = model.ID;
                ad.UserName = model.UserName;
                ad.Contact = model.Contact;
                ad.Privacy = model.Privacy;
                ad.ItemName = model.ItemName;
                ad.UserID = model.UserID;
                ad.ItemDescription = model.ItemDescription;
                ad.AvailableFrom = model.AvailableFrom;  //ye nayta hai
                ad.AvailableTo = model.AvailableTo;
                ad.Authenticity = model.Authenticity;
                ad.ItemCategory = model.ItemCategory;
                ad.Type = model.Type;
                ad.Negotiable = model.Negotiable;
                ad.Condition = model.Condition;
                ad.EntryDate = DateTime.Now;
                ad.Location = model.Location;
                ad.Price = model.Price;
                ad.Note = model.Note;
                ad.AdStatus = model.AdStatus;
                ad.RentingPeriod = model.RentingPeriod;
                ad.Featured = model.Featured;
                AdServices.Instance.SaveAd(ad);
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
        }


        //public ActionResult Product(int ID)
        //{
        //    ProductViewModel model = new ProductViewModel();

        //}

        public string RenderAdsHtml(List<AdWithTimeModel> ads)
        {
            // Render the ads HTML as a string
            StringBuilder htmlBuilder = new StringBuilder();
            var updatedAds = new List<AdWithTimeModel>();
            
            foreach (var item in ads)
            {
                // Build the HTML for each ad item
                htmlBuilder.Append("<div class='col-md-4 col-6'>");
                htmlBuilder.Append("<div class='product'>");
                htmlBuilder.Append("<div class='product_img'>");
                htmlBuilder.Append("<a href='@Url.Action(\"View\", \"Home\", new {ID=item.Ad.ID })'>");
                htmlBuilder.Append($"<img src='{item.Ad.MainImage}' alt='product_img1'>");
                htmlBuilder.Append("</a>");
                htmlBuilder.Append("<div class='product_action_box'>");
                htmlBuilder.Append("<ul class='list_none pr_action_btn'>");
                htmlBuilder.Append($"<li class='add-to-cart'><a href='@Url.Action(\"View\", \"Home\", new {{ID=item.Ad.ID}})'><i class='icon-basket-loaded'></i> Add To Cart</a></li>");
                htmlBuilder.Append("<li class='add-to-favorites'>");
                htmlBuilder.Append($"<a data-id='{item.Ad.ID}' onclick='addToFavorites(\"{item.Ad.ID}\")'>");
                htmlBuilder.Append($"<i data-flow='{item.Ad.ID}' class='fa fa-solid fa-heart' style='color: black;'></i>");
                htmlBuilder.Append("</a>");
                htmlBuilder.Append("</li>");
                htmlBuilder.Append("</ul>");
                htmlBuilder.Append("</div>");
                htmlBuilder.Append("</div>");
                htmlBuilder.Append("<div class='product_info'>");
                htmlBuilder.Append($"<h6 class='product_title'><a href='@Url.Action(\"View\", \"Home\", new {{ID=item.Ad.ID }})'>{item.Ad.ItemName}</a></h6>");
                htmlBuilder.Append("<div class='product_price'>");
                htmlBuilder.Append($"<span class='price'>{item.Ad.Price} PKR</span>");
                htmlBuilder.Append("</div>");
                htmlBuilder.Append("<div class='pr_desc'>");
                htmlBuilder.Append($"<p><b>{item.Ad.ItemCategory}</b></p>");
                htmlBuilder.Append("<p>");
                htmlBuilder.Append(item.Ad.ItemDescription);
                htmlBuilder.Append("</p>");
                htmlBuilder.Append("</div>");
                htmlBuilder.Append("<div class='list_product_action_box'>");
                htmlBuilder.Append("<ul class='list_none pr_action_btn'>");
                htmlBuilder.Append("<li class='add-to-cart'><a href='#'><i class='icon-basket-loaded'></i> Add To Cart</a></li>");
                htmlBuilder.Append("<li><a href='shop-compare.html' class='popup-ajax'><i class='icon-shuffle'></i></a></li>");
                htmlBuilder.Append("<li><a href='shop-quick-view.html' class='popup-ajax'><i class='icon-magnifier-add'></i></a></li>");
                htmlBuilder.Append("<li class='add-to-favorites'>");
                htmlBuilder.Append($"<a data-id='{item.Ad.ID}' onclick='addToFavorites(\"{item.Ad.ID}\")'>");
                htmlBuilder.Append($"<i data-flow='{item.Ad.ID}' class='fa fa-solid fa-heart' style='color: black;'></i>");
                htmlBuilder.Append("</a>");
                htmlBuilder.Append("</li>");
                htmlBuilder.Append("</ul>");
                htmlBuilder.Append("</div>");
                htmlBuilder.Append("<hr />");
                htmlBuilder.Append($"<span>{item.Time}</span>");
                htmlBuilder.Append("</div>");
                htmlBuilder.Append("</div>");
                htmlBuilder.Append("</div>");
            }

            return htmlBuilder.ToString();
        }

        public ActionResult SortAds(string sortingMethod)
        {
            // Retrieve the updated ads based on the selected sorting method
            var updatedAds = AdServices.Instance.GetAdViaSort(sortingMethod);
            var AdsList = new List<AdWithTimeModel>();
            foreach (var ad in updatedAds)
            {
                DateTime adEntryDate = ad.EntryDate; // Assuming the entry date property of the Ad entity is named "EntryDate"

                // Calculate the time difference between the ad entry date and the current date
                TimeSpan timeDifference = DateTime.Now - adEntryDate;

                // Initialize variables to store the result
                string result;
                int totalMinutes = (int)timeDifference.TotalMinutes;
                int totalHours = (int)timeDifference.TotalHours;
                int totalDays = (int)timeDifference.TotalDays;

                // Check the time difference and format the result accordingly
                if (totalMinutes < 60)
                {
                    result = $"{totalMinutes} minutes";
                }
                else if (totalHours < 24)
                {
                    result = $"{totalHours} hours";
                }
                else
                {
                    result = $"{totalDays} days";
                }

                // Create a new AdWithTime object and add it to the list
                AdWithTimeModel adWithTime = new AdWithTimeModel
                {
                    Ad = ad,
                    Time = result
                };
                AdsList.Add(adWithTime);
            }


            var updatedAdsHtml = RenderAdsHtml(AdsList); ;
            return Json(updatedAdsHtml, JsonRequestBehavior.AllowGet);

        }

        public ActionResult View(int ID)
        {
            AdActionViewModel model = new AdActionViewModel();
            var ad = AdServices.Instance.GetAd(ID);
            model.ID = ad.ID;
            model.UserName = ad.UserName;
            model.Contact = ad.Contact;
            model.Privacy = ad.Privacy;
            model.ItemName = ad.ItemName;
            model.UserID = ad.UserID;
            model.ItemDescription = ad.ItemDescription;
            model.AvailableFrom = ad.AvailableFrom;
            model.AvailableTo = ad.AvailableTo;
            model.Authenticity = ad.Authenticity;
            model.ItemCategory = ad.ItemCategory;
            model.Type = ad.Type;
            model.Negotiable = ad.Negotiable;
            model.Condition = ad.Condition;
            model.EntryDate = ad.EntryDate;
            model.Location = ad.Location;
            model.Price = ad.Price;
            model.Note = ad.Note;
            model.AdStatus = ad.AdStatus;
            model.RentingPeriod = ad.RentingPeriod;
            model.Featured = ad.Featured;
            model.Tag = ad.Tag;
            model.MainImage = ad.MainImage;

            model.otherImages = AdServices.Instance.GetAdImages(ad.ID).Select(x => x.ImageURL).ToList();
            var RelatedAds = AdServices.Instance.GetAd().Where(x => x.ItemCategory == ad.ItemCategory).Take(5).ToList();
            var RelatedAdLists = new List<AdWithTimeModel>();
            foreach (var adNew in RelatedAds)
            {
                DateTime adEntryDate = adNew.EntryDate; // Assuming the entry date property of the Ad entity is named "EntryDate"

                // Calculate the time difference between the ad entry date and the current date
                TimeSpan timeDifference = DateTime.Now - adEntryDate;

                // Initialize variables to store the result
                string result;
                int totalMinutes = (int)timeDifference.TotalMinutes;
                int totalHours = (int)timeDifference.TotalHours;
                int totalDays = (int)timeDifference.TotalDays;

                // Check the time difference and format the result accordingly
                if (totalMinutes < 60)
                {
                    result = $"{totalMinutes} minutes";
                }
                else if (totalHours < 24)
                {
                    result = $"{totalHours} hours";
                }
                else
                {
                    result = $"{totalDays} days";
                }

                // Create a new AdWithTime object and add it to the list
                AdWithTimeModel adWithTime = new AdWithTimeModel
                {
                    Ad = ad,
                    Time = result
                };
                RelatedAdLists.Add(adWithTime);
            }
            model.RelatedAds = RelatedAdLists;

            var userRatings= UserRatingServices.Instance.GetUserRating(model.UserID);
            var list = new List<UserRatingModel>();
            foreach (var userRating in userRatings)
            {
                var userinfo = UserInfoServices.Instance.GetUserInfo(model.UserID);
                list.Add(new UserRatingModel { UserInfo = userinfo, UserRating = userRating });
            }
            model.UserRatings = list;
            return View(model);
        }

        public ActionResult Shop()
        {
            Session["ACTIVER"] = "SHOP";

            HomeShopViewModel model = new HomeShopViewModel();
            model.ItemsCategories = CategoryServices.Instance.GetRentItemCategories();
            model.ItemCategory = CategoryServices.Instance.GetRentItemCategories();

            var ads = AdServices.Instance.GetAdWithTime();
            var List = new List<AdWithTimeModel>();
            foreach (var ad in ads)
            {
                DateTime adEntryDate = ad.EntryDate; // Assuming the entry date property of the Ad entity is named "EntryDate"

                // Calculate the time difference between the ad entry date and the current date
                TimeSpan timeDifference = DateTime.Now.Subtract(adEntryDate);

                // Initialize variables to store the result
                string result;
                int totalMinutes = (int)timeDifference.TotalMinutes;
                int totalHours = (int)timeDifference.TotalHours;
                int totalDays = (int)timeDifference.TotalDays;

                // Check the time difference and format the result accordingly
                if (totalMinutes < 60)
                {
                    result = $"{totalMinutes} minutes";
                }
                else if (totalHours < 24)
                {
                    result = $"{totalHours} hours";
                }
                else
                {
                    result = $"{totalDays} days";
                }

                // Create a new AdWithTime object and add it to the list
                AdWithTimeModel adWithTime = new AdWithTimeModel
                {
                    Ad = ad,
                    Time = result
                };
                List.Add(adWithTime);
            }

            model.Ads = List;


            return View("Shop", "_Layout", model);
        }

        public ActionResult ShopByCategoryandSearchTerm(string Category, string SearchTerm="")
        {
            Session["ACTIVER"] = "SHOP";

            HomeShopViewModel model = new HomeShopViewModel();
            model.ItemsCategories = CategoryServices.Instance.GetRentItemCategories();
            var ads = AdServices.Instance.GetAdWithTime(Category,SearchTerm);
            var List = new List<AdWithTimeModel>();
            foreach (var ad in ads)
            {
                DateTime adEntryDate = ad.EntryDate; // Assuming the entry date property of the Ad entity is named "EntryDate"

                // Calculate the time difference between the ad entry date and the current date
                TimeSpan timeDifference = DateTime.Now.Subtract(adEntryDate);

                // Initialize variables to store the result
                string result;
                int totalMinutes = (int)timeDifference.TotalMinutes;
                int totalHours = (int)timeDifference.TotalHours;
                int totalDays = (int)timeDifference.TotalDays;

                // Check the time difference and format the result accordingly
                if (totalMinutes < 60)
                {
                    result = $"{totalMinutes} minutes";
                }
                else if (totalHours < 24)
                {
                    result = $"{totalHours} hours";
                }
                else
                {
                    result = $"{totalDays} days";
                }

                // Create a new AdWithTime object and add it to the list
                AdWithTimeModel adWithTime = new AdWithTimeModel
                {
                    Ad = ad,
                    Time = result
                };
                List.Add(adWithTime);
            }

            model.Ads = List;


            return View("Shop", "_Layout", model);
        }


   
        public ActionResult ShopByCategory(string Category)
        {
            Session["ACTIVER"] = "SHOP";
            HomeShopViewModel model = new HomeShopViewModel();
            model.ItemsCategories = CategoryServices.Instance.GetRentItemCategories();
            var ads = AdServices.Instance.GetAdWithTime().Where(x=>x.ItemCategory == Category).ToList();
            var List = new List<AdWithTimeModel>();
            foreach (var ad in ads)
            {
                DateTime adEntryDate = ad.EntryDate; // Assuming the entry date property of the Ad entity is named "EntryDate"

                // Calculate the time difference between the ad entry date and the current date
                TimeSpan timeDifference = DateTime.Now.Subtract(adEntryDate);

                // Initialize variables to store the result
                string result;
                int totalMinutes = (int)timeDifference.TotalMinutes;
                int totalHours = (int)timeDifference.TotalHours;
                int totalDays = (int)timeDifference.TotalDays;

                // Check the time difference and format the result accordingly
                if (totalMinutes < 60)
                {
                    result = $"{totalMinutes} minutes";
                }
                else if (totalHours < 24)
                {
                    result = $"{totalHours} hours";
                }
                else
                {
                    result = $"{totalDays} days";
                }

                // Create a new AdWithTime object and add it to the list
                AdWithTimeModel adWithTime = new AdWithTimeModel
                {
                    Ad = ad,
                    Time = result
                };
                List.Add(adWithTime);
            }

            model.Ads = List;


            return View("Shop", "_Layout", model);
        }

        public ActionResult About()
        {
            Session["ACTIVER"] = "ABOUT";

            ViewBag.Message = "Your application description page.";

            return View();
        }


        public ActionResult Contact()
        {
            Session["ACTIVER"] = "CONTACT";

            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Login(string Email, string Password)
        {
            if (!string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password))
            {
                return RedirectToAction("Dashboard", "Admin");
            }
            else
            {
                return View();
            }
        }



        //[HttpPost]
        //public ActionResult Login(string Email, string Password)
        //{
        //    var user = DBNull;/* AMUserManager(username, password);*/
        //    if (user != null)
        //    {
        //        Session["ID"] = user.ID.ToString();
        //        Session["UserName"] = user.UserName.ToString();
        //        Session["Email"] = user.Email.ToString();
        //        Session["Role"] = user.Role.ToString();
        //        if (user.Role == "Admin")
        //        {
        //            return RedirectToAction("AdminDashboard", "Home");
        //        }
        //        else if (user.Role == "Kitchen Staff")
        //        {
        //            return RedirectToAction("KitchenDashboard", "Home");
        //        }
        //        else if (user.Role == "Cashier")
        //        {
        //            return RedirectToAction("BillingDashboard", "Home");
        //        }

        //        else if (user.Role == "Waiter")
        //        {
        //            return RedirectToAction("WaiterApp", "Home");
        //        }
        //        else if (user.Role == "Kitchen Master")
        //        {
        //            return RedirectToAction("KitchenDashboard", "Home");
        //        }
        //    }
        //    else
        //    {

        //    }
        //    {
        //        string msg = "Invalid Password or UserName";
        //        TempData["ErrorMessage"] = msg;
        //    }

        //    Session["ID"] = null;
        //    Session["UserName"] = null;
        //    Session["Email"] = null;
        //    Session["Role"] = null;
        //    return RedirectToAction("Index", "Login");

        //}


    }
}