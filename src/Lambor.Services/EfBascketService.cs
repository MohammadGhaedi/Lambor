using DNTCommon.Web.Core;
using Lambor.DataLayer.Context;
using Lambor.Entities;
using Lambor.Services.Contracts;
using Lambor.ViewModels.Api;
using Lambor.ViewModels.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lambor.Services
{
    public class EfBascketService : IBascketService
    {
        private readonly IUnitOfWork _uow;
        private readonly DbSet<Bascket> _basckets;
        private readonly IHttpContextAccessor _contextAccessor;


        public EfBascketService(IUnitOfWork uow, IHttpContextAccessor contextAccessor)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(_uow));
            _basckets = _uow.Set<Bascket>();
            _contextAccessor = contextAccessor ?? throw new ArgumentNullException(nameof(contextAccessor));
        }

        public async Task InsertAsync(AddToBascketViewModel bascket)
        {
            await _basckets.AddAsync(new Bascket {
                Count = bascket.Count, ProductId = bascket.ProductId, UserId = GetCurrentUserId()
            });
            await _uow.SaveChangesAsync();
        }

        public async Task<List<BascketVeiwModel>> GetAllAsync(GetAllBascketVeiwModel input)
        {
            var query = _basckets.AsQueryable();
            if (input.UserId.HasValue)
            {
                query = query.Where(x => x.UserId == input.UserId.Value);
            }
            if (input.ProductId.HasValue)
            {
                query = query.Where(x => x.ProductId == input.ProductId.Value);
            }



            return await query.Select(p => new BascketVeiwModel()
            {
                Id = p.Id,
                Count = p.Count,
                UserName = p.User.DisplayName,
                UserId = p.UserId,
                ProductId = p.ProductId,
                ProductName = p.Product.Name
            }).ToListAsync();
        }


        private int GetCurrentUserId() => _contextAccessor.HttpContext.User.Identity.GetUserId<int>();


    }
}
