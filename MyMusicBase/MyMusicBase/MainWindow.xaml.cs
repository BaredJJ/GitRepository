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
            new Presenter(this);
        }

        public event EventHandler mainWindowEvent = null;

        private void User_Confurum_OnClick(object sender, RoutedEventArgs e)
        {
            mainWindowEvent.Invoke(sender, e);
            //Подключение к базе данных          
        }
    }
}
