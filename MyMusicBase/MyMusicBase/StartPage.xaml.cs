using System;
using System.Windows;


namespace MyMusicBase
{
    /// <summary>
    /// Логика взаимодействия для StartPage.xaml
    /// </summary>
    public partial class StartPage
    {
        public StartPage( )
        {
            InitializeComponent( );
            new Presenter(this);
        }

        public event EventHandler startPageEvent = null;

        private void Search_OnClick(object sender, RoutedEventArgs e)
        {
            startPageEvent.Invoke(sender, e);
        }

        private void AddButton_OnClick(object sender, RoutedEventArgs e)
        {
            //TO DO Реализация добавления в БД
            throw new System.NotImplementedException();
        }
    }
}
