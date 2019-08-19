using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;


namespace ProductsCategories.Models
{
    public class ViewProductViewModel
    {
       public List<Category> CategoryList {get;set;}
       public Product currentProduct {get;set;}
       public Association Association {get;set;}
    }
}