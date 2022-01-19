using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using DNTBreadCrumb.Core;
using Lambor.Services.Contracts;
using Lambor.Services.Identity;
using Lambor.ViewModels.Api;
using Lambor.ViewModels.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lambor.Controllers
{
    //[Route("api/app")]
    [ApiController]
    //[Authorize(Policy = ConstantPolicies.DynamicPermission)]
    [DisplayName("Application API")]
    public class AppApiController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IBrandService _brandService;
        private readonly IBascketService _bascketService;

        public AppApiController(IProductService productService,ICategoryService categoryService,IBrandService brandService,IBascketService bascketService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _brandService = brandService;
            _bascketService = bascketService;
        }

        [HttpGet]
        [Route("GetAllProducts")]
        public async Task<List<ProductViewModel>> GetAllProducts([FromQuery] GetAllProductInputViewModel input)
        {
            return await _productService.GetAllAsync(input);
        }

        [HttpGet]
        [Route("GetAllCategories")]
        public async Task<List<CategoryViewModel>> GetAllCategories()
        {
            return await _categoryService.GetAllAsync();
        }

        [HttpGet]
        [Route("GetAllBrands")]
        public async Task<List<BrandViewModel>> GetAllBrands()
        {
            return await _brandService.GetAllAsync();
        }

        [HttpGet]
        [Route("GetAllBasckets")]
        public async Task<List<BascketVeiwModel>> GetAllBasckets([FromQuery] GetAllBascketVeiwModel input)
        {
            return await _bascketService.GetAllAsync(input);
        }

        [HttpGet]
        [Route("AddToBascket")]
        public async Task AddToBascket([FromQuery] AddToBascketViewModel input)
        {
            await _bascketService.InsertAsync(input);
        }

    }
}
