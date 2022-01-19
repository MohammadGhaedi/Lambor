using System.Collections.Generic;
using System.Threading.Tasks;
using Lambor.Entities;
using Lambor.ViewModels.Api;
using Lambor.ViewModels.Identity;

namespace Lambor.Services.Contracts
{
    public interface IBascketService
    {
        Task<List<BascketVeiwModel>> GetAllAsync(GetAllBascketVeiwModel input);
        Task InsertAsync(AddToBascketViewModel bascket);
        Task DeleteAsync(RemoveFromBascketViewModel input);
        Task Clear();
        Task SubmitBascket(SubmitBascketViewModel input);
    }
}
