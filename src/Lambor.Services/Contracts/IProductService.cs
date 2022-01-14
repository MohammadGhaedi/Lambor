using System.Collections.Generic;
using System.Threading.Tasks;
using Lambor.Entities;
using Lambor.ViewModels.Identity;

namespace Lambor.Services.Contracts
{
    public interface IProductService
    {
        Task InsertAsync(ProductViewModel product);
        Task UpdateAsync(ProductViewModel input);
        Task DeleteAsync(int id);
        Task<IList<ProductViewModel>> GetAllAsync();
        Task<ProductViewModel> GetAsync(int id);
        Task<PagedProductViewModel> GetPagedProductListAsync(int pageNmber, int recordPerPage);
    }
}