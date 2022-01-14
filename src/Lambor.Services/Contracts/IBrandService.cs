

using System.Collections.Generic;
using System.Threading.Tasks;
using Lambor.Entities;
using Lambor.ViewModels.Identity;

namespace Lambor.Services.Contracts
{
    public interface IBrandService
    {
        Task InsertAsync(Brand brand);
        Task UpdateAsync(Brand brand);
        Task DeleteAsync(int id);
        Task<Brand> GetAsync(int id);
        Task<IList<BrandViewModel>> GetAllAsync();

    }
}
