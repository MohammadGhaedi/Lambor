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
        Task<OrderViewModel> UpdateAsync(OrderViewModel order);
        Task DeleteAsync(OrderViewModel input);
        Task<OrderViewModel> GetAsync(OrderViewModel input);
        Task<List<OrderViewModel>> GetAllAsync(GetAllOrderInputViewModel input);

    }
}
