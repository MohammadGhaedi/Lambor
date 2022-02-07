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
        Task<OrderViewModel> UpdateAsync(UpdateOrderViewModel order);
        Task DeleteAsync(DeleteOrderViewModel input);
        Task<List<OrderViewModel>> GetAllAsync(GetAllOrderInputViewModel input);
        Task<OrderViewModel> GetAsync(int id);
        Task ChangeOrderStatus(ChangeOrderStatusVeiwModel input);
    }
}
