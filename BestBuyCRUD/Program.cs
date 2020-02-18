using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;

namespace BestBuyCRUD
{
    class Program
    {
        static void Main(string[] args)
        {

            IDbConnection conn = new MySqlConnection();
            conn.ConnectionString = System.IO.File.ReadAllText("ConnectionString.txt");

            var deptRepo = new DapperDepartmentRepository(conn);

            //Console.WriteLine("What is the new product's Name?");
            //var response = Console.ReadLine();

            //Console.WriteLine("What is the new product's price?");
            //var price = double.Parse(Console.ReadLine());

            //Console.WriteLine("What is the new product's CategoryID?");
            //var cat = int.Parse(Console.ReadLine());


            var prodRepo = new DapperProductRepository(conn);

            //prodRepo.CreateProduct(response, price, cat);
            //Console.WriteLine("What is the ID of the product you would like to update?");
            //var id = int.Parse(Console.ReadLine());

            //Console.WriteLine("What would you like the product's new name to be?");
            //var newName = Console.ReadLine();

            //prodRepo.UpdateProductName(id, newName);

            Console.WriteLine("What is the ID of the product you would like to delete?");
            var prodID = int.Parse(Console.ReadLine());

            prodRepo.DeleteProduct(prodID);


            //var departments = deptRepo.GetAllDepartments();

            //Console.WriteLine("ID | Name");
            //foreach(var dept in departments)
            //{
            //    Console.WriteLine($"{dept.DepartmentID}  |  {dept.Name}");
            //}


            var products = prodRepo.GetAllProducts();

            foreach (var product in products)
            {
                Console.WriteLine($"{product.ProductID} {product.Name}");
            }

        }

        static IEnumerable GetAllDepartments()
        {
            MySqlConnection conn = new MySqlConnection();

            conn.ConnectionString = System.IO.File.ReadAllText("ConnectionString.txt");
      
            MySqlCommand cmd = conn.CreateCommand();

            cmd.CommandText = "SELECT Name FROM Departments;";

            using (conn)
            {
                conn.Open();
                List<string> allDepartments = new List<string>();

                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read() == true)
                {
                    var currentDepartment = reader.GetString("Name");
                    allDepartments.Add(currentDepartment);
                }

                return allDepartments;
            }
        }

        static void CreateDepartment(string departmentName)
        {
            //grab connection string text from the ConnectionString.txt file
            var connStr = System.IO.File.ReadAllText("ConnectionString.txt");

            //If you adopt initializing the connection inside the using statement then you can't make a mistake
            //later when reorganizing or refactoring code and accidentally doing something that implicitly
            //opens a connection that isn't closed
            using (var conn = new MySqlConnection(connStr))
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();

                // parameterized query to prevent SQL Injection
                cmd.CommandText = "INSERT INTO departments (Name) VALUES (@departmentName);";
                //this method gives our parameter above, ^^@departmentName, a value
                cmd.Parameters.AddWithValue("departmentName", departmentName);

                cmd.ExecuteNonQuery();
            }
        }

        static void UpdateDepartment(string currentName, string newDepartmentName)
        {
            var connStr = System.IO.File.ReadAllText("ConnectionString.txt");

            using (var conn = new MySqlConnection(connStr))
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();

                // parameterized query to prevent SQL Injection
                cmd.CommandText = "UPDATE departments SET Name = @newDepartmentName WHERE Name = @currentName;";
                //this method gives our parameter above, ^^@departmentName, a value
                cmd.Parameters.AddWithValue("currentName", currentName);
                cmd.Parameters.AddWithValue("newDepartmentName", newDepartmentName);

                cmd.ExecuteNonQuery();
            }
        }

    }
}
