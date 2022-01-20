using DNTCommon.Web.Core;
using DNTPersianUtils.Core;
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
    public class EfBasketService : IBasketService
    {
        private readonly IUnitOfWork _uow;
        private readonly DbSet<Basket> _Baskets;
        private readonly DbSet<Order> _orders;
        private readonly IHttpContextAccessor _contextAccessor;


        public EfBasketService(IUnitOfWork uow, IHttpContextAccessor contextAccessor)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(_uow));
            _Baskets = _uow.Set<Basket>();
            _orders = _uow.Set<Order>();
            _contextAccessor = contextAccessor ?? throw new ArgumentNullException(nameof(contextAccessor));
        }

        public async Task InsertAsync(AddToBasketViewModel input)
        {
            var item = await _Baskets.FirstOrDefaultAsync(p => p.UserId == GetCurrentUserId() && p.ProductId == input.ProductId);

            if (item == null)
            {
                await _Baskets.AddAsync(new Basket
                {
                    Count = input.Count.HasValue ? (int)input.Count : 1,
                    ProductId = input.ProductId,
                    UserId = GetCurrentUserId()
                });
            }
            else
            {
                item.Count = input.Count.HasValue ? (int)input.Count : item.Count + 1;

            }

            await _uow.SaveChangesAsync();
        }

        public async Task DeleteAsync(RemoveFromBasketViewModel input)
        {
            var item = await _Baskets.FirstOrDefaultAsync(p => p.UserId == GetCurrentUserId() && p.ProductId == input.ProductId);

            if (item == null)
            {
                throw new Exception();
            }

            if (item.Count == 1 || input.Count == 0)
            {
                _Baskets.Remove(item);
            }

            else
            {
                item.Count = input.Count.HasValue ? (int)input.Count : item.Count - 1;
            }
            await _uow.SaveChangesAsync();
        }

        public async Task SubmitBasket(SubmitBasketViewModel input)
        {
            var orderItems = await _Baskets.Select(p => new OrderItem()
            {
                Count = p.Count,
                ProductId = p.ProductId,
            }).ToListAsync();

            await _orders.AddAsync(new Order { CostumerName = input.CostumerName, CostumerPhone = input.CostumerPhone, CostumerAddress = input.CostumerAddress, Description = input.Description, OrderDateTime = DateTime.Now, OrderStatus = 0, UserId = GetCurrentUserId(), OrderItems = orderItems });

            await Clear();
            await _uow.SaveChangesAsync();
        }

        public async Task<List<BasketVeiwModel>> GetAllAsync(GetAllBasketVeiwModel input)
        {
            var query = _Baskets.AsQueryable();

            if (input.ProductId.HasValue)
            {
                query = query.Where(x => x.ProductId == input.ProductId.Value);
            }

            return await query.Select(p => new BasketVeiwModel()
            {
                Count = p.Count,
                UserName = p.User.DisplayName,
                UserId = p.UserId,
                ProductId = p.ProductId,
                ProductName = p.Product.Name
            }).ToListAsync();
        }

        public async Task Clear()
        {
            _uow.RemoveRange(_Baskets);
            await _uow.SaveChangesAsync();
        }
        private int GetCurrentUserId()
        {
            return _contextAccessor.HttpContext.User.Identity.GetUserId<int>();
        }


    }
}
