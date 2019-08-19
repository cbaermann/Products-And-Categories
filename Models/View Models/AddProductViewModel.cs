using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;


namespace ProductsCategories.Models
{
    public class AddProductViewModel
    {
        public Product newProduct {get;set;}
        public List<Product> Products {get;set;}
    }
}