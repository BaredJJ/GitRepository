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
            new Presenter(this, new MessageService());
        }

        public event EventHandler StartPageEvent;
        public event EventHandler AddEvent;

        private void Search_OnClick(object sender, RoutedEventArgs e)
        {
            StartPageEvent?.Invoke(sender, e);
        }

        private void AddButton_OnClick(object sender, RoutedEventArgs e)
        {
            AddEvent?.Invoke(sender, e);
        }
    }
}
