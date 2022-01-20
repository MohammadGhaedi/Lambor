using System.Collections.Generic;
using System.Threading.Tasks;
using Lambor.Entities;
using Lambor.ViewModels.Api;
using Lambor.ViewModels.Identity;

namespace Lambor.Services.Contracts
{
    public interface IBasketService
    {
        Task<List<BasketVeiwModel>> GetAllAsync(GetAllBasketVeiwModel input);
        Task InsertAsync(AddToBasketViewModel Basket);
        Task DeleteAsync(RemoveFromBasketViewModel input);
        Task Clear();
        Task SubmitBasket(SubmitBasketViewModel input);
    }
}
