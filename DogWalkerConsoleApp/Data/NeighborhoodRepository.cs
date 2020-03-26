using DogWalkerConsoleApp.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace DogWalkerConsoleApp.Data
{
    class NeighborhoodRepository
    {
        public SqlConnection Connection
        {
            get
            {
                string _connectionString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=DogWalk;Integrated Security=True";
                return new SqlConnection(_connectionString);
            }
        }
        public List<Neighborhood> GetAllNeighborhoods()
        {
            // Establish connection between server and database
            using (SqlConnection conn = Connection)
            {
                // Opens the connection
                conn.Open();

                // SQL Command
                using SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT Id, Name FROM Neighborhood";

                SqlDataReader reader = cmd.ExecuteReader();

                var neighborhoods = new List<Neighborhood>();

                while (reader.Read())
                {

                    int idColumnPosition = reader.GetOrdinal("Id");
                    int idValue = reader.GetInt32(idColumnPosition);

                    int nameColumnPosition = reader.GetOrdinal("Name");
                    string nameValue = reader.GetString(nameColumnPosition);

                    var neighborhood = new Neighborhood
                    {
                        Id = idValue,
                        Name = nameValue
                    };

                    neighborhoods.Add(neighborhood);
                }

                reader.Close();

                return neighborhoods;
            }
        }
    }
}
