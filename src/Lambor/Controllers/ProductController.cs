using DNTBreadCrumb.Core;
using DNTCommon.Web.Core;
using Lambor.Services.Contracts;
using Lambor.Services.Identity;
using Lambor.ViewModels.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Lambor.Controllers
{


    [Authorize(Policy = ConstantPolicies.DynamicPermission)]
    [BreadCrumb(UseDefaultRouteUrl = true, Order = 0)]
    [DisplayName("مدیریت محصولات")]
    public class ProductController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IBrandService _brandService;
        private const string ProductNotFound = "محصول مورد نظر یافت نشد";
        private const string UploadDirectoryName = "Uploads";

        public ProductController(IProductService productService, ICategoryService categoryService, IBrandService brandService, IWebHostEnvironment webHostEnvironment)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
            _categoryService = categoryService;
            _brandService = brandService;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult> Index(int? page = 1)
        {
            var products = await _productService.GetPagedProductListAsync(page.Value - 1, 7);
            return View(products);
        }

        [AjaxOnly]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(ModifyProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.ProductImage != null && model.ProductImage.Length > 0)
                {
                    var saveResult = await SaveAndReturnAddress(model.ProductImage);
                    if (!string.IsNullOrWhiteSpace(saveResult.Error))
                    {
                        return BadRequest(saveResult.Error);
                    }
                    model.Image = saveResult.Address;
                }
                await _productService.InsertAsync(model);
            }
            return Ok();
        }

        [AjaxOnly]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ModifyProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                var product = await _productService.GetAsync(model.Id);
                if (product == null)
                {
                    ModelState.AddModelError("", ProductNotFound);
                }
                else
                {

                    product.Name = model.Name;
                    product.Price = model.Price;
                    product.Description = model.Description;
                    product.CategoryId = model.CategoryId;
                    product.BrandId = model.BrandId;

                    product.Image = model.Image;

                    if (model.ProductImage.Length > 0)
                    {
                        var saveResult = await SaveAndReturnAddress(model.ProductImage);
                        if (!string.IsNullOrWhiteSpace(saveResult.Error))
                        {
                            return BadRequest(saveResult.Error);
                        }
                        model.Image = saveResult.Address;
                    }
                    await _productService.UpdateAsync(model);
                }
            }

            return Ok();
        }

        public async Task<IActionResult> Delete(ProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (string.IsNullOrWhiteSpace(model?.Id.ToString()))
            {
                return BadRequest("model is null.");
            }

            var product = await _productService.GetAsync(model.Id);
            if (product == null)
            {
                ModelState.AddModelError("", ProductNotFound);
            }
            else
            {
                await _productService.DeleteAsync(product.Id);
            }
            return Ok();
        }

        [AjaxOnly]
        public async Task<IActionResult> Render([FromBody] ModelIdViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var viewModel = new ProductRenderViewModel();
            viewModel.Product = new ProductViewModel();

            if (model == null || model.Id == 0)
            {
                await FillProductComboCollections(viewModel);
                return PartialView("_Create", model: viewModel);
            }

            var product = await _productService.GetAsync(model.Id);
            if (product == null)
            {
                ModelState.AddModelError("", ProductNotFound);
                await FillProductComboCollections(viewModel);
                return PartialView("_Create", model: viewModel);
            }

            viewModel.Product = new ProductViewModel { Id = product.Id, Name = product.Name, Price = product.Price, Description = product.Description, CategoryId = product.CategoryId, Image = product.Image };
            await FillProductComboCollections(viewModel, product.CategoryId, product.BrandId);
            return PartialView("_Create", model: viewModel);
        }

        private async Task FillProductComboCollections(ProductRenderViewModel productViewModel, int? categoryId = null, int? brandId = null)
        {
            productViewModel.Categories = new SelectList(await _categoryService.GetAllForDropdown(), "Id", "Name", categoryId);
            productViewModel.Brands = new SelectList(await _brandService.GetAllForDropdown(), "Id", "Name", brandId);

        }

        [AjaxOnly]
        public async Task<IActionResult> RenderDelete([FromBody] ModelIdViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model == null)
            {
                return BadRequest("model is null.");
            }

            var product = await _productService.GetAsync(model.Id);
            if (product == null)
            {
                ModelState.AddModelError("", ProductNotFound);
                return PartialView("_Delete", model: new ProductViewModel());
            }
            return PartialView("_Delete", model: new ProductViewModel { Id = product.Id, Name = product.Name, Price = product.Price, Description = product.Description, CategoryId = product.CategoryId, Image = product.Image });
        }



        private string GetUploadDirectoryPath()
        {
            var uploadDirectoryPath = Path.Combine(_webHostEnvironment.WebRootPath, UploadDirectoryName);
            if (!Directory.Exists(uploadDirectoryPath))
            {
                Directory.CreateDirectory(uploadDirectoryPath);
            }

            return uploadDirectoryPath;
        }

        private async Task<(string Address, string Error)> SaveAndReturnAddress(IFormFile file)
        {

            if (file.Length == 0) return (null, "فایل یافت نشد");
            if (file.Length > 5 * 1024 * 1024) return (null, "حجم مجاز عکس 5 مگابایت است");

            var uploadDirectoryPath = GetUploadDirectoryPath();
            var fileName = $"{Guid.NewGuid()}_" + file.FileName;
            var filePath = Path.Combine(uploadDirectoryPath, fileName);

            await using Stream fileStream = new FileStream(filePath, FileMode.Create);

            await file.CopyToAsync(fileStream);

            return ($"{UploadDirectoryName}/{fileName}", null);
        }
    }

}
