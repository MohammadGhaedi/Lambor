using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using DNTBreadCrumb.Core;
using Lambor.Entities;
using Lambor.Services.Contracts;
using Lambor.Services.Identity;
using Lambor.Tools;
using Lambor.ViewModels.Api;
using Lambor.ViewModels.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Math.EC.Rfc7748;

namespace Lambor.Controllers
{
    //[Route("api/app")]
    [ApiController]
    //[Authorize(Policy = ConstantPolicies.DynamicPermission)]
    [DisplayName("Application API")]
    [ApiAuthFilter]
    public class AppApiController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IBrandService _brandService;
        private readonly IBasketService _BasketService;
        private readonly IOrderService _orderService;

        public AppApiController(IProductService productService, ICategoryService categoryService, IBrandService brandService, IBasketService BasketService, IOrderService orderService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _brandService = brandService;
            _BasketService = BasketService;
            _orderService = orderService;
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
        [Route("GetAllBaskets")]
        public async Task<List<BasketVeiwModel>> GetAllBaskets([FromQuery] GetAllBasketVeiwModel input)
        {
            return await _BasketService.GetAllAsync(input);
        }

        [HttpGet]
        [Route("AddToBasket")]
        public async Task AddToBasket([FromQuery] AddToBasketViewModel input)
        {
            await _BasketService.InsertAsync(input);
        }

        [HttpGet]
        [Route("RemoveFromBasket")]
        public async Task RemoveFromBasket([FromQuery] RemoveFromBasketViewModel input)
        {
            await _BasketService.DeleteAsync(input);
        }

        [HttpGet]
        [Route("ClearBasket")]
        public async Task ClearBasket()
        {
            await _BasketService.Clear();
        }

        [HttpGet]
        [Route("SubmitBasket")]
        public async Task SubmitBasket([FromQuery] SubmitBasketViewModel input)
        {
            await _BasketService.SubmitBasket(input);
        }

        [HttpPut]
        [Route("UpdateOrder")]
        public async Task<OrderViewModel> UpdateOrder([FromBody] OrderViewModel input)
        {
           return await _orderService.UpdateAsync(input);
        }
        [HttpGet]
        [Route("DeleteOrder")]
        public async Task DeleteOder([FromQuery] OrderViewModel input)
        {
            await _orderService.DeleteAsync(input);
        }
        [HttpGet]
        [Route("GetAllOrders")]
        public async Task<List<OrderViewModel>> GetAllOrders([FromQuery] GetAllOrderInputViewModel input)
        {
            return await _orderService.GetAllAsync(input);
        }
        [HttpGet]
        [Route("GetOrder")]
        public async Task<OrderViewModel> GetOrder([FromQuery] OrderViewModel input)
        {
            return await _orderService.GetAsync(input);
        }


    }
}
