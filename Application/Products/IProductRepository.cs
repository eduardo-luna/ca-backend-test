﻿using Domain.Product;

namespace Application.Products
{
    public interface IProductRepository
    {
        void Add(Product product);
        void Update(Product product);
        Task<bool> ProductExistsAsync(string name);
        Task<Product?> GetByIdAsync(int id);
    }
}
