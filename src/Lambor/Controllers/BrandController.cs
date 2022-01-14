using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using DNTBreadCrumb.Core;
using DNTCommon.Web.Core;
using Lambor.Entities;
using Lambor.Services.Contracts;
using Lambor.Services.Identity;
using Lambor.ViewModels.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lambor.Controllers
{
    [Authorize(Policy = ConstantPolicies.DynamicPermission)]
    [BreadCrumb(UseDefaultRouteUrl = true,Order = 0)]
    [DisplayName("برند")]
    public class BrandController:Controller
    {
        private const string BrandNotFound = "برند مورد نظر یافت نشد";
        private readonly IBrandService _brandService;

        public BrandController(IBrandService brandService)
        {
            _brandService = brandService ?? throw new ArgumentNullException(nameof(brandService));
        }

        public async Task<IActionResult> Index()
        {
            var brand = await _brandService.GetAllAsync();
            return View(brand);
        }

        [AjaxOnly]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(BrandViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _brandService.InsertAsync(new Brand(model.Name));
            }

            return Ok();
        }

        [AjaxOnly]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(BrandViewModel model)
        {
            if (ModelState.IsValid)
            {
                var brand = await _brandService.GetAsync(model.Id);
                if (brand == null)
                {
                    ModelState.AddModelError("",BrandNotFound);
                }
                else
                {
                    brand.Name = model.Name;
                    await _brandService.UpdateAsync(brand);
                }
            }

            return PartialView("_Create", model: model);
        }

        [AjaxOnly]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(BrandViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (string.IsNullOrWhiteSpace(model?.Id.ToString()))
            {
                return BadRequest("model is null.");
            }

            var brand = await _brandService.GetAsync(model.Id);
            if (brand==null)
            {
                ModelState.AddModelError("",BrandNotFound);
            }
            else
            {
                await _brandService.DeleteAsync(brand.Id);
            }

            return PartialView("_Delete", model:model);
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
                return PartialView("_Create", model: new BrandViewModel());
            }

            var brand = await _brandService.GetAsync(model.Id);
            if (brand == null)
            {
                ModelState.AddModelError("", BrandNotFound);
                return PartialView("_Create", model: new BrandViewModel());
            }
            return PartialView("_Create", model: new BrandViewModel{ Id = brand.Id, Name = brand.Name });
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

            var brand = await _brandService.GetAsync(model.Id);
            if (brand == null)
            {
                ModelState.AddModelError("", BrandNotFound);
                return PartialView("_Delete", model: new BrandViewModel());
            }
            return PartialView("_Delete", model: new BrandViewModel { Id = brand.Id, Name = brand.Name });
        }

    }
}
