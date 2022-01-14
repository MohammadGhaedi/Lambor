using DNTBreadCrumb.Core;
using DNTCommon.Web.Core;
using Lambor.Services.Contracts;
using Lambor.Services.Identity;
using Lambor.ViewModels.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Lambor.Controllers
{


    [Authorize(Policy = ConstantPolicies.DynamicPermission)]
    [BreadCrumb(UseDefaultRouteUrl = true, Order = 0)]
    [DisplayName("مدیریت محصولات")]
    public class ProductController : Controller
    {
        private const string ProductNotFound = "محصول مورد نظر یافت نشد";
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
        }
        public async Task<IActionResult> Index(int? page=1)
        {
            var products = await _productService.GetPagedProductListAsync(page.Value,10);
            return View(products);
        }

        [AjaxOnly]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _productService.InsertAsync(model);
            }
            return Ok();
        }

        [AjaxOnly]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                var product = await _productService.GetAsync(model.Id);
                if (product == null)
                {
                    ModelState.AddModelError("", ProductNotFound);
                }
                else
                    product.Name = model.Name;
                product.Price = model.Price;
                product.Description = model.Description;
                product.Image = model.Image;
                product.CategoryId = model.CategoryId;

                await _productService.UpdateAsync(model);

            }
            return PartialView("_Create", model: model);
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
            return PartialView("_Delete", model: model);
        }

        [AjaxOnly]
        public async Task<IActionResult> Render([FromBody] ModelIdViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model == null || model.Id == 0)
            {
                return PartialView("_Create", model: new ProductViewModel());
            }

            var product = await _productService.GetAsync(model.Id);
            if (product == null)
            {
                ModelState.AddModelError("", ProductNotFound);
                return PartialView("_Create", model: new ProductViewModel());
            }
            return PartialView("_Create", model: new ProductViewModel { Id = product.Id, Name = product.Name,Price=product.Price,Description=product.Description,CategoryId=product.CategoryId,Image=product.Image });
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
                return PartialView("_Delete", model: new CategoryViewModel());
            }
            return PartialView("_Delete", model: new ProductViewModel { Id = product.Id, Name = product.Name, Price = product.Price, Description = product.Description, CategoryId = product.CategoryId, Image = product.Image });
        }

    }

}
