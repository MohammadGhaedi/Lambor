using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lambor.DataLayer.Context;
using Lambor.Entities;
using Lambor.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using Lambor.ViewModels.Identity;

namespace Lambor.Services
{
    public class EfProductService : IProductService
    {
        private readonly IUnitOfWork _uow;
        private readonly DbSet<Product> _products;

        public EfProductService(IUnitOfWork uow)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(_uow));
            _products = _uow.Set<Product>();
        }

        public async Task InsertAsync(ProductViewModel product)
        {
           await _products.AddAsync(new Product { Name = product.Name,Price=product.Price,Description=product.Description,CategoryId=product.CategoryId,Image=product.Image});
          await  _uow.SaveChangesAsync();
        }

        public async Task UpdateAsync(ProductViewModel input)
        {

            var item = await _products.FindAsync(input.Id);
            if (item == null)
            {
                throw new Exception();
            }
            item.Name = input.Name;
            item.Price = input.Price;
            item.Description = input.Description;
            item.CategoryId  = input.CategoryId;
            item.Image = input.Image;
            await _uow.SaveChangesAsync();

        }

        public async Task DeleteAsync(int id)
        {
            var item = await _products.FindAsync(id);
            if (item == null)
            {
                throw new Exception();
            }
            _products.Remove(item);
            await _uow.SaveChangesAsync();
        }

        public async Task<IList<ProductViewModel>> GetAllAsync()
        {
            return  await _products.Select(p =>
            new ProductViewModel()
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                CategoryName = p.Category.Name,
                CategoryId = p.CategoryId,
                Image = p.Image
            }).ToListAsync();
        }

        public async Task<ProductViewModel> GetAsync(int id)
        {
            var item = await _products.FindAsync(id);
            if (item == null)
            {
                throw new Exception();
            }
            return new ProductViewModel { 
                Id=item.Id,
                Name = item.Name,
                Price = item.Price,
                CategoryName = item.Category.Name,
                CategoryId = item.CategoryId,
                Image = item.Image
            } ;
        }

        public async Task<PagedProductViewModel> GetPagedProductListAsync(int pageNmber,int recordPerPage)
        {
            var skipCount = pageNmber * recordPerPage;
            var query = _products.Select(p =>
            new ProductViewModel()
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                CategoryName = p.Category.Name,
                CategoryId = p.CategoryId,
                Image = p.Image
            });
            var data = 
            await query.Skip(skipCount).Take(recordPerPage).ToListAsync();
            var totalCount = await query.CountAsync();
            return new PagedProductViewModel { Paging = { TotalItems= totalCount},Items = data  };
        }
    }
}
