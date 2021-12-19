using System;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Unit1.Config;

namespace Unit1
{
    public class MainConnector
    {
        private SqlConnection _connection;

        public async Task<bool> ConnectAsync()
        {
            bool result;
            try
            {
                _connection = new SqlConnection(ConnectionString.MsSqlConnection);
                Console.WriteLine(_connection.ConnectionString);
                await _connection.OpenAsync();
                result = true;
            }
            catch
            {
                result = false;
            }

            return result;
        }

        public async void DisconnectAsync()
        {
            if (_connection.State == ConnectionState.Open)
            {
                await _connection.CloseAsync();
            }
        }
        
        public SqlConnection GetConnection() 
        {
            if (_connection.State == ConnectionState.Open) {
                return _connection;
            } 
            else 
            {
                throw new Exception("Подключение уже закрыто!");
            }
        }
    }
}