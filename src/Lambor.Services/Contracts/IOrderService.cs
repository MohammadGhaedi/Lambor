using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lambor.Entities;
using Lambor.ViewModels.Identity;
using Microsoft.VisualBasic;

namespace Lambor.Services.Contracts
{
    public interface IOrderService
    {
        Task InsertAsync(Order order);
        Task UpdateAsync(Order order);
        Task DeleteAsync(int Id);
        Task<List<OrderListItemViewModel>> GetAllAsync(DateTime orderDateTime, string costumerName, string costumerPhone,
            OrderStatus orderStatus, string address);

    }
}
