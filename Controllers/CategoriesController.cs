using DealDash.Entities;
using DealDash.web.ViewModels;
using DealDouble1.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DealDash.web.Controllers
{
    public class CategoriesController : Controller
    {
        CategoriesService categoriesService = new CategoriesService();
        [HttpGet]
        public ActionResult Index()
        {

            CategoryDetailsViewModels model = new CategoryDetailsViewModels();
            model.PageTitle = "Categories";
            model.PageDescription = "Categories Listing Page";

            return View(model);


        }
        [HttpGet]
        public ActionResult Listing()
        {

            CategoriesListingViewModels model = new CategoriesListingViewModels();
            model.Categories = categoriesService.GetAllCategories();
            return PartialView(model);

        }
        [HttpGet]
        public ActionResult Create()
        {
           CategoryViewModels model = new CategoryViewModels();
           
            return PartialView(model);

        }
        [HttpPost]
        public ActionResult Create(CategoryViewModels model)
        {
            Category category = new Category();
            category.Name = model.Name;
            category.Description = model.Description;
           

            categoriesService.SaveCategory(category);

            return RedirectToAction("Listing");

        }

        [HttpGet]
        public ActionResult Edit(int ID)
        {
            CategoryViewModels model = new CategoryViewModels();
            var category = categoriesService.GetCategoryByID(ID);
            model.ID = category.ID;
            model.Name = category.Name;
            model.Description = category.Description;
         return PartialView(model);

        }
        [HttpPost]
        public ActionResult Edit(CategoryViewModels model)
        {
            Category category = new Category();
            category.ID = model.ID;
            category.Name = model.Name;
            category.Description = model.Description;
           
            
            categoriesService.UpdateCategory(category);

            return RedirectToAction("Listing");



        }
        [HttpPost]
        public ActionResult Delete(Category category)
        {
            categoriesService.DeleteCategory(category);

            return RedirectToAction("Listing");

        }
        [HttpGet]
        public ActionResult Details(int ID)
        {
           CategoryDetailsViewModels model = new CategoryDetailsViewModels();

            model.Category = categoriesService.GetCategoryByID(ID);
            model.PageTitle = "Category Details:" + model.Category.Name;
            model.PageDescription = model.Category.Description.Substring(0, 10);



            return View(model);

        }

    }
}