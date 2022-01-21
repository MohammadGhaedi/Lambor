using System;
using System.Collections.Generic;
using System.Linq;
using Lambor.Entities;
using Lambor.Services.Contracts;
using System.Threading.Tasks;
using Lambor.DataLayer.Context;
using Lambor.ViewModels.Api;
using Microsoft.EntityFrameworkCore;
using Lambor.ViewModels.Identity;

namespace Lambor.Services
{
    public class EfOrderService : IOrderService
    {
        private readonly IUnitOfWork _uow;
        private readonly DbSet<Order> _orders;

        public EfOrderService(IUnitOfWork uow)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _orders = _uow.Set<Order>();
        }
        public async Task InsertAsync(Order order)
        {
            await _orders.AddAsync(order);
            await _uow.SaveChangesAsync();
        }
        public async Task<OrderViewModel> UpdateAsync(UpdateOrderViewModel order)
        {
            var item = await _orders.FirstOrDefaultAsync(q=>q.Id==order.Id);
            if (item == null)
            {
                throw new Exception();
            }
            item.CostumerName = order.CostumerName;
            item.CostumerPhone = order.CostumerPhone;
            item.CostumerAddress = order.CostumerAddress;
            item.Description = order.Description;
            await _uow.SaveChangesAsync();

            return new OrderViewModel
            {
                Id = item.Id,
                CostumerName = item.CostumerName,
                CostumerPhone = item.CostumerPhone,
                CostumerAddress = item.CostumerAddress,
                Description = item.Description,
                OrderDateTime = item.OrderDateTime,
                OrderStatus = item.OrderStatus,
            };
        }
        public async Task DeleteAsync(DeleteOrderViewModel input)
        {
            var item = await _orders.FindAsync(input.Id);
            if (item == null)
            {
                throw new Exception();
            }

            _orders.Remove(item);
            await _uow.SaveChangesAsync();
        }

        public async Task<List<OrderViewModel>> GetAllAsync(GetAllOrderInputViewModel input)
        {
            var query = _orders.AsQueryable();
            if (input.OrderStatus.HasValue)
            {
                query = query.Where(x => x.OrderStatus == input.OrderStatus.Value);
            }

            if (input.FromDate.HasValue)
            {
                query = query.Where(x => x.OrderDateTime >= input.FromDate.Value);
            }
            if (input.ToDate.HasValue)
            {
                query = query.Where(x => x.OrderDateTime >= input.ToDate.Value);
            }
            if (!string.IsNullOrWhiteSpace(input.CustomerName))
            {
                query = query.Where(x => x.CostumerName.Contains(input.CustomerName));
            }

            if (!string.IsNullOrWhiteSpace(input.CustomerPhone))
            {
                query = query.Where(x => x.CostumerPhone.Contains(input.CustomerPhone));
            }

            if (!string.IsNullOrWhiteSpace(input.Address))
            {
                query = query.Where(x => x.CostumerAddress.Contains(input.Address));
            }


            return await query.Select(x => new OrderViewModel
            {
                CostumerAddress = x.CostumerAddress,
                CostumerName = x.CostumerName,
                CostumerPhone = x.CostumerPhone,
                Description = x.Description,
                Id = x.Id,
                OrderDateTime = x.OrderDateTime,
                OrderStatus = x.OrderStatus
            }).ToListAsync();
        }

        public async Task ChangeOrderStatus(ChangeOrderStatusVeiwModel input)
        {
            var item = await _orders.FirstOrDefaultAsync(q => q.Id == input.Id);
            if (item == null)
            {
                throw new Exception();
            }
            item.OrderStatus = (OrderStatus)1;
            await _uow.SaveChangesAsync();

        }
    }
}
