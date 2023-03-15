using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StyleSphere.Models;

namespace StyleSphere.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly StyleSphereDbContext _context;

        public ProductsController(StyleSphereDbContext context)
        {
            _context = context;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        [Route("ProductByCategory")]
        [HttpGet]
        public async Task<ActionResult<ProductViewModel>> GetTblProductByCategory(int id)
        {
            List<ProductViewModel> products = new List<ProductViewModel>();
            var tblProduct = _context.Products.Where(e => e.CategoryId == id).ToList();
            products = _GetProductViewModels(tblProduct);
            return Ok(Ok(products));
        }

        [Route("ProductBySubCategory")]
        [HttpGet]
        public async Task<ActionResult<ProductViewModel>> GetTblProductBySubCategory(int id)
        {
            List<ProductViewModel> products = new List<ProductViewModel>();
            var tblProduct = _context.Products.Where(e => e.SubCategoryId == id).ToList();
            products = _GetProductViewModels(tblProduct);
            return Ok(products);
        }

        [Route("ProductUnderPrice")]
        [HttpGet]
        public async Task<ActionResult<ProductViewModel>> GetTblProductUnderPrice(decimal price)
        {
            List<ProductViewModel> products = new List<ProductViewModel>();
            var tblProduct = _context.Products.Where(e => e.Price <= price).ToList();
            products = _GetProductViewModels(tblProduct);
            return Ok(products);
        }

        [Route("search")]
        [HttpPost]
        public async Task<ActionResult<ProductViewModel>> SearchProduct(string SearchText)
        {
            List<ProductViewModel> products = new List<ProductViewModel>();
            var tblproduct3 = _context.Products.Where(e => e.ProductName.Contains(SearchText) || e.Description.Contains(SearchText)).ToList();
            products = _GetProductViewModels(tblproduct3);
            return Ok(products);
        }
        private List<ProductViewModel> _GetProductViewModels(List<Product> tblproduct)
        {
            List<ProductViewModel> products = new List<ProductViewModel>();
            foreach (var items in tblproduct)
            {
                ProductViewModel model = new ProductViewModel();
                model.ProductId = items.ProductId;
                model.ProductName = items.ProductName;
                model.Image1 = items.Image1;
                model.Image2 = items.Image2;
                model.Image3 = items.Image3;
                model.ThumbnailImage = items.ThumbnailImage;
                model.Price = items.Price;
                model.Description = items.Description;
                //model.ColorCount = items.TblProductMappings.Select(a => a.ColorId).Distinct().Count();
                // Ratings Count
                var ratingsData = _context.Ratings.Where(a => a.ProductId == items.ProductId).ToList();
                model.NoOfRatings = ratingsData.Count();
                if (ratingsData.Count() > 0)
                    model.ratings = (ratingsData.Sum(a => a.Rating1) / ratingsData.Count());
                else
                    model.ratings = 0;

                List<SizesMaster> sizeList = new List<SizesMaster>();
                List<ColorMaster> ColorList = new List<ColorMaster>();
                var mapppingsData = _context.ProductMappings.Where(a => a.ProductId == items.ProductId).ToList();
                model.ColorCount = mapppingsData.Select(a => a.ColorId).Distinct().Count();
                foreach (var item in mapppingsData)
                {
                    var colorData = _context.ColorMasters.Where(a => a.ColorId == item.ColorId).FirstOrDefault();
                    var sizeData = _context.SizesMasters.Where(a => a.SizeId == item.SizeId).FirstOrDefault();

                    if (sizeData != null)
                    {
                        SizesMaster objSize = new SizesMaster();
                        objSize.SizeId = item.SizeId;
                        objSize.Eusize = sizeData.Eusize;
                        objSize.Ussize = sizeData.Ussize;
                        sizeList.Add(objSize);
                    }

                    if (colorData != null)
                    {
                        ColorMaster objColor = new ColorMaster();
                        objColor.ColorId = item.ColorId;
                        objColor.Color = colorData.Color;
                        ColorList.Add(objColor);
                    }
                }
                model.ColorList = ColorList;
                model.sizeList = sizeList;
                products.Add(model);
            }
            return products;
        }
    }
}

    