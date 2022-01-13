using System.Collections.Generic;
using System.Threading.Tasks;
using Lambor.Entities;
using Lambor.ViewModels.Identity;

namespace Lambor.Services.Contracts
{
    public interface ICategoryService
    {
        Task InsertAsync(Category category);
        Task UpdateAsync(Category input);
        Task DeleteAsync(int id);
        Task<Category> GetAsync(int id);
        Task<IList<CategoryViewModel>> GetAllAsync();


    }
}