using System;
using System.Collections.Generic;
using System.Data;
using Unit1;

namespace Unit1_app
{
    public class Manager
    {
        private Table userTable;
        private MainConnector connector;
        private DbExecutor dbExecutor;
        public Manager() {
            connector = new MainConnector();
            userTable = new Table();
            userTable.Name = "NetworkUser";
            userTable.ImportantField = "Login";
            userTable.Fields.Add("Id");
            userTable.Fields.Add("Login");
            userTable.Fields.Add("Name");
        }

        public void Connect() 
        {
            var result = connector.ConnectAsync();

            if (result.Result) 
            {
                Console.WriteLine("Подключено успешно!");

                dbExecutor = new DbExecutor(connector);
            } 
            else 
            {
                Console.WriteLine("Ошибка подключения!");
            }
        }

        public void Disconnect() 
        {
            Console.WriteLine("Отключаем БД!");
            connector.DisconnectAsync();
        }

        public void ShowData() 
        {
            var tableName = userTable.Name;

            Console.WriteLine("Получаем данные таблицы " + tableName);

            var data = dbExecutor.SelectAll(tableName);

            Console.WriteLine("Количество строк в " + tableName + ": " + data.Rows.Count);

            Console.WriteLine();
            foreach(DataColumn column in data.Columns) {
                Console.Write($"{column.ColumnName}\t");
            }

            Console.WriteLine();

            foreach(DataRow row in data.Rows) 
            {

                var cells = row.ItemArray;
                foreach(var cell in cells) 
                {
                    Console.Write($"{cell}\t");
                }
                Console.WriteLine();
            }

            Console.WriteLine();
        }

        internal void UseReader()
        {
            Console.WriteLine("Using SqlDataReader\n");
            var reader = dbExecutor.SelectAllCommandReader(userTable.Name);
            var columnList = new List<string>();

            for (int i = 0; i < reader.FieldCount; i++)
            {
                var name = reader.GetName(i);
                columnList.Add(name);
            }

            foreach (var t in columnList)
            {
                Console.Write($"{t}\t");
            }

            var counter = 0;

            Console.WriteLine();
            while (reader.Read())
            {
                foreach (var t in columnList)
                {
                    var value = reader[t];
                    Console.Write($"{value}\t");
                }

                counter++;
                Console.WriteLine();
            }
            Console.WriteLine($"Number of strings in the {userTable.Name}: {counter}");
        }
        
        public int DeleteUserByLogin(string value) 
        {
            return dbExecutor.DeleteByColumn(userTable.Name, userTable.ImportantField, value);
        }
        public void AddUser(string login, string name) 
        {
            dbExecutor.ExecProcedureAdding(name, login);
        }
        
        public int UpdateUserByLogin(string value, string newValue) {
            return dbExecutor.UpdateByColumn(userTable.Name, userTable.ImportantField, value, userTable.Fields[2], newValue);
        }
        
        // internal  void UseDataTable()
        // {
        //     Console.WriteLine("Using DataTable\n");
        //     data = dbExecutor.SelectAll(_tablename);
        //     Console.WriteLine("Количество строк в " + tablename + ": " + _data.Rows.Count);
        //
        //     foreach (DataColumn column in _data.Columns)
        //     {
        //         Console.Write($"{column.ColumnName}\t");
        //     }
        //
        //     Console.WriteLine();
        //
        //     foreach (DataRow row in _data.Rows)
        //     {
        //         var cells = row.ItemArray;
        //         foreach (var cell in cells)
        //         {
        //             Console.Write($"{cell}\t");
        //         }
        //
        //         Console.WriteLine();
        //     }
        // }
    }
}