using FirstASPDotNetApp.Data;
using FirstASPDotNetApp.Entities;
using FirstASPDotNetApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace FirstASPDotNetApp.Controllers
{
    public class ProductsController : Controller
    {
        MyDBContext _context;

        public ProductsController(MyDBContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ProductList()
        {
            List<ProductModel> products = new List<ProductModel>();
            products.Add(new ProductModel
            {
                Name = "Cap",
                Category = "Accesories",
                Description = "Bucket Hat",
                Price = 1500m
            });
            products.Add(new ProductModel
            {
                Name = "Shirt",
                Category = "Accesories",
                Description = "Native Wear",
                Price = 60000m
            });
            products.Add(new ProductModel
            {
                Name = "Belt",
                Category = "Accesories",
                Description = "Imported",
                Price = 500m
            });
            products.Add(new ProductModel
            {
                Name = "Car",
                Category = "Automobile",
                Description = "Used",
                Price = 500m
            }); products.Add(new ProductModel
            {
                Name = "phone",
                Category = "Accesories",
                Description = "Imported",
                Price = 500m
            }); products.Add(new ProductModel
            {
                Name = "Tv",
                Category = "Electronics",
                Description = "Imported",
                Price = 500m
            });
            ProductListModel listModel = new ProductListModel();
            listModel.Products = products;
            return View(listModel);

        }
        public IActionResult ProductsFromDb()
        {

            var products = _context.Products.ToList();
            var productModels = products.Select(p => new ProductModel
            {
                Id = p.Id,
                Name = p.Name,
                Category = p.Category,
                Description = p.Description,
                Price = p.Price
            }).ToList();
            ProductListModel listModel = new ProductListModel();
            listModel.Products = productModels;
            return View(listModel);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(ProductModel model)

        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var productEntity = new Product
            {
                Name = model.Name,
                Category = model.Category,
                Description = model.Description,
                Price = model.Price
            };
            _context.Products.Add(productEntity);
            _context.SaveChanges();
            return RedirectToAction("ProductsFromDb");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var productEntity = _context.Products.Find(id);
            if (productEntity == null)
            {
                return RedirectToAction("index");
            }
            var productModel = new ProductModel
            {
                Name = productEntity.Name,
                Category = productEntity.Category,
                Description = productEntity.Description,
                Price = productEntity.Price,
                Id = productEntity.Id
            };
            return View(productModel);
        }
        [HttpPost]
        public IActionResult Edit(ProductModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var productEntity = _context.Products.Find(model.Id);
            if (productEntity == null)
            {
                return RedirectToAction("index");
            }
            productEntity.Description = model.Description;
            productEntity.Price = model.Price;
            productEntity.Category = model.Category;
            productEntity.Name = model.Name;
            _context.SaveChanges();
            return RedirectToAction("ProductsFromDb");

        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Delete(int id) 
        {
            var productEntity = _context.Products.Find(id);
            if(productEntity == null) 
            {
                return NotFound();
            }   
            _context.Products.Remove(productEntity);
            _context.SaveChanges();
            return Json(Url.Action("ProductsFromDb", "Products"));

        }
    }
}
