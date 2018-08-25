using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Data;
using ProductCatalog.Models;
using ProductCatalog.ViewModels.ProductViewModels;

namespace ProductCatalog.Controllers
{
    public class ProductController : Controller
    {
        private readonly StoreDataContext _context;

        public ProductController(StoreDataContext context)
        {
            _context = context;
        }

        [Route("v1/products")]
        [HttpGet]
        public IEnumerable<ListProductViewModel> Get()
        {
            return _context.Products
                .Include(x => x.Category)
                .Select(s => new ListProductViewModel
                {
                    Id = s.Id,
                    Title = s.Title,
                    Price = s.Price,
                    Category = s.Category.Title,
                    CategoryId = s.Category.Id
                })
                .AsNoTracking()
                .ToList();
        }

        [Route("v1/products/{id}")]
        [HttpGet]
        public Product Get(int id)
        {
            return _context.Products
                .AsNoTracking()
                .Where(x => x.Id == id)
                .FirstOrDefault();
        }

        [Route("v1/products")]
        [HttpPost]
        public ResultViewModel Post([FromBody] EditorProductViewModel model)
        {
            // model.Validate();
            
            // if (model.Invalid)
            // {
            //     return new ResultViewModel
            //     {
            //         Success = false,
            //         Message = "",
            //         Data = model.Notifications
            //     };
            // }

            var product = new Product();
            product.Title = model.Title;
            product.CategoryId = model.CategoryId;
            product.CreateDate = DateTime.Now;
            product.Description = model.Description;
            product.Image = model.Image;
            product.LastUpdateDate = DateTime.Now;
            product.Price = model.Price;
            product.Quantity = model.Quantity;

            _context.Products.Add(product);
            _context.SaveChanges();

            return new ResultViewModel
            {
                Success = true,
                Message = "Produto cadastrado com sucesso",
                Data = product
            };
        }

        [Route("v1/products")]
        [HttpPut]
        public ResultViewModel Put([FromBody] EditorProductViewModel model)
        {
            // model.Validate();
            
            // if (model.Invalid)
            // {
            //     return new ResultViewModel
            //     {
            //         Success = false,
            //         Message = "",
            //         Data = model.Notifications
            //     };
            // }

            var product = _context.Products.Find(model.Id);
            product.Title = model.Title;
            product.CategoryId = model.CategoryId;
            product.Description = model.Description;
            product.Image = model.Image;
            product.LastUpdateDate = DateTime.Now;
            product.Price = model.Price;
            product.Quantity = model.Quantity;

            _context.Products.Add(product);
            _context.SaveChanges();

            return new ResultViewModel
            {
                Success = true,
                Message = "Produto alterado com sucesso",
                Data = product
            };
        }
    }
}