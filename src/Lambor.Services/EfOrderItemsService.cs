using Lambor.DataLayer.Context;
using Lambor.Entities;
using Lambor.Services.Contracts;
using Lambor.ViewModels.Api;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lambor.Services
{
    public class EfOrderItemsService : IOrderItemsService
    {
        private readonly IUnitOfWork _uow;
        private readonly DbSet<OrderItem> _orderItems;

        public EfOrderItemsService(IUnitOfWork uow)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(_uow));
            _orderItems = _uow.Set<OrderItem>();
        }

        public async Task<List<OrderItemsViewModel>> GetAllOrderItems(GetAllOrderItemsViewModel input)
        {
            var query = _orderItems.AsQueryable();

            if (input.ProductId.HasValue)
            {
                query = query.Where(x => x.ProductId == input.ProductId.Value);
            }

            if (input.OrderId.HasValue)
            {
                query = query.Where(x => x.OrderId == input.OrderId.Value);
            }

            return await query.Select(p => new OrderItemsViewModel()
            {
                Id = p.Id,
                Count = p.Count,
                OrderId = p.OrderId,
                ProductId = p.ProductId,
                ProductName = p.Product.Name
            }).ToListAsync();
        }

        public async Task<OrderItemsViewModel> UpdateAsync(UpdateOrderItemsViewModel input)
        {
            var item = await _orderItems.Include(p=>p.Product).FirstOrDefaultAsync(p => p.Id == input.Id);
            if (item == null)
            {
                throw new Exception();
            }
            item.Count = input.Count;

            await _uow.SaveChangesAsync();
            return new OrderItemsViewModel
            {
                Id = item.Id,
                Count = item.Count,
                ProductId = item.ProductId,
                ProductName = item.Product.Name,
                OrderId = item.OrderId
            };
        }

        public async Task DeleteAsync(DeleteOrderItemsViewModel input)
        {
            var item = await _orderItems.FindAsync(input.Id);
            if (item == null)
            {
                throw new Exception();
            }
            _orderItems.Remove(item);
            await _uow.SaveChangesAsync();
        }
    }
}
