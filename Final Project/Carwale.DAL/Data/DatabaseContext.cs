using System.Data;
using MySql.Data.MySqlClient;

namespace Carwale.DAL.Data
{
    public class DatabaseContext
    {
        private readonly string _connectionString;

        public DatabaseContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IDbConnection CreateConnection()
        {
            try
            {
                var connection = new MySqlConnection(_connectionString);

                if(connection == null)
                    throw new Exception("Failed to create database connection.");
                
                return connection;
                
            }
            catch (Exception ex)
            {   
                Console.WriteLine($"Error creating database connection: {ex.Message}");
                throw;
            }

        }
    }
}