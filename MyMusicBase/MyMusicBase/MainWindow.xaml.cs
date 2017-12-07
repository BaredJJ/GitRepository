using System;
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
            new Presenter(this, new MessageService());
        }

        public event EventHandler MainWindowEvent;

        private void User_Confurum_OnClick(object sender, RoutedEventArgs e)
        {
            MainWindowEvent?.Invoke(sender, e);
            //Подключение к базе данных          
        }
    }
}
