using System;
using System.Data.SqlClient;
using System.Windows;

namespace MyMusicBase
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow( )
        {
            InitializeComponent( );
        }

        private void User_Confurum_OnClick(object sender, RoutedEventArgs e)
        {
            //Подключение к базе данных
            SqlConnection instance = new SqlConnection(DataConnector.SqlStringBuilder(UserName.Text, Pasword.Password).ConnectionString);
            try
            {
                instance.Open( );
                Window startPage = new StartPage();
                instance.Close();
                startPage.Show();
                LoginWindow.Close( );
            }
            catch (Exception)
            {
                MessageBox.Show("Check you password or login");
            }
            finally
            {
                instance.Close( );
            }
        }
    }
}
