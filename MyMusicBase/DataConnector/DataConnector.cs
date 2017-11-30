using System.Collections.Generic;
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
    }
}
