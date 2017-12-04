using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using DataConnector;

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

        public static string GetString(string searchString)
        {
            SqlConnection connection = new SqlConnection(Connection.ConnectionString);
            SqlCommand cmd = new SqlCommand(searchString, connection);
            connection.Open( );
            SqlDataReader reader = cmd.ExecuteReader( );
            string result = "";
            while (reader.Read( ))
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    result += reader[i] + " ";
                }
            }
            return result;
        }

        public static int GetId(string searchString, string request)
        {
            SqlConnection connection = new SqlConnection(Connection.ConnectionString);
            SqlCommand cmd = new SqlCommand(request, connection);
            connection.Open( );
            SqlDataReader reader = cmd.ExecuteReader( );
            int result = -1;
            while (reader.Read( ))
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    if (reader.GetName(i) == searchString)
                        result = reader.GetInt32(i);
                }
            }
            return result;
        }

        public static List<int> GetList(string searchString)
        {
            SqlConnection connection = new SqlConnection(Connection.ConnectionString);
            SqlCommand cmd = new SqlCommand(searchString, connection);
            connection.Open( );
            SqlDataReader reader = cmd.ExecuteReader( );
            List<int> result = new List<int>( );
            while (reader.Read( ))
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    result.Add(reader.GetInt32(i));
                }
            }
            return result;
        }

        public static object GetScalar(string searchString)
        {
            SqlConnection connection = new SqlConnection(Connection.ConnectionString);
            SqlCommand cmd = new SqlCommand(searchString, connection);
            connection.Open( );
            object result = cmd.ExecuteScalar( );
            connection.Close( );
            return result;
        }


        ///////////////////////////////////////////////////////////
        /// //////////////////////////////////////////////////////////
        /// 
        /// ///////////////////////////////////////////////////////////
        /// 
        private static int GetInt<T>(T t)
        {
            int n;
            if (!int.TryParse(t.ToString(), out n))
                throw new FormatException();//TO DO Обработка события
            return n;
        }

        private static DateTime GetData<T>(T t)
        {
            DateTime date;
            if (!DateTime.TryParse(t.ToString(), out date))
                throw new FormatException();//TO DO обработчик
            return date;
        }

        //////////////////////////////////////

        public static string GetString1(string searchName)
        {
            string temp = "";
            SqlConnection connection = new SqlConnection(Connection.ConnectionString);
            SqlCommand cmd = new SqlCommand(searchName, connection);
            connection.Open( );
            SqlDataReader reader = cmd.ExecuteReader( );
            while (reader.Read( ))
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    temp += reader.GetString(i) + " ";
                }
            }
            return temp;
        }

        public static List<string> GetList1(string searchName)
        {
            List<string> list = new List<string>();
            SqlConnection connection = new SqlConnection(Connection.ConnectionString);
            SqlCommand cmd = new SqlCommand(searchName, connection);
            connection.Open( );
            SqlDataReader reader = cmd.ExecuteReader( );
            while (reader.Read( ))
            {
                string temp = "";
                for (int i = 0; i < reader.FieldCount; ++i)
                {
                    temp += reader.GetString(i) + " ";
                }
                list.Add(temp);
            }
            return list;
        }
    
    }
}
