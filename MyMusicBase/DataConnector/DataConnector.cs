using System.Data.SqlClient;

namespace DataConnector
{
    public static class DataConnector
    {
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
            return connection;
        }
    }
}
