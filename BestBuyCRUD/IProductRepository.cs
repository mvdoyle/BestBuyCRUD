using System;
using System.Collections.Generic;

namespace BestBuyCRUD
{
    public interface IProductRepository
    {
        public IEnumerable<Product> GetAllProducts();
        public void CreateProduct(string name, double price, int categoryID);
        public void UpdateProductName(int id, string newName);
        public void DeleteProduct(int id);
    }
}
