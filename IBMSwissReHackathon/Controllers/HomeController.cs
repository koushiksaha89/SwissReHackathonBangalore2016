using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IBMSwissReHackathon.Respository;

namespace IBMSwissReHackathon.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var twitterdata=TwitterDataGather.GetData();
            var alchemydata = AlchemyDataNewsApiDataGather.GetPositiveData();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpGet]
        public ActionResult TwitterAPIData()
        {
            var twitterdata = TwitterDataGather.GetData();
            //var twitterdata = string.Empty;
            // var alchemydata = AlchemyDataNewsApiDataGather.GetPositiveData();
            return Json(twitterdata, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult AlchemyAPIPositiveData()
        {
            var alchemydata = AlchemyDataNewsApiDataGather.GetPositiveData();
            return Json(alchemydata, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult AlchemyAPINegativeeData()
        {
            var alchemydata = AlchemyDataNewsApiDataGather.GetNegativeData();
            return Json(alchemydata, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult AlchemyAPINeutralData()
        {
            var alchemydata = AlchemyDataNewsApiDataGather.GetNeutralData();
            return Json(alchemydata, JsonRequestBehavior.AllowGet);
        }
    }
}