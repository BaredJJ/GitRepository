using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace MyMusicBase
{
    public static class DataConnector
    {
        private static SqlConnectionStringBuilder Connection { get; set; }
        //Создание экземпляра класса StringBuilder для подключения к БД
        public static SqlConnectionStringBuilder SqlStringBuilder(string name, string pasword)
        {
            try
            {
                var connection = new SqlConnectionStringBuilder
                {
                    DataSource = @".\SQLEXPRESS",
                    InitialCatalog = "MyMusic",
                    UserID = name,
                    Password = pasword,
                    Pooling = true
                };
                Connection = connection;
                return connection;
            }
            catch (Exception e)
            {
                throw;
            }
        }
      
        public static int GetInt<T>(T t)
        {
            try
            {
                int n;
                if (!int.TryParse(t.ToString(), out n))
                    throw new FormatException();//TO DO Обработка события
                return n;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public static DateTime GetData<T>(T t)
        {
            try
            {
                DateTime date;
                if (!DateTime.TryParse(t.ToString(), out date))
                    throw new FormatException();//TO DO обработчик
                return date;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public static string GetString(string searchName)
        {
            try
            {
                string temp = "";
                SqlConnection connection = new SqlConnection(Connection.ConnectionString);
                SqlCommand cmd = new SqlCommand(searchName, connection);
                connection.Open( );
                cmd.Transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted);
                SqlDataReader reader = cmd.ExecuteReader( );
                while (reader.Read( ))
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        if (!reader.IsDBNull(i))
                            temp += reader[i] + "@@@";
                    }
                }
                return temp;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public static List<string> GetList(string searchName)
        {
            try
            {
                List<string> list = new List<string>();
                SqlConnection connection = new SqlConnection(Connection.ConnectionString);
                SqlCommand cmd = new SqlCommand(searchName, connection);
                connection.Open( );
                cmd.Transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted);
                SqlDataReader reader = cmd.ExecuteReader( );
                while (reader.Read( ))
                {
                    string temp = "";
                    for (int i = 0; i < reader.FieldCount; ++i)
                    {
                        if (!reader.IsDBNull(i))
                            temp += reader[i] + "@@@";
                    }
                    list.Add(temp);
                }
                return list;
            }
            catch (Exception e)
            {
                throw;
            }
        }
    
    }
}
