using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RentPe.Controllers
{
    public class SharedController : Controller
    {
        // GET: Shared
        public JsonResult UploadImage()
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            try
            {
                var file = Request.Files[0];

                var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);

                var path = Path.Combine(Server.MapPath("~/Photos/"), fileName);
                file.SaveAs(path);
                result.Data = new { success = true, DocURL = string.Format("/Photos/{0}", fileName) };
            }
            catch (Exception ex)
            {
                result.Data = new { success = false, Message = ex.Message };
                throw;
            }
            return result;
        }


        public JsonResult UploadProof()
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            try
            {
                var file = Request.Files[0];

                var fileName = DateTime.Now.ToString("dd-MM-yyyy") + file.FileName;
                var directoryPath = Server.MapPath("~/Proofs/");

                var path = Path.Combine(directoryPath, fileName);

                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath); // Create the directory if it doesn't exist
                }
                file.SaveAs(path);
                result.Data = new { success = true, DocURL = string.Format("/Proofs/{0}", fileName) };
            }
            catch (Exception ex)
            {
                result.Data = new { success = false, Message = ex.Message };
                throw;
            }
            return result;
        }


        public JsonResult UploadImages()
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            try
            {
                List<string> imageUrls = new List<string>();

                foreach (string fileName in Request.Files)
                {
                    var file = Request.Files[fileName];

                    var uniqueFileName = Guid.NewGuid() + Path.GetExtension(file.FileName);

                    var path = Path.Combine(Server.MapPath("~/Photos/"), uniqueFileName);
                    file.SaveAs(path);

                    string imageUrl = string.Format("/Photos/{0}", uniqueFileName);
                    imageUrls.Add(imageUrl);
                }

                result.Data = new { success = true, ImageUrls = imageUrls.Select(url => new { Url = url }) };
            }
            catch (Exception ex)
            {
                result.Data = new { success = false, Message = ex.Message };
                throw;
            }

            return result;
        }




    }
}