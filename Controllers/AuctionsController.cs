using DealDash.Entities;
using DealDouble1.Services;
using DealDash.web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Cryptography.X509Certificates;

namespace DealDash.web.Controllers
{
    public class AuctionsController : Controller
    {
        AuctionsService auctionService = new AuctionsService();
        CategoriesService categoriesService = new CategoriesService();
        private int length = 0;

        // GET: Auctions
        
        public ActionResult Index(int? categoryID, string searchTerm, int? pageNO)
        {
            
           AuctionsListingViewModels model = new AuctionsListingViewModels();
            model.PageTitle = "Auctions";
            model.PageDescription = "Auction Listing Page";

           model.CategoryID = categoryID;
           model.SearchTerm = searchTerm;
           model.PageNO = pageNO;
            model.Categories = categoriesService.GetAllCategories();

            return View(model);
            

        }
        public ActionResult Listing(int? categoryID, string searchTerm, int? pageNO)
        {
            var pageSize = 5;
            AuctionsListingViewModels model = new AuctionsListingViewModels();
            model.Auctions = auctionService.SearchAuctions(categoryID, searchTerm, pageNO, pageSize);
            var totalAuctions = auctionService.GetAuctionCount(categoryID, searchTerm);
            model.Pager = new Pager(totalAuctions, pageNO, pageSize);
          
            return PartialView(model);

        }
        [HttpGet]
        public ActionResult Listing()
        {

            AuctionsListingViewModels model = new AuctionsListingViewModels();
            model.Auctions = auctionService.GetAllAuctions();
            return PartialView(model);

        }
        [HttpGet]
        public ActionResult Create()
       { 
            CreateAuctionsViewModels model = new CreateAuctionsViewModels();
            model.Categories = categoriesService.GetAllCategories();
            return PartialView(model);

        }
        [HttpPost]
        public JsonResult Create(CreateAuctionsViewModels model)
        {
            JsonResult result = new JsonResult();
            if (ModelState.IsValid)
            {
                Auction auction = new Auction();
                auction.Title = model.Title;
                auction.CategoryID = model.CategoryID;
                auction.Description = model.Description;
                auction.ActualAmount = model.ActualAmount;
                auction.StartingTime = model.StartingTime;
                auction.EndingTime = model.EndingTime;
                if (!string.IsNullOrEmpty(model.AuctionPictures))
                {
                    var pictureIDs = model.AuctionPictures.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(ID => int.Parse(ID)).ToList();

                    auction.AuctionPictures = new List<AuctionPicture>();

                    auction.AuctionPictures.AddRange(pictureIDs.Select(x => new AuctionPicture() { PictureID = x }).ToList());
                }

                auctionService.SaveAuction(auction);

                result.Data = new { Success = true };
            }
            else
            {
                result.Data = new { Success = false , Error = "Unable to save Auction. Please enter valid values."};
            }
            return result;
        }

        [HttpGet]
        public ActionResult Edit(int ID)
        {
            CreateAuctionsViewModels model = new CreateAuctionsViewModels();
            var auction = auctionService.GetAuctionByID(ID);
            model.ID = auction.ID;
            model.Title = auction.Title;
            model.CategoryID = auction.CategoryID;
            model.Description = auction.Description;
            model.ActualAmount = auction.ActualAmount;
            model.StartingTime = auction.StartingTime;
            model.EndingTime = auction.EndingTime;

            model.Categories = categoriesService.GetAllCategories();
            model.AuctionPictureList = auction.AuctionPictures;

           
            return PartialView(model);

        }
        [HttpPost]
        public ActionResult Edit(CreateAuctionsViewModels model)
        {
            Auction auction = new Auction();
            auction.ID = model.ID;
            auction.Title = model.Title;
            auction.CategoryID = model.CategoryID;
            auction.Description = model.Description;
            auction.ActualAmount = model.ActualAmount;
            auction.StartingTime = model.StartingTime;
            auction.EndingTime = model.EndingTime;

            if (!string.IsNullOrEmpty(model.AuctionPictures))
            {

                var pictureIDs = model.AuctionPictures.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(ID => int.Parse(ID)).ToList();

                auction.AuctionPictures = new List<AuctionPicture>();

                auction.AuctionPictures.AddRange(pictureIDs.Select(x => new AuctionPicture() {AuctionID = auction.ID ,PictureID = x }).ToList());
            }
            auctionService.UpdateAuction(auction);

            return RedirectToAction("Listing");

            

        }
        [HttpPost]
        public ActionResult Delete(Auction auction)
        {
            auctionService.DeleteAuction(auction);

            return RedirectToAction("Listing");

        }
        [HttpGet]
        public ActionResult Details(int ID)
        {
             AuctionsDetailsViewModels model = new AuctionsDetailsViewModels();

            model.Auction = auctionService.GetAuctionByID(ID);
            model.PageTitle = "Auctions Details:" + model.Auction.Title;
            model.PageDescription = model.Auction.Description.Substring(0, length);

         

            return View(model);

        }




    }
}