using DogWalkerConsoleApp.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace DogWalkerConsoleApp.Data
{
    class OwnerRepository
    {
        public SqlConnection Connection
        {
            get
            {
                string _connectionString = "Data Source=localhost\\SQLEXPRESS; Initial Catalog=DogWalk; Integrated Security=True";
                return new SqlConnection(_connectionString);
            }
        }

        public List<Owner> getAllOwners()
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
                        SELECT o.Id, o.Name, o.Address, o.Phone, o.NeighborhoodId, n.Name NeighborhoodName
                        FROM Owner o
                        LEFT JOIN Neighborhood n
                        ON o.NeighborhoodId = n.Id";

                    //Execute reader actually runs the sql command
                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Owner> allOwners = new List<Owner>();

                    while (reader.Read())
                    {
                        //get ordinal returns us what position the Id column is in 
                        int idColumn = reader.GetOrdinal("Id");
                        int idValue = reader.GetInt32(idColumn);

                        int nameColumn = reader.GetOrdinal("Name");
                        string nameValue = reader.GetString(nameColumn);

                        int addressColumn = reader.GetOrdinal("Address");
                        string addressValue = reader.GetString(addressColumn);

                        int neighborIdColumn = reader.GetOrdinal("NeighborhoodId");
                        int neighborhoodIdValue = reader.GetInt32(neighborIdColumn);

                        int phoneColumn = reader.GetOrdinal("Phone");
                        string phoneValue = reader.GetString(phoneColumn);

                        int neighborhoodNameColumn = reader.GetOrdinal("NeighborhoodName");
                        string neighborhoodNameValue = reader.GetString(neighborhoodNameColumn);

                        var owner = new Owner()
                        {
                            Id = idValue,
                            Name = nameValue,
                            Address = addressValue,
                            Phone = phoneValue,
                            NeighborhoodId = neighborhoodIdValue,
                            Neighborhood = new Neighborhood()
                            {
                                Id = neighborhoodIdValue,
                                Name = neighborhoodNameValue

                            }

                        };

                        allOwners.Add(owner);
                    }

                    reader.Close();

                    return allOwners;
                }
            }
        }


        public Owner addOwner(Owner owner)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO Owner (Name, Address, NeighborhoodId, Phone)
                        OUTPUT INSERTED.Id
                        VALUES (@Name, @Address, @NeighborhoodId, @Phone)";

                    cmd.Parameters.Add(new SqlParameter("@Name", owner.Name));
                    cmd.Parameters.Add(new SqlParameter("@Address", owner.Address));
                    cmd.Parameters.Add(new SqlParameter("@NeighborhoodId", owner.NeighborhoodId));
                    cmd.Parameters.Add(new SqlParameter("@Phone", owner.Name));

                    int id = (int)cmd.ExecuteScalar();

                    owner.Id = id;

                    return owner;
                }
            }

        }
    }
}
