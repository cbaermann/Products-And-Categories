using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;


namespace ProductsCategories.Models
{
    public class ViewCategoryViewModel
    {
       public List<Product> ProductList {get;set;}
       public Category currentCategory {get;set;}
       public Association Association {get;set;}
    }
}