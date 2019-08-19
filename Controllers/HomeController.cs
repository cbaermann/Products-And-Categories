using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProductsCategories.Models;

namespace ProductsCategories.Controllers
{
    public class HomeController : Controller
    {
        private MyContext dbContext;
        public HomeController(MyContext context)
        {
            dbContext = context;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            AddProductViewModel viewModel = new AddProductViewModel();
            viewModel.Products = dbContext.Products
                .ToList();
            return View("Index", viewModel);
        }

        [HttpPost("addProduct")]

        public IActionResult addProduct(AddProductViewModel viewModel)
        {
            if(ModelState.IsValid)
            {
                Product newProduct = viewModel.newProduct;
                dbContext.Add(newProduct);
                dbContext.SaveChanges();
                return RedirectToAction("Index", new {id = newProduct.ProductId});
            }
            return Redirect("Index");
        }

        [HttpGet("/product/{ProductId}")]
        public IActionResult Product (int ProductId)
        {
            ViewProductViewModel viewModel = new ViewProductViewModel();
            viewModel.currentProduct = dbContext.Products
                .Include(p=> p.Associations)
                .ThenInclude(pc=>pc.Category)
                .FirstOrDefault(p=> p.ProductId == ProductId);
            viewModel.CategoryList = dbContext.Categories
                .Include(c=> c.Associations)
                .ThenInclude(pc=>pc.Product)
                .Where(c=> !viewModel.currentProduct.Associations.Select(pc=>pc.Category).Contains(c))
                .ToList();
            return View("Product", viewModel);
        }

        [HttpPost("addCategories/{ProductId}")]
        public IActionResult AddInCategory(ViewProductViewModel viewModel, int ProductId)
        {
            System.Console.WriteLine("******************************");
            viewModel.Association.ProductId = ProductId;

            dbContext.Add(viewModel.Association);
            dbContext.SaveChanges();
            System.Console.WriteLine("################################");
            return RedirectToAction("Product", new{ProductId = ProductId});
        }

        [HttpGet("Categories")]
        public IActionResult Categories()
        {
            AddCategoryViewModel viewModel = new AddCategoryViewModel();
            viewModel.Categories = dbContext.Categories
                .ToList();
            return View("Categories", viewModel);
        }

        [HttpPost("addCategory")]
        public IActionResult addCategory(AddCategoryViewModel viewModel)
        {
            if(ModelState.IsValid)
            {
                Category newCategory = viewModel.newCategory;
                dbContext.Add(newCategory);
                dbContext.SaveChanges();
                return RedirectToAction("Categories", new {id = newCategory.CategoryId});
            }
            return Redirect("Categories");
        }

        [HttpGet("/category/{CategoryId}")]
        public IActionResult Category(int CategoryId)
        {
            ViewCategoryViewModel viewModel = new ViewCategoryViewModel();
            viewModel.currentCategory = dbContext.Categories
                .Include(p=> p.Associations)
                .ThenInclude(pc=>pc.Product)
                .FirstOrDefault(p=> p.CategoryId == CategoryId);
            viewModel.ProductList = dbContext.Products
                .Include(c=> c.Associations)
                .ThenInclude(pc=>pc.Category)
                .Where(c=> !viewModel.currentCategory.Associations.Select(pc=>pc.Product).Contains(c))
                .ToList();
            return View("Category", viewModel);
        }

        [HttpPost("addProducts/{CategoryId}")]
        public IActionResult AddInProduct(ViewCategoryViewModel viewModel, int CategoryId)
        {
            viewModel.Association.CategoryId=CategoryId;
            dbContext.Add(viewModel.Association);
            dbContext.SaveChanges();
            return RedirectToAction("Category", new{CategoryId=CategoryId});
        }

       
        
    }
}
