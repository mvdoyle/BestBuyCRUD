using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;

namespace BestBuyCRUD
{
    public class DapperDepartmentRepository
    {
        private readonly IDbConnection _connection;

        public DapperDepartmentRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public IEnumerable<Department> GetAllDepartments()
        {
            return _connection.Query<Department>("SELECT * FROM Departments;").ToList();
        }

        public void InsertNewDepartment(string newDepartmentName)
        {
            _connection.Execute("INSERT INTO Departments (Name) VALUES (@departmentName);",  new { departmentName = newDepartmentName });
        }

        public void InsertNewProduct(string productName, int productPrice, int categoryID)
        {
            _connection.Execute("INSERT INTO Products (Name, Price, CategoryID) VALUES (@name, @price, @catID);",
                new { name = productName, price = productPrice, catID = categoryID} );
        }
    }
}
