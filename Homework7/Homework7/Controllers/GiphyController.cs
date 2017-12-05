using Homework7.DAL;
using Homework7.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Homework7.Controllers
{
    public class GiphyController : Controller
    {
        //Database Context
        SearchDataContext db = new SearchDataContext();

        // GET: Giphy
        /// <summary>
        /// Main page
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        // GET: Search
        /// <summary>
        /// Giphy Search
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult Search()
        {
            List<GiphyResult> list = new List<GiphyResult>();
            //URI to be built base on input
            string httpQuery = "http://api.giphy.com/v1/gifs/search?api_key="
                             + "DjAlDDSXi04C9p3UQRm557sfSwZQAuXK"
                             + "&q=" 
                             + Request.QueryString["find"]
                             + "&rating="
                             + Request.QueryString["rating"];

            //New web request with the URI above
            WebRequest dataRequest = WebRequest.Create(httpQuery);
            //Get the response stream from Giphy
            Stream dataStream = dataRequest.GetResponse().GetResponseStream();

            var returnedData = new System.Web.Script.Serialization.JavaScriptSerializer()
                                   .Deserialize<RootObject>(new StreamReader(dataStream)
                                   .ReadToEnd());
            dataStream.Close();

            for (int i = 0; i < 25; i++)
            {
                GiphyResult result = new GiphyResult();
                result.image = returnedData.data[i].images.downsized_medium.url;
                list.Add(result);
            }

            //Get the querying user's information
            string ipAddress = Request.UserHostAddress;
            string userAgent = Request.UserAgent;
            string search = Request.QueryString["find"];

            //New DataLog object for storing the user's information
            DataLog dl = new DataLog();

            //Store the querying user's information
            dl.IPAddress = ipAddress;
            dl.Agent = userAgent;
            dl.SearchDate = DateTime.Now;
            dl.SearchRequest = search;

            //Add the object to the database
            db.datalogs.Add(dl);
            db.SaveChanges();

            //Return the results to the calling method
            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}