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
        private readonly DbSet<Basket> _baskets;
        private readonly DbSet<Order> _orders;
        private readonly IHttpContextAccessor _contextAccessor;


        public EfBasketService(IUnitOfWork uow, IHttpContextAccessor contextAccessor)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(_uow));
            _baskets = _uow.Set<Basket>();
            _orders = _uow.Set<Order>();
            _contextAccessor = contextAccessor ?? throw new ArgumentNullException(nameof(contextAccessor));
        }

        public async Task InsertAsync(AddToBasketViewModel input)
        {
            var item = await _baskets.FirstOrDefaultAsync(p => p.UserId == GetCurrentUserId() && p.ProductId == input.ProductId);

            if (item == null)
            {
                await _baskets.AddAsync(new Basket
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
            var item = await _baskets.FirstOrDefaultAsync(p => p.UserId == GetCurrentUserId() && p.ProductId == input.ProductId);

            if (item == null)
            {
                throw new Exception();
            }

            if (item.Count == 1 || input.Count == 0)
            {
                _baskets.Remove(item);
            }

            else
            {
                item.Count = input.Count.HasValue ? (int)input.Count : item.Count - 1;
            }
            await _uow.SaveChangesAsync();
        }

        public async Task SubmitBasket(SubmitBasketViewModel input)
        {
            var orderItems = await _baskets.Select(p => new OrderItem()
            {
                Count = p.Count,
                ProductId = p.ProductId,
            }).ToListAsync();

            await _orders.AddAsync(new Order { CostumerName = input.CostumerName, CostumerPhone = input.CostumerPhone, CostumerAddress = input.CostumerAddress, Description = input.Description, OrderDateTime = DateTime.Now, OrderStatus = 0, UserId = GetCurrentUserId(), OrderItems = orderItems });

            await Clear();
            await _uow.SaveChangesAsync();
        }

        public async Task<List<BasketVeiwModel>> GetAllAsync()
        {
            var userId = GetCurrentUserId();
            var query = _baskets.Where(x => x.UserId == userId);
            return await query.Select(p => new BasketVeiwModel()
            {
                Count = p.Count,
                UserName = p.User.DisplayName,
                UserId = p.UserId,
                ProductId = p.ProductId,
                ProductName = p.Product.Name,
                UnitPrice = p.Product.Price
            }).ToListAsync();
        }

        public async Task Clear()
        {
            _uow.RemoveRange(_baskets);
            await _uow.SaveChangesAsync();
        }
        private int GetCurrentUserId()
        {
            return _contextAccessor.HttpContext.User.Identity.GetUserId<int>();
        }


    }
}
