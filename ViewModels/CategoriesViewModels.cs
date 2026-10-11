using DealDash.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DealDash.web.ViewModels
{
    public class CategoriesListingViewModels : PageViewModel
    {
        public List<Category> Categories { get; set; }
    }

    public class CategoriesViewModels: PageViewModel
    {
        public List<Category> AllCategories{ get; set; }

    }
    public class CategoryDetailsViewModels : PageViewModel
    {
        public Category Category { get; set; }
    }

    public class CategoryViewModels : PageViewModel
    {
         public string Name { get; set; }
        public int ID { get; set; }
        public string Description { get; set; }
       public List<Auction> Auctions { get; set; }
    }


}



