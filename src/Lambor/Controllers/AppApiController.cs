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

        public AppApiController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<List<ProductViewModel>> GetAllProducts(GetAllProductInputViewModel input)
        {
            return await _productService.GetAllAsync(input);
        }
    }
}
