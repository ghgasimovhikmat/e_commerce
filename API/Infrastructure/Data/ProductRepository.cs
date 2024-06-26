﻿using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreContext _storeContext;
        public ProductRepository(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }
        //Task<Product> GetProductByIdAsync(int id);

        //Task<IReadOnlyList<Product>> GetProductAsync();
        //Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync();
        //Task<IReadOnlyList<ProductType>> GetProductTypesAsync();
        public async Task<IReadOnlyList<Product>> GetProductsAsync()
        {
            return await _storeContext.Products.
                Include(p =>p.ProductBrand)
               .Include(p => p.ProductType)
               .ToListAsync();
        }

        public async Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync()
        {
            return await _storeContext.ProductBrands.ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _storeContext.Products.
                 Include(p => p.ProductBrand)
                .Include(p => p.ProductType)
                .FirstOrDefaultAsync(p=>p.Id==id);
        }

        public async Task<IReadOnlyList<ProductType>> GetProductTypesAsync()
        {
            return await _storeContext.ProductTypes.ToListAsync();
        }
    }
}
