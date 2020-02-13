using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace BestBuyCRUD
{
    class Program
    {
        static void Main(string[] args)
        {
            var departments = GetAllDepartments(); //a list of departments from the database

            foreach (var department in departments) // for each item in our list of departments, print the department item
            {
                Console.WriteLine(department);

            }
        }

        static List<string> GetAllDepartments()
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
    }

}
