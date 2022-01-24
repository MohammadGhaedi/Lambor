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
    [Route("api/app")]
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
        private readonly IOrderItemsService _orderItemsService;

        public AppApiController(IProductService productService, ICategoryService categoryService, IBrandService brandService, IBasketService BasketService, IOrderService orderService, IOrderItemsService orderItemsService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _brandService = brandService;
            _BasketService = BasketService;
            _orderService = orderService;
            _orderItemsService = orderItemsService;
        }

        [HttpGet]
        [Route("GetAllProducts")]
        public async Task<List<ProductViewModel>> GetAllProducts([FromQuery] GetAllProductInputViewModel input,[FromHeader] string apikey)
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
        public async Task<List<BasketVeiwModel>> GetAllBaskets()
        {
            return await _BasketService.GetAllAsync();
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
        public async Task<OrderViewModel> UpdateOrder([FromQuery] UpdateOrderViewModel input)
        {
            return await _orderService.UpdateAsync(input);
        }
        [HttpGet]
        [Route("DeleteOrder")]
        public async Task DeleteOder([FromQuery] DeleteOrderViewModel input)
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
        [Route("GetAllOrderItems")]
        public async Task<List<OrderItemsViewModel>> GetAllOrderItems([FromQuery] GetAllOrderItemsViewModel input)
        {
            return await _orderItemsService.GetAllOrderItems(input);
        }


        [HttpPut]
        [Route("UpdateOrderItems")]
        public async Task<OrderItemsViewModel> UpdateOrderItems([FromQuery] UpdateOrderItemsViewModel input)
        {
            return await _orderItemsService.UpdateAsync(input);
        }

        [HttpGet]
        [Route("DeleteOrderItems")]
        public async Task DeleteOrderItems([FromQuery] DeleteOrderItemsViewModel input)
        {
            await _orderItemsService.DeleteAsync(input);
        }

        [HttpGet]
        [Route("ChangeOrderStatus")]
        public async Task ChangeOrderStatus ([FromQuery] ChangeOrderStatusVeiwModel input)
        {
            await _orderService.ChangeOrderStatus(input);
        }

    }
}
