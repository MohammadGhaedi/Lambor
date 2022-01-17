using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lambor.DataLayer.Context;
using Lambor.Entities;
using Lambor.Services.Contracts;
using Lambor.ViewModels.Api;
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

        public async Task InsertAsync(ModifyProductViewModel product)
        {
            await _products.AddAsync(new Product { Name = product.Name, Price = product.Price, Description = product.Description, CategoryId = product.CategoryId, Image = product.Image, BrandId = product.BrandId });
            await _uow.SaveChangesAsync();
        }

        public async Task UpdateAsync(ModifyProductViewModel input)
        {

            var item = await _products.FindAsync(input.Id);
            if (item == null)
            {
                throw new Exception();
            }
            item.Name = input.Name;
            item.Price = input.Price;
            item.Description = input.Description;
            item.CategoryId = input.CategoryId;
            item.Image = input.Image;
            item.BrandId = input.BrandId;
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

        public async Task<List<ProductViewModel>> GetAllAsync()
        {
            return await _products.Select(p =>
           new ProductViewModel()
           {
               Id = p.Id,
               Name = p.Name,
               Price = p.Price,
               CategoryName = p.Category.Name,
               CategoryId = p.CategoryId,
               Image = p.Image,
               Description = p.Description,
               BrandId = p.BrandId,
               BrandName = p.Brand.Name
           }).ToListAsync();
        }

        public async Task<List<ProductViewModel>> GetAllAsync(GetAllProductInputViewModel input)
        {
            var query = _products.AsQueryable();
            if (input.CategoryId.HasValue)
            {
                query = query.Where(x => x.CategoryId == input.CategoryId.Value);
            }
            if (input.BrandId.HasValue)
            {
                query = query.Where(x => x.BrandId == input.BrandId.Value);
            }

            if (!string.IsNullOrWhiteSpace(input.Filter))
            {
                query = query.Where(x => x.Name.Contains(input.Filter));
            }

            return await query.Select(p => new ProductViewModel()
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                CategoryName = p.Category.Name,
                CategoryId = p.CategoryId,
                Image = p.Image,
                Description = p.Description,
                BrandId = p.BrandId,
                BrandName = p.Brand.Name
            }).ToListAsync();
        }

        public async Task<ProductViewModel> GetAsync(int id)
        {

            var item = await _products.Select(item => new ProductViewModel
            {
                Id = item.Id,
                Name = item.Name,
                Price = item.Price,
                CategoryName = item.Category.Name,
                CategoryId = item.CategoryId,
                Image = item.Image,
                Description = item.Description,
                BrandId = item.BrandId,
                BrandName = item.Brand.Name
            }).FirstOrDefaultAsync(p => p.Id == id);

            //var item = await _products.FindAsync(id);
            if (item == null)
            {
                throw new Exception();
            }

            return item;
        }

        public async Task<PagedProductViewModel> GetPagedProductListAsync(int pageNmber, int recordPerPage)
        {
            var skipRecords = pageNmber * recordPerPage;
            var query = _products.Select(p =>
            new ProductViewModel()
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                CategoryName = p.Category.Name,
                CategoryId = p.CategoryId,
                Image = p.Image,
                Description = p.Description,
                BrandId = p.BrandId,
                BrandName = p.Brand.Name
            });
            var data = await query.Skip(skipRecords).Take(recordPerPage).ToListAsync();
            return
                new PagedProductViewModel
                {
                    Paging =
                    { TotalItems = await query.CountAsync() },
                    Items = data
                };
        }
    }
}
