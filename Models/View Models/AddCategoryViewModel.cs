using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;


namespace ProductsCategories.Models
{
    public class AddCategoryViewModel
    {
        public Category newCategory {get;set;}
        public List<Category> Categories {get;set;}
    }
}