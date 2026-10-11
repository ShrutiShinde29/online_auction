using DealDash.web.Models;
using DealDash.web.ViewModels;
using DealDouble1.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace DealDash.web.Controllers
{
    public class HomeController : Controller
    {
        AuctionsService service = new AuctionsService();
        public ActionResult Index()
        {
            AuctionsViewModels vModel = new AuctionsViewModels();
            vModel.PageTitle = "Home Page";
            vModel.PageDescription = "This is Home Page";

           vModel.AllAuctions = service.GetAllAuctions();
           vModel.PromotedAuctions = service.GetAllAuctions();


            return View(vModel);
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
       

        public ActionResult Feedback()
        {
            ViewBag.Message = "Your Feedback page.";
            return View();
        }
        


    }
}