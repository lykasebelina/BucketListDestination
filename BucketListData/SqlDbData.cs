using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using BucketListModels;

namespace BucketListData
{
    public class SqlDbData
    {
        string connectionString = "Data Source=LAPTOP-2V85TBH6\\SQLEXPRESS01;Initial Catalog=BucketList;Integrated Security=True;";
        SqlConnection sqlConnection;

        public SqlDbData()
        {
            sqlConnection = new SqlConnection(connectionString);
        }

        public List<Destination> GetAllDestinations()
        {
            string selectStatement = "SELECT name, capital, language, currency, citizenship FROM destinations";
            SqlCommand selectCommand = new SqlCommand(selectStatement, sqlConnection);

            sqlConnection.Open();
            List<Destination> destinations = new List<Destination>();

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

            sqlConnection.Close();

            return destinations;
        }

        public Destination GetDestinationByName(string name)
        {
            string selectStatement = "SELECT name, capital, language, currency, citizenship FROM destinations WHERE name = @name";
            SqlCommand selectCommand = new SqlCommand(selectStatement, sqlConnection);
            selectCommand.Parameters.AddWithValue("@name", name);

            sqlConnection.Open();
            SqlDataReader reader = selectCommand.ExecuteReader();

            Destination destination = null;
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

            sqlConnection.Close();

            return destination;
        }

        public void AddNewDestination(Destination newDestination)
        {
            string insertStatement = "INSERT INTO destinations (name, capital, language, currency, citizenship) " +
                                     "VALUES (@name, @capital, @language, @currency, @citizenship)";

            SqlCommand insertCommand = new SqlCommand(insertStatement, sqlConnection);
            insertCommand.Parameters.AddWithValue("@name", newDestination.Name);
            insertCommand.Parameters.AddWithValue("@capital", newDestination.Capital);
            insertCommand.Parameters.AddWithValue("@language", newDestination.Language);
            insertCommand.Parameters.AddWithValue("@currency", newDestination.Currency);
            insertCommand.Parameters.AddWithValue("@citizenship", newDestination.Citizenship);

            sqlConnection.Open();
            insertCommand.ExecuteNonQuery();
            sqlConnection.Close();
        }

        public void DeleteDestinationByName(string name)
        {
            string deleteStatement = "DELETE FROM destinations WHERE name = @name";
            SqlCommand deleteCommand = new SqlCommand(deleteStatement, sqlConnection);
            deleteCommand.Parameters.AddWithValue("@name", name);

            sqlConnection.Open();
            deleteCommand.ExecuteNonQuery();
            sqlConnection.Close();
        }

        public void UpdateDestination(Destination updatedDestination)
        {
            string updateStatement = "UPDATE destinations SET capital = @capital, language = @language, currency = @currency, citizenship = @citizenship WHERE name = @name";

            SqlCommand updateCommand = new SqlCommand(updateStatement, sqlConnection);
            updateCommand.Parameters.AddWithValue("@name", updatedDestination.Name);
            updateCommand.Parameters.AddWithValue("@capital", updatedDestination.Capital);
            updateCommand.Parameters.AddWithValue("@language", updatedDestination.Language);
            updateCommand.Parameters.AddWithValue("@currency", updatedDestination.Currency);
            updateCommand.Parameters.AddWithValue("@citizenship", updatedDestination.Citizenship);

            sqlConnection.Open();
            updateCommand.ExecuteNonQuery();
            sqlConnection.Close();
        }

        public bool ValidateUser(string username, string password)
        {
            string selectStatement = "SELECT COUNT(*) FROM users WHERE username = @username AND password = @password";
            SqlCommand selectCommand = new SqlCommand(selectStatement, sqlConnection);
            selectCommand.Parameters.AddWithValue("@username", username);
            selectCommand.Parameters.AddWithValue("@password", password);

            sqlConnection.Open();
            int userCount = (int)selectCommand.ExecuteScalar();
            sqlConnection.Close();

            return userCount > 0;
        }
    }
}
