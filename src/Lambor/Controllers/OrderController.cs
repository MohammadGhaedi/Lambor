using System;
using System.ComponentModel;
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
    [DisplayName("سفارش")]
    public class OrderController : Controller
    {
        private const string OrderNotFound = "سفارش مورد نظر یافت نشد";
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
        }

        public async Task<IActionResult> Index()
        {
            var order = await _orderService.GetAllAsync(null);
            return View(order);
        }
        
    }
}
