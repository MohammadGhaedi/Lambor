using System.ComponentModel;
using Lambor.Services.Identity;
using Lambor.ViewModels.Identity;
using DNTBreadCrumb.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Lambor.Services.Contracts;
using System;
using System.Threading.Tasks;
using DNTCommon.Web.Core;
using Lambor.Entities;
using Lambor.Common.IdentityToolkit;

namespace Lambor.Controllers
{


    [Authorize(Policy = ConstantPolicies.DynamicPermission)]
    [BreadCrumb(UseDefaultRouteUrl = true, Order = 0)]
    [DisplayName("مدیریت دسته بندی محصولات")]
    public class CategoryController : Controller
    {
        private const string CategoryNotFound = "دسته بندی مورد نظر یافت نشد";

        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));

        }
        public async Task<IActionResult> Index()
        {
            var category = await _categoryService.GetAllAsync();
            return View(category);
        }

        [AjaxOnly]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(CategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _categoryService.InsertAsync(new Category(model.Name));
            }
            return Ok();
        }

        [AjaxOnly]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var category = await _categoryService.GetAsync(model.Id);
                if (category == null)
                {
                    ModelState.AddModelError("", CategoryNotFound);
                }
                else
                {
                    category.Name = model.Name;
                    await _categoryService.UpdateAsync(category);

                }
            }
            return PartialView("_Create", model: model);
        }

        [AjaxOnly]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(CategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (string.IsNullOrWhiteSpace(model?.Id.ToString()))
            {
                return BadRequest("model is null.");
            }

            var category = await _categoryService.GetAsync(model.Id);
            if (category == null)
            {
                ModelState.AddModelError("", CategoryNotFound);
            }
            else
            {
                await _categoryService.DeleteAsync(category.Id);
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
                return PartialView("_Create", model: new CategoryViewModel());
            }

            var category = await _categoryService.GetAsync(model.Id);
            if (category == null)
            {
                ModelState.AddModelError("", CategoryNotFound);
                return PartialView("_Create", model: new CategoryViewModel());
            }
            return PartialView("_Create", model: new CategoryViewModel { Id = category.Id, Name = category.Name });
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

            var category = await _categoryService.GetAsync(model.Id);
            if (category == null)
            {
                ModelState.AddModelError("", CategoryNotFound);
                return PartialView("_Delete", model: new CategoryViewModel());
            }
            return PartialView("_Delete", model: new CategoryViewModel { Id = category.Id, Name = category.Name });
        }
    }
}
