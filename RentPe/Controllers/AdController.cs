using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RentPe.Controllers
{
    public class AdController : Controller
    {
        // GET: Ad
        public ActionResult Index(string SearchTerm = "")
        {

            return View();
        }
    }
}