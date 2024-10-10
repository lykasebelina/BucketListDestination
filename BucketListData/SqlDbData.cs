using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using BucketListModels;

namespace BucketListData
{
    public class SqlDbData
    {
        static string connectionString
            = "Data Source=DESKTOP-0DJQ261\\SQLEXPRESS;Initial Catalog=BucketList;Integrated Security=True;";
            //= "Server=tcp:20.2.81.61,1433; Database=BucketList; User Id=sa; Password=Sebelina.bsit21;";

        public List<Destination> GetAllDestinations()
        {
            List<Destination> destinations = new List<Destination>();
            string selectStatement = "SELECT name, capital, language, currency, citizenship FROM destinations";

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlCommand selectCommand = new SqlCommand(selectStatement, sqlConnection);
                sqlConnection.Open();
                SqlDataReader reader = selectCommand.ExecuteReader();

                while (reader.Read())
                {
                    Destination destination = new Destination
                    {
                        Name = reader["name"].ToString(),
                        Capital = reader["capital"].ToString(),
                        Language = reader["language"].ToString(),
                        Currency = reader["currency"].ToString(),
                        Citizenship = reader["citizenship"].ToString()
                    };

                    destinations.Add(destination);
                }
            }

            return destinations;
        }

        public Destination GetDestinationByName(string name)
        {
            Destination destination = null;
            string selectStatement = "SELECT name, capital, language, currency, citizenship FROM destinations WHERE name = @name";

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlCommand selectCommand = new SqlCommand(selectStatement, sqlConnection);
                selectCommand.Parameters.AddWithValue("@name", name);
                sqlConnection.Open();
                SqlDataReader reader = selectCommand.ExecuteReader();

                if (reader.Read())
                {
                    destination = new Destination
                    {
                        Name = reader["name"].ToString(),
                        Capital = reader["capital"].ToString(),
                        Language = reader["language"].ToString(),
                        Currency = reader["currency"].ToString(),
                        Citizenship = reader["citizenship"].ToString()
                    };
                }
            }

            return destination;
        }

        public void AddNewDestination(Destination newDestination)
        {
            string insertStatement = "INSERT INTO destinations (name, capital, language, currency, citizenship) " +
                                     "VALUES (@name, @capital, @language, @currency, @citizenship)";

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlCommand insertCommand = new SqlCommand(insertStatement, sqlConnection);
                insertCommand.Parameters.AddWithValue("@name", newDestination.Name);
                insertCommand.Parameters.AddWithValue("@capital", newDestination.Capital);
                insertCommand.Parameters.AddWithValue("@language", newDestination.Language);
                insertCommand.Parameters.AddWithValue("@currency", newDestination.Currency);
                insertCommand.Parameters.AddWithValue("@citizenship", newDestination.Citizenship);

                sqlConnection.Open();
                insertCommand.ExecuteNonQuery();
            }
        }

        public void DeleteDestinationByName(string name)
        {
            string deleteStatement = "DELETE FROM destinations WHERE name = @name";

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlCommand deleteCommand = new SqlCommand(deleteStatement, sqlConnection);
                deleteCommand.Parameters.AddWithValue("@name", name);
                sqlConnection.Open();
                deleteCommand.ExecuteNonQuery();
            }
        }

        public void UpdateDestination(Destination updatedDestination)
        {
            string updateStatement = "UPDATE destinations SET capital = @capital, language = @language, currency = @currency, citizenship = @citizenship WHERE name = @name";

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlCommand updateCommand = new SqlCommand(updateStatement, sqlConnection);
                updateCommand.Parameters.AddWithValue("@name", updatedDestination.Name);
                updateCommand.Parameters.AddWithValue("@capital", updatedDestination.Capital);
                updateCommand.Parameters.AddWithValue("@language", updatedDestination.Language);
                updateCommand.Parameters.AddWithValue("@currency", updatedDestination.Currency);
                updateCommand.Parameters.AddWithValue("@citizenship", updatedDestination.Citizenship);

                sqlConnection.Open();
                updateCommand.ExecuteNonQuery();
            }
        }

        public bool ValidateUser(string username, string password)
        {
            string selectStatement = "SELECT COUNT(*) FROM users WHERE username = @username AND password = @password";

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlCommand selectCommand = new SqlCommand(selectStatement, sqlConnection);
                selectCommand.Parameters.AddWithValue("@username", username);
                selectCommand.Parameters.AddWithValue("@password", password);

                sqlConnection.Open();
                int userCount = (int)selectCommand.ExecuteScalar();

                return userCount > 0;
            }
        }
    }
}
 