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
      
        public static int GetInt<T>(T t)
        {
            int n;
            if (!int.TryParse(t.ToString(), out n))
               throw new FormatException();//TO DO Обработка события
            return n;
        }

        public static DateTime GetData<T>(T t)
        {

            DateTime date;
            if (!DateTime.TryParse(t.ToString(), out date))
               throw new FormatException();//TO DO обработчик
            return date;
        }

        public static string GetString(string searchName)
        {
           string temp = "";
           SqlConnection connection = new SqlConnection(Connection.ConnectionString);
           SqlCommand cmd = new SqlCommand(searchName, connection);
            try
            {
                connection.Open();
                cmd.Transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        if (!reader.IsDBNull(i))
                            temp += reader[i] + "@@@";
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
            return temp;
        }

        public static List<string> GetList(string searchName)
        {

          List<string> list = new List<string>();
          SqlConnection connection = new SqlConnection(Connection.ConnectionString);
          SqlCommand cmd = new SqlCommand(searchName, connection);
            try
            {
                connection.Open();
                cmd.Transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string temp = "";
                    for (int i = 0; i < reader.FieldCount; ++i)
                    {
                        if (!reader.IsDBNull(i))
                            temp += reader[i] + "@@@";
                    }
                    list.Add(temp);
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
            return list;
        }
    
    }
}
