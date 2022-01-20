

using System;
using System.Collections.Generic;
using Lambor.Entities;
using Lambor.Services.Contracts;
using System.Threading.Tasks;
using Lambor.DataLayer.Context;
using Microsoft.EntityFrameworkCore;
using Lambor.ViewModels.Identity;
using Microsoft.VisualBasic;

namespace Lambor.Services
{
    public class EfOrderService : IOrderService
    {
        private readonly IUnitOfWork _uow;
        private readonly DbSet<Order> _orders;
        private readonly DbSet<OrderItem> _orderItems;

        public EfOrderService(IUnitOfWork uow)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _orders = _uow.Set<Order>();
            _orderItems=_uow.Set<OrderItem>();
        }
        public async Task InsertAsync(Order order)
        {
            await _orders.AddAsync(order);
            await _uow.SaveChangesAsync();
        }
        public async Task UpdateAsync(Order order)
        {
            var item =await _orders.FindAsync(order.Id);
            if (item == null)
            {
                throw new Exception();
            }
        item.CostumerName = order.CostumerName;
        item.CostumerPhone = order.CostumerPhone;
        item.CostumerAddress = order.CostumerAddress;
        item.Description = order.Description;
        item.OrderDateTime = order.OrderDateTime;
        item.OrderStatus= order.OrderStatus;
        item.OrderItems = order.OrderItems;
        await _uow.SaveChangesAsync();
    }
        public async Task DeleteAsync(int Id)
        {
            var item = await _orders.FindAsync(Id);
            if (item == null)
            {
                throw new Exception();
            }

            _orders.Remove(item);
            await _uow.SaveChangesAsync();
        }

        public Task<List<OrderListItemViewModel>> GetAllAsync(DateTime orderDateTime, string costumerName, string costumerPhone, OrderStatus orderStatus, string address)
        {
            throw new NotImplementedException();

            //return await _orderItems.Select(p => new OrderListItemViewModel
            //{

            //})
        }
    }
}
