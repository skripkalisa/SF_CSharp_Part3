using System.Data;
using Microsoft.Data.SqlClient;

namespace Unit1
{
    public class DbExecutor
    {
        private readonly MainConnector _connector;
        public DbExecutor(MainConnector connector) 
        {
            _connector = connector;
        }
        
        public DataTable SelectAll(string table)
        {
            var selectCommandText = $"select * from {table}";
            
            var adapter = new SqlDataAdapter(
                selectCommandText, 
                _connector.GetConnection()
            );
            var ds = new DataSet();
            adapter.Fill(ds);
            return ds.Tables[0];
        }

        public int DeleteByColumn(string table, string column, string value)
        {
            var command = new SqlCommand 
            {
                CommandType = CommandType.Text,
                CommandText = "delete from " + table + " where " + column + " = '" + value + "';",
                Connection = _connector.GetConnection(),
            };

            return command.ExecuteNonQuery();
        }
        public SqlDataReader SelectAllCommandReader(string table) 
        {
            var command = new SqlCommand 
            {
                CommandType = CommandType.Text,
                CommandText = "select * from " + table,
                Connection = _connector.GetConnection()
            };

            SqlDataReader reader = command.ExecuteReader();

            return reader.HasRows ? reader : null;
        }
        
        public int ExecProcedureAdding(string name, string login) 
        {
            var command = new SqlCommand 
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "AddingUserProc",
                Connection = _connector.GetConnection(),
            };

            command.Parameters.Add(new SqlParameter("@Name", name));
            command.Parameters.Add(new SqlParameter("@Login", login));

            return command.ExecuteNonQuery();

        }
        
        public int UpdateByColumn(string table, string columntocheck, string valuecheck, string columntoupdate, string valueupdate) 
        {
            var command = new SqlCommand 
            {
                CommandType = CommandType.Text,
                CommandText = "update   " + table + " set " + columntoupdate + " = '" + valueupdate + "'  where " + columntocheck + " = '" + valuecheck + "';",
                Connection = _connector.GetConnection(),
            };

            return command.ExecuteNonQuery();

        }


    }
}