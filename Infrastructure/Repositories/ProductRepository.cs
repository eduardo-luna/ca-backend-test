﻿using Application.Products;
using Domain.Product;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(Product product)
        {
            _context.Add(product);
        }

        public Task<Product?> GetByIdAsync(int id)
        {
            return _context.Products.FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<bool> ProductExistsAsync(string name)
        {
            return _context.Products.AnyAsync(x => x.Name.ToLower().Equals(name.ToLower()));
        }

        public void Update(Product product)
        {
            _context.Update(product);
        }
    }
}
