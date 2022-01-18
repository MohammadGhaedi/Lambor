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

        public async Task InsertAsync(CategoryViewModel category)
        {
            await _categories.AddAsync(new Category { Name = category.Name });
            await _uow.SaveChangesAsync();
        }

        public async Task UpdateAsync(CategoryViewModel input)
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

        public async Task<List<CategoryViewModel>> GetAllAsync()
        {
            return await _categories.Select(p =>
                                    new CategoryViewModel
                                    {
                                        Name = p.Name,
                                        Id = p.Id
                                    }).ToListAsync();
        }

        public async Task<CategoryViewModel> GetAsync(int id)
        {
            var item = await _categories.FindAsync(id);
            if (item == null)
            {
                throw new Exception();
            }
            return  new CategoryViewModel { Name = item.Name,Id=item.Id };
        }

        public async Task<List<ComboViewModel>> GetAllForDropdown()
        {
            return await _categories.Select(p => new ComboViewModel
            {
                Name = p.Name,
                Id = p.Id
            }).ToListAsync();
        }


    }
}