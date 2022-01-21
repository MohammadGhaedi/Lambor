using Lambor.ViewModels.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lambor.Services.Contracts
{
    public interface IOrderItemsService
    {
        Task<List<OrderItemsViewModel>> GetAllOrderItems(GetAllOrderItemsViewModel input);
        Task<OrderItemsViewModel> UpdateAsync(UpdateOrderItemsViewModel input);
        Task DeleteAsync(DeleteOrderItemsViewModel input);
    }
}
