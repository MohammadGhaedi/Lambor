using System.Collections.Generic;
using Lambor.Entities;

namespace Lambor.Services.Contracts
{
    public interface IProductService
    {
        void AddNewProduct(Product product);
        IList<Product> GetAllProducts();
    }
}