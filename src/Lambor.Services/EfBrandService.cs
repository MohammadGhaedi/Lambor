using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lambor.DataLayer.Context;
using Lambor.Entities;
using Lambor.Services.Contracts;
using Lambor.ViewModels.Identity;
using Microsoft.EntityFrameworkCore;

namespace Lambor.Services
{
    public class EfBrandService:IBrandService
    {
        private readonly IUnitOfWork _uow;
        private readonly DbSet<Brand> _brands;

        public EfBrandService(IUnitOfWork uow)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _brands = _uow.Set<Brand>();

        }

        public async Task InsertAsync(Brand brand)
        {
            await _brands.AddAsync(brand);
            await _uow.SaveChangesAsync();
        }

        public async Task UpdateAsync(Brand brand)
        {
            var item =await _brands.FindAsync(brand.Id);
            if (item == null)
            {
                throw new Exception();
            }

            item.Name = brand.Name;
            await _uow.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var item = await _brands.FindAsync(id);
            if (item==null)
            {
                throw new Exception();
            }
            _brands.Remove(item);
            await _uow.SaveChangesAsync();
        }

        public async Task<Brand> GetAsync(int id)
        {
            var item= await _brands.FindAsync(id);
            if (item==null)
            {
                throw new Exception();
            }

            return item;
        }

        public async Task<IList<BrandViewModel>> GetAllAsync()
        {
            return await _brands.Select(p => new BrandViewModel {Name = p.Name, Id = p.Id}).ToListAsync();
        }
    }
}
