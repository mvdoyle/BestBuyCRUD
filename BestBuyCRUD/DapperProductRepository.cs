using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;

namespace BestBuyCRUD
{
    public class DapperProductRepository : IProductRepository
    {
        private readonly IDbConnection _connection;

        public DapperProductRepository(IDbConnection conn)
        {
            _connection = conn;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _connection.Query<Product>("SELECT * FROM Products;");
        }

        public void CreateProduct(string name, double price, int categoryID)
        {
            _connection.Execute("INSERT INTO Products (Name, Price, CategoryID) VALUES (@name, @price, @categoryID);",
                new {name = name, price = price, categoryID = categoryID });
        }

        public void UpdateProductName(int id, string newName)
        {
            _connection.Execute("UPDATE Products SET Name = @newName WHERE ProductID = @id",
                new {newName = newName, id = id});
        }

        public void DeleteProduct(int id)
        {
            _connection.Execute("DELETE FROM Reviews WHERE ProductID = @id;", new { id = id });
            _connection.Execute("DELETE FROM Sales WHERE ProductID = @id;", new { id = id });
            _connection.Execute("DELETE FROM Products WHERE ProductID = @id;", new { id = id });
        }
    }
}
