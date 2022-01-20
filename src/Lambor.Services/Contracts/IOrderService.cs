using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lambor.Entities;
using Lambor.ViewModels.Api;
using Lambor.ViewModels.Identity;
using Microsoft.VisualBasic;

namespace Lambor.Services.Contracts
{
    public interface IOrderService
    {
        Task InsertAsync(Order order);
        Task UpdateAsync(Order order);
        Task DeleteAsync(long Id);
        Task<Order> GetAsync(long Id);
        Task<List<OrderViewModel>> GetAllAsync(GetAllOrderInputViewModel input);

    }
}
