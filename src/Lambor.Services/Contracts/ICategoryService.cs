using System.Collections.Generic;
using System.Threading.Tasks;
using Lambor.Entities;
using Lambor.ViewModels.Identity;

namespace Lambor.Services.Contracts
{
    public interface ICategoryService
    {
        Task InsertAsync(CategoryViewModel category);
        Task UpdateAsync(CategoryViewModel input);
        Task DeleteAsync(int id);
        Task<CategoryViewModel> GetAsync(int id);
        Task<IList<CategoryViewModel>> GetAllAsync();


    }
}