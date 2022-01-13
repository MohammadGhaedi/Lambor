using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lambor.DataLayer.Context;
using Lambor.Entities;
using Lambor.Services.Contracts;
using Lambor.ViewModels.Identity;
using Microsoft.EntityFrameworkCore;

namespace Lambor.Services
{
    public class EfCategoryService : ICategoryService
    {
        private readonly IUnitOfWork _uow;
        private readonly DbSet<Category> _categories;

        public EfCategoryService(IUnitOfWork uow)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));

            _categories = _uow.Set<Category>();
        }

        public async Task InsertAsync(Category category)
        {
            await _categories.AddAsync(category);
        }

        public async Task UpdateAsync(Category input)
        {

            var item = await _categories.FindAsync(input.Id);
            if (item == null)
            {
                throw new Exception();
            }
            item.Name = input.Name;
            await _uow.SaveChangesAsync();

        }

        public async Task DeleteAsync(int id)
        {
            var item = await _categories.FindAsync(id);
            if (item == null)
            {
                throw new Exception();
            }
            _categories.Remove(item);
            await _uow.SaveChangesAsync();
        }

        public async Task<IList<Category>> GetAllAsync()
        {
            return await _categories.ToListAsync();
        }

        public IList<CategoryViewModel> GetAll()
        {
            return _categories.Select(p =>
                                    new CategoryViewModel
                                    {
                                        Name = p.Name,
                                        Id =p.Id
                                    }).ToList();
        }


        public async Task<Category> GetAsync(int id)
        {
            var item = await _categories.FindAsync(id);
            if (item == null)
            {
                throw new Exception();
            }
            return item;
        }

        
    }
}