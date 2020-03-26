using DogWalkerConsoleApp.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace DogWalkerConsoleApp.Data
{
    class DogRepository
    {
        public SqlConnection Connection
        {
            get
            {
                string _connectionString = "Data Source=localhost\\SQLEXPRESS; Initial Catalog=DogWalk; Integrated Security=True";
                return new SqlConnection(_connectionString);
            }
        }

        public List<Dog> GetAllDogs()
        {
            // Establish connection between server and database
            using (SqlConnection conn = Connection)
            {
                // Opens the connection
                conn.Open();

                // SQL Command
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT d.Id, d.Name, d.OwnerId, d.Breed, d.Notes, o.Name
                        FROM Dog d
                        LEFT JOIN Owner o
                        ON d.OwnerId = o.Id";

                    // Execute reader actually runs the SQL command
                    SqlDataReader reader = cmd.ExecuteReader();

                    var allDogs = new List<Dog>();

                    while (reader.Read())
                    {
                        //get ordinal returns us what position the Id column is in 
                        int idColumn = reader.GetOrdinal("Id");
                        int idValue = reader.GetInt32(idColumn);

                        int nameColumn = reader.GetOrdinal("Name");
                        string nameValue = reader.GetString(nameColumn);

                        int ownerIdColumn = reader.GetOrdinal("OwnerId");
                        int ownerIdValue = reader.GetInt32(ownerIdColumn);

                        int breedIdColumn = reader.GetOrdinal("Breed");
                        string breedValue = reader.GetString(breedIdColumn);

                        int notesIdColumn = reader.GetOrdinal("Notes");
                        string notesValue = reader.GetString(notesIdColumn);

                        int ownerNameColumn = reader.GetOrdinal("Name");
                        string ownerNameValue = reader.GetString(ownerNameColumn);

                        var dog = new Dog()
                        {
                            Id = idValue,
                            Name = nameValue,
                            OwnerId = ownerIdValue,
                            Breed = breedValue,
                            Notes = notesValue,
                            Owner = new Owner()
                            {
                                Id = ownerIdValue,
                                Name = ownerNameValue

                            }

                        };

                        allDogs.Add(dog);
                    }

                    reader.Close();

                    return allDogs;
                }
            }
        }



    }
}
